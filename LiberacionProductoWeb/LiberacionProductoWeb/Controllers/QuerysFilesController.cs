using DevExpress.Pdf;
using DevExpress.XtraReports.UI;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Reports;
using LiberacionProductoWeb.Reports.ConditioningOrder;
using LiberacionProductoWeb.Reports.OrderProduction;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class QuerysFilesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPrincipalService _principalService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ILogger<QuerysFilesController> _logger;
        private readonly IProductionOrderService _productionOrderService;
        private readonly IConditioningOrderService _conditioningOrderService;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExportPDFService _exportPDFService;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IConfiguration _config;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly IFormulaCatalogRepository _formulaRepository;
        private readonly IVariablesFileReaderService _variablesFileReaderService; //MEG
        private readonly IStateRepository _stateRepository;
        public QuerysFilesController(UserManager<ApplicationUser> userManager, IPrincipalService principalService,
        IStringLocalizer<Resource> resource, ILogger<QuerysFilesController> logger,
        IProductionOrderService productionOrderService, IConditioningOrderService conditioningOrderService,
        IGeneralCatalogRepository generalCatalogRepository, IHttpContextAccessor httpContextAccessor, IExportPDFService exportPDFService,
        IProductionOrderAttributeRepository productionOrderAttributeRepository, IConfiguration config,
        ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository, IFormulaCatalogRepository formulaRepository, IVariablesFileReaderService variablesFileReaderService,
        IStateRepository stateRepository)
        {
            _userManager = userManager;
            _principalService = principalService;
            _resource = resource;
            _logger = logger;
            _productionOrderService = productionOrderService;
            _conditioningOrderService = conditioningOrderService;
            _generalCatalogRepository = generalCatalogRepository;
            _httpContextAccessor = httpContextAccessor;
            _exportPDFService = exportPDFService;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _config = config;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _formulaRepository = formulaRepository;
            _variablesFileReaderService = variablesFileReaderService;
            _stateRepository = stateRepository;
        }
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> Index(String SelectedPlantsFilter, String SelectedProductsFilter, String SelectedTanksFilter,
        String SelectedStatesFilter, string StartDate, string EndDate)
        {
            ConfiguracionUsuarioVM model = new ConfiguracionUsuarioVM();
            List<SelectListItem> Tank = new List<SelectListItem>();
            List<SelectListItem> States = new List<SelectListItem>();
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                model.ListqueryFilesModels = new List<QueryFilesModel>();
                ///states
                var states = await _stateRepository.GetAllAsync();
                foreach (var item in states)
                {
                    States.Add(new SelectListItem
                    {
                        Text = item.Description,
                        Value = item.Description
                    });
                }

                model.ListStates = States;
                if (!string.IsNullOrEmpty(SelectedStatesFilter))
                    SelectedStatesFilter = SelectedStatesFilter.Replace(ProductionOrderStatus.Released.Value, ConditioningOrderStatus.InProgress.Value);

                model.SelectedPlantsFilter = string.IsNullOrEmpty(SelectedPlantsFilter) ? new List<string>() : SelectedPlantsFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedProductsFilter = string.IsNullOrEmpty(SelectedProductsFilter) ? new List<string>() : SelectedProductsFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedTanksFilter = string.IsNullOrEmpty(SelectedTanksFilter) ? new List<string>() : SelectedTanksFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedStatesFilter = string.IsNullOrEmpty(SelectedStatesFilter) ? new List<string>() : SelectedStatesFilter.Trim().Replace(" ", "").Split(",").ToList();
                //init serch
                if (model.SelectedPlantsFilter.Any())
                {
                    var qryFiles = await _principalService.GetQueryFiles();
                    var mapped = ObjectMapper.Mapper.Map<IEnumerable<QueryFilesModel>>(qryFiles);
                    model.ListqueryFilesModels = (List<QueryFilesModel>)mapped;
                }

                //filter by criteria plant
                if (model.SelectedPlantsFilter != null && model.SelectedPlantsFilter.Count > 0)
                {
                    model.ListqueryFilesModels = (from r in model.ListqueryFilesModels where model.SelectedPlantsFilter.Contains(r.PlantId) select r).ToList();
                }
                //filter by criteria product
                if (model.SelectedProductsFilter != null && model.SelectedProductsFilter.Count > 0)
                {
                    model.ListqueryFilesModels = (from r in model.ListqueryFilesModels where model.SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                //filter by criteria tank
                if (model.SelectedTanksFilter != null && model.SelectedTanksFilter.Count > 0)
                {
                    model.ListqueryFilesModels = (from r in model.ListqueryFilesModels where model.SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                //filter by criteria state
                if (model.SelectedStatesFilter != null && model.SelectedStatesFilter.Count > 0)
                {
                    model.ListqueryFilesModels = (from r in model.ListqueryFilesModels where SelectedStatesFilter.Contains(r.State) select r).ToList();
                }
                if (StartDate != null && EndDate != null)
                {
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", true).DateTimeFormat;
                    var date = Convert.ToDateTime(StartDate, usDtfi);
                    model.ListqueryFilesModels = (from r in model.ListqueryFilesModels.Where(archive => Convert.ToDateTime(StartDate, usDtfi) <= archive.StartDateProd
                                            && archive.StartDateProd <= (Convert.ToDateTime(EndDate, usDtfi)).AddDays(1))
                                                  select r).ToList();
                    var dateEnd = Convert.ToDateTime(EndDate, usDtfi);
                    model.StartDate = date;
                    model.EndDate = dateEnd;
                }

                ViewBag.ShowModal = (Request.Query.Count > 0 && model.ListqueryFilesModels.Count <= 0);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }
            return View(model);
        }
        public async Task<IActionResult> GetQueryFiles()
        {
            List<QueryFilesModel> queryFilesModels = new List<QueryFilesModel>();
            try
            {
                queryFilesModels = await _principalService.GetQueryFiles();

                return Json(new { data = queryFilesModels });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public IActionResult ClearFiltersExpediente()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> DetailOP(int Id)
        {
            DetailOP model = new DetailOP();
            try
            {
                model = await _principalService.GetDetailOP(Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }

            return PartialView("_ProductionOrder", model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> DetailOA(int Id)
        {
            List<DetailOA> model = new List<DetailOA>();
            try
            {
                model = await _principalService.GetDetailOA(Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }

            return PartialView("_ConditioningOrder", model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> DetailDB(int Id)
        {
            List<DetailDistributionBatch> model = new List<DetailDistributionBatch>();
            try
            {
                model = await _principalService.GetDetailDB(Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }

            return PartialView("_DistributionBatch", model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> Summary(int id)
        {
            var model = new SummaryOrderViewModel();

            model.ProductionOrder = await _productionOrderService.GetByIdAsync(id);
            model.ConditioningOrder = await _conditioningOrderService.GetByProductionOrderIdAsync(id);

            var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
            foreach (var item in generalCatalogFilter)
            {
                if (item.PlantId == model.ProductionOrder.SelectedPlantFilter)
                {
                    generalCatalogFilter = (from r in generalCatalogFilter where model.ProductionOrder.SelectedPlantFilter.Contains(r.PlantId) select r).ToList();
                }
                if (item.ProductId == model.ProductionOrder.SelectedProductFilter)
                {
                    generalCatalogFilter = (from r in generalCatalogFilter where model.ProductionOrder.SelectedProductFilter.Contains(r.ProductId) select r).ToList();
                }
                if (item.TankId == model.ProductionOrder.SelectedTankFilter)
                {
                    generalCatalogFilter = (from r in generalCatalogFilter where model.ProductionOrder.SelectedTankFilter.Contains(r.TankId) select r).ToList();
                }
            }

            var bayAreaIndex = 0;
            model.ConditioningOrder.BayAreaList = generalCatalogFilter
                                    .Where(x => x.PlantId == model.ProductionOrder.SelectedPlantFilter
                                        && x.ProductId == model.ProductionOrder.SelectedProductFilter
                                        && x.TankId == model.ProductionOrder.SelectedTankFilter && !string.IsNullOrEmpty(x.BayArea))
                                    .Select(x => new BayAreaItem
                                    {
                                        Index = bayAreaIndex++,
                                        BayArea = x.BayArea,
                                        FillingPump = x.FillingPump,
                                        FillingHose = x.FillingHose
                                    }).ToList();
            model.ConditioningOrder.BayAreaFilter = model.ConditioningOrder.BayAreaList
                                    .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();
            for (int i = 0; i < model.ConditioningOrder.EquipamentProcessesList.Count(); i++)
            {
                var txt = model.ConditioningOrder.BayAreaFilter.Where(p => p.Value == model.ConditioningOrder.EquipamentProcessesList.ElementAt(i).Bay).FirstOrDefault()?.Text;
                model.ConditioningOrder.EquipamentProcessesList[i].Bay = txt;
            }

            // fill filters
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var plants = await GetPlantsItemsAsync();
            var products = await GetProdutsItemsAsync(model.ProductionOrder.SelectedPlantFilter);
            var tanks = await GetTanksItemsAsync(model.ProductionOrder.SelectedPlantFilter, model.ProductionOrder.SelectedProductFilter);

            // This are the placeholders in text description
            model.ProductionOrder.Plant = plants.Where(p => p.Value == model.ProductionOrder.SelectedPlantFilter).FirstOrDefault()?.Text;
            model.ProductionOrder.Location = plants.Where(p => p.Value == model.ProductionOrder.SelectedPlantFilter).FirstOrDefault()?.Text;
            model.ProductionOrder.ProductName = products.Where(p => p.Value == model.ProductionOrder.SelectedProductFilter).FirstOrDefault()?.Text;
            model.ProductionOrder.ProductCode = products.Where(p => p.Value == model.ProductionOrder.SelectedProductFilter).FirstOrDefault()?.Value;


            try
            {
                model.ProductionOrder = await _variablesFileReaderService.FillVariablesAsync(model.ProductionOrder);
            }
            catch (Exception ex)
            {

                _logger.LogInformation("Ocurrio un error en orden en la lectura de variables " + ex);
            }



            return View(model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> HistoryNotestRecord(int ProductionOrderId)
        {
            List<HistoryNotes> queryFilesModels = new List<HistoryNotes>();
            try
            {
                var filtered = await _principalService.HistoryNotestRecord(ProductionOrderId, 1);
                queryFilesModels = filtered.Where(x => x.Source == _resource.GetString("OrderSend")
                                    || x.Source == _resource.GetString("OrderApprove")
                                    || x.Source == _resource.GetString("OrderReject")).ToList();
                return Json(new { data = queryFilesModels });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //AHF
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<ActionResult> DownloadFile(int Id, [FromServices] IProductionOrderAttributeRepository productionOrderAttributeRepository, [FromServices] IConfiguration configuration)
        {
            try
            {
                var resultado = await productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == Id && x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
                if (resultado != null && resultado.Any())
                {
                    string basePath = configuration["BasePathQualityFiles"];
                    string path = resultado.Where(x => x.AvgValue == "N/A" && !string.IsNullOrEmpty(x.ChartPath)).Select(x => x.ChartPath).FirstOrDefault();
                    path = $"{basePath}\\{path}";
                    if (!System.IO.File.Exists(path))
                        return NotFound();
                    var fileData = await System.IO.File.ReadAllBytesAsync(path);
                    return File(fileData, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<ActionResult> DownloadFileOA(int Id, string Analisis, [FromServices] IPipeFillingAnalysisRepository pipeFillingAnalysisRepository, [FromServices] IConfiguration configuration)
        {
            try
            {
                string type = $"{Analisis}Analysis";
                var resultado = await pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == Id && x.Type == type && x.ParameterName == "Identidad");
                if (resultado != null && resultado.Any())
                {
                    string basePath = configuration["BasePathControlPipeFiles"];
                    string path = resultado.Where(x => !string.IsNullOrEmpty(x.PathFile)).Select(x => x.PathFile).FirstOrDefault();
                    path = $"{basePath}\\{path}";
                    if (!System.IO.File.Exists(path))
                        return NotFound();
                    var fileData = await System.IO.File.ReadAllBytesAsync(path);
                    return File(fileData, "application/pdf");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> GetPlants()
        {
            var response = await GetPlantsItemsAsync();

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> GetProduts(string plantId)
        {
            var response = await GetProdutsItemsAsync(plantId);

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE)]
        public async Task<IActionResult> GetTanks(string plantId, string productId)
        {
            var response = await GetTanksItemsAsync(plantId, productId);

            return Json(new { Result = "Ok", Data = response });
        }

        private async Task<List<SelectListItem>> GetPlantsItemsAsync()
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(name);

            var plants = await _principalService.GetPlants();
            var plantsByUser = userInfo != null ? userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",") : null;

            var response = plantsByUser != null
                                ? plants.Where(x => plantsByUser.Contains(x.Value))
                                    .Select(x => new SelectListItem
                                    {
                                        Text = x.Text,
                                        Value = x.Value
                                    }).ToList()
                                : new List<SelectListItem>();

            return response;
        }

        private async Task<List<SelectListItem>> GetProdutsItemsAsync(string plantId)
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(name);

            // Verify user has access to the plant requested
            var plantsByUser = userInfo != null ? userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",") : null;
            var plantIds = string.IsNullOrEmpty(plantId) ? new String[0] : plantId.Trim().Replace(" ", "").Split(",");
            if (!plantsByUser.Any(x => plantIds.Any(y => y == x)))
            {
                return new List<SelectListItem>();
            }

            //gets products
            var products = await _principalService.GetProducts();
            var productsFiltered = (await _generalCatalogRepository.GetAsync(x => plantIds.Contains(x.PlantId) && x.Estatus)).Select(x => x.ProductId);

            var response = products.Where(x => productsFiltered.Contains(x.Value)).ToList();

            return response;
        }

        private async Task<List<SelectListItem>> GetTanksItemsAsync(string plantId, string productId)
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(name);

            // Verify user has access to the plant requested
            var plantsByUser = userInfo != null ? userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",") : null;
            var plantIds = string.IsNullOrEmpty(plantId) ? new String[0] : plantId.Trim().Replace(" ", "").Split(",");
            var productIds = string.IsNullOrEmpty(productId) ? new String[0] : productId.Trim().Replace(" ", "").Split(",");
            if (!plantsByUser.Any(x => plantIds.Any(y => y == x)))
            {
                return new List<SelectListItem>();
            }

            //gets tanks
            var tanks = new List<SelectListItem>();
            foreach (var id in productIds)
            {
                tanks.AddRange(await _principalService.GetTanks(id, plantsByUser));
            }
            var tanksFiltered = (await _generalCatalogRepository.GetAsync(x => plantIds.Contains(x.PlantId) && productIds.Contains(x.ProductId))).Select(x => x.TankId);

            var response = tanks.Where(x => tanksFiltered.Contains(x.Value)).ToList();

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> ExportSummary(int Id, [FromServices] ILeyendsCertificateHistoryRepository leyendsCertificateHistoryRepository, [FromServices] ILeyendsFooterCertificateHistoryRepository leyendsFooterCertificateHistoryRepository)
        {
            //ProductionOrder
            MemoryStream ms = new MemoryStream();
            using MemoryStream finalFile = new MemoryStream();
            List<ProductionOrderViewModel> rptDataSource = new List<ProductionOrderViewModel>();
            ///ConditioningOrder
            List<ConditioningOrderViewModel> rptDataSourceOA = new List<ConditioningOrderViewModel>();
            List<ConditioningOrderViewModel> rptDataSourceOA1 = new List<ConditioningOrderViewModel>();
            List<PipeFillingControlViewModel> rptDataSourceOAPC = new List<PipeFillingControlViewModel>();
            MemoryStream msOA = new MemoryStream();
            MemoryStream msOAFull = new MemoryStream();
            using MemoryStream finalFileOA = new MemoryStream();
            PdfDocumentProcessor pdfDocumentProcessorUnion = new PdfDocumentProcessor();
            #region productionOrder
            if (Id <= 0)
            {
                return BadRequest();
            }


            var model = await _exportPDFService.GetOrderProductionModelById(Id);

            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                rptDataSource.Add(model);
                rptOP _rptOP = new rptOP();
                Step5 stp5 = new Step5();
                Step6 stp6 = new Step6();
                Step7 stp7 = new Step7();
                Step8 stp8 = new Step8();
                _rptOP.DataSource = rptDataSource;
                stp5.DataSource = rptDataSource;
                stp6.DataSource = rptDataSource;
                stp7.DataSource = rptDataSource;
                stp8.DataSource = rptDataSource;
                ///validate informe desviación
                XRPanel informe = _rptOP.FindControl("xrPanel1", true) as XRPanel;
                if (!model.PipelineClearanceHistory.Any())
                {
                    informe.Visible = false;
                }
                ///Product Plant 
                XRLabel Leyend = _rptOP.FindControl("xrLabel4", true) as XRLabel;
                var leyend = string.Empty;
                leyend = _resource.GetString("PDFOpEP").Value.Replace("ParamPlant", model.Location);
                leyend = leyend.Replace("ParamProduct", model.ProductName);
                Leyend.Text = leyend;

                //pipeLine
                XRLabel LeyendPl = _rptOP.FindControl("xrLabel26", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("PDFOpPL").Value.Replace("ParamPlant", model.Location);
                leyend = leyend.Replace("ParamProduct", model.ProductName);
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamRawMaterial", "(aire)") : leyend.Replace("ParamRawMaterial", "(raw gas)");
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamRawMaterial2", "aire ambientaL") : leyend.Replace("ParamRawMaterial2", "CO2 gas crudo(raw gas)");
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamChangeCO2", _resource.GetString("InitialTextDLOP").Value) : leyend.Replace("ParamChangeCO2", _resource.GetString("OnlyCO2TextOP").Value);
                LeyendPl.Text = leyend;

                ///lote prod table 4
                leyend = string.Empty;
                XRLabel LeyendLote = _rptOP.FindControl("xrLabel60", true) as XRLabel;
                LeyendLote.Text = LeyendLote.Text.Replace("ParamPlant", model.Location);

                //Change text CO2

                //batchDetails
                XRLabel LeyendBatch = stp7.FindControl("xrLabel3", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("PDFOpBatchDetails").Value.Replace("ParamPlant", model.Location);
                leyend = leyend.Replace("ParamProduct", model.ProductName);
                leyend = leyend.Replace("ParamTankCode", model.SelectedTankFilter);
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamLot", "(recipiente criogénico)") : leyend.Replace("ParamLot", "");
                LeyendBatch.Text = leyend;

                //ReleaseProduct
                XRLabel LeyendRelease = stp8.FindControl("xrLabel6", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("PDFOpReleaseProduct").Value.Replace("ParamPlant", model.Location);
                leyend = leyend.Replace("ParamProduct", model.ProductName);
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamChangeLPCO2", _resource.GetString("SubsectionRPyLPOP").Value) : leyend.Replace("ParamChangeCO2", _resource.GetString("SubsectionRPyLPCO2").Value);
                LeyendRelease.Text = leyend;

                _rptOP.CreateDocument();
                stp5.CreateDocument();
                stp6.CreateDocument();

                stp8.CreateDocument();

                int numRows = model.BatchDetails.Analysis.Count();

                XRTable xrTableValues = stp7.FindControl("xrTable4", true) as XRTable;
                XRTable rTableCell = stp7.FindControl("xrTable1", true) as XRTable;
                ///headers beginers
                for (int i = 0; i < numRows; i++)
                {
                    XRTableCell paramsBatch = new XRTableCell();
                    paramsBatch.Text = model.BatchDetails.Analysis[i].ParameterName;
                    rTableCell.Rows[0].Cells.Add(paramsBatch);
                }
                ///values
                for (int i = 0; i < numRows; i++)
                {
                    XRTableCell BatchValues = new XRTableCell();
                    BatchValues.Text = model.BatchDetails.Analysis[i].Value;
                    BatchValues.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (model.BatchDetails.Analysis[i].ParameterName.ToLower().Trim() == "olor")
                    {
                        BatchValues.Text = "No detectado";
                    }
                    if (model.BatchDetails.Analysis[i].ParameterName.ToLower().Trim() == "identidad")
                    {
                        BatchValues.Text = "Positivo";
                    }
                    if ((model.BatchDetails.Analysis[i].ParameterName.ToLower().Trim() == "olor" || model.BatchDetails.Analysis[i].ParameterName.Trim() == "identidad") && model.BatchDetails.Analysis[i].ParameterName.ToLower().Trim() == "bull")
                    {
                        BatchValues.Text = "NA";
                    }

                    xrTableValues.Rows[0].Cells.Add(BatchValues);
                }
                xrTableValues.Rows.FirstRow.Cells.RemoveAt(0);
                XRTable rTableCellClone = new XRTable();
                rTableCellClone.Rows.Add(rTableCell.Rows.FirstRow);
                rTableCell.Rows.Clear();
                ///parameters
                XRTableRow paramsBatchRow = new XRTableRow();
                rTableCell.Rows.Add(paramsBatchRow);
                ///MeasureUnit
                XRTableRow MeasureUniteRow = new XRTableRow();
                XRTableRow LowerLimitRow = new XRTableRow();
                ///LowerLimit
                rTableCell.Rows.Add(LowerLimitRow);
                rTableCell.Rows.Add(MeasureUniteRow);
                ///UpperLimit
                XRTableRow UpperLimitRow = new XRTableRow();
                rTableCell.Rows.Add(UpperLimitRow);

                ///boby
                foreach (XRTableCell item in rTableCellClone.Rows.FirstRow.Cells)
                {
                    if (item.Text != "")
                    {
                        ///headers
                        XRTableCell paramsBatch = new XRTableCell();
                        paramsBatch.Text = model.BatchDetails.Analysis
                                     .Where(x => x.ParameterName == item.Text).Select(x => x.ParameterName).FirstOrDefault();
                        rTableCell.Rows[0].Cells.Add(paramsBatch);

                        ///MeasureUnit
                        XRTableCell MeasureUniteCell = new XRTableCell();
                        MeasureUniteCell.Text = model.BatchDetails.Analysis
                                     .Where(x => x.ParameterName == item.Text).Select(x => x.MeasureUnit).FirstOrDefault();
                        rTableCell.Rows[1].Cells.Add(MeasureUniteCell);

                        ///LowerLimit 
                        XRTableCell LowerLimitCell = new XRTableCell();
                        LowerLimitCell.Text = "Limite inferior:" + "\r \n" + model.BatchDetails.Analysis
                                                        .Where(x => x.ParameterName == item.Text).Select(x => x.LowerLimit.Replace(",", ".")).FirstOrDefault();
                        rTableCell.Rows[2].Cells.Add(LowerLimitCell);

                        ///UpperLimit 
                        XRTableCell UpperLimitCell = new XRTableCell();
                        UpperLimitCell.Text = "Limite superior:" + "\r \n" + model.BatchDetails.Analysis
                                                        .Where(x => x.ParameterName == item.Text).Select(x => x.UpperLimit.Replace(",", ".")).FirstOrDefault();
                        rTableCell.Rows[3].Cells.Add(UpperLimitCell);
                    }
                }

                ///styles
                var width = rTableCell.WidthF / model.BatchDetails.Analysis.Count;
                foreach (XRTableRow item in rTableCell.Rows)
                {
                    foreach (XRTableCell itemx in item)
                    {
                        item.Cells[itemx.Index].WidthF = width;
                    }

                }
                rTableCell.HeightF = 140;
                rTableCell.WidthF = 412;
                xrTableValues.HeightF = 26.79f;
                //validate 
                foreach (XRTableCell itemx in xrTableValues.Rows.FirstRow.Cells)
                {
                    itemx.Row.Cells[itemx.Index].WidthF = width;
                }

                stp7.CreateDocument();

                List<XtraReport> lstRptControl = new List<XtraReport>();

                List<XtraReport> lstRptCritricalQuality = new List<XtraReport>();

                List<XtraReport> lstRptCriticalParameters = new List<XtraReport>();

                /// to do validate
                model = await _exportPDFService.GetGraphicReport(model);

                //MOCK
                string jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "Reports\\OrderProduction\\datamock.json");
                string json_mock = System.IO.File.ReadAllText(jsonFile);
                var general = await _principalService.GetLimits();

                model.ControlVariables.ForEach(async cv =>
                {

                    //cv.Historical = json_mock;

                    if (!string.IsNullOrEmpty(cv.Historical))
                    {
                        var tittle = string.Format("{0}: {1}", "Variable de control de proceso", cv.Variable);
                        //var str = cv.Variable.Split('/')?[1]; //tittle.Remove(tittle.Length - 1, 1);
                        var val = cv.Variable.Split("/")?[0].TrimEnd(' ');
                        //var Historical = JsonConvert.DeserializeObject<List<HistoricalToGrap>>(cv.Historical);
                        //var max = Historical.Select(x => x.Value).Max();
                        //var min = Historical.Select(x => x.Value).Min();
                        var max = general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                            general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().UpperLimit : "0";

                        var min = general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                            general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().LowerLimit : "0";

                        ControlVariableViewModel control = new ControlVariableViewModel();
                        control = (await _variablesFileReaderService.GetMaxMin(cv.Historical, max, min));

                        XtraReport rpt = _exportPDFService.GetReport(cv.Historical, tittle, control.MaxValue, control.MinValue, cv.Specification, cv.LowLimit, cv.TopLimit);
                        rpt.DataSource = rptDataSource;
                        rpt.CreateDocument();
                        lstRptControl.Add(rpt);
                    }
                });


                model.CriticalParameters.ForEach(async cp =>
                {
                    if (!string.IsNullOrEmpty(cp.Historical))
                    {
                        var tittle = string.Format("{0}: {1}", "Parámetro critico de proceso", cp.Parameter);
                        var val = cp.Parameter.Split("/")?[0].TrimEnd(' ');
                        //var Historical = JsonConvert.DeserializeObject<List<HistoricalToGrap>>(cp.Historical);
                        //var max = Historical.Select(x => x.Value).Max();
                        //var min = Historical.Select(x => x.Value).Min();
                        var max = general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                            general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().UpperLimit : "0";

                        var min = general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                            general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().LowerLimit : "0";

                        ControlVariableViewModel control = new ControlVariableViewModel();
                        control = (await _variablesFileReaderService.GetMaxMin(cp.Historical, max, min));

                        XtraReport rpt = _exportPDFService.GetReport(cp.Historical, tittle, control.MaxValue, control.MinValue, cp.Specification, cp.LowLimit, cp.TopLimit);
                        rpt.DataSource = rptDataSource;
                        rpt.CreateDocument();
                        lstRptCriticalParameters.Add(rpt);
                    }
                });

                model.CriticalQualityAttributes.ForEach(async cq =>
                {
                    if (!string.IsNullOrEmpty(cq.Historical))
                    {
                        var tittle = string.Format("{0}: {1}", "Atributo crítico de calidad", cq.Attribute);
                        var val = cq.Attribute.Split("/")?[0].TrimEnd(' ');
                        //var Historical = JsonConvert.DeserializeObject<List<HistoricalToGrap>>(cq.Historical);
                        //var max = Historical.Select(x => x.Value).Max();
                        //var min = Historical.Select(x => x.Value).Min();
                        var max = general.Where(x => x.PlantId == model.SelectedPlantFilter
                             && x.ProductId == model.SelectedProductFilter
                             && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                             general.Where(x => x.PlantId == model.SelectedPlantFilter
                             && x.ProductId == model.SelectedProductFilter
                             && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().UpperLimit : "0";

                        var min = general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).Any() ?
                            general.Where(x => x.PlantId == model.SelectedPlantFilter
                            && x.ProductId == model.SelectedProductFilter
                            && x.TankId == model.SelectedTankFilter && x.Variable == val).FirstOrDefault().LowerLimit : "0";

                        ControlVariableViewModel control = new ControlVariableViewModel();
                        control = (await _variablesFileReaderService.GetMaxMin(cq.Historical, max, min));


                        XtraReport rpt = _exportPDFService.GetReport(cq.Historical, tittle, control.MaxValue, control.MinValue, cq.Specification, cq.LowLimit,cq.TopLimit);
                        rpt.DataSource = rptDataSource;
                        rpt.CreateDocument();
                        lstRptCritricalQuality.Add(rpt);
                    }
                });

                _rptOP.ModifyDocument(x =>
                {
                    x.AddPages(stp5.Pages);
                    x.AddPages(stp6.Pages);
                    x.AddPages(stp7.Pages);
                    x.AddPages(stp8.Pages);

                    lstRptControl.ForEach(rc =>
                    {
                        rc.CreateDocument();
                        x.AddPages(rc.Pages);
                    });

                    lstRptCriticalParameters.ForEach(rc =>
                    {
                        rc.CreateDocument();
                        x.AddPages(rc.Pages);
                    });

                    lstRptCritricalQuality.ForEach(rc =>
                    {
                        rc.CreateDocument();
                        x.AddPages(rc.Pages);
                    });


                });
                _rptOP.ExportToPdf(ms);
                var ids = model.CriticalQualityAttributes
                    .Where(x => x.Attribute.Contains("Identidad"))
                    .Select(x => x.Id)
                    .ToList();
                var attributes = await this._productionOrderAttributeRepository.GetAsync(x => ids.Contains(x.Id));
                var fileNames = attributes
                    .Where(x => !string.IsNullOrEmpty(System.IO.Path.GetExtension(x.ChartPath)))
                    .Select(x => x.ChartPath)
                    .ToList();
                if (fileNames.Any())
                {
                    using PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor();
                    pdfDocumentProcessor.LoadDocument(ms);
                    foreach (var item in fileNames)
                    {
                        string path = $"{this._config["BasePathQualityFiles"]}\\{item}";
                        if (System.IO.File.Exists(path))
                            pdfDocumentProcessor.AppendDocument(path);
                    }
                    pdfDocumentProcessor.SaveDocument(finalFile);
                    //return File(finalFile.ToArray(), "application/pdf", string.Format("{0}.{1}", model.BatchDetails.Number, "pdf").Replace(" - ", "-"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en ExportSummary " + ex);
                return BadRequest();
            }
            #endregion
            //return File(ms.ToArray(), "application/pdf", string.Format("{0}.{1}", model.BatchDetails.Number, "pdf").Replace(" - ", "-"));
            ////validate isrelease ConditioningOrder
            #region conditioningOrder
            var modelOA = new ConditioningOrderViewModel();
            if (Id <= 0)
            {
                return BadRequest();
            }
            try
            {
                modelOA = await _exportPDFService.GetModel(Id, 0);
                if (modelOA == null)
                {
                    return BadRequest();
                }


                var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
                //foreach (var item in generalCatalogFilter)
                //{
                //    if (item.PlantId == modelOA.PlantId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where modelOA.PlantId.Contains(r.PlantId) select r).ToList();
                //    }
                //    if (item.ProductId == modelOA.ProductId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where modelOA.ProductId.Contains(r.ProductId) select r).ToList();
                //    }
                //    if (item.TankId == modelOA.Tank)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where modelOA.Tank.Contains(r.TankId) select r).ToList();
                //    }
                //}

                var bayAreaIndex = 0;
                modelOA.BayAreaList = generalCatalogFilter
                                        .Where(x => x.PlantId == modelOA.PlantId 
                                        && x.ProductId == modelOA.ProductId && x.TankId == modelOA.Tank 
                                        && !string.IsNullOrEmpty(x.BayArea))
                                        .Select(x => new BayAreaItem
                                        {
                                            Index = bayAreaIndex++,
                                            BayArea = x.BayArea,
                                            FillingPump = x.FillingPump,
                                            FillingHose = x.FillingHose
                                        }).ToList();
                modelOA.BayAreaFilter = modelOA.BayAreaList
                                        .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();



                for (int i = 0; i < modelOA.EquipamentProcessesList.Count(); i++)
                {
                    var txt = modelOA.BayAreaFilter.Where(p => p.Value == modelOA.EquipamentProcessesList.ElementAt(i).Bay).FirstOrDefault()?.Text;
                    modelOA.EquipamentProcessesList[i].Bay = txt;
                }


                rptDataSourceOA.Add(modelOA);
                if ((modelOA != null && modelOA.IsReleased.HasValue && modelOA.IsReleased.Value))
                {
                    RptOrAP _rptOA = new RptOrAP();
                    Rpt6 tabla6 = new Rpt6();
                    Rpt7 tabla7 = new Rpt7();

                    List<RptChkList> rptChkList = new List<RptChkList>();
                    List<Report1> rptCertList = new List<Report1>();
                    var formulas = await _formulaRepository.GetAllAsync();
                    var f1 = formulas.Where(f => f.ProductId == modelOA.ProductId).FirstOrDefault();
                    List<CheckListGeneralViewModel> chkGeneralList = new List<CheckListGeneralViewModel>();
                    List<PipeFillingControlViewModel> pipes = new List<PipeFillingControlViewModel>();
                    foreach (PipeFillingControlViewModel item in modelOA.PipeFillingControl)
                    {
                        ConditioningOrderViewModel m1 = new ConditioningOrderViewModel();
                        m1.Presentation = modelOA.Presentation;
                        m1.Product = modelOA.Product;
                        foreach (PipeFillingViewModel itemx in item.PipesList)
                        {
                            var checklistDB = (from record in await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.TourNumber == itemx.TourNumber
                                                && x.PipeNumber == itemx.PipeNumber && x.DistributionBatch == itemx.DistributionBatch && x.NumOA == modelOA.Id)
                                               select new CheckListGeneralViewModel
                                               {
                                                   Id = record.Id,
                                                   ConditioningOrderId = modelOA.Id,
                                                   TourNumber = record.TourNumber,
                                                   DistributionBatch = record.DistributionBatch,
                                                   Source = record.Source,
                                                   Alias = record.Alias
                                               });
                            if (checklistDB.Any())
                            {
                                foreach (var itemy in checklistDB)
                                {
                                    if (itemy.Id > 0 && itemy.Source == true && !string.IsNullOrEmpty(itemy.Alias))
                                    {
                                        CheckListGeneralViewModel chkGnrl = new CheckListGeneralViewModel();
                                        chkGnrl.Id = itemy.Id;
                                        chkGnrl.ConditioningOrderId = modelOA.Id;
                                        chkGnrl.TourNumber = item.TourNumber;
                                        chkGnrl.DistributionBatch = itemx.DistributionBatch;
                                        chkGeneralList.Add(chkGnrl);
                                    }
                                }

                            }
                            foreach (PipeFillingCustomerViewModel itemy in itemx.Customers)
                            {
                                var pipeg = new PipeFillingViewModel();
                                if (itemy.Folio != null)
                                {
                                    PipeFillingControlViewModel pc = new PipeFillingControlViewModel();
                                    m1.LotProd = modelOA.LotProd;

                                    m1.PipeFillingControl.Clear();
                                    pc.PipesList = new List<PipeFillingViewModel>();
                                    pipeg.Customers = new List<PipeFillingCustomerViewModel>();
                                    pipeg.Customers.Add(new PipeFillingCustomerViewModel
                                    {
                                        ConditioningOrderId = model.Id,
                                        InCompliance = itemy.InCompliance,
                                        AnalysisReport = itemy.AnalysisReport,
                                        TourNumber = itemy.TourNumber,
                                        DeliveryNumber = itemy.DeliveryNumber,
                                        DistributionBatch = itemy.DistributionBatch,
                                        Folio = itemy.AnalysisReport,
                                        Id = itemy.Id,
                                        EmailsList = itemy.EmailsList,
                                        PipeFillingControlId = itemy.PipeFillingControlId,
                                        Name = itemy.Name,
                                        Notes = itemy.Notes,
                                        PlantIdentificador = itemy.PlantIdentificador,
                                        ProductId = itemy.ProductId,
                                        ReviewedBy = itemy.ReviewedBy,
                                        ReviewedDate = itemy.ReviewedDate,
                                        Tank = itemy.Tank,
                                        TankId = itemy.TankId,
                                        state = itemy.state
                                    });
                                    pc.PipesList.Add(new PipeFillingViewModel
                                    {
                                        InCompliance = itemx.InCompliance,
                                        PipeNumber = itemx.PipeNumber,
                                        Date = itemx.Date,
                                        TourNumber = itemx.TourNumber,
                                        DueDate = itemx.DueDate,
                                        AnalyzedBy = itemx.AnalyzedBy,
                                        AnalyzedDate = itemx.AnalyzedDate,
                                        DistributionBatch = itemx.DistributionBatch,
                                        DiffWeight = itemx.DiffWeight,
                                        FinalAnalysis = itemx.FinalAnalysis,
                                        FinalWeight = itemx.FinalWeight,
                                        FinalAnalyzedDate = itemx.FinalAnalyzedDate,
                                        Customers = pipeg?.Customers.ToList(),
                                        ReleasedBy = itemx.ReleasedBy,
                                        ReleasedDate = itemx.ReleasedDate,
                                        IsReleased = itemx.IsReleased,
                                        ConditioningOrderId = itemx.ConditioningOrderId,
                                        InitialAnalysis = itemx.InitialAnalysis,
                                        InitialAnalyzedDate = itemx.InitialAnalyzedDate,
                                        ReportPNCFolio = itemx.ReportPNCFolio,
                                        InitialWeight = itemx.InitialWeight,
                                        ReportPNCNotes = itemx.ReportPNCNotes
                                    });
                                    //pc.PipesList.Add(itemx);
                                    m1.PipeFillingControl.Add(new PipeFillingControlViewModel
                                    {
                                        Id = item.Id,
                                        TourNumber = item.TourNumber,
                                        PipesList = pc.PipesList
                                    });
                                    //m1.PipeFillingControl.Add(pc);

                                    Report1 rtp1 = new Report1();
                                    XRLabel LblRegister = rtp1.FindControl("XRLabel35", true) as XRLabel;
                                    LblRegister.Text = f1.RegisterCode != null ? f1.RegisterCode : null;
                                    XRLabel LblNumPraxiar = rtp1.FindControl("xrLabel24", true) as XRLabel;
                                    LblNumPraxiar.Text = await _exportPDFService.GetTankClient(itemx.DistributionBatch, itemx.TourNumber, itemy.Tank);
                                    //layout
                                    XRRichText LabelHeaderOne = rtp1.FindControl("xrRichText1", true) as XRRichText;
                                    XRRichText LabelLeyendOne = rtp1.FindControl("xrRichText2", true) as XRRichText;
                                    XRRichText LabelLeyendTwo = rtp1.FindControl("xrRichText3", true) as XRRichText;
                                    //LabelLeyendTwo.Font = new System.Drawing.Font("Arial", 9);
                                    XRRichText LabelFooter = rtp1.FindControl("xrRichText4", true) as XRRichText;
                                    XRPictureBox xRPictureBox = rtp1.FindControl("xrPictureBox1", true) as XRPictureBox;
                                    var Certificate = (await leyendsCertificateHistoryRepository.GetAsync(x => x.Id.Equals(modelOA.CertificateId))).FirstOrDefault();
                                    var Footer = (await leyendsFooterCertificateHistoryRepository.GetAsync(x => x.Id.Equals(modelOA.FooterCertificateId))).FirstOrDefault();
                                    if (Certificate != null && Footer != null)
                                    {
                                        LabelHeaderOne.Html = Certificate.HeaderOne;
                                        LabelLeyendOne.Html = Certificate.LeyendOne;
                                        LabelLeyendTwo.Html = Certificate.LeyendTwo;
                                        LabelFooter.Html = Footer.Footer;
                                        xRPictureBox.ImageUrl = this._config["urlWebApp"] + "/" + _resource.GetString("LogoFiles").Value + "/" + Certificate.HeaderTwo;
                                        xRPictureBox.HeightF = 83;
                                        xRPictureBox.WidthF = 151;
                                    }
                                    #region states certificate 
                                    XRLabel LblDescriptionState = rtp1.FindControl("xrLabel28", true) as XRLabel;
                                    if (itemy.state.HasValue && itemy.state == false)
                                    {
                                        LblDescriptionState.Text = _resource.GetString("ReRounting").Value;
                                        LblDescriptionState.ForeColor = Color.Red;
                                    }
                                    #endregion
                                    #region parameters
                                    XRTable xrTableParameters = rtp1.FindControl("xrTable1", true) as XRTable;
                                    XRTableCell xRTableCellParameterName = xrTableParameters.FindControl("xrTableCell1", true) as XRTableCell;
                                    XRTable xrTableValueExpected = rtp1.FindControl("xrTable2", true) as XRTable;
                                    XRTableCell xRTableCellValueExpected = xrTableParameters.FindControl("xrTableCell2", true) as XRTableCell;
                                    XRTable xrTableValueReal = rtp1.FindControl("xrTable3", true) as XRTable;
                                    XRTableCell xRTableCellValueReal = xrTableParameters.FindControl("xrTableCell3", true) as XRTableCell;
                                    XRTable xrTableAnalizador = rtp1.FindControl("xrTable4", true) as XRTable;
                                    XRTableCell xRTableCellAnalizador = xrTableParameters.FindControl("xrTableCell4", true) as XRTableCell;
                                    int numRows = itemx.FinalAnalysis.Count();
                                    for (int i = 0; i < numRows; i++)
                                    {
                                        XRTableCell rTableCellParameterName = new XRTableCell();
                                        rTableCellParameterName.Text = itemx.FinalAnalysis[i].ParameterName;
                                        XRTableRow RowParameterName = new XRTableRow();
                                        RowParameterName.Cells.Add(rTableCellParameterName);
                                        xrTableParameters.Rows.Add(RowParameterName);

                                        XRTableCell rTableCellValueExpected = new XRTableCell();
                                        rTableCellValueExpected.Text = itemx.FinalAnalysis[i].ParameterName == "Identidad" ?
                                              "Positiva" : itemx.FinalAnalysis[i].ValueExpected;
                                        XRTableRow RowValueExpected = new XRTableRow();
                                        RowValueExpected.Cells.Add(rTableCellValueExpected);
                                        xrTableValueExpected.Rows.Add(RowValueExpected);
                                        //string.Format("{0:f2}", float.Parse(item.FinalAnalysis[i].ValueReal.Replace(",", ".")))

                                        var plit = itemx.FinalAnalysis[i].ValueReal.Split(",");

                                        decimal myNumber = 0;
                                        if (plit?[1].Length > 2)
                                            myNumber = decimal.Parse(string.Format("{0:f2}", decimal.Parse(plit?[0] + "," + plit?[1].Substring(0, 2))));
                                        else
                                            myNumber = decimal.Parse(string.Format("{0:f2}", decimal.Parse(itemx.FinalAnalysis[i].ValueReal)));

                                        XRTableCell rTableCellValueReal = new XRTableCell();
                                        rTableCellValueReal.Text = itemx.FinalAnalysis[i].ParameterName == "Identidad" ?
                                            itemx.FinalAnalysis[i].ValueReal == "1,0000" ? "Positiva" : "Negativa" :
                                        myNumber.ToString().Replace(",", ".");
                                        XRTableRow RowValueReal = new XRTableRow();
                                        RowValueReal.Cells.Add(rTableCellValueReal);
                                        xrTableValueReal.Rows.Add(RowValueReal);

                                        XRTableCell rTableCellAnalizador = new XRTableCell();
                                        rTableCellAnalizador.Text = await _exportPDFService.GetAnalizador(itemx.DistributionBatch,
                                                                     itemx.FinalAnalysis[i].ParameterName);
                                        //XRLabel LblNumPraxiar = report.FindControl("xrLabel24", true) as XRLabel;
                                        //LblNumPraxiar.Text = await _exportPDFService.GetTankClient(itemx.DistributionBatch, item.TourNumber);
                                        XRTableRow RowValueAnalizador = new XRTableRow();
                                        RowValueAnalizador.Cells.Add(rTableCellAnalizador);
                                        xrTableAnalizador.Rows.Add(RowValueAnalizador);
                                    }

                                    xrTableParameters.Rows.FirstRow.Cells.RemoveAt(0);
                                    xrTableValueExpected.Rows.FirstRow.Cells.RemoveAt(0);
                                    xrTableValueReal.Rows.FirstRow.Cells.RemoveAt(0);
                                    xrTableAnalizador.Rows.FirstRow.Cells.RemoveAt(0);

                                    #endregion
                                    rptDataSourceOA1.Clear();
                                    rptDataSourceOA1.Add(m1);
                                    rtp1.DataSource = rptDataSourceOA1;
                                    rtp1.CreateDocument();
                                    rptCertList.Add(rtp1);
                                }
                            }
                        }
                    }

                    if (chkGeneralList.Count > 0)
                    {
                        for (int i = 0; i < chkGeneralList.Count; i++)
                        {
                            var t = await _exportPDFService.GetRptChkList(chkGeneralList[i]);
                            rptChkList.Add(t);
                        }
                    }
                    var steps4 = new List<Step4>();
                    var pipesList = modelOA.PipeFillingControl.Where(x => x != null && x.PipesList != null && x.PipesList.Any()).SelectMany(x => x.PipesList).ToList();
                    foreach (var item in pipesList)
                    {
                        var step4 = await this._exportPDFService.BuildTableAnalisys(item, rptDataSourceOA);
                        steps4.Add(step4);
                    }
                    _rptOA.DataSource = rptDataSourceOA;
                    tabla6.DataSource = rptDataSourceOA;
                    tabla7.DataSource = rptDataSourceOA;


                    XRLabel LeyendRelease = _rptOA.FindControl("label32", true) as XRLabel;
                    var leyend = string.Empty;
                    leyend = _resource.GetString("AnaliticsEqOA").Value.Replace("ParamPlant", modelOA.Plant);
                    leyend = leyend.Replace("ParamProduct", modelOA.Product);
                    LeyendRelease.Text = leyend;

                    LeyendRelease = _rptOA.FindControl("label26", true) as XRLabel;
                    leyend = string.Empty;
                    leyend = _resource.GetString("APTextOA").Value.Replace("ParamPlant", modelOA.Plant);
                    leyend = leyend.Replace("ParamProduct", modelOA.Product);
                    LeyendRelease.Text = leyend;

                    LeyendRelease = _rptOA.FindControl("label50", true) as XRLabel;
                    leyend = string.Empty;
                    leyend = _resource.GetString("CLPTextOA").Value.Replace("ParamPlant", modelOA.Plant);
                    leyend = leyend.Replace("ParamProduct", modelOA.Product);
                    LeyendRelease.Text = leyend;

                    LeyendRelease = tabla6.FindControl("label32", true) as XRLabel;
                    leyend = string.Empty;
                    leyend = _resource.GetString("CATextOA").Value.Replace("ParamProduct", modelOA.Product);
                    LeyendRelease.Text = leyend;

                    LeyendRelease = tabla6.FindControl("label5", true) as XRLabel;
                    leyend = string.Empty;
                    leyend = _resource.GetString("RPATextOA").Value.Replace("ParamProduct", modelOA.Product);
                    leyend = modelOA.ProductName != "CO2" ? leyend.Replace("ParamChangeCO2_RPA", _resource.GetString("ParagraphRPA").Value) : leyend.Replace("ParamChangeCO2_RPA", _resource.GetString("ParagraphCO2_RPA").Value);
                    LeyendRelease.Text = leyend;

                    LeyendRelease = tabla7.FindControl("label32", true) as XRLabel;
                    leyend = string.Empty;
                    leyend = _resource.GetString("LPTTextOA").Value.Replace("ParamPlant", modelOA.Plant);
                    LeyendRelease.Text = leyend;

                    _rptOA.CreateDocument();
                    tabla6.CreateDocument();
                    tabla7.CreateDocument();
                    _rptOA.ModifyDocument(x =>
                    {
                        if (steps4.Any())
                        {
                            steps4.ForEach(y => x.AddPages(y.Pages));
                        }
                        x.AddPages(tabla6.Pages);
                        x.AddPages(tabla7.Pages);
                        if (rptChkList.Count > 0)
                        {
                            rptChkList.ForEach(chk =>
                            {
                                x.AddPages(chk.Pages);
                            });
                        }
                        rptCertList.ForEach(cert =>
                        {

                            x.AddPages(cert.Pages);
                        });
                    });
                    _rptOA.ExportToPdf(msOA);
                    var fileNames = modelOA.PipeFillingControl.SelectMany(x => x.PipesList)
                        .SelectMany(x => x.FinalAnalysis)
                        .Union(modelOA.PipeFillingControl.SelectMany(x => x.PipesList)
                        .SelectMany(x => x.InitialAnalysis))
                        .Where(x => x.ParameterName == "Identidad" && !string.IsNullOrEmpty(System.IO.Path.GetExtension(x.PathFile)))
                        .Select(x => x.PathFile)
                        .ToList();
                    if (fileNames.Any())
                    {
                        using PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor();
                        pdfDocumentProcessor.LoadDocument(msOA);
                        foreach (var item in fileNames)
                        {
                            string path = $"{this._config["BasePathControlPipeFiles"]}\\{item}";
                            if (System.IO.File.Exists(path))
                                pdfDocumentProcessor.AppendDocument(path);
                        }

                        pdfDocumentProcessor.SaveDocument(finalFileOA);
                        //return File(finalFileOA.ToArray(), "application/pdf", string.Format("{0}.{1}", modelOA.LotProd, "pdf").Replace(" - ", "-"));
                    }


                    if (finalFileOA.Length > 0)
                    {
                        pdfDocumentProcessorUnion.LoadDocument(finalFileOA);
                        pdfDocumentProcessorUnion.AppendDocument(msOA);
                        pdfDocumentProcessorUnion.SaveDocument(msOA);
                    }
                    ///validate OP full file
                    if (finalFile.Length > 0)
                    {
                        pdfDocumentProcessorUnion.LoadDocument(finalFile);
                        pdfDocumentProcessorUnion.AppendDocument(msOA);
                        pdfDocumentProcessorUnion.SaveDocument(msOA);
                    }
                    else
                    {
                        pdfDocumentProcessorUnion.LoadDocument(ms);
                        pdfDocumentProcessorUnion.AppendDocument(msOA);
                        pdfDocumentProcessorUnion.SaveDocument(msOA);
                    }
                    return File(msOA.ToArray(), "application/pdf", string.Format("{0}.{1}", modelOA.LotProd, "pdf").Replace(" - ", "-"));


                }
                else
                {
                    if (finalFile.Length > 0)
                        return File(finalFile.ToArray(), "application/pdf", string.Format("{0}.{1}", model.BatchDetails.Number, "pdf").Replace(" - ", "-"));
                    else
                        return File(ms.ToArray(), "application/pdf", string.Format("{0}.{1}", modelOA.LotProd, "pdf").Replace(" - ", "-"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de ExportToPdf " + ex);
                return BadRequest();
            }
        }
        #endregion

        //AHF
        public string GetDateFromNumberProduction(string NumberProduction)
        {
            var list = !string.IsNullOrEmpty(NumberProduction) ? NumberProduction.Replace(@"\n", "").Trim().Split("-") : new string[1];
            string date = list.Length > 3 ? $"{list[2]}{list[list.Length - 1]}" : string.Empty;
            return date;
        }

        [HttpGet]
        public async Task<IActionResult> ExportDistribuitionBatchClients(int Id)
        {
            var DistribuitionBatch = new QuerysFiles();
            List<QuerysFiles> rptCertList = new List<QuerysFiles>();
            DetailExpLote model = new DetailExpLote();
            MemoryStream msOA = new MemoryStream();
            var basicInfo = new ConditioningOrderViewModel();
            try
            {
                basicInfo = await _conditioningOrderService.GetBasicInfoConditioningOrder(Id);
                model.detailDB = await _principalService.GetDetailDB(Id);

                //DistribuitionBatch.CreateDocument();
                foreach (var item in model.detailDB.Where(x => x.Items.Count > 0 && !string.IsNullOrEmpty(x.DistributionBatch)))
                {
                    QuerysFiles rtp1 = new QuerysFiles();
                    if (item.Items.Count > 0)
                    {
                        rtp1.DataSource = item.Items;
                        XRLabel LblPlant = rtp1.FindControl("label37", true) as XRLabel;
                        LblPlant.Text = basicInfo.Plant;
                        XRLabel LblProduct = rtp1.FindControl("label38", true) as XRLabel;
                        LblProduct.Text = basicInfo.Product;
                        XRLabel LblLote = rtp1.FindControl("label39", true) as XRLabel;
                        LblLote.Text = basicInfo.LotProd;
                        rtp1.CreateDocument();
                        rptCertList.Add(rtp1);
                    }
                }

                DistribuitionBatch.ModifyDocument(x =>
                {
                    rptCertList.ForEach(cert =>
                    {

                        x.AddPages(cert.Pages);
                    });
                });

                DistribuitionBatch.ExportToPdf(msOA);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de ExportDistribuitionBatchClients " + ex);
                return BadRequest();
            }

            return File(msOA.ToArray(), "application/pdf", string.Format("{0}.{1}", basicInfo.LotProd, "pdf").Replace(" - ", "-"));
        }
    }
}
