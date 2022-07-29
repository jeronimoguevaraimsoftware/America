using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Reports.OrderProduction;
using LiberacionProductoWeb.Reports;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using System.Drawing;
using AutoMapper;
using DevExpress.Pdf;
using Org.BouncyCastle.Asn1.X500;
using System.Drawing;
using LiberacionProductoWeb.Models;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LiberacionProductoWeb.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    public class ProductionOrderController : Controller
    {
        private readonly ILogger<ProductionOrderController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<ProductionOrderController> _localizer;
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IProductCatalogRepository _productRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IProductionEquipmentRepository _productionEquipmentRepository;
        private readonly IMonitoringEquipmentRepository _monitoringEquipmentRepository;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        private readonly IPrincipalService _principalService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductionOrderService _productionOrderService;
        private readonly AppDbExternalContext _appDbContext;
        private readonly IBatchAnalysisRepository _batchAnalysisRepository;
        private readonly IHistoryNotesRepository _historyNotesRepository;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IHistoryStatesRepository _historyStatesRepository;
        private readonly IVariablesFileReaderService _variablesFileReaderService; //MEG
        private readonly IConfiguration _config; //MEG
        private readonly IExportPDFService _exportPDFService;
        private readonly INotification _notification;
        private readonly ILayoutCertificateService _layoutCertificateService;
        public ProductionOrderController(
            IStringLocalizer<ProductionOrderController> localizer,
            ILogger<ProductionOrderController> logger,
            UserManager<ApplicationUser> userManager,
            IAnalyticsCertsService analyticsCertsService,
            IProductCatalogRepository productCatalogRepository,
            IProductionOrderRepository productionOrderRepository,
            IProductionEquipmentRepository productionEquipmentRepository,
            IMonitoringEquipmentRepository monitoringEquipmentRepository,
            IPipelineClearanceRepository pipelineClearanceRepository,
            IProductionOrderAttributeRepository productionOrderAttributeRepository,
            IBatchDetailsRepository batchDetailsRepository,
            IHttpContextAccessor httpContextAccessor,
            IContainerCatalogRepository containerCatalogRepository,
            IGeneralCatalogRepository generalCatalogRepository,
            IConditioningOrderRepository conditioningOrderRepository,
            IPrincipalService principalService,
            IVariablesFileReaderService variablesFileReaderService,
            IProductionOrderService productionOrderService,
            AppDbExternalContext external,
            IBatchAnalysisRepository batchAnalysisRepository,
            IHistoryNotesRepository historyNotesRepository,
            IStringLocalizer<Resource> resource,
            IHistoryStatesRepository historyStatesRepository,
            IExportPDFService exportPDFService,
            IConfiguration config,
            INotification notification,
            ILayoutCertificateService layoutCertificateService)
        {
            _localizer = localizer;
            _logger = logger;
            _userManager = userManager;
            _analyticsCerts = analyticsCertsService;
            _productRepository = productCatalogRepository;
            _productionOrderRepository = productionOrderRepository;
            _productionEquipmentRepository = productionEquipmentRepository;
            _monitoringEquipmentRepository = monitoringEquipmentRepository;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _generalCatalogRepository = generalCatalogRepository;
            _conditioningOrderRepository = conditioningOrderRepository;
            _principalService = principalService;
            _httpContextAccessor = httpContextAccessor;
            _productionOrderService = productionOrderService;
            _appDbContext = external;
            _batchAnalysisRepository = batchAnalysisRepository;
            _historyNotesRepository = historyNotesRepository;
            _resource = resource;
            _historyStatesRepository = historyStatesRepository;
            _variablesFileReaderService = variablesFileReaderService;
            _config = config;
            _exportPDFService = exportPDFService;
            this._notification = notification;
            _layoutCertificateService = layoutCertificateService;
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_OP)]
        public async Task<IActionResult> Index(int id)
        {
            var model = await _productionOrderService.GetByIdAsync(id);
            if (model == null)
            {
                return BadRequest();
            }

            // fill filters
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var plants = await GetPlantsItemsAsync();
            var products = await GetProdutsItemsAsync(model.SelectedPlantFilter);
            var tanks = await GetTanksItemsAsync(model.SelectedPlantFilter, model.SelectedProductFilter);

            // This are the placeholders in text description
            model.Plant = plants.Where(p => p.Value == model.SelectedPlantFilter).FirstOrDefault()?.Text;
            model.Location = plants.Where(p => p.Value == model.SelectedPlantFilter).FirstOrDefault()?.Text;
            model.ProductName = products.Where(p => p.Value == model.SelectedProductFilter).FirstOrDefault()?.Text;
            model.ProductCode = products.Where(p => p.Value == model.SelectedProductFilter).FirstOrDefault()?.Value;


            #region files MEG
            try
            {
                model = await _variablesFileReaderService.FillVariablesAsync(model);
            }
            catch (Exception ex)
            {
                model.MensajeError = ex.Message;
                _logger.LogInformation("Ocurrio un error en orden en la lectura de variables " + ex);
            }
            #endregion



            return View(model);
        }

        [HttpGet]
        [Authorize("CreateEditOP")]
        public async Task<IActionResult> New(int Id, string SelectedPlantFilter, string SelectedProductFilter, string SelectedTankFilter, string SelectedPurityFilter)
        {
            var model = new ProductionOrderViewModel();
            model.CreatedDate = DateTime.Now;
            model.Status = "OP-En proceso";
            model.MonitoringEquipmentList = new List<MonitoringEquipmentViewModel>();
            var userInfo = HttpContext.User.Claims.ToList();
            if (Id == 0)
            {
                if (!userInfo.Where(x => x.Value == SecurityConstants.CREAR_OP).Any())
                {
                    return RedirectToAction("AccessDenied", "Account");
                }
                else
                {
                    // there is no Id so no data should be loaded
                    return View(model);
                }
            }

            var entity = new ProductionOrder();
            entity = await _productionOrderRepository.GetByIdAsync(Id);
            if (entity == null)
            {
                // Entity with the Id requested does not exists
                return View(model);
            }

            if (userInfo.Where(x => x.Value == SecurityConstants.EDITAR_OP).Any())
            {
                try
                {
                    //show panels
                    model.ShowPanelSteps = "panel-collapse collapse overflow-hidden";

                    model.Id = entity.Id;
                    model.StepSaved = entity.StepSaved;
                    model.CreatedDate = entity.CreatedDate;
                    model.Status = entity.State;

                    // fill filters
                    var plants = await GetPlantsItemsAsync();
                    var products = await GetProdutsItemsAsync(entity.PlantId);
                    var tanks = await GetTanksItemsAsync(entity.PlantId, entity.ProductId);

                    model.SelectedPlantFilter = entity.PlantId;
                    model.SelectedProductFilter = entity.ProductId;
                    model.SelectedTankFilter = entity.TankId;
                    model.SelectedPurityFilter = entity.Purity?.ToString();

                    model.Plant = plants.Where(p => p.Value == entity.PlantId).FirstOrDefault()?.Value;
                    model.Location = plants.Where(p => p.Value == entity.PlantId).FirstOrDefault()?.Text;
                    model.ProductName = products.Where(p => p.Value == entity.ProductId).FirstOrDefault()?.Text;
                    model.ProductCode = products.Where(p => p.Value == entity.ProductId).FirstOrDefault()?.Value;

                    model.InCompliance = entity.InCompliance;
                    model.IsReleased = entity.IsReleased;

                    model.ReasonReject = entity.ReasonReject;
                    model.ReleasedBy = entity.ReleasedBy;
                    model.ReleasedDate = entity.ReleasedDate;
                    model.ReleasedNotes = entity.ReleasedNotes;

                    var generalCatalogFilter = new List<GeneralCatalog>();
                    var generalCatalogFilterDB = await _generalCatalogRepository.GetAllAsync();
                    if (Id == 0)
                    {
                        generalCatalogFilter = generalCatalogFilterDB.Where(x => x.Estatus).ToList();
                    }
                    else
                    {
                        generalCatalogFilter = generalCatalogFilterDB.Where(x => x.PlantId == entity.PlantId && x.ProductId == entity.ProductId && x.TankId == entity.TankId && x.Estatus).ToList();    //AHF
                    }

                    foreach (var item in generalCatalogFilter)
                    {
                        if (item.PlantId == model.SelectedPlantFilter)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where entity.PlantId.Contains(r.PlantId) select r).ToList();
                        }
                        if (item.ProductId == model.SelectedProductFilter)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where entity.ProductId.Contains(r.ProductId) select r).ToList();
                        }
                        if (item.TankId == model.SelectedTankFilter)
                        {
                            generalCatalogFilter = (from r in generalCatalogFilter where entity.TankId.Contains(r.TankId) select r).ToList();
                        }
                    }
                    //mapped toModelView 
                    model.MonitoringEquipmentList = new List<MonitoringEquipmentViewModel>();
                    var productionEquipment = (await _productionEquipmentRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();
                    if (productionEquipment != null)
                    {
                        model.ProductionEquipment = new ProductionEquipmentViewModel
                        {
                            IsAvailable = productionEquipment.IsAvailable,
                            ReviewedBy = productionEquipment.ReviewedBy,
                            ReviewedDate = productionEquipment.ReviewedDate,
                        };
                    }

                    foreach (var item in generalCatalogFilter.Where(x => x.VariableClasification != "Variable de control de proceso"))
                    {
                        var monitoringEquipmentEntity = await _monitoringEquipmentRepository.GetAsync(x => x.ProductionOrderId == entity.Id);
                        if (monitoringEquipmentEntity.Count > 0)
                        {
                            foreach (var itemx in monitoringEquipmentEntity)
                            {
                                if (item.CodeTool == itemx.Code && item.DescriptionTool == itemx.Description)
                                {
                                    model.MonitoringEquipmentList.Add(new MonitoringEquipmentViewModel
                                    {
                                        Id = item.Id,
                                        Code = item.CodeTool,
                                        Description = item.DescriptionTool,
                                        IsCalibrated = itemx.IsCalibrated,
                                        ReviewedBy = itemx.ReviewedBy,
                                        ReviewedDate = itemx.ReviewedDate,
                                        Notes = itemx.Notes,
                                        ProductionOrderId = model.Id
                                    }); ;
                                }
                            }

                        }
                        else
                        {
                            model.MonitoringEquipmentList.Add(new MonitoringEquipmentViewModel
                            {
                                Id = item.Id,
                                Code = item.CodeTool,
                                Description = item.DescriptionTool
                            });
                        }
                    }

                    var pipelineClearanceList = await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == entity.Id);
                    var pipelineClearance = pipelineClearanceList.Where(x => x.InCompliance.HasValue && x.InCompliance.Value).FirstOrDefault();
                    if (pipelineClearance != null)
                    {
                        model.PipelineClearance = new PipelineClearanceViewModel
                        {
                            InCompliance = pipelineClearance.InCompliance,
                            ReviewedBy = pipelineClearance.ReviewedBy,
                            ReviewedDate = pipelineClearance.ReviewedDate,
                            ProductionStartedDate = pipelineClearance.ProductionStartedDate,
                            HasPendingReview = false,
                            ProductionEndDate = pipelineClearance.ProductionEndDate
                        };
                    }
                    else
                    {
                        var hasPendingReview = pipelineClearanceList != null && pipelineClearanceList.Any() && pipelineClearanceList.Where(x => x.InCompliance.HasValue && !x.InCompliance.Value && (string.IsNullOrEmpty(x.ReviewedBySecond) || !x.ReviewedDateSecond.HasValue)).Any();

                        model.PipelineClearance = new PipelineClearanceViewModel
                        {
                            HasPendingReview = !hasPendingReview
                        };
                    }

                    var controlVariablesList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.ControlVariable);
                    if (controlVariablesList.Count() > 0)
                    {
                        model.ControlVariables = controlVariablesList.Select(x =>
                            new ControlVariableViewModel
                            {
                                Id = x.Id,
                                Area = x.Area,
                                Description = x.Description,
                                DisplayName = x.Variable + " / " + x.Specification,
                                Variable = x.Variable,
                                Specification = x.Specification,
                                ChartPath = x.ChartPath,
                                MaxValue = x.MaxValue,
                                MinValue = x.MinValue,
                                AvgValue = x.AvgValue,
                                InCompliance = x.InCompliance,
                                DeviationReport = new DeviationReportViewModel
                                {
                                    Folio = x.DeviationReportFolio,
                                    Notes = x.DeviationReportNotes
                                },
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                Notes = x.Notes,
                                Type = ProductionOrderAttributeType.ControlVariable,
                            //MEG find this variable on General Catalog Table TODO - Optimization
                            VariableCode = x.ChartPath,
                                LowLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                                TopLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                            }
                        ).ToList();
                    }
                    else
                    {
                        model.ControlVariables = generalCatalogFilter
                                                .Where(x => x.VariableClasification.Equals(VariableClasificationType.ControlVariable.Value))
                                                .Select(x => new ControlVariableViewModel
                                                {
                                                    Id = x.Id,
                                                    Area = x.Area,
                                                    Description = x.DescriptionTool,
                                                    DisplayName = x.Variable + " / " + x.VariableSpecification,
                                                    Variable = x.Variable,
                                                    Specification = x.VariableSpecification,
                                                    ChartPath = x.CodeTool, //GetChartPath(x.Variable, VariableClasificationType.ControlVariable.Value, entity), MEG
                                                Type = ProductionOrderAttributeType.ControlVariable,

                                                //MEG 20211108
                                                VariableCode = x.CodeTool,
                                                    LowLimit = x.LowerLimit,
                                                    TopLimit = x.UpperLimit

                                                }).ToList();
                    }

                    var criticalParametersList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalParameter);
                    if (criticalParametersList.Count > 0)
                    {
                        model.CriticalParameters = criticalParametersList.Select(x =>
                            new CriticalParameterViewModel
                            {
                                Id = x.Id,
                                Area = x.Area,
                                Description = x.Description,
                                DisplayName = x.Variable + " / " + x.Specification,
                                Parameter = x.Variable,
                                Specification = x.Specification,
                                ChartPath = x.ChartPath,
                                MaxValue = x.MaxValue,
                                MinValue = x.MinValue,
                                AvgValue = x.AvgValue,
                                InCompliance = x.InCompliance,
                                DeviationReport = new DeviationReportViewModel
                                {
                                    Folio = x.DeviationReportFolio,
                                    Notes = x.DeviationReportNotes
                                },
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                Notes = x.Notes,
                                Type = ProductionOrderAttributeType.CriticalParameter,
                            //MEG find this variable on General Catalog Table TODO - Optimization
                            VariableCode = x.ChartPath,
                                LowLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                                TopLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                            }
                        ).ToList();
                    }
                    else
                    {
                        model.CriticalParameters = generalCatalogFilter
                                                .Where(x => x.VariableClasification.Equals(VariableClasificationType.CriticalParameter.Value))
                                                .Select(x => new CriticalParameterViewModel
                                                {
                                                    Id = x.Id,
                                                    Area = x.Area,
                                                    Description = x.DescriptionTool,
                                                    DisplayName = x.Variable + " / " + x.VariableSpecification,
                                                    Parameter = x.Variable,
                                                    Specification = x.VariableSpecification,
                                                    ChartPath = x.CodeTool,  //GetChartPath(x.Variable, VariableClasificationType.CriticalParameter.Value, entity),
                                                Type = ProductionOrderAttributeType.CriticalParameter,

                                                //MEG 20211108
                                                VariableCode = x.CodeTool,
                                                    LowLimit = x.LowerLimit,
                                                    TopLimit = x.UpperLimit
                                                }).ToList();
                    }

                    var criticalQualityAttributesList = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
                    if (criticalQualityAttributesList.Count > 0)
                    {
                        model.CriticalQualityAttributes = criticalQualityAttributesList.Select(x =>
                            new CriticalQualityAttributeViewModel
                            {
                                Id = x.Id,
                                Area = x.Area,
                                Description = x.Description,
                                DisplayName = x.Variable + " / " + x.Specification,
                                Attribute = x.Variable,
                                Specification = x.Specification,
                                ChartPath = x.ChartPath,
                                MaxValue = x.MaxValue,
                                MinValue = x.MinValue,
                                AvgValue = x.AvgValue,
                                InCompliance = x.InCompliance,
                                DeviationReport = new DeviationReportViewModel
                                {
                                    Folio = x.DeviationReportFolio,
                                    Notes = x.DeviationReportNotes
                                },
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                Notes = x.Notes,
                                Type = ProductionOrderAttributeType.CriticalQualityAttribute,
                            //MEG find this variable on General Catalog Table TODO - Optimization
                            VariableCode = x.ChartPath,
                                LowLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.LowerLimit,
                                TopLimit = generalCatalogFilter
                                                .Where(g => g.CodeTool == x.ChartPath).FirstOrDefault()?.UpperLimit
                            }
                        ).ToList();
                    }
                    else
                    {
                        model.CriticalQualityAttributes = generalCatalogFilter
                                                .Where(x => x.VariableClasification.Equals(VariableClasificationType.CriticalQualityAttribute.Value))
                                                .Select(x => new CriticalQualityAttributeViewModel
                                                {
                                                    Id = x.Id,
                                                    Area = x.Area,
                                                    Description = x.DescriptionTool,
                                                    DisplayName = x.Variable + " / " + x.VariableSpecification,
                                                    Attribute = x.Variable,
                                                    Specification = x.VariableSpecification,
                                                    ChartPath = x.Variable.Contains("Identidad") ? string.Empty : x.CodeTool, //AHF
                                                Type = ProductionOrderAttributeType.CriticalQualityAttribute,

                                                //MEG 20211108
                                                VariableCode = x.CodeTool,
                                                    LowLimit = x.LowerLimit,
                                                    TopLimit = x.UpperLimit
                                                }).ToList();
                    }

                    var batchDetails = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();
                    if (batchDetails != null)
                    {
                        var Analysis = await _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetails.Id);
                        var mapped = ObjectMapper.Mapper.Map<IEnumerable<BatchAnalysisViewModel>>(Analysis);

                        model.BatchDetails = new BatchDetailsViewModel
                        {
                            Number = batchDetails.Number,
                            Tank = batchDetails.Tank,
                            Level = batchDetails.Level,
                            Size = batchDetails.Size,
                            AnalyzedBy = batchDetails.AnalyzedBy,
                            AnalyzedDate = batchDetails.AnalyzedDate,
                            InCompliance = batchDetails.InCompliance,
                            NotInComplianceFolio = batchDetails.NotInComplianceFolio,
                            NotInComplianceNotes = batchDetails.NotInComplianceNotes,
                            IsReleased = batchDetails.IsReleased,
                            ReleasedBy = batchDetails.ReleasedBy,
                            ReleasedDate = batchDetails.ReleasedDate,
                            ReleasedNotes = batchDetails.ReleasedNotes,
                            Analysis = mapped.ToList()
                        };
                    }
                    else
                    {
                        List<GeneralCatalog> lst = generalCatalogFilter
                                                    .Where(x => x.PlantId.Equals(model.SelectedPlantFilter)
                                                        && x.TankId.Equals(model.SelectedTankFilter)
                                                        && x.ProductId.Equals(model.SelectedProductFilter)
                                                        && !string.IsNullOrEmpty(x.ConversionFactor)).OrderByDescending(x => x.Id).ToList();
                        if (lst.Count > 1)
                            ViewBag.MultipleFactor = true;
                        else
                        {
                            ViewBag.MultipleFactor = false;

                        }

                        decimal factorConversion = 0;
                        decimal.TryParse(lst.FirstOrDefault().ConversionFactor, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out factorConversion);

                        var batchDetailsEmpty = await ObtenerLotesDetalle(model.SelectedPlantFilter, model.SelectedProductFilter, model.SelectedTankFilter, model.ProductCode);

                        decimal initialLevel, finalLevel;
                        decimal.TryParse(batchDetailsEmpty[0]?.NIVEL_FINAL, out initialLevel);
                        decimal.TryParse(batchDetailsEmpty[0]?.NIVEL_INICIAL, out finalLevel);
                        var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        model.BatchDetails = new BatchDetailsViewModel
                        {
                            Number = batchDetailsEmpty[0].ID_LOTE.Count() > 0 ? batchDetailsEmpty[0].ID_LOTE : "Por lotificar",
                            Tank = batchDetailsEmpty[0].TANQUE,
                            Level = decimal.Parse(batchDetailsEmpty[0].NIVEL_FINAL.Split(' ').First(), numberFormatInfo),
                            Size = finalLevel - initialLevel,
                            AnalyzedBy = batchDetailsEmpty[0].CREADO_POR,
                            AnalyzedDate = batchDetailsEmpty[0].FECHA_ALTA,
                            Analysis = new List<BatchAnalysisViewModel>(),
                            InCompliance = batchDetailsEmpty[0].STATUS.ToUpper().Equals("PRODUCTO CONFORME")
                        };

                        model.BatchDetails.Size = model.BatchDetails.Level * factorConversion;

                        model.BatchDetails.Analysis = batchDetailsEmpty.Select(x => new BatchAnalysisViewModel
                        {
                            ParameterName = x.PARAMETRO,
                            Value = x.VALOR_ANALISIS.ToString().Replace(',', '.'),
                            LowerLimit = x.LIMITE_INFERIOR.ToString(),
                            UpperLimit = x.LIMITE_SUPERIOR.ToString(),
                            MeasureUnit = x.UNIDAD_DE_MEDIDA
                        }).ToList();
                    }
                    if (model.StepSaved > 2)
                    {
                        model = await _variablesFileReaderService.FillVariablesAsync(model);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Ocurrio un error en orden de producción " + ex);
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(SecurityConstants.CREAR_OP)]
        public async Task<IActionResult> GetPurity(string productId)
        {
            try
            {
                var productSelected = (await _productRepository.GetAllAsync())
                                    .Where(x => (string.IsNullOrEmpty(productId) || x.ProductId == productId));
                var result = productSelected.Any() ? productSelected.FirstOrDefault().Purity : null;

                return Json(new { Result = "Ok", Data = result });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de producción " + ex);
                return Json(new { Result = "Error", Data = string.Empty });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.CANCELAR_OP)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Cancel([FromForm] ProductionOrderViewModel model, int step)
        {
            try
            {
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ProductionOrder();
                entityClone = (ProductionOrder)entity.Clone();
                if (entity.State == ProductionOrderStatus.InProgress.Value || entity.State == ProductionOrderStatus.ToBeReleased.Value)
                {
                    entity.State = ProductionOrderStatus.InCancellation.Value;
                    entity.ReasonReject = model.ReasonReject;
                    entity.EndDate = DateTime.Now;
                    entity.DelegateUser = userInfo.Id;
                    await _productionOrderRepository.UpdateAsync(entity, entityClone, step >= 5 ? model.BatchDetails.Number.Trim() : null);

                    historyStates.ProductionOrderId = model.Id;
                    historyStates.State = ProductionOrderStatus.InCancellation.Value;
                    historyStates.Date = DateTime.Now;
                    historyStates.User = userInfo.Id;
                    historyStates.Type = HistoryStateType.OrdenProduccion.Value;
                    await _historyStatesRepository.AddAsync(historyStates);
                    var enabledNotificationValue = this._config["enabledNotification"];
                    var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                    if (enabledNotificationResult && enabledNotification)
                    {
                        var paramters = new List<Parameter>();
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramProduct}", Value = model.ProductName });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetails.Number?.Replace("\n", "").Trim() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.Id) });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                        await this._notification.SendNotification(paramters, model.Plant, "RESPONSABLE SANITARIO", this._config["EmailSubjects:CancelPath"], this._config["Emailtemplates:CancelPath"]);
                    }
                    return Json(new { Result = "Ok", Message = _resource.GetString("OrderSend").Value, Status = entity.State });
                }
                else
                {
                    return Json(new
                    {
                        Result = "Fail",
                        Message = "La solicitud de cancelación no se encuentra en status " + ProductionOrderStatus.InProgress.Value +
                     " o en " + ProductionOrderStatus.ToBeReleased.Value
                    });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al cancelar la orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.CANCELAR_OP)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CancelApprove(ProductionOrderViewModel model, int step)
        {
            try
            {
                HistoryStates historyStates = new HistoryStates();
                List<HistoryNotes> historyNotes = new List<HistoryNotes>();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ProductionOrder();
                entityClone = (ProductionOrder)entity.Clone();
                if (entity.State == ProductionOrderStatus.InCancellation.Value)
                {
                    entity.DelegateUser = userInfo.Id;
                    entity.State = ProductionOrderStatus.Cancelled.Value;
                    entity.StepSaved = 6;
                    entity.ReasonReject = model.ReasonReject;
                    await _productionOrderRepository.UpdateAsync(entity, entityClone, step >= 5 ? model.BatchDetails.Number.Trim() : null);

                    historyStates.ProductionOrderId = model.Id;
                    historyStates.State = ProductionOrderStatus.Cancelled.Value;
                    historyStates.Date = DateTime.Now;
                    historyStates.User = userInfo.Id;
                    historyStates.Type = HistoryStateType.OrdenProduccion.Value;
                    await _historyStatesRepository.AddAsync(historyStates);
                    var enabledNotificationValue = this._config["enabledNotification"];
                    var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                    if (enabledNotificationResult && enabledNotification)
                    {
                        var paramters = new List<Parameter>();
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramProduct}", Value = model.ProductName });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetails.Number?.Replace("\n", "").Trim() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.Id) });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                        await this._notification.SendNotification(paramters, model.Plant, "USUARIO DE PRODUCCION", this._config["EmailSubjects:CancelAprovePath"], this._config["Emailtemplates:CancelAprovePath"]);
                    }
                    return Json(new { Result = "Ok", Message = _resource.GetString("OrderApprove").Value, Status = entity.State });
                }
                else
                {
                    return Json(new { Result = "Fail", Message = "La orden de producción no se encuentra en status " + ProductionOrderStatus.InCancellation.Value });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al aprovar la cancelación de la orden, favor de intentar más tarde." });
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.CANCELAR_OP)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CancelReject([FromForm] ProductionOrderViewModel model, int step)
        {
            try
            {
                HistoryStates historyStates = new HistoryStates();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ProductionOrder();
                entityClone = (ProductionOrder)entity.Clone();
                if (entity.State == ProductionOrderStatus.InCancellation.Value)
                {
                    entity.State = ProductionOrderStatus.InProgress.Value;
                    entity.ReasonReject = model.ReasonReject;
                    entity.DelegateUser = userInfo.Id;
                    await _productionOrderRepository.UpdateAsync(entity, entityClone, step >= 5 ? model.BatchDetails.Number.Trim() : null);

                    historyStates.ProductionOrderId = model.Id;
                    historyStates.State = ProductionOrderStatus.InProgress.Value;
                    historyStates.Date = DateTime.Now;
                    historyStates.User = userInfo.Id;
                    historyStates.Type = HistoryStateType.OrdenProduccion.Value;
                    await _historyStatesRepository.AddAsync(historyStates);
                    var enabledNotificationValue = this._config["enabledNotification"];
                    var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                    if (enabledNotificationResult && enabledNotification)
                    {
                        var paramters = new List<Parameter>();
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramProduct}", Value = model.ProductName });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetails.Number?.Replace("\n", "").Trim() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.Id) });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramObservaciones}", Value = model.ReasonReject?.Trim() });
                        await this._notification.SendNotification(paramters, model.Plant, "USUARIO DE PRODUCCION", this._config["EmailSubjects:CancelRejectPath"], this._config["Emailtemplates:CancelRejectPath"]);
                    }
                    return Json(new { Result = "Ok", Message = _resource.GetString("OrderReject").Value, Status = entity.State });
                }
                else
                {
                    return Json(new { Result = "Fail", Message = "La orden de producción no esta " + ProductionOrderStatus.InCancellation.Value });
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al rechazar la cancelación de la orden, favor de intentar más tarde." });
            }

        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OP)]
        public async Task<JsonResult> Save([FromForm] ProductionOrderViewModel model, int step)
        {
            try
            {
                switch (step)
                {
                    case 1:
                        return await SaveProductionEquipment(model);
                    case 2:
                        return await SavePipelineClearance(model);
                    case 3:
                        return await SaveMonitoringProcess(model);
                    case 4:
                        return await SaveQualityProcess(model);
                    case 5:
                        return await SaveBatchDetails(model);
                    case 6:
                        return await SaveRelease(model);
                    default:
                        return Json(new { Result = "Fail", Message = "Parametro invalido" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de producción " + ex);
                return Json(new { Result = "Fail", Message = ex.Message });
            }

        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_OP)]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveDespeje(PipelineClearanceViewModel model)
        {
            var modelOP = new ProductionOrderViewModel();
            if (model.ProductionOrderId == 0)
            {
                return Json(new { Result = "Error", Message = "Orden de producción sin asociar" });
            }

            if (string.IsNullOrEmpty(model.Bill) || string.IsNullOrEmpty(model.Notes))
            {
                return Json(new { Result = "Error", Message = "Folio y Observaciones son requeridos es requirido." });
            }

            var pipelineClearanceList = await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == model.ProductionOrderId);
            var hasPendingReview = pipelineClearanceList.Where(x => x.InCompliance.HasValue && !x.InCompliance.Value && (string.IsNullOrEmpty(x.ReviewedBySecond) || !x.ReviewedDateSecond.HasValue)).Any();
            if (hasPendingReview)
            {
                return Json(new { Result = "Error", Message = "Existe una evaluación pendiente" });
            }

            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());

            PipelineClearance pipeline = new PipelineClearance();
            try
            {
                pipeline.ProductionOrderId = model.ProductionOrderId;
                pipeline.Bill = string.IsNullOrEmpty(model.Bill) ? "NA" : model.Bill;
                pipeline.Notes = string.IsNullOrEmpty(model.Notes) ? "NA" : model.Notes;
                pipeline.ReviewedBy = model.ReviewedBy;
                pipeline.ReviewedDate = model.ReviewedDate;
                pipeline.InCompliance = false;
                pipeline.Activitie = model.Activitie;

                await _pipelineClearanceRepository.AddAsync(pipeline);

                //notes
                if (!string.IsNullOrEmpty(model.Notes) && model.Notes != "NA")
                {
                    var historyNotes = new List<HistoryNotes>
                    {
                        new HistoryNotes {
                            ProductionOrderId = model.Id,
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
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetailsNumber?.Replace("\n", "").Trim() });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.ProductionOrderId) });
                    await this._notification.SendNotification(paramters, model.PlantId, "SUPERINTENDENTE DE PLANTA", this._config["EmailSubjects:LineClearancePath"], this._config["Emailtemplates:LineClearancePath"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de producción " + ex);
                return Json(new { Result = "Error", Message = "Surgio un error, favor de intentar más tarde." });
            }
            return Json(new { Result = "Ok", Message = "Despeje guardado con éxito" });
        }
        [HttpGet]
        public async Task<IActionResult> GetBatchDetails(string plantId, string productId, string tankId, string product)
        {
            var batchDetails = await ObtenerLotesDetalle(plantId, productId, tankId, product);
            var model = new ProductionOrderViewModel();
            try
            {
                if (batchDetails != null && batchDetails.Any())
                {
                    var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    decimal valueLevel = 0;
                    var resul = decimal.TryParse(batchDetails.FirstOrDefault().NIVEL_FINAL.Split(' ').First(), NumberStyles.Number, numberFormatInfo, out valueLevel);
                    model.BatchDetails = new BatchDetailsViewModel
                    {
                        Number = batchDetails[0].ID_LOTE.Count() > 0 ? batchDetails[0].ID_LOTE : "Por lotificar",
                        Tank = batchDetails[0].TANQUE,
                        Level = resul ? valueLevel : 0,
                        //Size = decimal.Parse(batchDetails[0].NIVEL_FINAL) * factor,
                        //Size = decimal.Parse(batchDetails[0].NIVEL_FINAL) * 1,
                        Size = resul ? valueLevel : 0,
                        AnalyzedBy = batchDetails[0].CREADO_POR,
                        AnalyzedDate = batchDetails[0].FECHA_ALTA,
                        Analysis = batchDetails.Select(x => new BatchAnalysisViewModel
                        {
                            ParameterName = x.PARAMETRO,
                            Value = x.VALOR_ANALISIS.ToString(),
                            LowerLimit = x.LIMITE_INFERIOR.ToString(),
                            UpperLimit = x.LIMITE_SUPERIOR.ToString(),
                            MeasureUnit = x.UNIDAD_DE_MEDIDA
                        }).ToList(),
                        InCompliance = batchDetails[0].STATUS.ToUpper().Equals("PRODUCTO CONFORME"),
                    };
                }
                else
                    model.BatchDetails.Number = "No encontrado";
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView("_BatchAnalysis", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryDetail(int Id)
        {
            if (Id != 0)
            {
                ///historial observaciones
                var historyNotes = await _principalService.GetHistoryDetail(Id, 1);

                return Json(new { data = historyNotes });
            }
            else
            {
                return Json(new { Result = "Ok", Message = "Orden de producción sin asociar" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetDetailDL(int Id)
        {
            List<PipelineClearanceViewModel> pipelines = new List<PipelineClearanceViewModel>();

            if (Id != 0)
            {
                var conditions = await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == Id && (!x.InCompliance.HasValue || !x.InCompliance.Value));

                pipelines = conditions.Select(item => new PipelineClearanceViewModel
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
                    ProductionOrderId = item.ProductionOrderId,
                    Bill = item.Bill
                }).ToList();
            }

            return PartialView("_PipelineClearanceDeviationReport", pipelines);
        }

        [HttpGet]
        public async Task<IActionResult> _RptProductionOrder(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            List<ProductionOrderViewModel> rptDataSource = new List<ProductionOrderViewModel>();
            var model = await _exportPDFService.GetOrderProductionModelById(Id);
            MemoryStream ms = new MemoryStream();

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
                leyend = model.ProductName != "CO2" ? _resource.GetString("PDFOpReleaseProduct").Value.Replace("ParamPlant", model.Location) : _resource.GetString("PDFOpReleaseProductCO2").Value.Replace("ParamPlant", model.Location);
                leyend = leyend.Replace("ParamProduct", model.ProductName);
                //leyend = model.ProductName != "CO2" ? leyend.Replace("ParamChangeLPCO2", _resource.GetString("SubsectionRPyLPOP").Value) : leyend.Replace("ParamChangeCO2", _resource.GetString("SubsectionRPyLPCO2").Value);
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
                        control =  (await _variablesFileReaderService.GetMaxMin(cv.Historical, max, min));
                   
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
                    using MemoryStream finalFile = new MemoryStream();
                    pdfDocumentProcessor.SaveDocument(finalFile);
                    return File(finalFile.ToArray(), "application/pdf", string.Format("{0}-OP.{1}", model.BatchDetails.Number.Replace("\n", "").Trim(), "pdf").Replace(" - ", "-"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de producción al exportar a PDF " + ex);
                return BadRequest();
            }

            return File(ms.ToArray(), "application/pdf", string.Format("{0}-OP.{1}", model.BatchDetails.Number.Replace("\n", "").Trim(), "pdf").Replace(" - ", "-"));
        }

        public async Task<IActionResult> GetPlants()
        {
            var response = await GetPlantsItemsAsync();

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        public async Task<IActionResult> GetProduts(string plantId)
        {
            var response = await GetProdutsItemsAsync(plantId);

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        public async Task<IActionResult> GetTanks(string plantId, string productId)
        {
            var response = await GetTanksItemsAsync(plantId, productId);

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        public async Task<JsonResult> GetHistoryStateDetail(int Id)
        {
            if (Id != 0)
            {
                ///historial estados
                var userDb = _userManager.Users;
                var historyStates = from states in await _historyStatesRepository.GetAsync(x => x.ProductionOrderId == Id
                                   && x.Type == HistoryStateType.OrdenProduccion.Value)
                                    select new HistoryStates
                                    {
                                        ProductionOrderId = states.ProductionOrderId,
                                        User = userDb.Where(x => x.Id == states.User).FirstOrDefault().NombreUsuario,
                                        Date = states.Date,
                                        State = states.State,
                                        Type = states.Type
                                    };

                return Json(new { data = historyStates });
            }
            else
            {
                return Json(new { Result = "Ok", Message = "Orden de producción sin asociar" });
            }

        }
        //AHF
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string path)
        {
            try
            {
                if (Path.GetExtension(path) != ".pdf")
                    return NotFound();
                string completePath = $"{this._config["BasePathQualityFiles"]}\\{path}";
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

        #region helper methods

        private async Task<JsonResult> SaveProductionEquipment(ProductionOrderViewModel model)
        {
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            List<MonitoringEquipment> monitoringEquipment = new List<MonitoringEquipment>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            if (model.StepSaved > 1)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }

            var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
            }

            var productionEquipment = _productionEquipmentRepository.GetAllAsync().Result.Where(x => x.ProductionOrderId == entity.Id).SingleOrDefault();
            var productionEquipmentClone = new ProductionEquipment();
            if (productionEquipment != null)
                productionEquipmentClone = (ProductionEquipment)productionEquipment.Clone();
            if (productionEquipment == null)
            {
                productionEquipment = new ProductionEquipment
                {
                    ProductionOrderId = entity.Id,
                    IsAvailable = model.ProductionEquipment.IsAvailable,
                    ReviewedBy = model.ProductionEquipment.ReviewedBy,
                    ReviewedDate = model.ProductionEquipment.ReviewedDate
                };

                await _productionEquipmentRepository.AddAsync(productionEquipment);
            }
            else
            {
                productionEquipment.IsAvailable = model.ProductionEquipment.IsAvailable;
                productionEquipment.ReviewedBy = model.ProductionEquipment.ReviewedBy;
                productionEquipment.ReviewedDate = model.ProductionEquipment.ReviewedDate;
                ///get current user
                productionEquipment.ProductionOrder = entity;
                productionEquipment.ProductionOrder.CreatedBy = userInfo;
                await _productionEquipmentRepository.UpdateAsync(productionEquipment, productionEquipmentClone);
            }

            var monitoringEquipmentEntityList = await _monitoringEquipmentRepository.GetAsync(x => x.ProductionOrderId == entity.Id);
            if (monitoringEquipmentEntityList.Any())
            {
                var ObjControl = from c in model.MonitoringEquipmentList
                                 join b in monitoringEquipmentEntityList on c.Description equals b.Description
                                 where c.Code == b.Code
                                 select new MonitoringEquipmentViewModel
                                 {
                                     Id = b.Id,
                                     Description = b.Description,
                                     Code = b.Code,
                                     Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                     ReviewedBy = c.ReviewedBy,
                                     ReviewedDate = c.ReviewedDate,
                                     IsCalibrated = c.IsCalibrated
                                 };

                foreach (var itemModel in model.MonitoringEquipmentList)
                {
                    var item = ObjControl.Where(x => x.Description == itemModel.Description
                                                    && x.Code == itemModel.Code)
                                        .FirstOrDefault();

                    if (item != null)
                    {
                        var info = await _monitoringEquipmentRepository.GetByIdAsync(item.Id);
                        info.Notes = string.IsNullOrEmpty(itemModel?.Notes) ? "NA" : itemModel?.Notes;
                        info.IsCalibrated = itemModel?.IsCalibrated;
                        info.ReviewedBy = itemModel?.ReviewedBy;
                        info.ReviewedDate = itemModel?.ReviewedDate;
                        ///get current user
                        info.ProductionOrder = entity;
                        info.ProductionOrder.CreatedBy = userInfo;
                        monitoringEquipment.Add(info);
                    }
                }
                var monitoringEquipmentOld = Clone(monitoringEquipmentEntityList);
                await _monitoringEquipmentRepository.UpdateAsync(monitoringEquipment, monitoringEquipmentOld);
                //notes
                foreach (var item in model.MonitoringEquipmentList)
                {
                    if (!string.IsNullOrEmpty(item.Notes) && item.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                            Source = "Tabla 2: Equipos de monitoreo",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
                await AddNotes(historyNotes);

            }
            else
            {
                List<MonitoringEquipment> monitoringEquipmentList = new List<MonitoringEquipment>();
                foreach (var item in model.MonitoringEquipmentList)
                {
                    monitoringEquipmentList.Add(new MonitoringEquipment
                    {
                        Code = item.Code,
                        Description = item.Description,
                        IsCalibrated = item.IsCalibrated,
                        ProductionOrderId = entity.Id,
                        Notes = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                        ReviewedBy = item.ReviewedBy,
                        ReviewedDate = item.ReviewedDate
                    });
                }

                await _monitoringEquipmentRepository.AddAsync(monitoringEquipmentList);

                //notes
                foreach (var item in monitoringEquipmentList)
                {
                    if (!string.IsNullOrEmpty(item.Notes) && item.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(item.Notes) ? "NA" : item.Notes,
                            Source = "Tabla 2: Equipos de monitoreo",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
                await AddNotes(historyNotes);
            }

            // activitie is completed
            if (!model.ProductionEquipment.IsAvailable.HasValue || !model.ProductionEquipment.IsAvailable.Value)
            {
                entity.StepSavedDescription = "Equipos de producción";
                entity.StepSaved = 0;

            }
            else if (model.MonitoringEquipmentList.Where(x => !x.IsCalibrated.HasValue || !x.IsCalibrated.Value).Any())
            {
                entity.StepSavedDescription = "Equipos de monitoreo";
                entity.StepSaved = 0;
            }
            else
            {
                entity.StepSavedDescription = "Despeje de Línea";
                entity.StepSaved = 1;

            }

            entity.DelegateUser = userInfo;
            await _productionOrderRepository.UpdateAsync(entity);
            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden de producción ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SavePipelineClearance(ProductionOrderViewModel model)
        {
            var userInfoDB = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (model.StepSaved > 2)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }

            var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ProductionOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
            }

            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());

            // Save historical pipeline clearance signature / notes
            var historyNotes = new List<HistoryNotes>();
            foreach (var item in model.PipelineClearanceHistory)
            {
                var pipelineClearanceEntity = await _pipelineClearanceRepository.GetByIdAsync(item.Id);
                var pipelineClearanceEntityClone = new PipelineClearance();
                pipelineClearanceEntityClone = (PipelineClearance)pipelineClearanceEntity.Clone();
                if (pipelineClearanceEntity != null)
                {
                    if (!string.IsNullOrEmpty(item.NotesSecond)
                        && item.NotesSecond != "NA"
                        && (pipelineClearanceEntity.ReviewedBySecond != item.ReviewedBySecond
                            || pipelineClearanceEntity.ReviewedDateSecond != item.ReviewedDateSecond
                            || pipelineClearanceEntity.NotesSecond != item.NotesSecond))
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(model.PipelineClearance.NotesSecond) ? "NA" : model.PipelineClearance.NotesSecond,
                            Source = "Tabla 3: Despeje de línea",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }

                    pipelineClearanceEntity.ReviewedBySecond = item.ReviewedBySecond;
                    pipelineClearanceEntity.ReviewedDateSecond = item.ReviewedDateSecond;
                    pipelineClearanceEntity.NotesSecond = string.IsNullOrEmpty(item.NotesSecond) ? "NA" : item.NotesSecond;
                    ///get current user 
                    pipelineClearanceEntity.ProductionOrder = entityClone;
                    pipelineClearanceEntity.ProductionOrder.CreatedBy = userInfo;
                    await _pipelineClearanceRepository.UpdateAsync(pipelineClearanceEntity, pipelineClearanceEntityClone);
                }
            }
            await AddNotes(historyNotes);

            if (model.PipelineClearance.InCompliance.HasValue && model.PipelineClearance.InCompliance.Value)
            {
                // Only save when in compliance, when not in compliance PipelineClearanceHistory is used
                var pipeline = (await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == entity.Id && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
                if (pipeline == null)
                {
                    pipeline = new PipelineClearance
                    {
                        ProductionOrderId = model.Id,
                        Bill = "NA",
                        Notes = "NA",
                        ReviewedBy = model.PipelineClearance.ReviewedBy,
                        ReviewedDate = model.PipelineClearance.ReviewedDate,
                        InCompliance = model.PipelineClearance.InCompliance,
                        Activitie = "Se lleva a cabo el despeje de línea de acuerdo a los puntos anteriores " + model.SelectedProductFilter + " de la planta " + model.SelectedPlantFilter,
                        ProductionStartedDate = model.PipelineClearance.ProductionStartedDate,
                        ProductionEndDate = model.PipelineClearance.ProductionEndDate
                    };

                    await _pipelineClearanceRepository.AddAsync(pipeline);
                }
                else
                {
                    var pipelineClearanceEntityClone = (PipelineClearance)pipeline.Clone();

                    pipeline.Bill = "NA";
                    pipeline.Notes = "NA";
                    pipeline.ReviewedBy = model.PipelineClearance.ReviewedBy;
                    pipeline.ReviewedDate = model.PipelineClearance.ReviewedDate;
                    pipeline.InCompliance = model.PipelineClearance.InCompliance;
                    pipeline.Activitie = "Se lleva a cabo el despeje de línea de acuerdo a los puntos anteriores " + model.SelectedProductFilter + " de la planta " + model.SelectedPlantFilter;
                    pipeline.ProductionStartedDate = model.PipelineClearance.ProductionStartedDate;
                    pipeline.ProductionEndDate = model.PipelineClearance.ProductionEndDate;
                    //get current user
                    pipeline.ProductionOrder = entityClone;
                    pipeline.ProductionOrder.CreatedBy = userInfo;
                    await _pipelineClearanceRepository.UpdateAsync(pipeline, pipelineClearanceEntityClone);
                }
            }

            if (!model.PipelineClearance.InCompliance.HasValue || !model.PipelineClearance.InCompliance.Value)
            {
                entity.StepSavedDescription = "Despeje de Línea";
                entity.StepSaved = 1;
            }
            else
            {
                entity.StepSavedDescription = "Variables de control de proceso";
                entity.StepSaved = 2;
            }

            entity.InCompliance = model.PipelineClearance.InCompliance;
            entity.DelegateUser = userInfo;
            await _productionOrderRepository.UpdateAsync(entity);

            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden de producción ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveMonitoringProcess(ProductionOrderViewModel model)
        {
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            if (model.StepSaved >= 6)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }

            var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ProductionOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
            }

            List<ProductionOrderAttribute> productionOrderAttributesToAdd = new List<ProductionOrderAttribute>();
            List<ProductionOrderAttribute> productionOrderAttributes = new List<ProductionOrderAttribute>();
            var controlVariablesDb = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == model.Id
                                                                        && x.Type == ProductionOrderAttributeType.ControlVariable);
            var criticalParametersDb = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == model.Id
                                                                        && x.Type == ProductionOrderAttributeType.CriticalParameter);

            if (controlVariablesDb.Any())
            {
                var ObjControl = from c in model.ControlVariables
                                 join b in controlVariablesDb on c.Variable equals b.Variable
                                 select new ControlVariableViewModel
                                 {
                                     Id = b.Id,
                                     Area = b.Area,
                                     Variable = c.Variable,
                                     Specification = c.Specification,
                                     ChartPath = c.VariableCode, //MEG 20211206
                                     MaxValue = c.MaxValue,
                                     MinValue = c.MinValue,
                                     AvgValue = c.AvgValue,
                                     InCompliance = c.InCompliance,
                                     ProductionOrderId = model.Id,
                                     DeviationReport = new DeviationReportViewModel
                                     {
                                         Folio = string.IsNullOrEmpty(c.DeviationReport.Folio) ? "NA" : c.DeviationReport.Folio,
                                         Notes = string.IsNullOrEmpty(c.DeviationReport.Notes) ? "NA" : c.DeviationReport.Notes
                                     },
                                     Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                     ReviewedBy = c.ReviewedBy,
                                     ReviewedDate = c.ReviewedDate,
                                     Type = c.Type,
                                     Description = b.Description,

                                 };

                foreach (var itemModel in model.ControlVariables)
                {
                    var item = ObjControl.Where(x => x.Variable == itemModel.Variable
                                                    && x.Area == itemModel.Area
                                                    && x.Description == itemModel.Description)
                                        .FirstOrDefault();

                    if (item != null)
                    {
                        var info = await _productionOrderAttributeRepository.GetByIdAsync(item.Id);
                        info.Area = itemModel.Area;
                        // info.ProductionOrderId = itemModel.ProductionOrderId;
                        info.Description = itemModel.Description;
                        info.Variable = itemModel.Variable;
                        info.Specification = itemModel.Specification;
                        info.ChartPath = itemModel.ChartPath;
                        info.MaxValue = itemModel.MaxValue;
                        info.MinValue = itemModel.MinValue;
                        info.AvgValue = itemModel.AvgValue;
                        info.InCompliance = itemModel.InCompliance;
                        info.DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio;
                        info.DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes;
                        info.Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes;
                        info.ReviewedBy = itemModel.ReviewedBy;
                        info.ReviewedDate = itemModel.ReviewedDate;
                        info.Type = ProductionOrderAttributeType.ControlVariable;
                        ///get current user
                        info.ProductionOrder = entityClone;
                        info.ProductionOrder.CreatedBy = userInfo;
                        productionOrderAttributes.Add(info);
                    }
                    else
                    {
                        productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                        {
                            ProductionOrderId = model.Id,
                            Area = itemModel.Area,
                            Description = itemModel.Description,
                            Variable = itemModel.Variable,
                            Specification = itemModel.Specification,
                            ChartPath = itemModel.ChartPath, //MEG 20211206
                            MaxValue = itemModel.MaxValue,
                            MinValue = itemModel.MinValue,
                            AvgValue = itemModel.AvgValue,
                            InCompliance = itemModel.InCompliance,
                            DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                            DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                            Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            ReviewedBy = itemModel.ReviewedBy,
                            ReviewedDate = itemModel.ReviewedDate,
                            Type = ProductionOrderAttributeType.ControlVariable
                        });
                    }

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 5: Variables de control en proceso",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                foreach (var itemModel in model.ControlVariables)
                {
                    productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                    {
                        ProductionOrderId = model.Id,
                        Area = itemModel.Area,
                        Description = itemModel.Description,
                        Variable = itemModel.Variable,
                        Specification = itemModel.Specification,
                        ChartPath = itemModel.ChartPath, //MEG 20211206
                        MaxValue = itemModel.MaxValue,
                        MinValue = itemModel.MinValue,
                        AvgValue = itemModel.AvgValue,
                        InCompliance = itemModel.InCompliance,
                        DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                        DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                        Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                        ReviewedBy = itemModel.ReviewedBy,
                        ReviewedDate = itemModel.ReviewedDate,
                        Type = ProductionOrderAttributeType.ControlVariable
                    });

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 5: Variables de control en proceso",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }

            if (criticalParametersDb.Any())
            {
                var ObjCritical = from c in model.CriticalParameters
                                  join b in criticalParametersDb on c.Parameter equals b.Variable
                                  select new CriticalParameterViewModel
                                  {
                                      Id = b.Id,
                                      Area = b.Area,
                                      Parameter = c.Parameter,
                                      Specification = c.Specification,
                                      ChartPath = c.ChartPath, //MEG 20211206
                                      MaxValue = c.MaxValue,
                                      MinValue = c.MinValue,
                                      AvgValue = c.AvgValue,
                                      InCompliance = c.InCompliance,
                                      ProductionOrderId = model.Id,
                                      DeviationReport = new DeviationReportViewModel
                                      {
                                          Folio = string.IsNullOrEmpty(c.DeviationReport.Folio) ? "NA" : c.DeviationReport.Folio,
                                          Notes = string.IsNullOrEmpty(c.DeviationReport.Notes) ? "NA" : c.DeviationReport.Notes
                                      },
                                      Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                      ReviewedBy = c.ReviewedBy,
                                      ReviewedDate = c.ReviewedDate,
                                      Type = c.Type,
                                      Description = b.Description,
                                  };

                foreach (var itemModel in model.CriticalParameters)
                {
                    var item = ObjCritical.Where(x => x.Parameter == itemModel.Parameter
                                                    && x.Area == itemModel.Area
                                                    && x.Description == itemModel.Description)
                                        .FirstOrDefault();

                    if (item != null)
                    {
                        var info = await _productionOrderAttributeRepository.GetByIdAsync(item.Id);
                        info.Area = itemModel.Area;
                        // info.ProductionOrderId = itemModel.ProductionOrderId;
                        info.Description = itemModel.Description;
                        info.Variable = itemModel.Parameter;
                        info.Specification = itemModel.Specification;
                        info.ChartPath = itemModel.ChartPath; //MEG 20211206
                        info.MaxValue = itemModel.MaxValue;
                        info.MinValue = itemModel.MinValue;
                        info.AvgValue = itemModel.AvgValue;
                        info.InCompliance = itemModel.InCompliance;
                        info.DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio;
                        info.DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes;
                        info.Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes;
                        info.ReviewedBy = itemModel.ReviewedBy;
                        info.ReviewedDate = itemModel.ReviewedDate;
                        info.Type = ProductionOrderAttributeType.CriticalParameter;
                        ///get current user
                        info.ProductionOrder = entityClone;
                        info.ProductionOrder.CreatedBy = userInfo;
                        productionOrderAttributes.Add(info);
                    }
                    else
                    {
                        productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                        {
                            ProductionOrderId = model.Id,
                            Area = itemModel.Area,
                            Description = itemModel.Description,
                            Variable = itemModel.Parameter,
                            Specification = itemModel.Specification,
                            ChartPath = itemModel.ChartPath, //MEG 20211206
                            MaxValue = itemModel.MaxValue,
                            MinValue = itemModel.MinValue,
                            AvgValue = itemModel.AvgValue,
                            InCompliance = itemModel.InCompliance,
                            DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                            DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                            Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            ReviewedBy = itemModel.ReviewedBy,
                            ReviewedDate = itemModel.ReviewedDate,
                            Type = ProductionOrderAttributeType.CriticalParameter
                        });
                    }

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 6: Parámetros críticos de proceso",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                foreach (var itemModel in model.CriticalParameters)
                {
                    productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                    {
                        ProductionOrderId = model.Id,
                        Area = itemModel.Area,
                        Description = itemModel.Description,
                        Variable = itemModel.Parameter,
                        Specification = itemModel.Specification,
                        ChartPath = itemModel.ChartPath, //MEG 20211206
                        MaxValue = itemModel.MaxValue,
                        MinValue = itemModel.MinValue,
                        AvgValue = itemModel.AvgValue,
                        InCompliance = itemModel.InCompliance,
                        DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                        DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                        Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                        ReviewedBy = itemModel.ReviewedBy,
                        ReviewedDate = itemModel.ReviewedDate,
                        Type = ProductionOrderAttributeType.CriticalParameter
                    });

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 6: Parámetros críticos de proceso",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }

            if (productionOrderAttributes.Any())
            {
                var union = controlVariablesDb.Union(criticalParametersDb);
                var productionOrderAttOld = Clone(union);
                await _productionOrderAttributeRepository.UpdateAsync(productionOrderAttributes, productionOrderAttOld);
            }

            if (productionOrderAttributesToAdd.Any())
            {
                await _productionOrderAttributeRepository.AddAsync(productionOrderAttributesToAdd);
            }

            //notes
            if (historyNotes.Any())
            {
                await AddNotes(historyNotes);
            }

            ///activitie to be completed
            if (model.ControlVariables.Where(x => !x.InCompliance.HasValue).Any())
            {
                entity.StepSavedDescription = "Variables de control de proceso";
                entity.StepSaved = 3;
            }
            else if (model.CriticalParameters.Where(x => !x.InCompliance.HasValue).Any())
            {
                entity.StepSavedDescription = "Parámetros críticos de proceso";
                entity.StepSaved = 3;
            }
            else
            {
                entity.StepSavedDescription = "Atributos críticos de calidad";
                entity.StepSaved = 3;
            }

            entity.DelegateUser = userInfo;
            await _productionOrderRepository.UpdateAsync(entity);
            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden de producción ha sido guardada con éxito" });
        }

        private async Task<JsonResult> SaveQualityProcess(ProductionOrderViewModel model)
        {
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            if (model.StepSaved >= 6)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }

            var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ProductionOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
            }

            List<ProductionOrderAttribute> productionOrderAttributesToAdd = new List<ProductionOrderAttribute>();
            List<ProductionOrderAttribute> productionOrderAttributes = new List<ProductionOrderAttribute>();
            //foreach CriticalQualityAttributes si no cumple con alguno se seta entity.StepSavedDescription
            var QualityEntity = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == model.Id && x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
            if (QualityEntity.Any())
            {
                var ObjQA = from c in model.CriticalQualityAttributes
                            join b in QualityEntity on c.Attribute equals b.Variable
                            select new CriticalQualityAttributeViewModel
                            {
                                Id = b.Id,
                                Area = b.Area,
                                ChartPath = b.ChartPath, //MEG
                                MaxValue = c.MaxValue,
                                MinValue = c.MinValue,
                                AvgValue = c.AvgValue,
                                InCompliance = c.InCompliance,
                                DeviationReport = new DeviationReportViewModel
                                {
                                    Folio = string.IsNullOrEmpty(c.DeviationReport.Folio) ? "NA" : c.DeviationReport.Folio,
                                    Notes = string.IsNullOrEmpty(c.DeviationReport.Notes) ? "NA" : c.DeviationReport.Notes
                                },
                                Notes = string.IsNullOrEmpty(c.Notes) ? "NA" : c.Notes,
                                ReviewedBy = c.ReviewedBy,
                                ReviewedDate = c.ReviewedDate,
                                Type = c.Type,
                                Description = b.Description,
                                Attribute = c.Attribute,
                                Specification = c.Specification,
                                FileValue = c.FileValue //MEG
                            };
                foreach (var itemModel in ObjQA)
                {
                    var item = ObjQA.Where(x => x.Attribute == itemModel.Attribute
                                                    && x.Area == itemModel.Area
                                                    && x.Description == itemModel.Description)
                                        .FirstOrDefault();

                    //MEG add Quality File
                    String filePath = string.Empty;
                    if (!String.IsNullOrEmpty(itemModel.FileValue))
                    {
                        var idFile = $"{DateTime.Now.ToFileTime()}"; // AHF
                        filePath = await SaveQualityFile(idFile, itemModel.FileValue); // AHF
                    }

                    if (item != null)
                    {
                        var info = await _productionOrderAttributeRepository.GetByIdAsync(item.Id);
                        info.Area = itemModel.Area;
                        // info.ProductionOrderId = itemModel.ProductionOrderId;
                        info.Description = itemModel.Description;
                        info.Variable = itemModel.Attribute;
                        info.Specification = itemModel.Specification;
                        info.ChartPath = filePath != String.Empty ? filePath : itemModel.ChartPath; //MEG
                        info.MaxValue = itemModel.MaxValue;
                        info.MinValue = itemModel.MinValue;
                        info.AvgValue = itemModel.AvgValue;
                        info.InCompliance = itemModel.InCompliance;
                        info.DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio;
                        info.DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes;
                        info.Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes;
                        info.ReviewedBy = itemModel.ReviewedBy;
                        info.ReviewedDate = itemModel.ReviewedDate;
                        info.Type = ProductionOrderAttributeType.CriticalQualityAttribute;
                        ///get current user
                        info.ProductionOrder = entityClone;
                        info.ProductionOrder.CreatedBy = userInfo;
                        productionOrderAttributes.Add(info);
                    }
                    else
                    {
                        productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                        {
                            ProductionOrderId = model.Id,
                            Area = itemModel.Area,
                            Description = itemModel.Description,
                            Variable = itemModel.Attribute,
                            Specification = itemModel.Specification,
                            ChartPath = filePath != String.Empty ? filePath : itemModel.ChartPath, //MEG
                            MaxValue = itemModel.MaxValue,
                            MinValue = itemModel.MinValue,
                            AvgValue = itemModel.AvgValue,
                            InCompliance = itemModel.InCompliance,
                            DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                            DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                            Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            ReviewedBy = itemModel.ReviewedBy,
                            ReviewedDate = itemModel.ReviewedDate,
                            Type = ProductionOrderAttributeType.CriticalQualityAttribute

                        });
                    }

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 7: Atributos críticos de calidad",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                foreach (var itemModel in model.CriticalQualityAttributes)
                {
                    //MEG add Quality File
                    String filePath = string.Empty;
                    if (!String.IsNullOrEmpty(itemModel.FileValue))
                    {
                        var idFile = $"{DateTime.Now.ToFileTime()}"; // AHF
                        filePath = await SaveQualityFile(idFile, itemModel.FileValue);
                    }

                    productionOrderAttributesToAdd.Add(new ProductionOrderAttribute
                    {
                        ProductionOrderId = model.Id,
                        Area = itemModel.Area,
                        Description = itemModel.Description,
                        Variable = itemModel.Attribute,
                        Specification = itemModel.Specification,
                        ChartPath = filePath != String.Empty ? filePath : itemModel.ChartPath, //MEG
                        MaxValue = itemModel.MaxValue,
                        MinValue = itemModel.MinValue,
                        AvgValue = itemModel.AvgValue,
                        InCompliance = itemModel.InCompliance,
                        DeviationReportFolio = string.IsNullOrEmpty(itemModel.DeviationReport?.Folio) ? "NA" : itemModel.DeviationReport.Folio,
                        DeviationReportNotes = string.IsNullOrEmpty(itemModel.DeviationReport?.Notes) ? "NA" : itemModel.DeviationReport.Notes,
                        Notes = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                        ReviewedBy = itemModel.ReviewedBy,
                        ReviewedDate = itemModel.ReviewedDate,
                        Type = ProductionOrderAttributeType.CriticalQualityAttribute
                    });

                    if (!string.IsNullOrEmpty(itemModel.Notes) && itemModel.Notes != "NA")
                    {
                        historyNotes.Add(new HistoryNotes
                        {
                            ProductionOrderId = model.Id,
                            Note = string.IsNullOrEmpty(itemModel.Notes) ? "NA" : itemModel.Notes,
                            Source = "Tabla 7: Atributos críticos de calidad",
                            User = userInfo,
                            Date = DateTime.Now
                        });
                    }
                }
            }

            if (productionOrderAttributes.Any())
            {
                var union = QualityEntity;
                var productionOrderAttOld = Clone(union);
                await _productionOrderAttributeRepository.UpdateAsync(productionOrderAttributes, productionOrderAttOld);
            }

            if (productionOrderAttributesToAdd.Any())
            {
                await _productionOrderAttributeRepository.AddAsync(productionOrderAttributesToAdd);
            }

            await AddNotes(historyNotes);

            ///activitie to be completed
            if (model.CriticalQualityAttributes.Where(x => !x.InCompliance.HasValue).Any())
            {
                entity.StepSavedDescription = "Atributos críticos de calidad";
                entity.StepSaved = 3;
            }
            else
            {
                entity.StepSavedDescription = "Lotificación y análisis del producto";
                entity.StepSaved = 4;
            }

            entity.DelegateUser = userInfo;
            await _productionOrderRepository.UpdateAsync(entity);
            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = " La orden de producción ha sido guardada con éxito" });
        }

        private async Task<string> SaveQualityFile(string idFile, string fileValue) // AHF
        {
            //MEG Save File
            String basePath = _config["BasePathQualityFiles"];
            if (!String.IsNullOrEmpty(basePath))
            {
                if (!Directory.Exists(basePath))    // AHF
                    Directory.CreateDirectory(basePath);    // AHF
                var type = fileValue.Split(";")[0].Split("data:")[1];
                var data = fileValue.Split(",")[1];
                idFile += $".{type.Substring(type.IndexOf("/") + 1)}";  //AHF
                var fileName = Path.Combine(basePath, idFile);
                _logger.LogDebug("Saving file " + fileName);    //AHF
                byte[] sPDFDecoded = Convert.FromBase64String(data);
                await System.IO.File.WriteAllBytesAsync(fileName, sPDFDecoded); // AHF
                _logger.LogDebug("file saved " + fileName);
                return idFile;
            }
            else
            {
                _logger.LogError("Error en la configuración de la ruta para almacenamiento de archivos");
            }
            return null;
        }

        private async Task<JsonResult> SaveBatchDetails(ProductionOrderViewModel model)
        {
            List<HistoryNotes> historyNotes = new List<HistoryNotes>();
            HistoryStates historyStates = new HistoryStates();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            var userPlant = await _userManager.FindByIdAsync(userInfo);
            if (model.StepSaved > 5)
            {
                return Json(new { Result = "BadRequest", Message = "Operación inválida" });
            }

            var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            var entityClone = (ProductionOrder)entity.Clone();
            if (entity == null)
            {
                return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
            }

            ProductionOrderViewModel batch = new ProductionOrderViewModel();
            batch.BatchDetails = new BatchDetailsViewModel();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<BatchAnalysis>>(model.BatchDetails.Analysis);
            var batchDetailsDb = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();

            if (batchDetailsDb != null)
            {
                var ObjDb = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == entity.Id)).FirstOrDefault();

                batchDetailsDb.NotInComplianceFolio = string.IsNullOrEmpty(model.BatchDetails?.NotInComplianceFolio) ? "NA" : model.BatchDetails.NotInComplianceFolio;
                batchDetailsDb.NotInComplianceNotes = string.IsNullOrEmpty(model.BatchDetails?.NotInComplianceNotes) ? "NA" : model.BatchDetails.NotInComplianceNotes;

                batchDetailsDb.IsReleased = model.BatchDetails.IsReleased;
                batchDetailsDb.ReleasedBy = model.BatchDetails.ReleasedBy;
                batchDetailsDb.ReleasedDate = model.BatchDetails.ReleasedDate;
                batchDetailsDb.ReleasedNotes = string.IsNullOrEmpty(model.BatchDetails?.ReleasedNotes) ? "NA" : model.BatchDetails.ReleasedNotes;
                batchDetailsDb.Number = model.BatchDetails.Number.Trim();
                batchDetailsDb.Size = model.BatchDetails.Size;
                batchDetailsDb.Level = model.BatchDetails.Level;
                batchDetailsDb.AnalyzedBy = model.BatchDetails.AnalyzedBy;
                batchDetailsDb.AnalyzedDate = model.BatchDetails.AnalyzedDate;
                batchDetailsDb.BatchAnalysis = model.BatchDetails.Analysis.Select(x => new BatchAnalysis
                {
                    ParameterName = x.ParameterName,
                    Value = x.Value.ToString(),
                    LowerLimit = x.LowerLimit.ToString(),
                    UpperLimit = x.UpperLimit.ToString(),
                    MeasureUnit = x.MeasureUnit
                }).ToList();
                ///get current user
                batchDetailsDb.ProductionOrder = entityClone;
                batchDetailsDb.ProductionOrder.CreatedBy = userInfo;
                var entityCloneBatch = new BatchDetails();
                ObjDb.BatchAnalysis = _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetailsDb.Id).Result.ToList();
                entityCloneBatch = (BatchDetails)ObjDb.Clone();
                await _batchDetailsRepository.UpdateAsync(batchDetailsDb, entityCloneBatch);

                /// delete analysis
                var ObjDBAnalysis = await _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetailsDb.Id);
                if (ObjDBAnalysis.Count > 0)
                {
                    await _batchAnalysisRepository.DeleteAsync(ObjDBAnalysis);
                }

                foreach (var item in mapped)
                {
                    item.BatchDetailsId = batchDetailsDb.Id;
                }
                var itemsToAdd = mapped.Select(x => new BatchAnalysis
                {
                    BatchDetailsId = batchDetailsDb.Id,
                    LowerLimit = x.LowerLimit,
                    MeasureUnit = x.MeasureUnit,
                    ParameterName = x.ParameterName,
                    UpperLimit = x.UpperLimit,
                    Value = x.Value,
                }).ToList();
                await _batchAnalysisRepository.AddAsync(itemsToAdd);

                //notes
                if (!string.IsNullOrEmpty(model.BatchDetails.ReleasedNotes) && model.BatchDetails.ReleasedNotes != "NA")
                {
                    historyNotes.Add(new HistoryNotes
                    {
                        ProductionOrderId = model.Id,
                        Note = string.IsNullOrEmpty(model.BatchDetails?.ReleasedNotes) ? "NA" : model.BatchDetails.ReleasedNotes,
                        Source = "Tabla 8: Lotificación y análisis de producto",
                        User = userInfo,
                        Date = DateTime.Now
                    });
                }

                await AddNotes(historyNotes);
            }
            else
            {
                var entityObj = new BatchDetails();

                entityObj = BatchDetails.Create(
                0,
                model.Id,
                model.BatchDetails.Number,
                model.BatchDetails.Level,
                model.BatchDetails.Size,
                model.BatchDetails.Tank,
                null,
                model.BatchDetails.AnalyzedBy,
                model.BatchDetails.AnalyzedDate,
                model.BatchDetails.InCompliance,
                string.IsNullOrEmpty(model.BatchDetails?.NotInComplianceFolio) ? "NA" : model.BatchDetails.NotInComplianceFolio,
                string.IsNullOrEmpty(model.BatchDetails?.NotInComplianceNotes) ? "NA" : model.BatchDetails.NotInComplianceNotes,
                model.BatchDetails.IsReleased,
                model.BatchDetails.ReleasedBy,
                model.BatchDetails.ReleasedDate,
                string.IsNullOrEmpty(model.BatchDetails?.ReleasedNotes) ? "NA" : model.BatchDetails.ReleasedNotes,
                mapped.ToList());

                var AddBatch = _batchDetailsRepository.AddAsync(entityObj);
                //notes
                if (!string.IsNullOrEmpty(model.BatchDetails.ReleasedNotes) && model.BatchDetails.ReleasedNotes != "NA")
                {
                    historyNotes.Add(new HistoryNotes
                    {
                        ProductionOrderId = model.Id,
                        Note = string.IsNullOrEmpty(model.BatchDetails?.ReleasedNotes) ? "NA" : model.BatchDetails.ReleasedNotes,
                        Source = "Tabla 8: Lotificación y análisis de producto",
                        User = userInfo,
                        Date = DateTime.Now
                    });
                }
                await AddNotes(historyNotes);
            }
            //foreach BatchDetails si no cumple con alguno se seta entity.StepSavedDescription
            if (!model.BatchDetails.InCompliance.HasValue || !model.BatchDetails.InCompliance.Value)
            {
                entity.StepSavedDescription = "Lotificación y análisis del producto";
                entity.StepSaved = 4;
            }
            else
            {
                entity.StepSavedDescription = "Liberación del producto";
                entity.StepSaved = 5;
            }

            entity.DelegateUser = userInfo;
            var State = entity.State;
            if (State != ProductionOrderStatus.Denied.Value)
            {
                entity.State = ProductionOrderStatus.ToBeReleased.Value;
                await _productionOrderRepository.UpdateAsync(entity);
            }

            var forRelesed = await _productionOrderService.ForReleasedProductionOrder(model.Id);
            if (!forRelesed)
            {
                historyStates.ProductionOrderId = model.Id;
                historyStates.State = ProductionOrderStatus.ToBeReleased.Value;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo;
                historyStates.Type = HistoryStateType.OrdenProduccion.Value;
                await _historyStatesRepository.AddAsync(historyStates);
            }
            return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = "La Orden de Producción a Sido Guardada con Exito", Status = entity.State });
        }

        private async Task<JsonResult> SaveRelease(ProductionOrderViewModel model)
        {
            try
            {
                List<HistoryNotes> historyNotes = new List<HistoryNotes>();
                HistoryStates historyStates = new HistoryStates();
                ProductionOrderViewModel batch = new ProductionOrderViewModel();

                var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
                if (model.StepSaved > 6)
                {
                    return Json(new { Result = "BadRequest", Message = "Operación inválida" });
                }

                var entity = await _productionOrderRepository.GetByIdAsync(model.Id);
                var entityClone = new ProductionOrder();
                entityClone = (ProductionOrder)entity.Clone();
                if (entity == null)
                {
                    return Json(new { Result = "NotFound", Message = "Orden de producción no existe" });
                }
                if (model.IsReleased.HasValue && model.IsReleased.Value)
                {
                    entity.State = ProductionOrderStatus.Released.Value;
                }
                else if (model.IsReleased.HasValue && !model.IsReleased.Value)
                {
                    entity.State = ProductionOrderStatus.Denied.Value;
                }

                entity.ReleasedNotes = string.IsNullOrEmpty(model.ReleasedNotes) ? "NA" : model.ReleasedNotes;
                entity.ReleasedBy = model.ReleasedBy;
                entity.ReleasedDate = model.ReleasedDate;
                entity.IsReleased = model.IsReleased;

                if (!model.BatchDetails.IsReleased.HasValue || !model.BatchDetails.IsReleased.Value)
                {
                    entity.StepSavedDescription = "Liberación del producto";
                    entity.StepSaved = 5;
                }
                else
                {
                    entity.StepSavedDescription = "Producto liberado";
                    entity.StepSaved = 6;
                }

                entity.DelegateUser = userInfo;
                entity.EndDate = DateTime.Now;
                await _productionOrderRepository.UpdateAsync(entity, entityClone, model.BatchDetails.Number.Trim());
                //notes
                if (!string.IsNullOrEmpty(model.ReleasedNotes) && model.ReleasedNotes != "NA")
                {
                    historyNotes.Add(new HistoryNotes
                    {
                        ProductionOrderId = model.Id,
                        Note = string.IsNullOrEmpty(model.ReleasedNotes) ? "NA" : model.ReleasedNotes,
                        Source = "Dictamen",
                        User = userInfo,
                        Date =
                        DateTime.Now
                    });
                }

                await AddNotes(historyNotes);

                if (model.IsReleased == true)
                {
                    var productionOrderEntity = await _productionOrderRepository.GetByIdAsync(model.Id);
                    var userId = userInfo;
                    var catproductos = (await _productRepository.GetAsync(x => x.ProductId == productionOrderEntity.ProductId)).FirstOrDefault();
                    ConditioningOrder co = new ConditioningOrder();
                    var footer = await _layoutCertificateService.GetLastFooter(model.Plant);
                    var certificate = await _layoutCertificateService.GetLastCertificate();
                    var conditioningOrderEntity = ConditioningOrder.Create
                        (0,
                        productionOrderEntity.Id,
                        userId, catproductos.Presentation,
                        productionOrderEntity.PlantId,
                        productionOrderEntity.ProductId,
                        certificate, 
                        footer);

                    await _conditioningOrderRepository.AddAsync(conditioningOrderEntity);
                    var idoa = conditioningOrderEntity.Id;
                    //AHF Agrega estatus en proceso y creada OA
                    var stateOA = new List<HistoryStates>();
                    stateOA.Add(new HistoryStates()
                    {
                        ProductionOrderId = idoa,
                        State = Models.ConditioningOrderViewModels.ConditioningOrderStatus.Created.Value,
                        Date = conditioningOrderEntity.CreatedDate,
                        User = userInfo,
                        Type = HistoryStateType.OrdenAcondicionamiento.Value
                    }); ;
                    stateOA.Add(new HistoryStates()
                    {
                        ProductionOrderId = idoa,
                        State = Models.ConditioningOrderViewModels.ConditioningOrderStatus.InProgress.Value,
                        Date = conditioningOrderEntity.CreatedDate,
                        User = userInfo,
                        Type = HistoryStateType.OrdenAcondicionamiento.Value
                    });
                    await _historyStatesRepository.AddAsync(stateOA);
                }
                //states
                historyStates.ProductionOrderId = model.Id;
                historyStates.State = entity.State;
                historyStates.Date = DateTime.Now;
                historyStates.User = userInfo;
                historyStates.Type = HistoryStateType.OrdenProduccion.Value;
                await _historyStatesRepository.AddAsync(historyStates);
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                if (model.IsReleased.HasValue && model.IsReleased.Value && enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetails.Number?.Replace("\n", "").Trim() });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.Id) });
                    await this._notification.SendNotification(paramters, model.Plant, "USUARIO DE PRODUCCION", this._config["EmailSubjects:ReleasedPath"], this._config["Emailtemplates:ReleasedPath"]);
                }
                else if (model.IsReleased.HasValue && !model.IsReleased.Value && enabledNotificationResult && enabledNotification)
                {
                    var paramters = new List<Parameter>();
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetails.Number?.Replace("\n", "").Trim() });
                    paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.Id) });
                    await this._notification.SendNotification(paramters, model.Plant, "USUARIO DE PRODUCCION", this._config["EmailSubjects:RejectOPPath"], this._config["Emailtemplates:RejectOPPath"]);
                }
                return Json(new { Result = "Ok", NextStep = entity.StepSaved, Message = "La Orden de Producción a Sido Guardada con Exito", Status = entity.State });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al liberar la orden, favor de intentar más tarde." });
            }
        }

        private async Task<List<VwLotesProduccionDetalle>> ObtenerLotesDetalle(string PlantaID, string ProductoID, string TanqueID, string Producto)
        {
            List<VwLotesProduccionDetalle> vwLotes = new List<VwLotesProduccionDetalle>();
            try
            {
                vwLotes = (await _productionOrderService.GetLot(PlantaID, ProductoID, TanqueID, Producto)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en obtener Numero de lote " + ex);

            }
            return vwLotes;

        }
        [HttpPost]
        [Authorize(SecurityConstants.CREAR_OP)]
        public async Task<IActionResult> SaveInitOP([FromForm] ProductionOrderViewModel model)
        {
            if (model.SelectedPlantFilter == null || model.SelectedProductFilter == null || model.SelectedTankFilter == null || model.SelectedPurityFilter == null)
            {
                return Json(new { Result = "fail", Message = "Selecciona Planta, Producto, Tanque y pureza" });
            }

            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var entity = new ProductionOrder();
            if (model.Id > 0)
            {
                entity = await _productionOrderRepository.GetByIdAsync(model.Id);
            }
            else
            {
                string percent = model.SelectedPurityFilter;
                percent = model.SelectedPurityFilter?.Replace("%", "");
                entity = ProductionOrder.Create(
                    0,
                    model.SelectedPlantFilter,
                    model.SelectedProductFilter,
                    model.SelectedTankFilter,
                    percent,
                    userInfo.Id,
                    "Equipos de producción",
                    false,
                    null
                );


                await _productionOrderRepository.AddAsync(entity);
            }
            model.Id = entity.Id;

            var historyStates = new List<HistoryStates>();
            historyStates.Add(new HistoryStates()
            {
                ProductionOrderId = model.Id,
                State = ProductionOrderStatus.Created.Value,
                Date = DateTime.Now,
                User = userInfo.Id,
                Type = HistoryStateType.OrdenProduccion.Value
            });
            historyStates.Add(new HistoryStates()
            {
                ProductionOrderId = model.Id,
                State = ProductionOrderStatus.InProgress.Value,
                Date = DateTime.Now,
                User = userInfo.Id,
                Type = HistoryStateType.OrdenProduccion.Value
            });
            await _historyStatesRepository.AddAsync(historyStates);

            return Json(new
            {
                Result = "Ok",
                Message = model.Id
            });
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
                            Type = HistoryNotesType.OrdenProduccion.Value
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

        #endregion

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
            if (!plantsByUser.Contains(plantId))
            {
                return new List<SelectListItem>();
            }

            //gets products
            var products = await _principalService.GetProducts();
            var productsFiltered = (await _generalCatalogRepository.GetAsync(x => x.PlantId == plantId)).Where(x=>x.Estatus).Select(x => x.ProductId);

            var response = products.Where(x => productsFiltered.Contains(x.Value)).ToList();

            return response;
        }

        private async Task<List<SelectListItem>> GetTanksItemsAsync(string plantId, string productId)
        {
            var name = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userInfo = await _userManager.FindByNameAsync(name);

            // Verify user has access to the plant requested
            var plantsByUser = userInfo != null ? userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",") : null;
            if (!plantsByUser.Contains(plantId))
            {
                return new List<SelectListItem>();
            }

            //gets tanks
            var tanks = await _principalService.GetTanks(productId, plantsByUser);
            var tanksFiltered = (await _generalCatalogRepository.GetAsync(x => x.PlantId == plantId && x.ProductId == productId)).Select(x => x.TankId);

            var response = tanks.Where(x => tanksFiltered.Contains(x.Value)).ToList();

            return response;
        }

        List<T> Clone<T>(IEnumerable<T> oldList)
        {
            List<T> newList = new List<T>();
            return newList = new List<T>(oldList);
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
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> EnviarCorreoConformidad(PipelineClearanceViewModel model)
        {
            try
            {
                var enabledNotificationValue = this._config["enabledNotification"];
                var enabledNotificationResult = bool.TryParse(enabledNotificationValue, out var enabledNotification);
                var paramters = new List<Parameter>();
                if (enabledNotificationResult && enabledNotification)
                {
                    if (model.InCompliance.HasValue && model.InCompliance.Value)
                    {
                        paramters.Add(new Parameter() { Name = "{CaseTag:ProductionOrderId}", Value = model.ProductionOrderId.ToString() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetailsNumber?.Replace("\n", "").Trim() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.ProductionOrderId) });
                        await this._notification.SendNotification(paramters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:ConformityPath"], this._config["Emailtemplates:ConformityPath"]);
                    }
                    else if (model.InCompliance.HasValue && !model.InCompliance.Value)
                    {
                        paramters.Add(new Parameter() { Name = "{CaseTag:ProductionOrderId}", Value = model.ProductionOrderId.ToString() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramLote}", Value = model.BatchDetailsNumber?.Replace("\n", "").Trim() });
                        paramters.Add(new Parameter() { Name = "{CaseTag:paramPathLink}", Value = this._notification.getUriOp(model.ProductionOrderId) });
                        await this._notification.SendNotification(paramters, model.PlantId, "RESPONSABLE SANITARIO", this._config["EmailSubjects:nonConformityPath"], this._config["Emailtemplates:nonConformityPath"]);
                        await this._notification.SendNotification(paramters, model.PlantId, "SUPERINTENDENTE DE PLANTA", this._config["EmailSubjects:NonConformitySPPath"], this._config["Emailtemplates:NonConformitySPPath"]);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al enviar la notificación." });
            }
            return Json(new { Result = "Ok", Message = "Se envió la notificación de manera exitosa." });
        }
        private string getLinkOA(int id)
        {
            string link = string.Empty;
            link = string.Concat(
                                     Request.Scheme,
            "://",
                                     Request.Host.ToUriComponent(),
            Request.PathBase.ToUriComponent(), "/ConditioningOrder/New?IdOP=", id);
            return link;

        }

        [HttpPost]
        public async Task<IActionResult> GetHistorianEndPipelineClearance([FromForm] ProductionOrderViewModel model, int step)
        {
            try
            {
                model = await _variablesFileReaderService.StorageVariablesAsync(model);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
            return PartialView("_ControlVariablesCriticalParams", model);
        }

        [HttpPost]
        public async Task<PartialViewResult> GetHistorianStep4(ProductionOrderViewModel model)
        {
            try
            {
                model.CriticalQualityAttributes = await _variablesFileReaderService.FillCriticalQualityAttributes(model.Id, model.CriticalQualityAttributes);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
            return PartialView("_Step4", model);
        }
    }
}