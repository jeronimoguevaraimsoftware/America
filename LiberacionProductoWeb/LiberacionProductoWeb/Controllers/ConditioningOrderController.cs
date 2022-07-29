using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using DevExpress.XtraReports.UI;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Reports;
using LiberacionProductoWeb.Reports.ConditioningOrder;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using DevExpress.Pdf;
using LiberacionProductoWeb.Models.SechToolViewModels;
using LiberacionProductoWeb.Reports.OrderProduction;
using LiberacionProductoWeb.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ConditioningOrderController : Controller
    {
        private readonly ILogger<ConditioningOrderController> _logger;
        private readonly IStringLocalizer<ConditioningOrderController> _localizer;
        private readonly IConditioningOrderService _conditioningOrderService;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMonitoringEquipmentRepository _monitoringEquipmentRepository;
        private readonly ILoteDistribuicionRepository _loteDistribuicionRepository;
        private readonly ILoteDistribuicionDetalleRepository _loteDistribuicionDetalleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        private readonly IPipelineClearanceOARepository _pipelineClearanceOARepository;
        private readonly IScalesFlowMetersRepository _scalesFlowMetersRepository;
        private readonly IAnalyticalEquipamentRepository _analyticalEquipamentRepository;
        private readonly IEquipmentProcessConditioningRepository _equipmentProcessConditioningRepository;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IPerformanceProcessConditioningRepository _performanceProcessConditioningRepository;
        private readonly IHistoryNotesRepository _historyNotesRepository;
        private readonly IHistoryStatesRepository _historyStatesRepository;
        private readonly IPipeFillingControlRepository _pipeFillingControlRepository;
        private readonly IPipeFillingRepository _pipeFillingRepository;
        private readonly IPipeFillingAnalysisRepository _pipeFillingAnalysisRepository;
        private readonly ICheckListPipeAnswerRepository _checkListPipeAnswerRepository;
        private readonly IPipeFillingCustomerRepository _pipeFillingCustomerRepository;
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly IAnalyticsCertsService _analyticsCertsService;
        private readonly IPrincipalService _principalService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly INotification _notification;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IExportPDFService _exportPDFService;
        // LAS DOI CheckList ----->
        private readonly ICheckListPipeCommentsAnswerReepository _checkListPipeCommentsAnswerReepository;
        private readonly IFormulaCatalogRepository _formulaRepository;
        public bool WSMexeFuncionalidad;
        private readonly int[] plantsByUser;
        public ConditioningOrderController(
            ILogger<ConditioningOrderController> logger,
            IConfiguration config,
            IStringLocalizer<ConditioningOrderController> localizer,
            IConditioningOrderService conditioningOrderService,
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            IMonitoringEquipmentRepository monitoringEquipmentRepository,
            ILoteDistribuicionRepository loteDistribuicionRepository,
            ILoteDistribuicionDetalleRepository loteDistribuicionDetalleRepository,
            IHttpContextAccessor httpContextAccessor,
            IConditioningOrderRepository conditioningOrderRepository,
            IPipelineClearanceOARepository pipelineClearanceOARepository,
            IScalesFlowMetersRepository scalesFlowMetersRepository,
            IAnalyticalEquipamentRepository analyticalEquipamentRepository,
            IEquipmentProcessConditioningRepository equipmentProcessConditioningRepository,
            IGeneralCatalogRepository generalCatalogRepository,
            IProductionOrderRepository productionOrderRepository,
            IPerformanceProcessConditioningRepository performanceProcessConditioningRepository,
            IHistoryNotesRepository historyNotesRepository,
            IHistoryStatesRepository historyStatesRepository,
            IPipeFillingControlRepository pipeFillingControlRepository,
            IPipeFillingRepository pipeFillingRepository,
            IPipeFillingAnalysisRepository pipeFillingAnalysisRepository,
            ICheckListPipeAnswerRepository checkListPipeAnswerRepository,
            IPipeFillingCustomerRepository pipeFillingCustomerRepository,
            ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository,
            ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository,
            IAnalyticsCertsService analyticsCertsService,
            IPrincipalService principalService,
            IStringLocalizer<Resource> resource,
            IWebHostEnvironment hostEnvironment,
            ICheckListPipeCommentsAnswerReepository checkListPipeCommentsAnswerReepository,
            IFormulaCatalogRepository formulaRepository,
            IExportPDFService exportPDFService,
            INotification notification
            )
        {
            _logger = logger;
            _localizer = localizer;
            _conditioningOrderService = conditioningOrderService;
            _context = context;
            _userManager = userManager;
            _monitoringEquipmentRepository = monitoringEquipmentRepository;
            _loteDistribuicionRepository = loteDistribuicionRepository;
            _loteDistribuicionDetalleRepository = loteDistribuicionDetalleRepository;
            _httpContextAccessor = httpContextAccessor;
            _conditioningOrderRepository = conditioningOrderRepository;
            _pipelineClearanceOARepository = pipelineClearanceOARepository;
            _scalesFlowMetersRepository = scalesFlowMetersRepository;
            _analyticalEquipamentRepository = analyticalEquipamentRepository;
            _equipmentProcessConditioningRepository = equipmentProcessConditioningRepository;
            _generalCatalogRepository = generalCatalogRepository;
            _productionOrderRepository = productionOrderRepository;
            _performanceProcessConditioningRepository = performanceProcessConditioningRepository;
            _historyNotesRepository = historyNotesRepository;
            _historyStatesRepository = historyStatesRepository;
            _pipeFillingControlRepository = pipeFillingControlRepository;
            _pipeFillingRepository = pipeFillingRepository;
            _pipeFillingAnalysisRepository = pipeFillingAnalysisRepository;
            _checkListPipeAnswerRepository = checkListPipeAnswerRepository;
            _pipeFillingCustomerRepository = pipeFillingCustomerRepository;
            _checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _analyticsCertsService = analyticsCertsService;
            _principalService = principalService;
            _hostEnvironment = hostEnvironment;
            this._notification = notification;
            _resource = resource;
            _checkListPipeCommentsAnswerReepository = checkListPipeCommentsAnswerReepository;
            _config = config;
            _formulaRepository = formulaRepository;
            _exportPDFService = exportPDFService;
            try
            {
                var name = httpContextAccessor.HttpContext.User.Identity.Name;
                var identity = userManager.FindByNameAsync(name);
                if (identity != null)
                {
                    var sourcePlants = identity.Result.PlantaUsuario?.Split(",")?.ToList();
                    if (sourcePlants != null)
                    {
                        var i = 0;
                        plantsByUser = new int[sourcePlants.Count];
                        sourcePlants.ForEach(
                            p =>
                            {
                                plantsByUser[i] = Convert.ToInt32(p);
                                i++;
                            }
                            );
                    }
                }
            }
            catch
            { }
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_OA)]
        public async Task<IActionResult> Index(int Id, int IdOP)
        {
            var model = new ConditioningOrderViewModel();
            try
            {
                if (IdOP > 0 && Id == 0)
                {
                    model = await _conditioningOrderService.GetByProductionOrderIdAsync(IdOP);
                }
                else if (Id > 0)
                {
                    model = await _conditioningOrderService.GetByIdAsync(Id);
                }

                if (model == null)
                {
                    return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
                }

                // Get info from general catalog
                var productionOrderEntity = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
                var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
                //foreach (var item in generalCatalogFilter)
                //{
                //    if (item.PlantId == productionOrderEntity.PlantId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.PlantId.Contains(r.PlantId) select r).ToList();
                //    }
                //    if (item.ProductId == productionOrderEntity.ProductId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.ProductId.Contains(r.ProductId) select r).ToList();
                //    }
                //    if (item.TankId == productionOrderEntity.TankId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.TankId.Contains(r.TankId) select r).ToList();
                //    }
                //}

                var bayAreaIndex = 0;
                model.BayAreaList = generalCatalogFilter
                                        .Where(x => x.PlantId == productionOrderEntity.PlantId
                                        && x.ProductId == productionOrderEntity.ProductId
                                        && x.TankId == productionOrderEntity.TankId && !string.IsNullOrEmpty(x.BayArea))
                                        .Select(x => new BayAreaItem
                                        {
                                            Index = bayAreaIndex++,
                                            BayArea = x.BayArea,
                                            FillingPump = x.FillingPump,
                                            FillingHose = x.FillingHose
                                        }).ToList();
                model.BayAreaFilter = model.BayAreaList
                                        .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();



                for (int i = 0; i < model.EquipamentProcessesList.Count(); i++)
                {
                    var txt = model.BayAreaFilter.Where(p => p.Value == model.EquipamentProcessesList.ElementAt(i).Bay).FirstOrDefault()?.Text;
                    model.EquipamentProcessesList[i].Bay = txt;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de acondicionamiento " + ex);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<IActionResult> New(int Id, int IdOP)
        {
            var model = new ConditioningOrderViewModel();
            try
            {
                if (IdOP > 0 && Id == 0)
                {
                    model = await _conditioningOrderService.GetByProductionOrderIdAsync(IdOP);
                }
                else if (Id > 0)
                {
                    model = await _conditioningOrderService.GetByIdAsync(Id);
                }

                if (model == null)
                {
                    return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
                }
                model.ReasonReject = null;
                // Get info from general catalog
                var productionOrderEntity = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
                var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
                //foreach (var item in generalCatalogFilter)
                //{
                //    if (item.PlantId == productionOrderEntity.PlantId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.PlantId.Contains(r.PlantId) select r).ToList();
                //    }
                //    if (item.ProductId == productionOrderEntity.ProductId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.ProductId.Contains(r.ProductId) select r).ToList();
                //    }
                //    if (item.TankId == productionOrderEntity.TankId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.TankId.Contains(r.TankId) select r).ToList();
                //    }
                //}

                var plantsList = await GetPlantsItemsAsync();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var plantsId = userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",");
                var productsList = await _principalService.GetProductsXPlants(plantsId, plantsList, 1);

                //model.Location = plantsList.Where(p => p.Value == model.Plant).FirstOrDefault()?.Text;
                //model.ProductName = productsList.Where(p => p.Value == model.Product).FirstOrDefault()?.Text;
                //model.ProductCode = productsList.Where(p => p.Value == model.Product).FirstOrDefault()?.Value;

                if (model.Plant == null)
                    model.Plant = plantsList.Where(p => p.Value == model.Plant).FirstOrDefault()?.Text;

                var bayAreaIndex = 0;

                model.BayAreaList = generalCatalogFilter
                                        .Where(x => x.PlantId == productionOrderEntity.PlantId 
                                        && x.ProductId == productionOrderEntity.ProductId
                                        && x.TankId == productionOrderEntity.TankId && !string.IsNullOrEmpty(x.BayArea))
                                        .Select(x => new BayAreaItem
                                        {
                                            Index = bayAreaIndex++,
                                            BayArea = x.BayArea,
                                            FillingPump = x.FillingPump,
                                            FillingHose = x.FillingHose
                                        }).ToList();
                model.BayAreaFilter = model.BayAreaList
                                        .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();
                model.LotProd = model.LotProd.Replace("\n", "").Trim();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de acondicionamiento " + ex);
                return BadRequest(ex);
            }

        }

        private async Task<List<SelectListItem>> GetPlantsItemsAsync(int id = -1)
        {
            //filter by user
            Dictionary<int, string> plants = await _analyticsCertsService.GetPlants();
            List<SelectListItem> response = new List<SelectListItem>();

            foreach (KeyValuePair<int, string> entry in plants)
            {
                if (plantsByUser != null)
                {
                    if (plantsByUser.ToList().Contains(entry.Key))
                    {
                        response.Add(new SelectListItem
                        {
                            Text = entry.Value,
                            Value = Convert.ToString(entry.Key)

                        });
                    }
                }
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetProdutsItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            Dictionary<string, string> productsFiltered = new Dictionary<string, string>();

            //gets products
            if (plantsByUser != null)
            {
                foreach (var item in plantsByUser.ToList())
                {
                    var products = await _analyticsCertsService.GetProducts(item);
                    foreach (var p in products)
                    {
                        if (!productsFiltered.ContainsKey(p.Key))
                            productsFiltered.Add(p.Key, p.Value);
                    }
                }
            }


            foreach (KeyValuePair<string, string> entry in productsFiltered)
            {
                response.Add(new SelectListItem
                {
                    Text = entry.Value,
                    Value = Convert.ToString(entry.Key)

                });
            }

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipment(string tournumberList, int id, DateTime? datePipelineClearance, bool refresh = false)
        {
            var model = new ConditioningOrderViewModel();
            var flagTnumber = string.Empty;

            if (id > 0)
            {
                try
                {
                    model = await _conditioningOrderService.GetByIdAsync(id);

                    if (model.EquipamentProcessesList == null)
                    {
                        model.EquipamentProcessesList = new List<EquipamentProcessConditioningViewModel>();
                    }
                    if (!string.IsNullOrEmpty(tournumberList))
                    {
                        foreach (var tournumber in tournumberList.Split(','))
                        {
                            if (refresh)
                            {
                                var equipamentProcessesList = await _conditioningOrderService.GetTable4(tournumber, id, datePipelineClearance);
                                var equipamentProcessesListValid = equipamentProcessesList.Where(x => string.IsNullOrEmpty(x.ErrorMessage));
                                var equipamentProcessesListInvalid = equipamentProcessesList.Where(x => !string.IsNullOrEmpty(x.ErrorMessage)).Select(x => x.PipeNumber);

                                if (!model.EquipamentProcessesList.Any(x => x.TourNumber == tournumber))
                                {
                                    // If does not contain the tour number, it should be added
                                    if (equipamentProcessesListValid != null && equipamentProcessesListValid.Any())
                                    {
                                        model.EquipamentProcessesList.AddRange(equipamentProcessesListValid);
                                        flagTnumber = tournumber;
                                    }
                                }
                                else
                                {
                                    foreach (var item in equipamentProcessesListValid)
                                    {
                                        if (!model.EquipamentProcessesList.Any(x => x.TourNumber == tournumber && x.PipeNumber == item.PipeNumber))
                                        {
                                            if (equipamentProcessesListValid != null && equipamentProcessesListValid.Any())
                                            {
                                                model.EquipamentProcessesList.AddRange(equipamentProcessesListValid);
                                                flagTnumber = tournumber;
                                            }
                                        }
                                    }
                                }

                                if (equipamentProcessesListInvalid != null && equipamentProcessesListInvalid.Any())
                                {
                                    model.EquipamentProcessesError += " " + string.Join(' ', equipamentProcessesListInvalid);
                                }
                            }
                            else if (!model.EquipamentProcessesList.Any(x => x.TourNumber == tournumber))
                            {
                                // Also add to PipeFillingControl
                                var equipamentProcessesList = await _conditioningOrderService.GetTable4(tournumber, id, datePipelineClearance);
                                var equipamentProcessesListValid = equipamentProcessesList.Where(x => string.IsNullOrEmpty(x.ErrorMessage));
                                var equipamentProcessesListInvalid = equipamentProcessesList.Where(x => !string.IsNullOrEmpty(x.ErrorMessage)).Select(x => x.PipeNumber);

                                if (equipamentProcessesListValid != null && equipamentProcessesListValid.Any())
                                {
                                    model.EquipamentProcessesList.AddRange(equipamentProcessesListValid);
                                    flagTnumber = tournumber;
                                }

                                if (equipamentProcessesListInvalid != null && equipamentProcessesListInvalid.Any())
                                {
                                    model.EquipamentProcessesError += " " + string.Join(' ', equipamentProcessesListInvalid);
                                }
                            }

                        }
                    }
                    // Get info from general catalog
                    var productionOrderEntity = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
                    var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
                    foreach (var item in generalCatalogFilter)
                    {
                        if (item.PlantId == productionOrderEntity.PlantId)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.PlantId.Contains(r.PlantId) select r).ToList();
                        }
                        if (item.ProductId == productionOrderEntity.ProductId)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.ProductId.Contains(r.ProductId) select r).ToList();
                        }
                        if (item.TankId == productionOrderEntity.TankId)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where productionOrderEntity.TankId.Contains(r.TankId) select r).ToList();
                        }
                    }

                    var bayAreaIndex = 0;
                    model.BayAreaList = generalCatalogFilter
                                            .Where(x => x.PlantId == productionOrderEntity.PlantId
                                            && x.ProductId == productionOrderEntity.ProductId
                                            && x.TankId == productionOrderEntity.TankId && !string.IsNullOrEmpty(x.BayArea))
                                            .Select(x => new BayAreaItem
                                            {
                                                Index = bayAreaIndex++,
                                                BayArea = x.BayArea,
                                                FillingPump = x.FillingPump,
                                                FillingHose = x.FillingHose
                                            }).ToList();
                    model.BayAreaFilter = model.BayAreaList
                                            .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Ocurrio un error en OA equipos (accesorios) " + ex);
                }
            }

            if (refresh && string.IsNullOrEmpty(flagTnumber))
            {
                // Refresh but no changes
                return PartialView("_EquipmentProcess", model);
            }
            else if (!string.IsNullOrEmpty(flagTnumber))
            {
                // There are changes with one or more tournumbers
                await SaveEquipmentProcess(model);

                return PartialView("_EquipmentProcess", model);
            }
            else
            {
                return Json(new { Result = "NotFound", Message = "TourNumber no encontrado" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPipeFillingControls(int id)
        {
            var model = new ConditioningOrderViewModel();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            try
            {
                model = await _conditioningOrderService.GetByIdAsync(id, true, userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en OA control de llenado de pipas " + ex);
            }

            return PartialView("_PipeFillingControl", model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<JsonResult> SaveInitOA(int Id)
        {
            var entity = new ConditioningOrder();
            if (Id > 0)
            {
                var info = await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == Id);
                if (info.Count == 0)
                {
                    var userInfo = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

                    entity = ConditioningOrder.Create(
                        0,
                        Id,
                        userInfo.NombreUsuario.ToString(),
                        "",
                        "",
                        "",
                        0,
                        0
                    );
                    var objDB = await _conditioningOrderService.AddAsync(entity);



                    return Json(new { Result = "Ok", Message = "Se guardó la OA" });
                }
                return Json(new { Result = "Ok", Message = "Ya existe la OA" });
            }
            else
            {
                return Json(new { Result = "Fail", Message = "Sin Id" });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OA)]
        [RequestFormLimits(ValueCountLimit = 10000)]
        public async Task<JsonResult> Save([FromForm] ConditioningOrderViewModel model, int step)
        {
            // Activity description
            // 10	Equipos analáticos
            // 11	Básculas y flujómetros
            // 12	Equipos (accesorios) empleados en el proceso de acondicionamiento
            // 13	Control de llenado de pipas
            // 14	Rendimiento del proceso de acondicionamiento
            // 15	Cierre del expediente de lote
            try
            {
                JsonResult result = Json(new { Result = "Fail", Message = "Parametro invalido" });
                if (step > 0)
                {
                    result = await SaveAnalyticalEquipament(model);
                }
                if (step > 1)
                {
                    result = await SaveScalesFlowMeters(model);
                }
                if (step > 2)
                {
                    result = await SaveEquipmentProcess(model);
                }
                if (step > 3)
                {
                    result = await SaveControlPipeFilling(model);
                }
                if (step > 4)
                {
                    result = await SavePerformanceConditioning(model);
                }
                if (step > 5)
                {
                    result = await SaveRelease(model);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de producción " + ex);
                return Json(new { Result = "Fail", Message = ex.Message });
            }

        }

        private async Task<JsonResult> SaveAnalyticalEquipament(ConditioningOrderViewModel model)
        {
            List<AnalyticalEquipament> analytics = new List<AnalyticalEquipament>();
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            if (model.StepSaved > 1)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }
            var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ConditioningOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
            }

            //validate AnalyticEquipmentList in DB
            var AnalyticDB = await _analyticalEquipamentRepository.GetAsync(x => x.ConditioningOrderId == model.Id);

            if (AnalyticDB.Count > 0)
            {
                var ObjAnalytic = from c in model.AnalyticEquipmentList
                                  join b in AnalyticDB on c.Code equals b.Code
                                  select new AnalyticEquipmentViewModel
                                  {
                                      Id = b.Id,
                                      Code = b.Code,
                                      Description = b.Description,
                                      IsCalibrated = c.IsCalibrated,
                                      ReviewedBy = c.ReviewedBy,
                                      ReviewedDate = c.ReviewedDate,
                                      Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                      ConditioningOrderId = model.Id
                                  };

                foreach (var item in ObjAnalytic)
                {
                    var info = await _analyticalEquipamentRepository.GetByIdAsync(item.Id);
                    info.Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes;
                    info.Description = item.Description;
                    info.IsCalibrated = item.IsCalibrated;
                    info.ReviewedBy = item.ReviewedBy;
                    info.ReviewedDate = item.ReviewedDate;
                    //GET CURRENT USER
                    info.ConditioningOrder = entityClone;
                    info.ConditioningOrder.CreatedBy = userInfo;
                    analytics.Add(info);
                }

                var mapped = ObjectMapper.Mapper.Map<IEnumerable<AnalyticalEquipament>>(analytics);
                await _analyticalEquipamentRepository.UpdateAsync(mapped, AnalyticDB, model.LotProd);
            }
            else
            {
                foreach (var item in model.AnalyticEquipmentList)
                {
                    var info = _conditioningOrderRepository.GetByIdAsync(item.Id);
                    analytics.Add(new AnalyticalEquipament
                    {

                        Code = item.Code,
                        Description = item.Description,
                        IsCalibrated = item.IsCalibrated,
                        ReviewedBy = item.ReviewedBy,
                        ReviewedDate = item.ReviewedDate,
                        Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                        ConditioningOrderId = model.Id
                    });
                }
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<AnalyticalEquipament>>(analytics);
                var AddAnalytical = await _analyticalEquipamentRepository.AddAsync(mapped);
            }

            // activitie to be completed
            if (model.AnalyticEquipmentList.Where(x => !x.IsCalibrated.HasValue || !x.IsCalibrated.Value).Any())
            {
                entity.StepSavedDescription = "Equipos analíticos";
                entity.StepSaved = 0;
            }
            else
            {
                entity.StepSavedDescription = "Básculas y flujómetros";
                entity.StepSaved = 1;
            }

            entity.DelegateUser = userInfo;
            await _conditioningOrderRepository.UpdateAsync(entity);

            //notes
            foreach (var item in model.AnalyticEquipmentList.Where(x => x.Notes != null))
            {
                historyNotes.Add(new HistoryNotes
                {
                    ProductionOrderId = entity.ProductionOrderId,
                    Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                    Source = "Tabla 1: Equipos analíticos",
                    User = userInfo,
                    Date = DateTime.Now
                });
            }
            await AddNotes(historyNotes);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveScalesFlowMeters(ConditioningOrderViewModel model)
        {
            List<ScalesFlowMeters> Scales = new List<ScalesFlowMeters>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            // if (model.StepSaved > 2)
            // {
            //     return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            // }

            var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ConditioningOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
            }

            // validate ScalesflowsList in DB
            var ScalesDB = await _scalesFlowMetersRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            if (ScalesDB.Count > 0)
            {
                // var ScalesDBClone = Clone(ScalesDB.ToList());
                var ObjScales = from c in model.ScalesflowsList
                                join b in ScalesDB on c.Description equals b.Description
                                select new ScalesflowsViewModel
                                {
                                    Id = b.Id,
                                    Description = c.Description,
                                    IsCalibrated = c.IsCalibrated,
                                    ReviewedBy = c.ReviewedBy,
                                    ReviewedDate = c.ReviewedDate,
                                    ConditioningOrderId = model.Id
                                };

                foreach (var item in ObjScales)
                {
                    var info = await _scalesFlowMetersRepository.GetByIdAsync(item.Id);
                    info.Description = item.Description;
                    info.IsCalibrated = item.IsCalibrated;
                    info.ReviewedBy = item.ReviewedBy;
                    info.ReviewedDate = item.ReviewedDate;
                    info.ConditioningOrderId = model.Id;
                    //GET CURRENT USER
                    info.ConditioningOrder = entityClone;
                    info.ConditioningOrder.CreatedBy = userInfo;
                    Scales.Add(info);
                }

                await _scalesFlowMetersRepository.UpdateAsync(Scales, ScalesDB, model.LotProd);
            }
            else
            {
                foreach (var item in model.ScalesflowsList)
                {
                    Scales.Add(new ScalesFlowMeters
                    {
                        Description = item.Description,
                        IsCalibrated = item.IsCalibrated,
                        ReviewedBy = item.ReviewedBy,
                        ReviewedDate = item.ReviewedDate,
                        ConditioningOrderId = model.Id
                    });
                }

                await _scalesFlowMetersRepository.AddAsync(Scales);
            }

            // Save historical pipeline clearance signature / notes
            var historyNotes = new List<HistoryNotes>();
            foreach (var item in model.PipelineClearanceHistory)
            {
                var pipelineClearanceEntity = await _pipelineClearanceOARepository.GetByIdAsync(item.Id);

                if (pipelineClearanceEntity != null)
                {
                    var pipelineClearanceEntityClone = new PipelineClearanceOA();
                    pipelineClearanceEntityClone = (PipelineClearanceOA)pipelineClearanceEntity.Clone();
                    if (!string.IsNullOrEmpty(item.NotesSecond)
                        && item.NotesSecond != "NA"
                        && (pipelineClearanceEntity.ReviewedBySecond != item.ReviewedBySecond
                            || pipelineClearanceEntity.ReviewedDateSecond != item.ReviewedDateSecond
                            || pipelineClearanceEntity.NotesSecond != item.NotesSecond))
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = entity.ProductionOrderId,
                            Note = string.IsNullOrEmpty(model.PipelineClearanceOA.NotesSecond) ? "NA" : model.PipelineClearanceOA.NotesSecond,
                            Source = "Tabla 3: Despeje de línea",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }

                    pipelineClearanceEntity.ReviewedBySecond = item.ReviewedBySecond;
                    pipelineClearanceEntity.ReviewedDateSecond = item.ReviewedDateSecond;
                    pipelineClearanceEntity.NotesSecond = string.IsNullOrEmpty(item.NotesSecond) ? "NA" : item.NotesSecond;
                    //GET CURRENT USER
                    pipelineClearanceEntity.ConditioningOrder = entityClone;
                    pipelineClearanceEntity.ConditioningOrder.CreatedBy = userInfo;
                    await _pipelineClearanceOARepository.UpdateAsync(pipelineClearanceEntity, pipelineClearanceEntityClone, model.LotProd);
                }
            }
            await AddNotes(historyNotes);

            if (model.PipelineClearanceOA.InCompliance.HasValue && model.PipelineClearanceOA.InCompliance.Value)
            {
                // Only save when in compliance, when not in compliance PipelineClearanceHistory is used
                var pipeline = (await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == entity.Id && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
                if (pipeline == null)
                {
                    pipeline = new PipelineClearanceOA
                    {
                        ConditioningOrderId = model.Id,
                        Bill = "NA",
                        Notes = "NA",
                        ReviewedBy = model.PipelineClearanceOA.ReviewedBy,
                        ReviewedDate = model.PipelineClearanceOA.ReviewedDate,
                        InCompliance = model.PipelineClearanceOA.InCompliance,
                        Activitie = "Se lleva a cabo el despeje de línea de acuerdo a los puntos anteriores " + model.ProductName + " de la planta " + model.Plant,
                    };

                    await _pipelineClearanceOARepository.AddAsync(pipeline);
                }
                else
                {
                    var pipelineClearanceEntityClone = new PipelineClearanceOA();
                    pipelineClearanceEntityClone = (PipelineClearanceOA)pipeline.Clone();
                    pipeline.Bill = "NA";
                    pipeline.Notes = "NA";
                    pipeline.ReviewedBy = model.PipelineClearanceOA.ReviewedBy;
                    pipeline.ReviewedDate = model.PipelineClearanceOA.ReviewedDate;
                    pipeline.InCompliance = model.PipelineClearanceOA.InCompliance;
                    pipeline.Activitie = "Se lleva a cabo el despeje de línea de acuerdo a los puntos anteriores " + model.ProductName + " de la planta " + model.Plant;
                    //GET CURRENT USER
                    pipeline.ConditioningOrder = entityClone;
                    pipeline.ConditioningOrder.CreatedBy = userInfo;
                    await _pipelineClearanceOARepository.UpdateAsync(pipeline, pipelineClearanceEntityClone, model.LotProd);
                }
            }

            // activitie to be completed
            if (model.ScalesflowsList.Where(x => !x.IsCalibrated.HasValue || !x.IsCalibrated.Value).Any())
            {
                entity.StepSavedDescription = "Básculas y flujómetros";
                entity.StepSaved = 1;
            }
            else if (!model.PipelineClearanceOA.InCompliance.HasValue || !model.PipelineClearanceOA.InCompliance.Value)
            {
                entity.StepSavedDescription = "Despeje de Línea";
                entity.StepSaved = 1;
            }
            else
            {
                entity.StepSavedDescription = "Equipos (accesorios) empleados en el proceso de acondicionamiento";
                entity.StepSaved = 2;
            }

            entity.DelegateUser = userInfo;
            await _conditioningOrderRepository.UpdateAsync(entity);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveEquipmentProcess(ConditioningOrderViewModel model)
        {
            List<EquipmentProcessConditioning> equipaments = new List<EquipmentProcessConditioning>();
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());

            var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ConditioningOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
            }

            var EquipamentsDB = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            var EquipamentsDBClone = Clone(EquipamentsDB);
            if (EquipamentsDB.Any())
            {
                var ObjEquipaments = from c in model.EquipamentProcessesList
                                     join b in EquipamentsDB on c.TourNumber equals b.TourNumber
                                     where c.PipeNumber == b.PipeNumber
                                     select new EquipamentProcessConditioningViewModel
                                     {
                                         Id = b.Id,
                                         TourNumber = c.TourNumber,
                                         PipeNumber = c.PipeNumber,
                                         DistributionBatchDate = c.DistributionBatchDate,
                                         Bay = c.Bay,
                                         Bomb = c.Bomb,
                                         Hosefill = c.Hosefill,
                                         HoseDownload = c.HoseDownload,
                                         ReviewedBy = c.ReviewedBy,
                                         ReviewedDate = c.ReviewedDate,
                                         Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                         ConditioningOrderId = model.Id
                                     };

                foreach (var item in ObjEquipaments)
                {
                    var info = await _equipmentProcessConditioningRepository.GetByIdAsync(item.Id);
                    info.TourNumber = item.TourNumber;
                    info.PipeNumber = item.PipeNumber;
                    info.Bay = item.Bay;
                    info.Bomb = item.Bomb;
                    info.Hosefill = item.Hosefill;
                    info.HoseDownload = item.HoseDownload;
                    info.ReviewedBy = item.ReviewedBy;
                    info.ReviewedDate = item.ReviewedDate;
                    info.Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes;
                    ///get current user
                    info.ConditioningOrder = entityClone;
                    info.ConditioningOrder.CreatedBy = userInfo;
                    equipaments.Add(info);
                }
                await _equipmentProcessConditioningRepository.UpdateAsync(equipaments, EquipamentsDBClone, model.LotProd);
            }

            equipaments = new List<EquipmentProcessConditioning>();
            foreach (var itemx in model.EquipamentProcessesList)
            {
                var info = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id
                           && x.TourNumber == itemx.TourNumber && x.PipeNumber == itemx.PipeNumber);
                //insert 
                if (info.Count == 0)
                {
                    equipaments.Add(new EquipmentProcessConditioning
                    {
                        TourNumber = itemx.TourNumber,
                        PipeNumber = itemx.PipeNumber,
                        Bay = itemx.Bay,
                        Bomb = itemx.Bomb,
                        Hosefill = itemx.Hosefill,
                        HoseDownload = itemx.HoseDownload,
                        ReviewedBy = itemx.ReviewedBy,
                        ReviewedDate = itemx.ReviewedDate,
                        Notes = string.IsNullOrEmpty(itemx.Notes) ? "NA" : itemx.Notes,
                        ConditioningOrderId = model.Id
                    });
                    await _equipmentProcessConditioningRepository.AddAsync(equipaments);
                    equipaments = new List<EquipmentProcessConditioning>();
                }
            }

            // activitie to be completed
            if (model.EquipamentProcessesList.Where(x => string.IsNullOrEmpty(x.Bay) || string.IsNullOrEmpty(x.HoseDownload)).Any())
            {
                entity.StepSavedDescription = "Equipos (accesorios) empleados en el proceso de acondicionamiento";
                entity.StepSaved = 2;
            }
            else
            {
                entity.StepSavedDescription = "Control de llenado de pipas";
                entity.StepSaved = 3;
            }

            entity.DelegateUser = userInfo;
            await _conditioningOrderRepository.UpdateAsync(entity);

            //notes
            foreach (var item in model.EquipamentProcessesList.Where(x => x.Notes != null))
            {
                historyNotes.Add(new HistoryNotes
                {
                    ProductionOrderId = entity.ProductionOrderId,
                    Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                    Source = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento",
                    User = userInfo,
                    Date = DateTime.Now
                });
            }
            await AddNotes(historyNotes);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveControlPipeFilling(ConditioningOrderViewModel model)
        {

            List<PipeFillingAnalysisViewModel> pipeFillingAnalyses = new List<PipeFillingAnalysisViewModel>();
            List<PipeFillingAnalysisViewModel> AddpipeFillingAnalyses = new List<PipeFillingAnalysisViewModel>();
            List<PipeFillingAnalysis> UpdpipeFillingAnalyses = new List<PipeFillingAnalysis>();
            List<PipeFillingCustomerViewModel> pipeFillingViewModelsCustomers = new List<PipeFillingCustomerViewModel>();
            List<PipeFillingCustomerViewModel> AddpipeFillingViewModelsCustomers = new List<PipeFillingCustomerViewModel>();
            List<PipeFillingCustomer> UpdpipeFillingViewModelsCustomers = new List<PipeFillingCustomer>();
            List<PipeFilling> pipeFillings = new List<PipeFilling>();
            List<PipeFillingViewModel> UpdpipeFillingsViewModel = new List<PipeFillingViewModel>();
            List<PipeFilling> pipeFillingsClone = new List<PipeFilling>();
            List<PipeFillingCustomer> UpdpipeCustomersClone = new List<PipeFillingCustomer>();
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            ///
            List<PipeFillingControl> pipeFillingControls = new List<PipeFillingControl>();
            List<PipeFillingControlViewModel> pipeFillingControlViews = new List<PipeFillingControlViewModel>();
            List<PipeFillingControlViewModel> pipeFillingControl = new List<PipeFillingControlViewModel>();
            var fileDto = new List<FilesDto>();

            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ConditioningOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
            }

            //guarda archivos AHF
            foreach (var item in model.PipeFillingControl.Where(x => x.PipesList != null).SelectMany(x => x.PipesList)
                .SelectMany(x => x.FinalAnalysis)
                .Union(model.PipeFillingControl.Where(x => x.PipesList != null).SelectMany(x => x.PipesList)
                .SelectMany(x => x.InitialAnalysis))
                .Where(x => x.ParameterName == "Identidad" && !string.IsNullOrEmpty(x.File))
                .ToList())
            {
                item.PathFile = await SaveFileAsync(item);
                fileDto.Add(new FilesDto()
                {
                    FileName = item.PathFile,
                    InputId = item.InputId
                });
            }

            ///control 
            model.PipeFillingControl.ForEach(a => pipeFillingControls.Add(new PipeFillingControl
            {
                ConditioningOrderId = model.Id,
                TourNumber = a.TourNumber

            }));

            ///pipe filling
            model.PipeFillingControl.ForEach(a => a.PipesList?.ForEach(
                 c => UpdpipeFillingsViewModel.Add(new PipeFillingViewModel
                 {
                     PipeNumber = c.PipeNumber,
                     CheckListStatus = c.CheckListStatus,
                     CheckListIncompliance = c.CheckListIncompliance,
                     Date = c.Date,
                     InitialWeight = c.InitialWeight,
                     FinalWeight = c.FinalWeight,
                     DiffWeight = c.DiffWeight,
                     AnalyzedBy = c.AnalyzedBy,
                     AnalyzedDate = c.AnalyzedDate,
                     InitialAnalyzedDate = c.InitialAnalyzedDate,
                     FinalAnalyzedDate = c.FinalAnalyzedDate,
                     DueDate = c.DueDate,
                     DistributionBatch = c.DistributionBatch,
                     InCompliance = c.InCompliance,
                     ReportPNCFolio = string.IsNullOrEmpty(c.ReportPNCFolio) ? "NA" : c.ReportPNCFolio,
                     ReportPNCNotes = string.IsNullOrEmpty(c.ReportPNCNotes) ? "NA" : c.ReportPNCNotes,
                     IsReleased = c.IsReleased,
                     ReleasedBy = c.ReleasedBy,
                     ReleasedDate = c.ReleasedDate,
                     ConditioningOrderId = model.Id,
                     PipeFillingControlId = c.PipeFillingControlId
                 })
             ));

            ///analysis Initial
            model.PipeFillingControl.ForEach(a => a.PipesList?.ForEach(
                   b => b.InitialAnalysis.ForEach(
                   c => pipeFillingAnalyses.Add(new PipeFillingAnalysisViewModel
                   {
                       ParameterName = c.ParameterName,
                       ValueExpected = c.ValueExpected,
                       ValueReal = c.ValueReal,
                       MeasureUnit = c.MeasureUnit,
                       Type = PipeFillingAnalysisType.InitialAnalysis.Value,
                       DistributionBatch = b.DistributionBatch,
                       PipeNumber = b.PipeNumber,
                       ConditioningOrderId = model.Id,
                       Unique = c.Unique,
                       PathFile = c.PathFile    //AHF
                   })
               )));
            ///analysis final
            model.PipeFillingControl.ForEach(a => a.PipesList?.ForEach(
                    b => b.FinalAnalysis.ForEach(
                    c => pipeFillingAnalyses.Add(new PipeFillingAnalysisViewModel
                    {
                        ParameterName = c.ParameterName,
                        ValueExpected = c.ValueExpected,
                        ValueReal = c.ValueReal,
                        MeasureUnit = c.MeasureUnit,
                        Type = PipeFillingAnalysisType.FinalAnalysis.Value,
                        DistributionBatch = b.DistributionBatch,
                        PipeNumber = b.PipeNumber,
                        ConditioningOrderId = model.Id,
                        Unique = c.Unique,
                        PathFile = c.PathFile   //AHF
                    })
                )));
            ///analysis customers
            model.PipeFillingControl.ForEach(a => a.PipesList?.ForEach(
             b => b.Customers.ForEach(c => pipeFillingViewModelsCustomers.Add(new PipeFillingCustomerViewModel
             {
                 Tank = c.Tank,
                 Name = c.Name,
                 DeliveryNumber = c.DeliveryNumber,
                 ReviewedBy = c.ReviewedBy,
                 ReviewedDate = c.ReviewedDate,
                 AnalysisReport = c.AnalysisReport,
                 TourNumber = a.TourNumber,
                 DistributionBatch = b.DistributionBatch,
                 ConditioningOrderId = model.Id,
                 Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                 EmailsList = string.IsNullOrEmpty(c.EmailsList) ? "NA" : c.EmailsList,
                 InCompliance = c.InCompliance,
                 Id = c.Id
             }))));

            #region control
            foreach (var item in pipeFillingControls)
            {
                var PipeDB = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id && x.TourNumber == item.TourNumber);
                var PipeDBClone = Clone(PipeDB);
                //get pipefillings
                if (PipeDB.Any())
                {
                    //updated
                    var PipeFillingsDB = await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == PipeDB.FirstOrDefault().Id);
                    if (PipeFillingsDB.Any())
                    {
                        var ObjPipe = from c in model.PipeFillingControl
                                      join b in PipeDB on c.TourNumber equals b.TourNumber
                                      select new PipeFillingControlViewModel
                                      {
                                          Id = b.Id,
                                          TourNumber = c.TourNumber,
                                          ConditioningOrderId = model.Id,
                                          PipesList = c.PipesList,
                                          ///get current user
                                          ConditioningOrder = entityClone,
                                      };
                        if (ObjPipe.Any())
                        {
                            var mappe = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingControl>>(ObjPipe);
                            var UpdPipeControl = await _pipeFillingControlRepository.UpdateAsync(mappe, PipeDBClone, model.LotProd);
                            pipeFillingControl.Add(ObjPipe.FirstOrDefault());

                        }
                    }

                }
                else
                {
                    ///insert 
                    foreach (var itemx in model.PipeFillingControl.Where(x => x.TourNumber == item.TourNumber))
                    {
                        pipeFillingControlViews.Add(new PipeFillingControlViewModel
                        {
                            TourNumber = item.TourNumber,
                            ConditioningOrderId = model.Id,
                            PipesList = itemx.PipesList.Select(x =>
                            {
                                x.ConditioningOrderId = model.Id;
                                return x;
                            }).ToList()
                        });
                    }
                }

            }
            #endregion
            #region fillings
            var mappedcontrol = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingControl>>(pipeFillingControlViews);
            await _pipeFillingControlRepository.AddAsync(mappedcontrol);
            var mappedFillings = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingControl>>(pipeFillingControl);
            foreach (var itemx in mappedFillings)
            {
                foreach (var item in UpdpipeFillingsViewModel)
                {
                    var info = (await _pipeFillingRepository.GetAsync(x => x.DistributionBatch == item.DistributionBatch
                      && x.PipeNumber == item.PipeNumber && x.PipeFillingControlId == itemx.Id)).FirstOrDefault();
                    var infoClone = new PipeFilling();  //(await _pipeFillingRepository.GetAsync(x => x.DistributionBatch == item.DistributionBatch
                       ///&& x.PipeNumber == item.PipeNumber && x.PipeFillingControlId == itemx.Id)).FirstOrDefault();
                    if (info != null)
                    {
                        infoClone = (PipeFilling)info.Clone();
                        pipeFillingsClone.Add((PipeFilling)infoClone);
                        info.PipeNumber = item.PipeNumber;
                        info.CheckListStatus = item.CheckListStatus;
                        info.CheckListIncompliance = !info.CheckListIncompliance.HasValue ? item.CheckListIncompliance : info.CheckListIncompliance;
                        info.Date = item.Date;
                        info.InitialWeight = item.InitialWeight;
                        info.FinalWeight = item.FinalWeight;
                        info.DiffWeight = item.DiffWeight;
                        info.AnalyzedBy = string.IsNullOrEmpty(info.AnalyzedBy) ? item.AnalyzedBy : info.AnalyzedBy;
                        info.AnalyzedDate = !info.AnalyzedDate.HasValue ? item.AnalyzedDate : info.AnalyzedDate;
                        info.InitialAnalyzedDate = !info.InitialAnalyzedDate.HasValue ? item.InitialAnalyzedDate : info.InitialAnalyzedDate;
                        info.FinalAnalyzedDate = item.FinalAnalyzedDate;
                        info.DueDate = item.DueDate;
                        info.DistributionBatch = item.DistributionBatch;
                        info.InCompliance = !info.InCompliance.HasValue ? item.InCompliance : info.InCompliance;
                        info.ReportPNCFolio = string.IsNullOrEmpty(item.ReportPNCFolio) ? "NA" : item.ReportPNCFolio;
                        info.ReportPNCNotes = string.IsNullOrEmpty(item.ReportPNCNotes) ? "NA" : item.ReportPNCNotes;
                        info.IsReleased = !info.IsReleased.HasValue ? item.IsReleased : info.IsReleased;
                        info.ReleasedBy = string.IsNullOrEmpty(info.ReleasedBy) ? item.ReleasedBy : info.ReleasedBy;
                        info.ReleasedDate = !info.ReleasedDate.HasValue ? item.ReleasedDate : info.ReleasedDate;
                        info.PipeFillingControlId = itemx.Id;

                        var mappedPipefilling = ObjectMapper.Mapper.Map<PipeFilling>(info);
                        ///get current user
                        info.ConditioningOrder = entityClone;
                        info.ConditioningOrder.CreatedBy = userInfo;
                        pipeFillings.Add(mappedPipefilling);
                    }
                }
            }

            var UpdFilling = _pipeFillingRepository.UpdateAsync(pipeFillings, pipeFillingsClone, model.LotProd);
            #endregion
            #region Analysis
            foreach (var item in pipeFillingAnalyses)
            {
                var info = (await _pipeFillingAnalysisRepository.GetAsync(x => x.DistributionBatch == item.DistributionBatch
                       && x.PipeNumber == item.PipeNumber && x.ConditioningOrderId == model.Id && x.ParameterName == item.ParameterName && x.Type == item.Type)).FirstOrDefault();  //AHF
                if (info != null)
                {
                    info.MeasureUnit = item.MeasureUnit;
                    info.ValueExpected = item.ValueExpected;
                    info.ValueReal = item.ValueReal;
                    info.PathFile = item.PathFile;  //AHF
                    var mapped = ObjectMapper.Mapper.Map<PipeFillingAnalysis>(info);
                    UpdpipeFillingAnalyses.Add(mapped);
                }
                //insert
                else
                {
                    AddpipeFillingAnalyses.Add(new PipeFillingAnalysisViewModel
                    {
                        MeasureUnit = item.MeasureUnit,
                        DistributionBatch = item.DistributionBatch,
                        ValueExpected = item.ValueExpected,
                        ValueReal = item.ValueReal,
                        PipeNumber = item.PipeNumber,
                        ParameterName = item.ParameterName,
                        Type = item.Type,
                        ConditioningOrderId = item.ConditioningOrderId,
                        Unique = item.Unique,
                        PathFile = item.PathFile    //AHF
                    });
                }
            }
            //update path file AHF
            var finalFilesName = new List<FilesDto>();
            if (UpdpipeFillingAnalyses.Any(x => x.ParameterName.Contains("Identidad") && !string.IsNullOrEmpty(x.PathFile)) && fileDto.Any())
            {
                var mappedAnalysisUpd = ObjectMapper.Mapper.Map<List<PipeFillingAnalysis>>(UpdpipeFillingAnalyses.Where(x => fileDto.Any(y => y.FileName == x.PathFile)).ToList());
                var AnalisysDBUpd = await _pipeFillingAnalysisRepository.UpdateAsync(mappedAnalysisUpd);
                var filesTemp = (from analisys in mappedAnalysisUpd
                                 join file in fileDto on analisys.PathFile equals file.FileName
                                 select new FilesDto()
                                 {
                                     FileName = analisys.PathFile,
                                     InputId = file.InputId,
                                     Type = analisys.Type
                                 }).ToList();
                finalFilesName.AddRange(filesTemp);
            }

            //insert
            var mappedAnalysis = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingAnalysis>>(AddpipeFillingAnalyses);
            var AnalisysDB = await _pipeFillingAnalysisRepository.AddAsync(mappedAnalysis);
            var filesTemp2 = (from analisys in mappedAnalysis
                              join file in fileDto on analisys.PathFile equals file.FileName
                              select new FilesDto()
                              {
                                  FileName = analisys.PathFile,
                                  InputId = file.InputId,
                                  Type = analisys.Type
                              }).ToList();
            finalFilesName.AddRange(filesTemp2);
            #endregion
            #region customers
            //customers
            foreach (var item in pipeFillingViewModelsCustomers)
            {
                var info = await this._pipeFillingCustomerRepository.GetByParametersAsync(model.Id, item.TourNumber, item.DistributionBatch, item.Tank);
                if (info != null)
                {
                    var infoClone = (PipeFillingCustomer)info.Clone();
                    UpdpipeCustomersClone.Add(infoClone);
                    info.TourNumber = item.TourNumber;
                    info.Name = string.IsNullOrEmpty(info.Name) ? item.Name : info.Name;
                    info.DeliveryNumber = string.IsNullOrEmpty(info.DeliveryNumber) ? item.DeliveryNumber : info.DeliveryNumber;
                    info.ReviewedBy = string.IsNullOrEmpty(info.ReviewedBy) ? item.ReviewedBy : info.ReviewedBy;
                    info.ReviewedDate = !info.ReviewedDate.HasValue ? item.ReviewedDate : info.ReviewedDate;
                    info.AnalysisReport = string.IsNullOrEmpty(info.AnalysisReport) ? item.AnalysisReport : info.AnalysisReport;
                    info.DistributionBatch = item.DistributionBatch;
                    info.Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes;
                    info.EmailsList = string.IsNullOrEmpty(item.EmailsList) ? "NA" : item.EmailsList;
                    info.InCompliance = !info.InCompliance.HasValue ? item.InCompliance : info.InCompliance;
                    info.PlantIdentificador = model.Location;
                    info.ProductId = model.ProductName;
                    info.TankId = model.Tank;
                    //info.EmailListSend = item.EmailsListSend;
                    ///get current user
                    info.ConditioningOrder = entityClone;
                    info.ConditioningOrderId = model.Id;
                    UpdpipeFillingViewModelsCustomers.Add(info);
                    _logger.LogInformation("PipeFillingCustomers " + info.Tank + "|" + item.Name + "|" + item.DeliveryNumber + "|" + "tournumber" + item.TourNumber + "|" + "ConditioningOrderId " + model.Id);
                }
                else
                {
                    AddpipeFillingViewModelsCustomers.Add(new PipeFillingCustomerViewModel
                    {
                        Tank = item.Tank,
                        Name = item.Name,
                        DeliveryNumber = item.DeliveryNumber,
                        ReviewedBy = item.ReviewedBy,
                        ReviewedDate = item.ReviewedDate,
                        AnalysisReport = item.AnalysisReport,
                        TourNumber = item.TourNumber,
                        DistributionBatch = item.DistributionBatch,
                        ConditioningOrderId = model.Id,
                        Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                        EmailsList = string.IsNullOrEmpty(item.EmailsList) ? "NA" : item.EmailsList,
                        InCompliance = item.InCompliance,
                        PlantIdentificador = model.Location,
                        ProductId = model.ProductName,
                        TankId = model.Tank,
                        state = null
                    });
                }
            }
            //updated customers
            await _pipeFillingCustomerRepository.UpdateAsync(UpdpipeFillingViewModelsCustomers, UpdpipeCustomersClone, model.LotProd);
            ///insert customers
            var mappedCustomers = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingCustomer>>(AddpipeFillingViewModelsCustomers.Distinct());
            await _pipeFillingCustomerRepository.AddAsync(mappedCustomers);
            #endregion
            // activitie to be completed
            if (pipeFillingViewModelsCustomers.Where(x => string.IsNullOrEmpty(x.ReviewedBy) || !x.ReviewedDate.HasValue).Any())
            {
                entity.StepSavedDescription = "Equipos (accesorios) empleados en el proceso de acondicionamiento";
                entity.StepSaved = 3;
            }
            else
            {
                entity.StepSavedDescription = "Control de llenado de pipas";
                entity.StepSaved = 4;
            }
            ////notes
            foreach (var item in mappedCustomers.Where(x => x.Notes != null))
            {
                historyNotes.Add(new HistoryNotes
                {
                    ProductionOrderId = entity.ProductionOrderId,
                    Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                    Source = "Tabla 5: Control de llenado de pipas/ Clientes",
                    User = userInfo,
                    Date = DateTime.Now
                });
            }
            await AddNotes(historyNotes);



            entity.DelegateUser = userInfo;
            await _conditioningOrderRepository.UpdateAsync(entity);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito", filesDto = finalFilesName });
        }

        private async Task<JsonResult> SavePerformanceConditioning(ConditioningOrderViewModel model)
        {
            List<PerformanceProcessConditioning> performances = new List<PerformanceProcessConditioning>();
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            if (model.StepSaved > 5)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }
            var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ConditioningOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
            }

            var ProcessDB = await _performanceProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            if (ProcessDB.Count > 0)
            {
                var ObjProcess = from c in model.PerformanceList
                                 join b in ProcessDB on c.ConditioningOrderId equals b.ConditioningOrderId
                                 select new PerformanceProcessConditioningViewModel
                                 {
                                     Id = b.Id,
                                     SizeLote = c.SizeLote,
                                     TotalTons = c.TotalTons,
                                     DifTons = c.DifTons,
                                     ReviewedBy = c.ReviewedBy,
                                     ReviewedDate = c.ReviewedDate,
                                     Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                     ConditioningOrderId = model.Id
                                 };

                foreach (var item in ObjProcess)
                {
                    var info = await _performanceProcessConditioningRepository.GetByIdAsync(item.Id);
                    info.SizeLote = item.SizeLote;
                    info.TotalTons = item.TotalTons;
                    info.DifTons = item.DifTons;
                    info.ReviewedBy = item.ReviewedBy;
                    info.ReviewedDate = item.ReviewedDate;
                    info.Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes;
                    info.TourNumber = item.TourNumber;
                    info.PipeNumber = item.PipeNumber;
                    ///get current user
                    info.ConditioningOrder = entityClone;
                    info.ConditioningOrder.CreatedBy = userInfo;
                    performances.Add(info);
                }
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<PerformanceProcessConditioning>>(performances);
                await _performanceProcessConditioningRepository.UpdateAsync(mapped, ProcessDB, model.LotProd);
            }
            else
            {
                foreach (var item in model.PerformanceList)
                {
                    performances.Add(new PerformanceProcessConditioning
                    {

                        SizeLote = item.SizeLote,
                        TotalTons = item.TotalTons,
                        DifTons = item.DifTons,
                        ReviewedBy = item.ReviewedBy,
                        ReviewedDate = item.ReviewedDate,
                        Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                        ConditioningOrderId = model.Id,
                        TourNumber = item.TourNumber,
                        PipeNumber = item.PipeNumber
                    });
                }
                await _performanceProcessConditioningRepository.AddAsync(performances);
            }

            // activitie to be completed
            if (model.PerformanceList.Where(x => string.IsNullOrEmpty(x.ReviewedBy) || !x.ReviewedDate.HasValue).Any())
            {
                entity.StepSavedDescription = "Rendimiento del proceso de acondicionamiento";
                entity.StepSaved = 4;
            }
            else
            {
                entity.StepSavedDescription = "Cierre del expediente de lote";
                entity.StepSaved = 5;

            }

            entity.DelegateUser = userInfo;
            await _conditioningOrderRepository.UpdateAsync(entity);

            //notes
            foreach (var item in model.PerformanceList.Where(x => x.Notes != null))
            {
                historyNotes.Add(new HistoryNotes
                {
                    ProductionOrderId = entity.ProductionOrderId,
                    Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                    Source = "Tabla 6: Rendimiento del proceso de acondicionamiento",
                    User = userInfo,
                    Date = DateTime.Now
                });
            }
            await AddNotes(historyNotes);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveRelease(ConditioningOrderViewModel model)
        {
            try
            {
                if (model.StepSaved > 6)
                {
                    return Json(new { Result = "BadRequest", Message = "Operación inválida" });
                }
                List<HistoryNotes> historyNotes = new List<HistoryNotes>();
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
                var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ConditioningOrder();
                entityClone = (ConditioningOrder)entity.Clone();
                if (entity == null)
                {
                    return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
                }
                if (model.IsReleased.HasValue && !model.IsReleased.Value)
                {
                    entity.State = ConditioningOrderStatus.InProgress.Value;
                    entity.IsReleased = false;
                    entity.StepSaved = 5;
                    entity.StepSavedDescription = "Se abrió expediente de lote";
                    entity.ReleasedBy = null;   //AHF
                    entity.ReleasedDate = null; //AHF
                    entity.ReleasedNotes = null; //AHF
                }
                else if (model.IsReleased.HasValue && model.IsReleased.Value)
                {
                    entity.State = ConditioningOrderStatus.Released.Value;
                    entity.IsReleased = true;
                    entity.StepSaved = 6;
                    entity.StepSavedDescription = "Cierre del expediente de lote";
                    entity.ReleasedNotes = string.IsNullOrEmpty(model.ReleasedNotes) ? "NA" : model.ReleasedNotes;
                    entity.ReleasedBy = model.ReleasedBy;
                    entity.ReleasedDate = model.ReleasedDate;
                }

                entity.DelegateUser = userInfo;
                entity.EndDate = DateTime.Now;
                ///get current user
                entity.CreatedBy = userInfo;

                // Replaces empty strings for NA

                await _conditioningOrderRepository.UpdateAsync(entity, entityClone, model.LotProd);

                //notes
                if (model.ReleasedNotes != null)
                {
                    historyNotes.Add(new HistoryNotes
                    {
                        ProductionOrderId = entity.ProductionOrderId,
                        Note = string.IsNullOrEmpty(model.ReleasedNotes) ? "NA" : model.ReleasedNotes,
                        Source = "Tabla 7: Cierre del expediente de lote",
                        User = userInfo,
                        Date = DateTime.Now
                    });
                    await AddNotes(historyNotes);
                }
                //states
                historyStates.ProductionOrderId = model.Id;
                historyStates.State = entity.State;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo;
                historyStates.Type = HistoryStateType.OrdenAcondicionamiento.Value;
                await _historyStatesRepository.AddAsync(historyStates);
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (model.IsReleased.HasValue && model.IsReleased.Value && enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    await this._notification.SendNotification(paramters, model.PlantId, "USUARIO DE PRODUCCION", this._config["EmailSubjects:ReleasedOAPath"], this._config["Emailtemplates:ReleasedOAPath"]);
                }
                return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden acodicionamiento ha sido guardada con éxito", Status = entity.State });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al liberar la orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<JsonResult> SaveDespeje(PipeClearanceOAViewModel model)
        {
            if (model.ConditioningOrderId == 0)
            {
                return Json(new { Result = "Error", Message = "Orden de acondicionamiento sin asociar" });
            }

            if (string.IsNullOrEmpty(model.Bill) || string.IsNullOrEmpty(model.Notes))
            {
                return Json(new { Result = "Error", Message = "Folio y Observaciones son requeridos es requirido." });
            }

            var pipelineClearanceList = await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == model.ConditioningOrderId);
            var hasPendingReview = pipelineClearanceList.Where(x => x.InCompliance.HasValue && !x.InCompliance.Value && (string.IsNullOrEmpty(x.ReviewedBySecond) || !x.ReviewedDateSecond.HasValue)).Any();
            if (hasPendingReview)
            {
                return Json(new { Result = "Error", Message = "Existe una evaluación pendiente" });
            }

            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());

            PipelineClearanceOA pipeline = new PipelineClearanceOA();
            try
            {
                pipeline.ConditioningOrderId = model.ConditioningOrderId;
                pipeline.Bill = string.IsNullOrEmpty(model.Bill) ? "NA" : model.Bill;
                pipeline.Notes = string.IsNullOrEmpty(model.Notes) ? "NA" : model.Notes;
                pipeline.ReviewedBy = model.ReviewedBy;
                pipeline.ReviewedDate = model.ReviewedDate;
                pipeline.InCompliance = false;
                pipeline.Activitie = model.Activitie;

                await _pipelineClearanceOARepository.AddAsync(pipeline);

                //notes
                if (!string.IsNullOrEmpty(model.Notes) && model.Notes != "NA")
                {
                    var historyNotes = new List<HistoryNotes>
                    {
                        new HistoryNotes {
                            ProductionOrderId = model.ProductionOrderId,
                            Note = string.IsNullOrEmpty(model.Notes) ? "NA" : model.Notes,
                             Source = "Tabla 3: Despeje de línea",
                            User = userInfo,
                            Date = DateTime.Now }
                    };
                    await AddNotes(historyNotes);
                }
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramBill}", Value = model.Bill });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramProduct}", Value = model.SelectedPlantFilter });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.ProductionOrderId) });
                    await this._notification.SendNotification(paramters, model.PlantId, "SUPERINTENDENTE DE PLANTA", this._config["EmailSubjects:LineClearanceOAPath"], this._config["Emailtemplates:LineClearanceOAPath"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de acondicionamiento " + ex);
            }

            return Json(new { Result = "Ok", Message = "Despeje guardado con éxito" });
        }


        [HttpGet]
        public async Task<IActionResult> GetDetailDL(int Id)
        {
            List<PipeClearanceOAViewModel> pipelines = new List<PipeClearanceOAViewModel>();

            if (Id != 0)
            {
                var conditions = await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == Id && (!x.InCompliance.HasValue || !x.InCompliance.Value));

                pipelines = conditions.Select(item => new PipeClearanceOAViewModel
                {
                    Id = item.Id,
                    Activitie = item.Activitie,
                    InCompliance = item.InCompliance,
                    Notes = item.Notes,
                    ReviewedBy = item.ReviewedBy,
                    ReviewedDate = item.ReviewedDate,
                    ReviewedBySecond = item.ReviewedBySecond,
                    ReviewedDateSecond = item.ReviewedDateSecond,
                    NotesSecond = item.NotesSecond,
                    ConditioningOrderId = item.ConditioningOrderId,
                    Bill = item.Bill
                }).ToList();
            }

            return PartialView("_PipelineClearanceDeviationReport", pipelines);
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Cancel([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ConditioningOrder();
                entityClone = (ConditioningOrder)entity.Clone();
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                entity.DelegateUser = userInfo.Id;
                if (entity.State != ConditioningOrderStatus.InProgress.Value && entity.State != ConditioningOrderStatus.ToBeReleased.Value)
                {
                    return Json(new
                    {
                        Result = "Fail",
                        Message = "La solicitud de cancelación no se encuentra en status " + ProductionOrderStatus.InProgress.Value + " o en " + ProductionOrderStatus.ToBeReleased.Value
                    });
                }
                entity.State = ConditioningOrderStatus.InCancellation.Value;
                entity.ReasonReject = model.ReasonReject;
                entity.EndDate = DateTime.Now;
                await _conditioningOrderRepository.UpdateAsync(entity, entityClone, model.LotProd);
                historyStates.ProductionOrderId = model.Id;
                historyStates.State = ConditioningOrderStatus.InCancellation.Value;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo.Id;
                historyStates.Type = HistoryStateType.OrdenAcondicionamiento.Value;
                var StateDB = _historyStatesRepository.AddAsync(historyStates);
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                    await this._notification.SendNotification(paramters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:CancelOAPath"], this._config["Emailtemplates:CancelOAPath"]);
                }
                return Json(new { Result = "Ok", Message = _resource.GetString("OrderSendOA").Value, Status = historyStates.State });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al actualizar la Orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CancelApprove([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ConditioningOrder();
                entityClone = (ConditioningOrder)entity.Clone();
                entity.DelegateUser = userInfo.Id;
                if (entity.State != ConditioningOrderStatus.InCancellation.Value)
                {
                    return Json(new { Result = "Fail", Message = "La orden de acondicionamiento no se encuentra en status " + ConditioningOrderStatus.InCancellation.Value });
                }
                entity.State = ConditioningOrderStatus.Cancelled.Value;
                entity.StepSaved = 6;
                entity.ReasonReject = model.ReasonReject;
                await _conditioningOrderRepository.UpdateAsync(entity, entityClone, model.LotProd);
                historyStates.State = ConditioningOrderStatus.Cancelled.Value;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo.Id;
                historyStates.Type = HistoryStateType.OrdenAcondicionamiento.Value;
                historyStates.ProductionOrderId = model.Id;
                var StateDB = _historyStatesRepository.AddAsync(historyStates);
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                    await this._notification.SendNotification(paramters, model.PlantId, "USUARIO DE PRODUCCION", this._config["EmailSubjects:CancelAproveOAPath"], this._config["Emailtemplates:CancelAproveOAPath"]);
                }
                return Json(new { Result = "Ok", Message = _resource.GetString("OrderApprove").Value, Status = historyStates.State });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al actualizar la Orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CancelReject([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                HistoryStates historyStates = new HistoryStates();
                var entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ConditioningOrder();
                entityClone = (ConditioningOrder)entity.Clone();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (entity.State != ConditioningOrderStatus.InCancellation.Value)
                {
                    return Json(new { Result = "Fail", Message = "La orden de acondicionamiento no esta " + ConditioningOrderStatus.InCancellation.Value });
                }
                entity.State = ConditioningOrderStatus.InProgress.Value;
                entity.ReasonReject = model.ReasonReject;
                entity.DelegateUser = userInfo.Id;
                await _conditioningOrderRepository.UpdateAsync(entity, entityClone, model.LotProd);
                historyStates.State = ConditioningOrderStatus.InProgress.Value;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo.Id;
                historyStates.Type = HistoryStateType.OrdenAcondicionamiento.Value;
                historyStates.ProductionOrderId = model.Id;
                var StateDB = _historyStatesRepository.AddAsync(historyStates);
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                    await this._notification.SendNotification(paramters, model.PlantId, "USUARIO DE PRODUCCION", this._config["EmailSubjects:CancelRejectOAPath"], this._config["Emailtemplates:CancelRejectOAPath"]);
                }
                return Json(new { Result = "Ok", Message = _resource.GetString("OrderRejectOA").Value, Status = historyStates.State });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al actualizar la Orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<JsonResult> DeleteTourNumber(string number, int id)
        {
            if (!string.IsNullOrEmpty(number))
            {
                var infoEquipment = await _equipmentProcessConditioningRepository.GetAsync(x => x.TourNumber == number && x.ConditioningOrderId == id);

                if (infoEquipment != null)
                {
                    //validate isRelease
                    var IsReleased = await _conditioningOrderService.ValidateIsReleaseTourNumberPipe(number, id);
                    if (IsReleased)
                        return Json(new { Result = "Ok", Message = "No se puede eliminar porque la pipa ya se encuentra dictaminada." });
                    await _equipmentProcessConditioningRepository.DeleteAsync(infoEquipment);
                }

                var infoFillingControl = await _pipeFillingControlRepository.GetAsync(x => x.TourNumber == number && x.ConditioningOrderId == id);

                if (infoFillingControl != null && infoFillingControl.Any())
                {
                    await _conditioningOrderService.DeleteTourNumber(number, id);
                }

                return Json(new { Result = "Ok", Message = "Se eliminó correctamente." });
            }
            else
            {
                return Json(new { Result = "Ok", Message = "Orden de producción sin asociar" });
            }
        }

        private async Task<HistoryNotes> AddNotes(List<HistoryNotes> historyNotes)
        {
            try
            {
                List<HistoryNotes> ListValidate = new List<HistoryNotes>();
                foreach (var item in historyNotes.Where(x => x.Note != null && x.Note != "NA"))
                {
                    var DbObj = await _historyNotesRepository.GetAsync(x => x.ProductionOrderId == item.ProductionOrderId
                                                                    && x.Source == item.Source
                                                                    && x.Note == item.Note);
                    if (DbObj.Count == 0)
                    {
                        ListValidate.Add(new HistoryNotes
                        {
                            Note = string.IsNullOrEmpty(item.Note) ? "NA" : item.Note,
                            ProductionOrderId = item.ProductionOrderId,
                            Source = item.Source,
                            Date = item.Date,
                            User = item.User,
                            Type = HistoryNotesType.OrdenAcondicionamiento.Value
                        });
                    }
                }
                return await _historyNotesRepository.AddAsync(ListValidate);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al guardar observaciones " + ex);
            }

            return new HistoryNotes();
        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryDetail(int Id)
        {
            if (Id != 0)
            {
                ///historial observaciones
                var historyNotes = await _principalService.GetHistoryDetail(Id, 2);

                return Json(new { data = historyNotes });
            }
            else
            {
                return Json(new { Result = "Ok", Message = "Orden de producción sin asociar" });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryStateDetail(int Id)
        {
            if (Id != 0)
            {
                ///historial de estados
                var userDb = _userManager.Users;
                var historyStates = from states in await _historyStatesRepository.GetAsync(x => x.ProductionOrderId == Id
                                  && x.Type == HistoryStateType.OrdenAcondicionamiento.Value)
                                    select new HistoryStates
                                    {
                                        ProductionOrderId = states.ProductionOrderId,
                                        User = userDb.Where(x => x.Id == states.User).Select(x => x.NombreUsuario).FirstOrDefault(),
                                        Date = states.Date,
                                        State = states.State,
                                        Type = states.Type
                                    };

                return Json(new { data = historyStates });
            }
            else
            {
                return Json(new { Result = "Ok", Message = "Orden de acondicionamiento sin asociar" });
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryChecklist(int idOA, string tourNumber, string distributionBatch)
        {
            List<CheckListPipeDictiumAnswerViewModel> checkLists = new List<CheckListPipeDictiumAnswerViewModel>();
            var checklists = await _checkListPipeDictiumAnswerRepository
                                        .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber && x.DistributionBatch == distributionBatch
                                        && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);

            foreach (var item in checklists)
            {
                checkLists.Add(new CheckListPipeDictiumAnswerViewModel
                {
                    Id = item.Id,
                    CreatedBy = item.CreatedBy,
                    Verification = "CheckList",
                    Date = item.Date,
                    NumOA = item.NumOA,
                    Comment = item.Comment,
                    DistributionBatch = item.DistributionBatch,
                    TourNumber = item.TourNumber,
                    InCompliance = item.InCompliance,
                    Source = item.Source,
                    Status = item.Status,
                    File = item.File,
                    StatusTwo = item.StatusTwo,
                    CheckListId = GetCheckListId(item.Id).Result,
                    PipeNumber = item.PipeNumber
                });
            }
            return Json(new { data = checklists.ToList() });
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<IActionResult> SaveChecklistManual([FromForm] PipeFillingViewModel model, int step)
        {
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                if (step > 3)
                {
                    var CreateDb = (await _checkListPipeDictiumAnswerRepository
                                   .GetAsync(x => x.NumOA == model.ConditioningOrderId && x.TourNumber == model.TourNumber));
                    if (CreateDb != null)
                    {
                        var filtered = CreateDb.Where(x => x.StatusTwo == CheckListType.Inprogress.Value);
                        if (filtered.Any())
                        {
                            ///if para validar si hay web
                            if (filtered.Where(x => x.StatusTwo == CheckListType.InCancellation.Value).Any())
                                return Json(new { Result = "Fail", Message = "se encontró un checkList pendiente web" });
                            if (filtered.Where(x => x.StatusTwo == CheckListType.Inprogress.Value && x.Source == CheckListGeneralViewModelStatus.Web.Value).Any())
                                return Json(new { Result = "Fail", Message = "se encontró un checkList pendiente web" });
                            dictiumAnswer = filtered.FirstOrDefault();
                            dictiumAnswer.InCompliance = model.CheckListIncompliance;
                            dictiumAnswer.Step = CheckListGeneralViewModelCheckListStep.Two.Value;
                            dictiumAnswer.DistributionBatch = model.DistributionBatch;
                            if (HttpContext.Request.Form.Files.Count > 0)
                            {
                                var files = HttpContext.Request.Form.Files;
                                string FileName = Guid.NewGuid().ToString();
                                var upload = Path.Combine(_resource.GetString("PathCheckListBackEnd").Value);
                                if (!Directory.Exists(upload))
                                    Directory.CreateDirectory(upload);
                                var ext = Path.GetExtension(files[0].FileName);

                                //delete file
                                if ((System.IO.File.Exists(upload + @"\" + dictiumAnswer.File)))
                                {
                                    System.IO.File.Delete(upload + @"\" + dictiumAnswer.File);
                                }


                                using (var fileStreams = new FileStream(Path.Combine(upload, FileName + ext), FileMode.Create))
                                {
                                    files[0].CopyTo(fileStreams);
                                }
                                dictiumAnswer.File = FileName + ext;
                                if (dictiumAnswer.InCompliance == true)
                                {
                                    dictiumAnswer.StatusTwo = CheckListType.CloseOk.Value;
                                    dictiumAnswer.VerificationTwo = "OK";
                                }
                                else if (dictiumAnswer.InCompliance == false)
                                {
                                    dictiumAnswer.StatusTwo = CheckListType.CloseNo.Value;
                                    dictiumAnswer.VerificationTwo = "NO";
                                }
                            }
                            var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(dictiumAnswer);
                            return Json(new { Result = "Ok", Message = "checkList actualizado" });
                        }
                        else
                        {
                            return Json(new { Result = "Ok", Message = "Genera un checklist nuevo" });
                        }

                    }
                    return Json(new { Result = "Ok", Message = "checkList actualizado" });
                }
                else
                {
                    dictiumAnswer.CreatedBy = userInfo.NombreUsuario;
                    dictiumAnswer.Date = DateTime.Now;
                    dictiumAnswer.Comment = null;
                    dictiumAnswer.NumOA = model.ConditioningOrderId;
                    dictiumAnswer.DistributionBatch = "";
                    dictiumAnswer.TourNumber = "";
                    dictiumAnswer.Source = CheckListGeneralViewModelStatus.Manual.Value;

                    dictiumAnswer.InCompliance = model.CheckListIncompliance;
                    if (model.CheckListIncompliance == true)
                        dictiumAnswer.Status = CheckListType.CloseOk.Value;
                    else
                        dictiumAnswer.Status = CheckListType.CloseNo.Value;
                    dictiumAnswer.Step = CheckListGeneralViewModelCheckListStep.One.Value;
                    dictiumAnswer.RelationShip = false;
                    if (HttpContext.Request.Form.Files.Count > 0)
                    {
                        var files = HttpContext.Request.Form.Files;
                        string FileName = Guid.NewGuid().ToString();
                        var upload = Path.Combine(_resource.GetString("PathCheckListBackEnd").Value);
                        if (!Directory.Exists(upload))
                            Directory.CreateDirectory(upload);
                        var ext = Path.GetExtension(files[0].FileName);
                        dictiumAnswer.Alias = files.FirstOrDefault().FileName;
                        using (var fileStreams = new FileStream(Path.Combine(upload, FileName + ext), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        dictiumAnswer.File = FileName + ext;
                    }
                    var saveDictium = await _checkListPipeDictiumAnswerRepository.AddAsync(dictiumAnswer);
                    return Json(new { Result = "Ok", Message = "CheckList Guardado correctamente" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al guardar el archivo " + ex);
                return Json(new { Result = "fail", Message = ex });
            }

        }

        [HttpGet]
        public async Task<IActionResult> CreateNewCheckList(int idOA, string tourNumber, string distributionBatch, int checkListId, string pipeNumber)
        {

            int Id = 0;
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var CreateDb = await _checkListPipeDictiumAnswerRepository
                           .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                                    && x.DistributionBatch == distributionBatch
                            && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value && x.Source == CheckListGeneralViewModelStatus.Web.Value);

            if (CreateDb.Count == 0)
                return BadRequest();
            //create New for tour number
            if (CreateDb != null)
            {
                var filtered = CreateDb.Where(x => x.StatusTwo == CheckListType.Inprogress.Value || x.StatusTwo == CheckListType.InCancellation.Value);
                if (filtered.Any())
                {
                    Id = filtered.LastOrDefault().Id;
                    await _conditioningOrderService.UpdateRelChecklistTourNumberPipe(idOA, tourNumber, distributionBatch, checkListId, pipeNumber);
                    return Json(new { Result = "Ok", Message = Id });
                }
                //else
                //{
                //    var checkListDB = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(checkListId);
                //    if (checkListDB != null)
                //    {
                //        dictiumAnswer = checkListDB;
                //        if (string.IsNullOrEmpty(checkListDB.StatusTwo))
                //        {
                //            checkListDB.StatusTwo = CheckListType.Inprogress.Value;
                //            checkListDB.Step = CheckListGeneralViewModelCheckListStep.Two.Value;
                //            checkListDB.DistributionBatch = distributionBatch;
                //            await _checkListPipeDictiumAnswerRepository.UpdateAsync(checkListDB, dictiumAnswer);
                //        }
                //        Id = checkListDB.Id;

                //        await _conditioningOrderService.UpdateRelChecklistTourNumberPipe(idOA, tourNumber, distributionBatch, checkListId, pipeNumber);
                //    }
                //    return Json(new { Result = "Ok", Message = Id });
                //}
            }
            return Json(new { Result = "Ok", Message = Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetCheckListPendding(int idOA, string tourNumber, string distributionBatch)
        {
            var CreateDb = await _checkListPipeDictiumAnswerRepository
                           .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                            && x.DistributionBatch == distributionBatch && x.Source == CheckListGeneralViewModelStatus.Web.Value);
            if (CreateDb.Where(x => x.Status != CheckListType.CloseNo.Value || x.Status != CheckListType.CloseOk.Value).Any())
            {
                var filtered = CreateDb.Where(x => x.Status != CheckListType.CloseNo.Value || x.Status != CheckListType.CloseOk.Value);


                var id = CreateDb.Where(x => x.Status != CheckListType.CloseNo.Value
                     || x.Status != CheckListType.CloseOk.Value).LastOrDefault().Id;
                return Json(new
                {
                    Result = "Ok",
                    Message = id
                });
            }
            else
            {
                return Json(new { Result = "Fail", Message = "Sin checkList pendientes" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetLastStatus(int idOA, string tourNumber, string distributionBatch, int checklistId)
        {
            try
            {
                var ObjDB = new List<CheckListPipeDictiumAnswer>();
                var pipe = distributionBatch.Split('-')[3];
                #region old
                //if (!string.IsNullOrEmpty(distributionBatch) && checklistId == 0)
                //{
                //    ObjDB = (await _checkListPipeDictiumAnswerRepository
                //               .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                //                && x.DistributionBatch == distributionBatch && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value)).ToList();

                //}
                //if (checklistId > 0)
                //{
                //    var InfoDB = (await _checkListPipeDictiumAnswerRepository.GetByIdAsync(checklistId));
                //    ObjDB.Add(new CheckListPipeDictiumAnswer { StatusTwo = InfoDB.StatusTwo, Date = InfoDB.Date });         
                //}
                #endregion

                ObjDB = (await _checkListPipeDictiumAnswerRepository
                               .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                               && x.DistributionBatch == distributionBatch
                                && x.PipeNumber == pipe && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value)).ToList();

                if (ObjDB.Any())
                {
                    return Json(new
                    {
                        Result = "Ok",
                        Message = ObjDB.OrderByDescending(x => x.Date).Select(x => x.StatusTwo).FirstOrDefault()
                    });
                }

                else
                {
                    return Json(new { Result = "fail", Message = "No existen checkList" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetLastStatus " + ex);
                return Json(new { Result = "fail", Message = ex });
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetLastStatusDictium(int idOA, string tourNumber, string distributionBatch)
        {
            try
            {
                var ObjDB = await _checkListPipeDictiumAnswerRepository
                          .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                           && x.DistributionBatch == distributionBatch && x.VerificationTwo == "OK" && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);
                if (ObjDB.Any())
                {
                    return Json(new
                    {
                        Result = "Ok",
                        Message = ObjDB.LastOrDefault().StatusTwo
                    });
                }
                else
                {
                    return Json(new { Result = "fail", Message = "No existen checkList" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetLastStatus " + ex);
                return Json(new { Result = "fail", Message = ex });
            }

        }
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        public async Task<ActionResult> GetBillAsignature([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                DateTime date = DateTime.Now;
                string lastTwoDigitsOfYear = date.ToString("yy");
                var folio = await _conditioningOrderService.GetBillAsignature(model);
                //M31 - ox - lox - 01 - 000121
                var billfull = model.Location + "-" + model.Product + "-" + model.Tank + "-" + folio + lastTwoDigitsOfYear;
                return Json(new { Result = "Ok", Message = billfull });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetBillAsignature " + ex);
                return Json(new { Result = "Fail", Message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ExportToPdf(int IdOP, int Id, [FromServices] ILeyendsCertificateHistoryRepository leyendsCertificateHistoryRepository, [FromServices] ILeyendsFooterCertificateHistoryRepository leyendsFooterCertificateHistoryRepository)
        {
            using MemoryStream finalFile = new MemoryStream();
            using MemoryStream ms = new MemoryStream();
            var model = new ConditioningOrderViewModel();
            if (IdOP <= 0)
            {
                return BadRequest();
            }
            try
            {
                List<ConditioningOrderViewModel> rptDataSource = new List<ConditioningOrderViewModel>();
                List<ConditioningOrderViewModel> rptDataSource1 = new List<ConditioningOrderViewModel>();
                List<PipeFillingControlViewModel> rptDataSourcePC = new List<PipeFillingControlViewModel>();

                model = await _exportPDFService.GetModel(IdOP, Id);//  

                if (model == null)
                {
                    return BadRequest();
                }

                var generalCatalogFilter = await _generalCatalogRepository.GetAllAsync();
                //foreach (var item in generalCatalogFilter)
                //{
                //    if (item.PlantId == model.PlantId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where model.PlantId.Contains(r.PlantId) select r).ToList();
                //    }
                //    if (item.ProductId == model.ProductId)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where model.ProductId.Contains(r.ProductId) select r).ToList();
                //    }
                //    if (item.TankId == model.Tank)
                //    {
                //        generalCatalogFilter = (from r in generalCatalogFilter where model.Tank.Contains(r.TankId) select r).ToList();
                //    }
                //}

                var bayAreaIndex = 0;
                model.BayAreaList = generalCatalogFilter
                                        .Where(x => x.PlantId == model.PlantId
                                        && x.ProductId == model.ProductId
                                        && x.TankId == model.Tank
                                        && !string.IsNullOrEmpty(x.BayArea))
                                        .Select(x => new BayAreaItem
                                        {
                                            Index = bayAreaIndex++,
                                            BayArea = x.BayArea,
                                            FillingPump = x.FillingPump,
                                            FillingHose = x.FillingHose
                                        }).ToList();
                model.BayAreaFilter = model.BayAreaList
                                        .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();

                if (model.EquipamentProcessesList.FirstOrDefault()?.TourNumber != "NA")
                {
                    for (int i = 0; i < model.EquipamentProcessesList.Count(); i++)
                    {
                        var txt = model.BayAreaFilter.Where(p => p.Value == model.EquipamentProcessesList.ElementAt(i).Bay).FirstOrDefault()?.Text;
                        model.EquipamentProcessesList[i].Bay = txt;
                    }
                }
                rptDataSource.Add(model);

                RptOrAP _rptOA = new RptOrAP();
                Rpt6 tabla6 = new Rpt6();
                Rpt7 tabla7 = new Rpt7();

                List<RptChkList> rptChkList = new List<RptChkList>();
                List<Report1> rptCertList = new List<Report1>();
                var formulas = await _formulaRepository.GetAllAsync();
                var f1 = formulas.Where(f => f.ProductId == model.ProductId).FirstOrDefault();
                List<CheckListGeneralViewModel> chkGeneralList = new List<CheckListGeneralViewModel>();
                List<PipeFillingControlViewModel> pipes = new List<PipeFillingControlViewModel>();
                foreach (PipeFillingControlViewModel item in model.PipeFillingControl)
                {
                    ConditioningOrderViewModel m1 = new ConditioningOrderViewModel();
                    m1.Presentation = model.Presentation;
                    m1.Product = model.Product;
                    foreach (PipeFillingViewModel itemx in item.PipesList)
                    {
                        var checklistDB = (from record in await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.TourNumber == itemx.TourNumber
                                            && x.PipeNumber == itemx.PipeNumber && x.DistributionBatch == itemx.DistributionBatch && x.NumOA == model.Id)
                                           select new CheckListGeneralViewModel
                                           {
                                               Id = record.Id,
                                               ConditioningOrderId = model.Id,
                                               TourNumber = record.TourNumber,
                                               DistributionBatch = record.DistributionBatch,
                                               Source = record.Source
                                           });
                        if (checklistDB.Any())
                        {
                            foreach (var itemy in checklistDB)
                            {
                                if (itemy.Id > 0 && itemy.Source == true)
                                {
                                    CheckListGeneralViewModel chkGnrl = new CheckListGeneralViewModel();
                                    chkGnrl.Id = itemy.Id;
                                    chkGnrl.ConditioningOrderId = model.Id;
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
                                m1.LotProd = model.LotProd;

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
                                //pc.PipesList.Add(pipeg);
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
                                var Certificate = (await leyendsCertificateHistoryRepository.GetAsync(x => x.Id.Equals(model.CertificateId))).FirstOrDefault();
                                var Footer = (await leyendsFooterCertificateHistoryRepository.GetAsync(x => x.Id.Equals(model.FooterCertificateId))).FirstOrDefault();
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
                                    //LblNumPraxiar.Text = await this.GetTankClient(itexm.DistributionBatch, item.TourNumber);
                                    XRTableRow RowValueAnalizador = new XRTableRow();
                                    RowValueAnalizador.Cells.Add(rTableCellAnalizador);
                                    xrTableAnalizador.Rows.Add(RowValueAnalizador);
                                }

                                xrTableParameters.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableValueExpected.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableValueReal.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableAnalizador.Rows.FirstRow.Cells.RemoveAt(0);

                                #endregion
                                rptDataSource1.Clear();

                                rptDataSource1.Add(m1);

                                rtp1.DataSource = rptDataSource1;

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
                var pipesList = model.PipeFillingControl.Where(x => x != null && x.PipesList != null && x.PipesList.Any()).SelectMany(x => x.PipesList).ToList();
                foreach (var item in pipesList)
                {
                    var step4 = await this._exportPDFService.BuildTableAnalisys(item, rptDataSource);
                    steps4.Add(step4);
                }
                _rptOA.DataSource = rptDataSource;
                tabla6.DataSource = rptDataSource;
                tabla7.DataSource = rptDataSource;
                //tabla5.DataSource = rptDataSource;


                XRLabel LeyendRelease = _rptOA.FindControl("label32", true) as XRLabel;
                var leyend = string.Empty;
                leyend = _resource.GetString("AnaliticsEqOA").Value.Replace("ParamPlant", model.Plant);
                leyend = leyend.Replace("ParamProduct", model.Product);
                LeyendRelease.Text = leyend;

                LeyendRelease = _rptOA.FindControl("label26", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("APTextOA").Value.Replace("ParamPlant", model.Plant);
                leyend = leyend.Replace("ParamProduct", model.Product);
                LeyendRelease.Text = leyend;

                LeyendRelease = _rptOA.FindControl("label50", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("CLPTextOA").Value.Replace("ParamPlant", model.Plant);
                leyend = leyend.Replace("ParamProduct", model.Product);
                LeyendRelease.Text = leyend;


                LeyendRelease = tabla6.FindControl("label32", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("CATextOA").Value.Replace("ParamProduct", model.Product);
                LeyendRelease.Text = leyend;

                LeyendRelease = tabla6.FindControl("label5", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("RPATextOA").Value.Replace("ParamProduct", model.Product);
                leyend = model.ProductName != "CO2" ? leyend.Replace("ParamChangeCO2_RPA", _resource.GetString("ParagraphRPA").Value) : leyend.Replace("ParamChangeCO2_RPA", _resource.GetString("ParagraphCO2_RPA").Value);
                LeyendRelease.Text = leyend;

                LeyendRelease = tabla7.FindControl("label32", true) as XRLabel;
                leyend = string.Empty;
                leyend = _resource.GetString("LPTTextOA").Value.Replace("ParamPlant", model.Plant);
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

                _rptOA.ExportToPdf(ms);

                var fileNames = model.PipeFillingControl.SelectMany(x => x.PipesList)
                    .SelectMany(x => x.FinalAnalysis)
                    .Union(model.PipeFillingControl.SelectMany(x => x.PipesList)
                    .SelectMany(x => x.InitialAnalysis))
                    .Where(x => x.ParameterName == "Identidad" && !string.IsNullOrEmpty(System.IO.Path.GetExtension(x.PathFile)))
                    .Select(x => x.PathFile)
                    .ToList();
                if (fileNames.Any())
                {
                    using PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor();
                    pdfDocumentProcessor.LoadDocument(ms);
                    foreach (var item in fileNames)
                    {
                        string path = $"{this._config["BasePathControlPipeFiles"]}\\{item}";
                        if (System.IO.File.Exists(path))
                            pdfDocumentProcessor.AppendDocument(path);
                    }

                    pdfDocumentProcessor.SaveDocument(finalFile);
                    return File(finalFile.ToArray(), "application/pdf", string.Format("{0}-OA.{1}", model.LotProd.Replace("\n", "").Trim(), "pdf").Replace(" - ", "-"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de ExportToPdf " + ex);
            }
            return File(ms.ToArray(), "application/pdf", string.Format("{0}-OA.{1}", model.LotProd.Replace("\n", "").Trim(), "pdf").Replace(" - ", "-"));
        }


        [HttpGet]
        public async Task<IActionResult> ExportCertificateToPDF(int IdOP, int Id, string tourNumber, string pipeNumber, string tank, string distributionBatch)
        {
            if (IdOP <= 0)
            {
                return BadRequest();
            }

            try
            {

                using MemoryStream ms = new MemoryStream();

                ConditioningOrderViewModel model = new ConditioningOrderViewModel();

                model = await _exportPDFService.getDatasource(IdOP, Id);

                Report1 report = await _exportPDFService.GetRptCertificate(IdOP, Id, tourNumber, pipeNumber, tank, model, distributionBatch, (int)model.CertificateId, (int)model.FooterCertificateId);

                report.ExportToPdf(ms);

                string Bill = string.Empty;

                var m = model.PipeFillingControl.FirstOrDefault(pc => pc.TourNumber == tourNumber);
                foreach (PipeFillingViewModel item in m.PipesList)
                {
                    foreach (var itemx in item.Customers)
                    {
                        if ((item.DistributionBatch == distributionBatch) && (itemx.ConditioningOrderId == Id && itemx.TourNumber == tourNumber && itemx.DistributionBatch == distributionBatch && itemx.Tank == tank))
                        {
                            Bill = itemx.Folio;
                        }
                    }
                }
                return File(ms.ToArray(), "application/pdf", string.Format("{0}.{1}", Bill, "pdf").Replace(" - ", "-"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de acondicionamiento " + ex);
            }

            return BadRequest();

        }

        [HttpGet]
        public async Task<IActionResult> AddScalesFlowMeters(int ConditioningId, String SelectedScalesFlowMeters)
        {
            List<ScalesFlowMeters> Scales = new List<ScalesFlowMeters>();
            ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            if (SelectedScalesFlowMeters == null)
                return Json(new { Result = "fail", Message = "Selecciona una báscula" });
            model.Id = ConditioningId;
            var array = SelectedScalesFlowMeters.Split(",");
            foreach (var item in array)
            {
                var ScalesDB = await _scalesFlowMetersRepository.GetAsync(x => x.ConditioningOrderId == ConditioningId && x.Description == item.ToString());
                if (!ScalesDB.Any())
                {
                    Scales.Add(new ScalesFlowMeters
                    {
                        Description = item.ToString(),
                        IsCalibrated = null,
                        ReviewedBy = null,
                        ReviewedDate = null,
                        ConditioningOrderId = ConditioningId
                    });
                }
                else
                {
                    var DeleteScales = await _scalesFlowMetersRepository.DeleteAsync(ScalesDB);
                }
            }
            var AddScales = await _scalesFlowMetersRepository.AddAsync(Scales);
            ///GET INFO
            model.ScalesflowsList = await _conditioningOrderService.GetTable2(model);
            return PartialView("_Scalesflows", model);
        }

        [HttpGet]
        public async Task<IActionResult> QueryLogStatusOrder(int Id)
        {
            try
            {
                var result = await this._conditioningOrderService.GetLogStatusOrderByIdAsync(Id);
                return PartialView("_LogStatusOrder", result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return StatusCode(500, "Surgio un error al consultar el Historico.");
            }
        }

        List<T> Clone<T>(IEnumerable<T> oldList)
        {
            List<T> newList = new List<T>();
            return newList = new List<T>(oldList);
        }

        public async Task<int> GetCheckListId(int CheckListPipeDictiumId)
        {
            int id = 0;
            var CheckListId = await _checkListPipeRecordAnswerRepository.GetAsync(x => x.CheckListPipeDictiumId == CheckListPipeDictiumId);
            foreach (var item in CheckListId)
            {
                id = item.CheckListPipeDictiumId;
            }
            return id;
        }

        public async Task<string> GetUserId(List<System.Security.Claims.Claim> claims)
        {
            string Id = string.Empty;
            foreach (var item in claims)
            {
                if (item.Type == "Id")
                {
                    Id = item.Value;
                }
            }
            return Id;
        }
        [HttpPost]
        public async Task<IActionResult> SaveCloseOpen(ConditioningOrderViewModel model)
        {
            var entity = new ConditioningOrder();
            try
            {
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
                entity = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ConditioningOrder();
                entityClone = (ConditioningOrder)entity.Clone();
                if (entity == null)
                {
                    return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
                }
                entity.State = ConditioningOrderStatus.WithoutPipes.Value;
                entity.StepSaved = 3;
                entity.DelegateUser = userInfo;
                entity.EndDate = DateTime.Now;
                entity.CreatedBy = userInfo;
                await _conditioningOrderRepository.UpdateAsync(entity, entityClone);

                historyStates.ProductionOrderId = model.Id;
                historyStates.State = entity.State;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo;
                historyStates.Type = HistoryStateType.OrdenAcondicionamiento.Value;
                await _historyStatesRepository.AddAsync(historyStates);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error" });
            }
            return Json(new { Result = "Ok", Status = entity.State });
        }
        //AHF
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string path)
        {
            try
            {
                if (Path.GetExtension(path) != ".pdf")
                    return NotFound();
                string completePath = $"{this._config["BasePathControlPipeFiles"]}\\{path}";
                if (!System.IO.File.Exists(completePath))
                    return NotFound();
                var fileData = await System.IO.File.ReadAllBytesAsync(completePath);
                return File(fileData, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //AHF
        private async Task<string> SaveFileAsync(PipeFillingAnalysisViewModel model)
        {
            string basePath = _config["BasePathControlPipeFiles"];
            if (!string.IsNullOrEmpty(basePath))
            {
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);
                string fileName = $"{DateTime.Now.ToFileTime()}{Path.GetExtension(model.PathFile)}";
                var data = model.File.Split(",")[1];
                var pathFile = $"{basePath}\\{fileName}";
                if (System.IO.File.Exists(pathFile))
                    System.IO.File.Delete(pathFile);
                byte[] sPDFDecoded = Convert.FromBase64String(data);
                await System.IO.File.WriteAllBytesAsync(pathFile, sPDFDecoded);
                return fileName;
            }
            else
            {
                _logger.LogError("Error en la configuración de la ruta para almacenamiento de archivos");
            }
            return null;
        }

        [HttpGet]
        public async Task<IActionResult> SerchCheckList(int Id, int option)
        {
            ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            var infoDB = new List<SechToolDistributionBatchViewModel>();
            if (option == 1)
            {
                infoDB = await _principalService.GetCheckListDictiumAnswer(Id, 1);
            }
            if (infoDB.Any())
            {
                model.SechToolDistributions = infoDB.ToList();
            }
            return PartialView("_SerchCheckList", model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCheckList(int Id)
        {
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                dictiumAnswer.CreatedBy = userInfo.NombreUsuario;
                dictiumAnswer.Date = DateTime.Now;
                dictiumAnswer.Comment = null;
                dictiumAnswer.NumOA = Id;
                dictiumAnswer.DistributionBatch = "";
                dictiumAnswer.TourNumber = "";
                dictiumAnswer.Status = CheckListType.Inprogress.Value;
                dictiumAnswer.Source = CheckListGeneralViewModelStatus.Web.Value;
                dictiumAnswer.RelationShip = false;
                var saveDictium = await _checkListPipeDictiumAnswerRepository.AddAsync(dictiumAnswer);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "NotFound", Message = "error al generar el checkList" + ex });
            }
            return Json(new { Result = "Ok", Message = "CheckList Guardado correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckListTourNumber(int Id)
        {
            List<SelectListItem> Control = new List<SelectListItem>();
            List<SelectListItem> Filling = new List<SelectListItem>();
            ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            List<PipeFillingControlViewModel> pipes = new List<PipeFillingControlViewModel>();
            var statusNon = new List<string>();
            statusNon.Add(CheckListType.CloseNo.Value);
            statusNon.Add(CheckListType.Cancelled.Value);
            model.Id = Id;
            try
            {
                var infoControl = await _conditioningOrderService.GetPipeFillingControlsXChecklist(model);
                var dictium = await _principalService.GetCheckListDictiumAnswer(Id, 2);
                ///control 
                foreach (var item in infoControl)
                {
                    var PipeFilling = new List<PipeFillingViewModel>();
                    foreach (var itemx in item.PipesList)
                    {
                        var validate = dictium.Where(x => x.TourNumber == item.TourNumber
                            && x.Status == CheckListType.CloseOk.Value && x.PipeNumber.Equals(itemx.PipeNumber)).ToList();
                        if (validate.Count == 0)
                        {
                            //Control.Add(new SelectListItem
                            //{
                            //    Text = item.TourNumber,
                            //    Value = item.TourNumber,
                            //});
                            PipeFilling.Add(new PipeFillingViewModel
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
                                Customers = itemx.Customers.ToList(),
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
                            pipes.Add(new PipeFillingControlViewModel
                            {
                                Id = item.Id,
                                ConditioningOrderId = item.ConditioningOrderId,
                                TourNumber = item.TourNumber,
                                PipesList = PipeFilling
                            });
                        }
                    }
  
                }

                ///filling

                var CheckListPipeAnswerdB = await _checkListPipeAnswerRepository.GetAsync(x => x.NumOA == Id);
                var infoDB = from info in dictium
                             select new CheckListRelationShip
                             {
                                 ConditioningOrderId = info.ConditioningOrderId,
                                 Id = info.Id,
                                 Source = info.Source,
                                 Complete = info.RelationShip,
                                 TourNumber = info.TourNumber,
                                 PipeNumber = info.PipeNumber,
                                 File = info.File,
                                 Alias = info.Alias,
                                 Status = info.Status
                             };

                var validfiltered = infoDB.Where(x => x.Complete == true && !statusNon.Contains(x.Status));
                //var validfiltered = infoDB.Where(x => x.Complete == true && x.Status != CheckListType.CloseNo.Value);
                //validfiltered = validfiltered.Where(x => x.Status != CheckListType.Cancelled.Value);

                //var filtered = Control.Where(x => validfiltered.Any(y => y.TourNumber == x.Text)).ToList();


                foreach (var item in pipes.Select(x=>x.PipesList))
                {
                    Control.Add(new SelectListItem
                    {
                        Text = item.FirstOrDefault().TourNumber,
                        Value = item.FirstOrDefault().TourNumber,
                    });
                }

                var filtered = (from ctrol in pipes.Where(x => 
                            validfiltered.Any(y => y.TourNumber.Equals(x.TourNumber) 
                            && y.PipeNumber.Equals(x.PipesList.Select(z=>z.PipeNumber))))
                                select new SelectListItem
                                {
                                    Text = ctrol.TourNumber,
                                    Value = ctrol.TourNumber,
                                }).ToList();

                var removed = Control.Where(x=> filtered.Select(y=>y.Text).Contains(x.Text)).ToList();
                foreach (var item in removed)
                {
                    Control.Remove(item);
                }
                model.ListPipeFillingControl = Control.ToList();
                model.CheckListRelationShip = infoDB.ToList();
                model.ListPipeFilling = Filling.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en CheckListTourNumber " + ex);
                return Json(new { Result = "fail", Message = "Ocurrio un error en CheckListTourNumber " + ex });
            }

            return PartialView("_CheckListRelationship", model);
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        public async Task<IActionResult> GetPipesCheckList(string TourNumber, [FromForm] ConditioningOrderViewModel model)
        {
            List<SelectListItem> Filling = new List<SelectListItem>();
            var pipe = model.PipeFillingControl.Where(x => x.TourNumber == TourNumber);
            #region old
            //var checkDictium = (await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.Id));
            //if (pipe != null)
            //{
            //    foreach (var item in pipe)
            //    {
            //        foreach (var itemx in item.PipesList)
            //        {
            //            if (checkDictium.Where(x => x.TourNumber == item.TourNumber && x.PipeNumber == itemx.PipeNumber).Any())
            //                Filling.Add(new SelectListItem { Value = itemx.PipeNumber, Text = itemx.PipeNumber });
            //        }
            //    }
            //}
            #endregion

            if (pipe != null)
            {
                foreach (var item in pipe)
                {
                    foreach (var itemx in item.PipesList)
                    {
                        if (item.TourNumber == TourNumber)
                            Filling.Add(new SelectListItem { Value = itemx.DistributionBatch, Text = itemx.PipeNumber });
                    }
                }
            }
            return Json(Filling);

        }


        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        public async Task<IActionResult> SaveTourNumberPipeNumberRel([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                model.CheckListRelationShip = model.CheckListRelationShip.Where(x => x.TourNumber != null && x.PipeNumber != "Pipa ya relacionada al TourNumber"
                                              && x.Complete == false).ToList();
                model.CheckListRelationShip = model.CheckListRelationShip.Where(x => x.PipeNumber != null).ToList();
                var upd = await _conditioningOrderService.UpdateRelChecklistTourNumberPipe(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en SaveTourNumberPipeNumberRel " + ex);
                return Json(new { Result = "fail", Message = "Ocurrio un error en SaveTourNumberPipeNumberRel " + ex });
            }
            return Json(new { Result = "Ok", Message = "CheckList Guardado correctamente" });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteChecklist(int Id, int ConditioningOrderId, int option)
        {
            ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            await _conditioningOrderService.DeleteChecklist(Id);
            if (option == 1)
            {

                var infoDB = await _principalService.GetCheckListDictiumAnswer(ConditioningOrderId, option);
                if (infoDB.Any())
                {
                    model.SechToolDistributions = infoDB.ToList();
                }
                return PartialView("_SerchCheckList", model);
            }

            else
                return PartialView("_CheckListRelationship", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCheckListIdWebManual(int idOA, string tourNumber, string pipeNumber, string distributionBatch)
        {
            try
            {
                var ObjDB = await _checkListPipeDictiumAnswerRepository
                          .GetAsync(x => x.NumOA == idOA && x.TourNumber == tourNumber
                              && x.PipeNumber == pipeNumber
                          && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);
                if (ObjDB.Any())
                {
                    return Json(new
                    {
                        Result = "Ok",
                        Message = ObjDB.FirstOrDefault().Id
                    });
                }
                else
                {
                    return Json(new { Result = "fail", Message = "No existen checkList" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetLastStatus " + ex);
                return Json(new { Result = "fail", Message = ex });
            }

        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = 10000)]
        public async Task<IActionResult> DuplicateOffPipesCheckList(string TourNumber, [FromForm] ConditioningOrderViewModel model, string PipeNumber)
        {
            List<SelectListItem> Filling = new List<SelectListItem>();
            var pipe = model.CheckListRelationShip;
            if (pipe.Where(x => x.TourNumber == TourNumber && x.PipeNumber == PipeNumber && x.Status != CheckListType.CloseNo.Value).Count() > 1)
                Filling.Add(new SelectListItem { Value = "Pipa ya relacionada al TourNumber", Text = "Pipa ya relacionada al TourNumber" });
            if (pipe.Where(x => x.TourNumber == TourNumber && x.PipeNumber == PipeNumber && x.Status != _resource.GetString("NoInformation").Value).Count() > 1)
                Filling.Add(new SelectListItem { Value = "Pipa ya relacionada al TourNumber", Text = "Pipa ya relacionada al TourNumber" });
            
            var dictium = await _principalService.GetCheckListDictiumAnswer(model.Id, 2);
            var CheckListPipeAnswerdB = await _checkListPipeAnswerRepository.GetAsync(x => x.NumOA == model.Id);
            var infoDB = from info in dictium
                         select new CheckListRelationShip
                         {
                             ConditioningOrderId = info.ConditioningOrderId,
                             Id = info.Id,
                             Source = info.Source,
                             Complete = info.RelationShip,
                             TourNumber = info.TourNumber,
                             PipeNumber = info.PipeNumber,
                             File = info.File,
                             Alias = info.Alias,
                             Status = info.Status
                         };

           var validate = infoDB.Where(x => x.TourNumber == TourNumber && x.PipeNumber == PipeNumber?.Split('-')[3]).ToList();
            if(validate.Any())
                Filling.Add(new SelectListItem { Value = "Pipa ya relacionada al TourNumber", Text = "Pipa ya relacionada al TourNumber" });
            if (pipe != null)
            {
                foreach (var item in pipe.Where(x => x.PipeNumber != null))
                {
                    if (pipe.Where(x => x.TourNumber == TourNumber && x.PipeNumber == PipeNumber).Count() >= 1)
                    {
                        //return Json(new { Result = "Ok", Message = "ok" });
                    }

                    //Filling.Add(new SelectListItem { Value = item.PipeNumber, Text = item.PipeNumber });
                    else
                        Filling.Add(new SelectListItem { Value = "Pipa ya relacionada al TourNumber", Text = "Pipa ya relacionada al TourNumber" });
                }
            }
            if (Filling.Count >= 1)
                return Json(new { Result = "fail", Message = "error" });///return Json(Filling);
            else
                return Json(new { Result = "Ok", Message = "error" });

        }

        [HttpGet]
        public async Task<IActionResult> ExportChecklistPdf(int IdOP, int Id, int checkListId, string distributionBatch, int option)
        {
            using MemoryStream ms = new MemoryStream();
            try
            {
                var oaDb = await _conditioningOrderRepository.GetByIdAsync(Id);
                if (oaDb != null)
                    IdOP = oaDb.ProductionOrderId;

                if (IdOP <= 0)
                {
                    return BadRequest();
                }

                List<ConditioningOrderViewModel> rptDataSource = new List<ConditioningOrderViewModel>();
                var model = await _exportPDFService.GetModel(IdOP, Id);//  
                if (model == null)
                {
                    return BadRequest();
                }
                rptDataSource.Add(model);
                RptOrAP _rptOA = new RptOrAP();
                List<RptChkList> rptChkList = new List<RptChkList>();
                var formulas = await _formulaRepository.GetAllAsync();
                var f1 = formulas.Where(f => f.ProductId == model.ProductId).FirstOrDefault();
                List<CheckListGeneralViewModel> chkGeneralList = new List<CheckListGeneralViewModel>();
                List<CheckListGeneralViewModel> checklistDB = new List<CheckListGeneralViewModel>();
                List<PipeFillingControlViewModel> pipes = new List<PipeFillingControlViewModel>();
                if (option == 2)
                {
                    foreach (PipeFillingControlViewModel item in model.PipeFillingControl)
                    {
                        ConditioningOrderViewModel m1 = new ConditioningOrderViewModel();
                        m1.Presentation = model.Presentation;
                        m1.Product = model.Product;
                        foreach (PipeFillingViewModel itemx in item.PipesList)
                        {
                            checklistDB = (from record in await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.TourNumber == x.TourNumber
                                               && x.PipeNumber == itemx.PipeNumber && x.DistributionBatch == itemx.DistributionBatch && x.Id == checkListId)
                                           select new CheckListGeneralViewModel
                                           {
                                               Id = record.Id,
                                               ConditioningOrderId = model.Id,
                                               TourNumber = record.TourNumber,
                                               DistributionBatch = record.DistributionBatch,
                                               Source = record.Source,
                                               Alias = record.Alias
                                           }).ToList();
                            if (checklistDB.Any())
                            {
                                foreach (var itemy in checklistDB)
                                {
                                    if (itemy.Id > 0 && itemy.Source == true)
                                    {
                                        CheckListGeneralViewModel chkGnrl = new CheckListGeneralViewModel();
                                        chkGnrl.Id = itemy.Id;
                                        chkGnrl.ConditioningOrderId = model.Id;
                                        chkGnrl.TourNumber = item.TourNumber;
                                        chkGnrl.DistributionBatch = itemx.DistributionBatch;
                                        chkGeneralList.Add(chkGnrl);
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    checklistDB = (from record in await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.Id == checkListId)
                                   select new CheckListGeneralViewModel
                                   {
                                       Id = record.Id,
                                       ConditioningOrderId = model.Id,
                                       TourNumber = record.TourNumber,
                                       DistributionBatch = record.DistributionBatch,
                                       Source = record.Source,
                                       Alias = record.Alias
                                   }).ToList();
                    if (checklistDB.Any())
                    {
                        foreach (var itemy in checklistDB)
                        {
                            if (itemy.Id > 0 && itemy.Source == true)
                            {
                                CheckListGeneralViewModel chkGnrl = new CheckListGeneralViewModel();
                                chkGnrl.Id = itemy.Id;
                                chkGnrl.ConditioningOrderId = model.Id;
                                chkGnrl.TourNumber = itemy.TourNumber;
                                chkGnrl.DistributionBatch = itemy.DistributionBatch;
                                chkGeneralList.Add(chkGnrl);
                            }
                            distributionBatch = itemy.Alias != null ? itemy.Alias : model.Id.ToString();
                        }
                    }
                }
                if (chkGeneralList.Count > 0)
                {
                    for (int i = 0; i < chkGeneralList.Count; i++)
                    {
                        if (option == 2)
                        {
                            var t = await _exportPDFService.GetRptChkList(chkGeneralList[i]);
                            rptChkList.Add(t);
                        }
                        else
                        {
                            var t = await _exportPDFService.GetRptChkListQuestionsOne(chkGeneralList[i]);
                            rptChkList.Add(t);
                        }
                    }

                }
                _rptOA.DataSource = rptDataSource;
                _rptOA.ModifyDocument(x =>
                {
                    if (rptChkList.Count > 0)
                    {
                        rptChkList.ForEach(chk =>
                        {
                            x.AddPages(chk.Pages);
                        });
                    }
                });

                _rptOA.ExportToPdf(ms);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en ExportChecklistPdf " + ex);
                return BadRequest();
            }

            return File(ms.ToArray(), "application/pdf", string.Format("{0}.{1}", distributionBatch, "pdf").Replace(" - ", "-"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SendNotification(SendNotificationViewModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request?.Emails?.Trim()))
                    return Json(new { Result = "Error", Message = "Parametros inválidos." });
                var info = (await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == request.Id && x.TourNumber == request.TourNumber && x.DistributionBatch == request.DistributionBatch && x.Tank == request.Tank)).FirstOrDefault();
                if (info == null)
                    return Json(new { Result = "Error", Message = "Parametros inválidos." });
                var infoClone = new PipeFillingCustomer();
                var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
                infoClone = (PipeFillingCustomer)info.Clone();
                info.EmailListSend = true;
                info.EmailsList = request.Emails?.Trim();
                info.ConditioningOrder = new ConditioningOrder() { CreatedBy = userInfo, PlantId = request.PlantId, ProductId = request.ProductId };
                info.ConditioningOrderId = request.Id;
                var valores = request.Emails.Split(",").Where(x => !string.IsNullOrEmpty(x?.Trim())).Select(x => x.Trim()).ToList();
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramProducto}", Value = request.Product });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramFolio}", Value = request.Bill.Trim() });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = request.DistributionBatch });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(request.ProductionOrderId) });
                    var collectionFiles = await this.GetFileCollectionCertificate(request.ProductionOrderId, request.Id, request.TourNumber, null, request.Tank, request.DistributionBatch);
                    await this._notification.NotificationOa(paramters, valores, this._config["EmailSubjects:AnalysisReportPath"], this._config["Emailtemplates:AnalysisReportPath"], collectionFiles);
                }
                await _pipeFillingCustomerRepository.UpdateAsync(info, infoClone);
                return Json(new { Result = "Ok", Message = "Se envio el correo correctamente" });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al enviar el Correo." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SendNotificationConfirm(ConditioningOrderViewModel model)
        {
            try
            {
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var parameters = new List<Parameter>();
                    parameters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    parameters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    var inCompliance = this.FindInComplice(model.PipeFillingControl.SelectMany(x => x.PipesList).ToList(), true);
                    var notInCompliance = this.FindInComplice(model.PipeFillingControl.SelectMany(x => x.PipesList).ToList(), false);
                    if (inCompliance != null && inCompliance.Any())
                        await this._notification.NotificationInCompliance(inCompliance, parameters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:InComplianceOAPath"], this._config["Emailtemplates:InComplianceOAPath"]);
                    if (notInCompliance != null && notInCompliance.Any())
                        await this._notification.NotificationInCompliance(notInCompliance, parameters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:NotInComplianceOAPath"], this._config["Emailtemplates:NotInComplianceOAPath"]);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al enviar la notificación." });
            }
            return Json(new { Result = "Ok", Message = "Se envió la notificación de manera exitosa." });
        }

        private List<Tournumber> FindInComplice(List<PipeFillingViewModel> pipeFillingViewModels, bool InCompliance)
        {
            return pipeFillingViewModels
                .Where(x => x.InCompliance.HasValue && x.InCompliance.Value == InCompliance)
                .Select(x => new Tournumber()
                {
                    Number = x.TourNumber,
                    PipeNumber = x.PipeNumber,
                    DistributionBatch = x.DistributionBatch,
                    IsRelased = x.IsReleased
                }).ToList();
        }

        private async Task<IEnumerable<FileDto>> GetFileCollectionCertificate(int IdOP, int Id, string tourNumber, string pipeNumber, string tank, string distributionBatch)
        {
            using MemoryStream ms = new MemoryStream();
            var model = await _exportPDFService.getDatasource(IdOP, Id);
            Report1 report = await _exportPDFService.GetRptCertificate(IdOP, Id, tourNumber, pipeNumber, tank, model, distributionBatch, (int)model.CertificateId, (int)model.FooterCertificateId);
            report.ExportToPdf(ms);
            string Bill = string.Empty;
            var m = model.PipeFillingControl.FirstOrDefault(pc => pc.TourNumber == tourNumber);
            Bill = m.PipesList
                .Where(x => x.DistributionBatch == distributionBatch)
                .SelectMany(x => x.Customers)
                .Where(x => x.ConditioningOrderId == Id && x.TourNumber == tourNumber && x.DistributionBatch == distributionBatch && x.Tank == tank)
                .Select(x => x.Folio)
                .FirstOrDefault();
            var collection = new List<FileDto>()
            {
                new FileDto(ms.ToArray(), string.Format("{0}.{1}", Bill, "pdf").Replace(" - ", "-"), "application/pdf")
            };
            return collection;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveCustomerFiles()
        {
            try
            {
                var request = HttpContext.Request.Form;
                var ConditioningOrderId = int.Parse(request?["Id"].ToString());
                var tournumber = request["Tournumber"].ToString();
                var distribuitionBatch = request["DistributionBatch"].ToString();
                var tournumberIndex = request["TournumberIndex"].ToString();
                var pipeIndex = request["PipeIndex"].ToString();
                var tank = request["Tank"].ToString();
                var pipeNumber = request["PipeNumber"].ToString();
                var files = HttpContext.Request.Form.Files;
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var fileNameGuid = new PipeFillingCustomersFiles();
                fileNameGuid =  await _conditioningOrderService.SaveCustomersFiles(ConditioningOrderId, tournumber, distribuitionBatch, userInfo.NombreUsuario, files, tank, pipeNumber);
                return Json(new { Ok = true, tournumber, distribuitionBatch, pipeNumber, tournumberIndex, pipeIndex, fileNameGuid});

            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al enviar archivo." });
            }
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_OA)]
        public async Task<ActionResult> DownloadFileCustomer(string FileName, [FromServices] IPipeFillingCustomersFilesRepository pipeFillingCustomersFiles)
        {
            try
            {
                var resultado = await pipeFillingCustomersFiles.GetAsync(x => x.FileName == FileName);
                if (resultado != null && resultado.Any())
                {
                    string basePath = _resource.GetString("PathPipeFillingCustomersFiles").Value;
                    string path = resultado.Where(x => !string.IsNullOrEmpty(x.FileName)).Select(x => x.FileName).FirstOrDefault();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteCustomerFile([FromForm] PipeFillingCustomersFilesViewModel model, [FromServices] IPipeFillingCustomersFilesRepository pipeFillingCustomersFiles)
        {
            try
            {
                var resultado = (await pipeFillingCustomersFiles.GetAsync(x => x.FileName == model.FileName)).FirstOrDefault();
                if (resultado != null)
                {
                    await pipeFillingCustomersFiles.DeleteAsync(resultado);
                    var upload = Path.Combine(_resource.GetString("PathPipeFillingCustomersFiles").Value);
                    //delete file
                    if ((System.IO.File.Exists(upload + @"\" + resultado.FileName)))
                    {
                        System.IO.File.Delete(upload + @"\" + resultado.FileName);
                    }
                    return Json(new { Result = "Ok", Message = "Se eliminó exitosamente." });
                }
                else
                {
                    return Json(new { Ok = false });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = "Surgio un error al eliminar archivo." + ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> PerformanceSendNotify([FromForm] ConditioningOrderViewModel model)
        {
            try
            {
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (enabledNotificationResult && enabledNotification)
                {
                    var parameters = new List<Parameter>();
                    parameters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.LotProd });
                    parameters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOa(model.ProductionOrderId) });
                    await this._notification.SendNotification(parameters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:PerformancePath"], this._config["Emailtemplates:PerformancePath"]);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al enviar la notificación." });
            }
            return Json(new { Result = "Ok", Message = "Se envió la notificación de manera exitosa." });
        }

    }
}