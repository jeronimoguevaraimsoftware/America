using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Reports;
using LiberacionProductoWeb.Reports.ConditioningOrder;
using LiberacionProductoWeb.Reports.OrderProduction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LiberacionProductoWeb.Data.Repository.Base.External;
using DevExpress.XtraPrinting.Drawing;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;

namespace LiberacionProductoWeb.Services
{
    public class ExportPDFService : Controller, IExportPDFService
    {
        private readonly ILogger<ExportPDFService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVariablesFileReaderService _variablesFileReaderService;
        private readonly IConfiguration _config;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IPrincipalService _principalService;
        private readonly IProductionEquipmentRepository _productionEquipmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IMonitoringEquipmentRepository _monitoringEquipmentRepository;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IBatchAnalysisRepository _batchAnalysisRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IConditioningOrderService _conditioningOrderService;
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly ICheckListPipeCommentsAnswerReepository _checkListPipeCommentsAnswerReepository;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly ICheckListPipeAnswerRepository _checkListPipeAnswerRepository;
        private readonly IFormulaCatalogRepository _formulaRepository;
        private readonly IAnalisisClienteRepository _analisisClienteRepository;
        private readonly ILotesDistribuicionClienteRepository _lotesDistribuicionClienteRepository;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ILeyendsCertificateHistoryRepository _leyendsCertificateHistoryRepository;
        private readonly ILeyendsFooterCertificateHistoryRepository _leyendsFooterCertificateHistoryRepository;
        public ExportPDFService(ILogger<ExportPDFService> logger, UserManager<ApplicationUser> userManager,
            IVariablesFileReaderService variablesFileReaderService, IConfiguration config,
            IProductionOrderRepository productionOrderRepository,
            IPrincipalService principalService, IProductionEquipmentRepository productionEquipmentRepository,
            IHttpContextAccessor httpContextAccessor, IGeneralCatalogRepository generalCatalogRepository,
            IProductionOrderAttributeRepository productionOrderAttributeRepository, IMonitoringEquipmentRepository monitoringEquipmentRepository,
            IPipelineClearanceRepository pipelineClearanceRepository, IBatchAnalysisRepository batchAnalysisRepository,
            IBatchDetailsRepository batchDetailsRepository,
            IConditioningOrderService conditioningOrderService, ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository,
            ICheckListPipeCommentsAnswerReepository checkListPipeCommentsAnswerReepository,
            ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository,
            ICheckListPipeAnswerRepository checkListPipeAnswerRepository, IFormulaCatalogRepository formulaCatalogRepository,
            IAnalisisClienteRepository analisisClienteRepository, ILotesDistribuicionClienteRepository lotesDistribuicionClienteRepository, IStringLocalizer<Resource> resource,
            ILeyendsCertificateHistoryRepository leyendsCertificateHistoryRepository,
            ILeyendsFooterCertificateHistoryRepository leyendsFooterCertificateHistoryRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _variablesFileReaderService = variablesFileReaderService;
            _config = config;
            _productionOrderRepository = productionOrderRepository;
            _principalService = principalService;
            _httpContextAccessor = httpContextAccessor;
            _generalCatalogRepository = generalCatalogRepository;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _monitoringEquipmentRepository = monitoringEquipmentRepository;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _batchAnalysisRepository = batchAnalysisRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _productionEquipmentRepository = productionEquipmentRepository;
            _conditioningOrderService = conditioningOrderService;
            _checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            _checkListPipeCommentsAnswerReepository = checkListPipeCommentsAnswerReepository;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _checkListPipeAnswerRepository = checkListPipeAnswerRepository;
            _formulaRepository = formulaCatalogRepository;
            _analisisClienteRepository = analisisClienteRepository;
            _lotesDistribuicionClienteRepository = lotesDistribuicionClienteRepository;
            _resource = resource;
            _leyendsCertificateHistoryRepository = leyendsCertificateHistoryRepository;
            _leyendsFooterCertificateHistoryRepository = leyendsFooterCertificateHistoryRepository;
        }
        #region ProductionOrder
        public XtraReport GetReport(string json, string nombre, string valorMaximo, string valorMinimo, string rango, string lowlimit,string toplimit)
        {
            XtraReport rpt1 = new VariablesChart();


            XRChart lineChart = (XRChart)rpt1.Controls[2].FindControl("xrChart1", true);

            Series series1 = new Series("Series 1", ViewType.Line);
            series1.ArgumentScaleType = ScaleType.Qualitative;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;


            List<HistoricalToGrap> listHistorical = new List<HistoricalToGrap>();

            listHistorical = JsonConvert.DeserializeObject<List<HistoricalToGrap>>(json);

            listHistorical.ForEach(item =>
            {
                DateTime argument = DateTime.Parse(item.Period);
                series1.Points.Add(new SeriesPoint(argument, item.Value));
            });

            ((LineSeriesView)series1.View).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
            ((LineSeriesView)series1.View).LineStyle.DashStyle = DashStyle.Solid;
            ((LineSeriesView)series1.View).Color = Color.FromName("SlateBlue");
            //((XYDiagram)lineChart.Diagram).EnableAxisXZooming = true;

            lineChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            lineChart.Titles.Add(new ChartTitle());
            lineChart.Titles[0].Font = new Font("Tahoma", 10, FontStyle.Bold);
            lineChart.Titles[0].Text = nombre;
            XYDiagram diagram = (XYDiagram)lineChart.Diagram;
            float MaxValue;
            float MinValue;
            float.TryParse(valorMaximo.Replace(",","."),NumberStyles.Float, CultureInfo.InvariantCulture, out MaxValue);
            float.TryParse(valorMinimo.Replace(",", "."),NumberStyles.Float, CultureInfo.InvariantCulture, out MinValue);

            float topValue;
            float lowValue;
            float.TryParse(toplimit.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out topValue);
            float.TryParse(lowlimit.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out lowValue);
            if (MinValue > lowValue)
            {
                MaxValue = Math.Max(topValue, MaxValue);
                MinValue = Math.Min(lowValue, MinValue);
            }
            diagram.AxisY.WholeRange.SetMinMaxValues(MinValue, MaxValue);
            //if (MinValue != lowValue)
            //{
            //    MinValue = lowValue;
            //    MaxValue = topValue;
            //    diagram.AxisY.WholeRange.SetMinMaxValues(MaxValue, MinValue);
            //}
            //else
            //{
            //    diagram.AxisY.WholeRange.SetMinMaxValues(MinValue, MaxValue);
            //}
            //if (MinValue < MaxValue)
            //    diagram.AxisY.WholeRange.SetMinMaxValues(MinValue, MaxValue);
            //else
            //    diagram.AxisY.WholeRange.SetMinMaxValues(MaxValue, MinValue);
            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisX.Label.Angle = -45;

            diagram.AxisY.Title.Visible = true;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = rango;
            //diagram.AxisY.Title.TextColor = Color.Blue;
            diagram.AxisY.Title.Antialiasing = true;
            diagram.AxisY.Title.Font = new Font("Tahoma", 8, FontStyle.Bold);

            lineChart.Series.Add(series1);
            rpt1.Bands[2].Controls.Add(lineChart);
            return rpt1;
        }

        public async Task<ProductionOrderViewModel> GetGraphicReport(ProductionOrderViewModel model)
        {
            try
            {
                return await _variablesFileReaderService.FillVariablesAsync(model);
            }
            catch (Exception ex)
            {
                model.MensajeError = ex.Message;
                _logger.LogInformation("Ocurrio un error en orden en la lectura de variables " + ex);
            }
            return model;
        }

        public async Task<ProductionOrderViewModel> GetOrderProductionModelById(int id)
        {

            ProductionOrderViewModel model;

            var entity = await _productionOrderRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }
            else
            {
                model = new ProductionOrderViewModel
                {
                    Id = entity.Id,
                    StepSaved = entity.StepSaved,
                    IsReleased = entity.IsReleased,
                    ReleasedBy = entity.ReleasedBy,
                    ReleasedDate = entity.ReleasedDate,
                    ReleasedNotes = entity.ReleasedNotes,
                    CreatedDate = entity.CreatedDate,
                    Status = entity.State
                };

                //select filters
                model.SelectedPlantFilter = entity.PlantId;
                model.SelectedProductFilter = entity.ProductId;
                model.SelectedTankFilter = entity.TankId;
                model.SelectedPurityFilter = entity.Purity?.ToString();

                // fill filters
                var userInfo = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                var plants = await GetPlantsItemsAsync();
                var products = await GetProdutsItemsAsync(model.SelectedPlantFilter);
                var tanks = await GetTanksItemsAsync(model.SelectedPlantFilter, model.SelectedProductFilter);

                // This are the placeholders in text description
                model.Plant = plants.Where(p => p.Value == model.SelectedPlantFilter).FirstOrDefault()?.Text;
                model.Location = plants.Where(p => p.Value == model.SelectedPlantFilter).FirstOrDefault()?.Text;
                model.ProductName = products.Where(p => p.Value == model.SelectedProductFilter).FirstOrDefault()?.Text;
                model.ProductCode = products.Where(p => p.Value == model.SelectedProductFilter).FirstOrDefault()?.Value;

                var productionEquipment = _productionEquipmentRepository.GetAllAsync().Result.Where(x => x.ProductionOrderId == entity.Id).FirstOrDefault();
                if (productionEquipment != null)
                {
                    model.ProductionEquipment = new ProductionEquipmentViewModel
                    {
                        IsAvailable = productionEquipment.IsAvailable,
                        ReviewedBy = productionEquipment.ReviewedBy,
                        ReviewedDate = productionEquipment.ReviewedDate,
                    };
                }

                var monitoringEquipmentList = _monitoringEquipmentRepository.GetAllAsync().Result.Where(x => x.ProductionOrderId == entity.Id);
                if (monitoringEquipmentList != null)
                {
                    model.MonitoringEquipmentList = monitoringEquipmentList.Select(x =>
                        new MonitoringEquipmentViewModel
                        {
                            Code = x.Code,
                            Description = x.Description,
                            IsCalibrated = x.IsCalibrated,
                            Notes = x.Notes,
                            ReviewedBy = x.ReviewedBy,
                            ReviewedDate = x.ReviewedDate,
                        }
                    ).ToList();
                }

                var pipelineClearance = (await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId ==
                entity.Id && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
                if (pipelineClearance != null)
                {
                    model.PipelineClearance = new PipelineClearanceViewModel
                    {
                        InCompliance = pipelineClearance.InCompliance,
                        ReviewedBy = pipelineClearance.ReviewedBy,
                        ReviewedDate = pipelineClearance.ReviewedDate,
                        Notes = pipelineClearance.Notes,
                        Bill = pipelineClearance.Bill,
                        ReviewedBySecond = pipelineClearance.ReviewedBySecond,
                        ReviewedDateSecond = pipelineClearance.ReviewedDateSecond,
                        NotesSecond = pipelineClearance.NotesSecond,
                        ProductionStartedDate = pipelineClearance.ProductionStartedDate,
                        ProductionEndDate = pipelineClearance.ProductionEndDate
                    };
                }

                var pipelineClearanceHistory = (from pipe in await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId ==
                                                entity.Id && x.ReviewedBySecond != null)
                                                select new PipelineClearanceViewModel
                                                {
                                                    InCompliance = pipe.InCompliance,
                                                    ReviewedBy = pipe.ReviewedBy,
                                                    ReviewedDate = pipe.ReviewedDate,
                                                    Notes = pipe.Notes,
                                                    Bill = pipe.Bill,
                                                    ReviewedBySecond = pipe.ReviewedBySecond,
                                                    ReviewedDateSecond = pipe.ReviewedDateSecond,
                                                    NotesSecond = pipe.NotesSecond,
                                                    ProductionStartedDate = pipe.ProductionStartedDate
                                                });
                if (pipelineClearanceHistory.Any())
                    model.PipelineClearanceHistory = pipelineClearanceHistory.ToList();

                var gralCatalog = await _generalCatalogRepository.GetAllAsync();
                var controlVariablesList = _productionOrderAttributeRepository.GetAllAsync().Result
                                            .Where(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.ControlVariable);
                if (controlVariablesList != null)
                {
                    model.ControlVariables = controlVariablesList.Select(x =>
                        new ControlVariableViewModel
                        {
                            Id = x.Id,
                            Area = x.Area,
                            Description = x.Description,
                            Variable = x.Variable + " / " + x.Specification,
                            Specification = x.Specification,
                            ChartPath = x.Variable.Contains("Identidad") ? "N/A" : x.ChartPath,
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
                            LowLimit = gralCatalog.Where(y=>y.PlantId== model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x=>x.LowerLimit).FirstOrDefault(),
                            TopLimit = gralCatalog.Where(y => y.PlantId == model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x => x.UpperLimit).FirstOrDefault(),
                            ReviewedByDate = x.ReviewedDate.HasValue ? x.ReviewedBy + " " + x.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm") : x.ReviewedBy
                        }
                    ).ToList();
                }

                var criticalParametersList = _productionOrderAttributeRepository.GetAllAsync().Result
                                            .Where(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalParameter);
                if (criticalParametersList != null)
                {
                    model.CriticalParameters = criticalParametersList.Select(x =>
                        new CriticalParameterViewModel
                        {
                            Id = x.Id,
                            Area = x.Area,
                            Description = x.Description,
                            Parameter = x.Variable + " / " + x.Specification,
                            Specification = x.Specification,
                            ChartPath = x.Variable.Contains("Identidad") ? "N/A" : x.ChartPath,
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
                            LowLimit = gralCatalog.Where(y => y.PlantId == model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x => x.LowerLimit).FirstOrDefault(),
                            TopLimit = gralCatalog.Where(y => y.PlantId == model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x => x.UpperLimit).FirstOrDefault(),
                            ReviewedByDate = x.ReviewedDate.HasValue ? x.ReviewedBy + " " + x.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm") : x.ReviewedBy
                        }
                    ).ToList();
                }

                var criticalQualityAttributesList = _productionOrderAttributeRepository.GetAllAsync().Result
                                            .Where(x => x.ProductionOrderId == entity.Id && x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
                if (criticalQualityAttributesList != null)
                {
                    model.CriticalQualityAttributes = criticalQualityAttributesList.Select(x =>
                        new CriticalQualityAttributeViewModel
                        {
                            Id = x.Id,
                            Area = x.Area,
                            Description = x.Description,
                            Attribute = x.Variable + " / " + x.Specification,
                            Specification = x.Specification,
                            ChartPath = x.Variable.Contains("Identidad") ? "N/A" : x.ChartPath,
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
                            LowLimit = gralCatalog.Where(y => y.PlantId == model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x => x.LowerLimit).FirstOrDefault(),
                            TopLimit = gralCatalog.Where(y => y.PlantId == model.SelectedPlantFilter && y.ProductId == model.ProductCode && y.CodeTool == x.ChartPath).Select(x => x.UpperLimit).FirstOrDefault(),
                            ReviewedByDate = x.ReviewedDate.HasValue ? x.ReviewedBy + " " + x.ReviewedDate.Value.ToString("yyyy-MM-dd HH:mm") : x.ReviewedBy
                        }
                    ).ToList();
                }

                var batchDetails = _batchDetailsRepository.GetAllAsync().Result.Where(x => x.ProductionOrderId == entity.Id).FirstOrDefault();
                if (batchDetails != null)
                {
                    var Analysis = _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetails.Id).Result.Count > 0 ?
                        _batchAnalysisRepository.GetAsync(x => x.BatchDetailsId == batchDetails.Id).Result.ToList() : new List<BatchAnalysis>();
                    var mapped = ObjectMapper.Mapper.Map<IEnumerable<BatchAnalysisViewModel>>(Analysis);
                    model.BatchDetails = new BatchDetailsViewModel
                    {
                        Number = batchDetails.Number,
                        Tank = batchDetails.Tank,
                        Level = batchDetails.Level,
                        SizeString = batchDetails.Size != 0 ? batchDetails.Size.ToString().Replace(",", ".") : null,
                        AnalyzedBy = batchDetails.AnalyzedBy,
                        AnalyzedDate = batchDetails.AnalyzedDate,
                        InCompliance = batchDetails.InCompliance,
                        NotInComplianceFolio = batchDetails.NotInComplianceFolio,
                        NotInComplianceNotes = batchDetails.NotInComplianceNotes,
                        IsReleased = batchDetails.IsReleased,
                        ReleasedBy = batchDetails.ReleasedBy,
                        ReleasedDate = batchDetails.ReleasedDate,
                        ReleasedNotes = batchDetails.ReleasedNotes,
                        ReleasedByDate = batchDetails.ReleasedDate.HasValue ? batchDetails.ReleasedBy + " " + batchDetails.ReleasedDate.Value.ToString("yyyy-MM-dd HH:mm") : batchDetails.ReleasedBy,
                        Analysis = mapped.ToList()
                    };
                }

                //jjgr
                //await GetVariableFileDataAsync(model);
            }

            return model;
        }

        public async Task<ProductionOrderViewModel> GetVariableFileDataAsync(ProductionOrderViewModel model)
        {
            return await _variablesFileReaderService.FillVariablesAsync(model);
        }

        //AHF
        public string GetDateFromNumberProduction(string NumberProduction)
        {
            var list = !string.IsNullOrEmpty(NumberProduction) ? NumberProduction.Replace(@"\n", "").Trim().Split("-") : new string[1];
            string date = list.Length > 3 ? $"{list[2]}{list[list.Length - 1]}" : string.Empty;
            return date;
        }
        #endregion
        #region ConditioningOrder
        public async Task<ConditioningOrderViewModel> GetModel(int IdOP, int Id)
        {
            var model = new ConditioningOrderViewModel();

            if (IdOP > 0 && Id == 0)
            {
                model = await _conditioningOrderService.GetByProductionOrderIdAsync(IdOP);
            }
            else if (Id > 0)
            {
                model = await _conditioningOrderService.GetByIdAsync(Id);
            }
            else
            {
                model.ProductionOrderId = 0;
                model.Plant = "Guadalajara";
                model.Product = "Oxígeno";
                model.Tank = "M31-LOX-01";
                model.ContainerPrimary = "Lorem ipsu";
                model.LotProd = "OX - M31 - 040820 - M31 - LOX - 01 - 0951";
                model.Presentation = "Loremipsu Loremipsu Loremipsu";

                model.AnalyticEquipmentList = new List<AnalyticEquipmentViewModel> {
                        new AnalyticEquipmentViewModel{
                            Code = "123456",
                            ConditioningOrderId = 1,
                            Description = "a",
                            Id = 1,
                            IsCalibrated = true,
                            Notes = string.Empty,
                            ReviewedBy = "Usuario 0",
                            ReviewedDate = DateTime.Now.AddDays(-3)
                        },
                        new AnalyticEquipmentViewModel{
                            Code = "654321",
                            ConditioningOrderId = 1,
                            Description = "b",
                            Id = 2,
                            IsCalibrated = false,
                            Notes = string.Empty,
                            ReviewedBy = "Usuario 0",
                            ReviewedDate = DateTime.Now.AddDays(-3)
                        },
                        new AnalyticEquipmentViewModel{
                            Code = "567890",
                            ConditioningOrderId = 1,
                            Description = "c",
                            Id = 3,
                            IsCalibrated = null,
                            Notes = string.Empty,
                            ReviewedBy = "Usuario 0",
                            ReviewedDate = DateTime.Now.AddDays(-3)
                        },
                    };
                model.EquipamentProcessesList = new List<EquipamentProcessConditioningViewModel>();
                model.ObservationsList = new List<ObservationHistoryViewModel>();
                model.PerformanceList = new List<PerformanceProcessConditioningViewModel>();
                model.PipeClearancesList = new List<PipeClearanceOAViewModel>();
                model.PipelineClearanceOA = new PipeClearanceOAViewModel();
                model.ScalesflowsList = new List<ScalesflowsViewModel> {
                        new ScalesflowsViewModel {
                            Id = 1,
                            Description = "Báscula BC-01",
                        },
                        new ScalesflowsViewModel {
                            Id = 2,
                            Description = "Báscula BC-02",
                        },
                        new ScalesflowsViewModel {
                            Id = 3,
                            Description = "Báscula BC-03",
                        }
                    };
                model.StatesList = new List<StatesHistoryViewModel>();

                // fill catalogs
                model.BayAreaFilter = new List<SelectListItem> {
                        new SelectListItem { Value = "1", Text = "1" },
                        new SelectListItem { Value = "2", Text = "2" },
                        new SelectListItem { Value = "3", Text = "3" }
                    };
                model.BayAreaList = new List<BayAreaItem> {
                        new BayAreaItem { Index = 1, BayArea = "1", FillingPump = "RP-01", FillingHose = "INST-01"  },
                        new BayAreaItem { Index = 2, BayArea = "2", FillingPump = "RP-02", FillingHose = "INST-02"  },
                        new BayAreaItem { Index = 3, BayArea = "3", FillingPump = "RP-03", FillingHose = "INST-03"  }
                    };
            }

            if (model == null)
            {
                //return Json(new { Result = "NotFound", Message = "Orden de acondicionamiento no existe" });
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
                                    .Where(x => !string.IsNullOrEmpty(x.BayArea))
                                    .Select(x => new BayAreaItem
                                    {
                                        Index = bayAreaIndex++,
                                        BayArea = x.BayArea,
                                        FillingPump = x.FillingPump,
                                        FillingHose = x.FillingHose
                                    }).ToList();
            model.BayAreaFilter = model.BayAreaList
                                    .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();

            if (model.EquipamentProcessesList == null || model.EquipamentProcessesList.Count == 0)
            {
                model.EquipamentProcessesList = new List<EquipamentProcessConditioningViewModel>()
                {
                    new EquipamentProcessConditioningViewModel()
                    {
                        TourNumber = "NA",
                        PipeNumber = "NA",
                        Bay = "NA",
                        Bomb = "NA",
                        Hosefill = "NA",
                        HoseDownload = "NA",
                        Notes = "NA",
                        ReviewedBy = "NA"
                    }
                };
                model.PipeFillingControl = new List<PipeFillingControlViewModel>()
                {
                    new PipeFillingControlViewModel()
                    {
                        TourNumber = "NA",
                        PipesList = new List<PipeFillingViewModel>()
                        {
                            new PipeFillingViewModel()
                            {
                                TourNumber = "NA",
                                Date = null,
                                PipeNumber = "NA",
                                InitialWeight = "NA",
                                FinalWeight = "NA",
                                DiffWeight = 0,
                                Customers = new List<PipeFillingCustomerViewModel>()
                                {
                                    new PipeFillingCustomerViewModel()
                                    {
                                        Tank = "NA",
                                        Name = "NA",
                                        DeliveryNumber = "NA",
                                        ReviewedBy = "NA",
                                        AnalysisReport = "NA",
                                        EmailsList = "NA",
                                        Notes = "NA"
                                    }
                                },
                                ReportPNCFolio = "NA",
                                ReportPNCNotes = "NA",
                                InitialAnalysis = new List<PipeFillingAnalysisViewModel>()
                                {
                                    new PipeFillingAnalysisViewModel()
                                    {
                                        ParameterName = "NA",
                                        MeasureUnit = "NA",
                                        ValueExpected = "NA",
                                        ValueReal = "0"
                                    }
                                },
                                FinalAnalysis = new List<PipeFillingAnalysisViewModel>()
                                {
                                    new PipeFillingAnalysisViewModel()
                                    {
                                        ParameterName = "NA",
                                        MeasureUnit = "NA",
                                        ValueExpected = "NA",
                                        ValueReal = "0"
                                    }
                                },
                                AnalyzedBy = "NA",
                                DistributionBatch = "NA",
                                ReleasedBy = "NA",
                                IsReleased = null,
                                InCompliance = null
                            }
                        }
                    }
                };
            }

            return model;

        }
        public async Task<RptChkList> GetRptChkList(CheckListGeneralViewModel chkGeneral)
        {
            List<RptChkList> retList = new List<RptChkList>();
            List<CheckListVM> lstCheckListVM = new List<CheckListVM>();

            int idOA, checkListId;
            string tourNumber, distributionBatch;


            CheckListVM model = new CheckListVM();
            RptChkList rptCheckList = new RptChkList();

            idOA = chkGeneral.ConditioningOrderId;
            checkListId = chkGeneral.Id;
            tourNumber = chkGeneral.TourNumber;
            distributionBatch = chkGeneral.DistributionBatch;

            model.NumberOrder = idOA;
            model.TourNumber = tourNumber;
            model.DistributionBatch = distributionBatch;
            model.checkListId = checkListId;

            var labelsDb = await _principalService.CheckListRecordLabels(idOA, checkListId);

            model.Localizate = labelsDb.Localizate;
            model.Product = labelsDb.Product;
            model.Pipe = labelsDb.Pipe;

            model = await GetRptCheckListVMs(model);

            model.checkListsCatalog.ForEach(item =>
            {

                switch (item.Verification)
                {
                    case "OK":
                        item.Verification = "Cumple";
                        break;
                    case "NO":
                        item.Verification = "No Cumple";
                        break;
                    default:
                        item.Verification = "NA";
                        break;
                }

            });

            model.checkListsfpCatalog.ForEach(item =>
            {

                switch (item.Verification)
                {
                    case "OK":
                        item.Verification = "Cumple";
                        break;
                    case "NO":
                        item.Verification = "No Cumple";
                        break;
                    default:
                        item.Verification = "NA";
                        break;
                }
            });

            model.checkListPipeDictiumAnswers.ForEach(item =>
            {

                switch (item.Verification)
                {
                    case "OK":
                        item.Verification = "Si";
                        break;
                    case "NO":
                        item.Verification = "No";
                        break;
                    default:
                        item.Verification = "";
                        break;
                }
            });

            lstCheckListVM.Add(model);
            rptCheckList.DataSource = lstCheckListVM;
            rptCheckList.CreateDocument();


            return rptCheckList;
        }
        public async Task<CheckListVM> GetRptCheckListVMs(CheckListVM model)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            try
            {
                ///Init add
                var dictiumDB = _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder
                       && x.TourNumber == model.TourNumber
                       && x.Id == model.checkListId).Result.ToList();
                if (dictiumDB.Any())
                {
                    model.checkListId = dictiumDB.FirstOrDefault().Id;
                }

                //STATUS INIT
                var DBRecord = _checkListPipeRecordAnswerRepository.
                    GetAsync(x => x.Status == CheckListType.Inprogress.Value).Result.Where(y => y.NumOA == model.NumberOrder && y.TourNumber == model.TourNumber
                             && y.CheckListPipeDictiumId == model.checkListId).ToList();



                //LOAD CATALOG INIT
                var questionsDefault = await _principalService.GenerateCheckListCat();
                var DbCatAnswer = new List<CheckListPipeAnswer>();

                DbCatAnswer = _checkListPipeAnswerRepository.
                                    GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                    && x.CheckListPipeDictiumId == model.checkListId).Result.Where(y => y.Group == "IV").ToList();
                if (DbCatAnswer.Count == 5)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "IV"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Notify = item.Notify, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsCatalog = UserInfo;
                }
                else
                {
                    model.checkListsCatalog = new List<CheckListVM>();
                }

                var DbCatFPAnswer = _checkListPipeAnswerRepository.
                                  GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                  && x.DistributionBatch == model.DistributionBatch
                                  && x.CheckListPipeDictiumId == model.checkListId).Result.Where(y => y.Group == "FP").ToList();
                if (DbCatFPAnswer.Count == 6)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatFPAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "FP"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Notify = item.Notify, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsfpCatalog = UserInfo;
                }
                else
                {
                    model.checkListsfpCatalog = new List<CheckListVM>();
                }

                ///BLOQ ELEMENT
                if (model.checkListsCatalog.Where(x => x.Verification != "OK").Count() > 0 && model.checkListsfpCatalog.Where(x => x.Verification != "OK").Count() > 0)
                {
                    model.Style = "pointer-events: none;";
                }

                model.ListUserNotify = await GetRptUsersItemsAsync();

                var recordsDB = (from record in await _checkListPipeRecordAnswerRepository
                                       .GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                       && x.CheckListPipeDictiumId == model.checkListId)
                                 select new CheckListPipeRecordAnswer
                                 {
                                     Status = record.Step == CheckListGeneralViewModelCheckListStep.One.Value ? "CL1 - " + record.Status : "CL2 - " + record.Status,
                                     Date = record.Date

                                 });

                model.checkListsRecord = recordsDB.ToList();

                model.LastStatusRecord = model.checkListsRecord.Count() > 0 ? model.checkListsRecord.Select(record => record.Status).LastOrDefault().Replace("CL2 - ", "") : null;
                model.FlagApproveSC =
                    (model.checkListsRecord.Where(x => x.ApproveSC == "NO").ToList().Count > 0) ? null :
                    (model.checkListsRecord.Where(x => x.ApproveSC == "SI").ToList().Count > 0) ?
                    model.checkListsRecord.Where(x => x.ApproveSC == "SI").LastOrDefault().Reason : "NULLA";
                model.LastDateRecord = model.checkListsRecord.Select(record => record.Date).FirstOrDefault();


                var CommentsDB = (from record in await _checkListPipeCommentsAnswerReepository
                                                 .GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                                 && x.CheckListPipeDictiumId == model.checkListId)
                                  select new CheckListPipeCommentsAnswer
                                  {
                                      Group = record.Step == CheckListGeneralViewModelCheckListStep.One.Value ? "CL1 - " + record.Group : "CL2 - " + record.Group,
                                      Author = record.Author,
                                      Date = record.Date,
                                      Comment = record.Comment
                                  });

                model.checkListPipeCommentsAnswers = CommentsDB.OrderBy(x => x.Date).ToList();
                model.CommentIv = (model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Inspección visual de Pipas al recibo")).ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Inspección visual de Pipas al recibo")).LastOrDefault().Comment : null;
                model.CommentFP = (model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Checklist llenado de pipa y verificación de pipas")).ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Checklist llenado de pipa y verificación de pipas")).LastOrDefault().Comment : null;
                model.checkListPipeDictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                var dictium = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder &&
                                x.TourNumber == model.TourNumber && x.DistributionBatch == model.DistributionBatch
                                && x.Id == model.checkListId);
                model.checkListPipeDictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                if (dictium.Any())
                {
                    foreach (var item in dictium)
                    {
                        var dictiumInCompliance = await _checkListPipeRecordAnswerRepository.GetAsync(x => x.CheckListPipeDictiumId == dictium.FirstOrDefault().Id);

                        var stepOne = dictiumInCompliance.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value);
                        stepOne = stepOne.Where(x => x.Step == CheckListGeneralViewModelCheckListStep.One.Value);

                        var stepTwo = dictiumInCompliance.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value);
                        stepTwo = stepTwo.Where(x => x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);

                        if (stepOne.Any())
                        {
                            model.checkListPipeDictiumAnswers.Add(new CheckListPipeDictiumAnswer
                            {
                                Status = stepOne.LastOrDefault().Status == CheckListType.CloseOk.Value ? "CL1 - " + "CUMPLE" : "CL1 - " + "NO CUMPLE",
                                CreatedBy = stepOne.LastOrDefault().CreatedBy + " " + stepOne.LastOrDefault().Date.ToString("yyyy-MM-dd HH:mm"),
                                Comment = item.Comment,
                                Date = stepOne.LastOrDefault().Date
                            });
                        }
                        if (stepTwo.Any())
                        {
                            model.checkListPipeDictiumAnswers.Add(new CheckListPipeDictiumAnswer
                            {
                                Status = stepTwo.LastOrDefault().Status == CheckListType.CloseOk.Value ? "CL2 - " + "CUMPLE" : "CL2 - " + "NO CUMPLE",
                                CreatedBy = stepTwo.LastOrDefault().CreatedBy + stepTwo.LastOrDefault().Date.ToString("yyyy-MM-dd HH:mm"),
                                Comment = item.CommentTwo,
                                Date = stepTwo.LastOrDefault().Date
                            });
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al actualizar CheckListPipeAnswer " + model.DistributionBatch + " " + model.TourNumber + " " + model.NumberOrder + ex);
            }

            return model;
        }
        public async Task<List<SelectListItem>> GetRptUsersItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            var usuarios = _userManager.Users;
            response.Add(new SelectListItem { Text = "NA", Value = "0" });
            foreach (var item in usuarios.Where(x => x.Rol == SecurityConstants.PERFIL_RESPONSABLE_SANITARIO))
            {
                response.Add(new SelectListItem
                {
                    Text = item.NombreUsuario,
                    Value = item.NombreUsuario

                });

            }

            return response;
        }
        public async Task<Report1> GetRptCertificate(int IdOP, int Id, string tourNumber, string pipeNumber, int IdCustomer, ConditioningOrderViewModel model, string distributionBatch)
        {
            List<ConditioningOrderViewModel> rptDataSource = new List<ConditioningOrderViewModel>();

            //ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            PipeFillingControlViewModel pipeFillingControl = new PipeFillingControlViewModel();
            pipeFillingControl.PipesList = new List<PipeFillingViewModel>();
            List<PipeFillingControlViewModel> ListPC = new List<PipeFillingControlViewModel>();
            Report1 report = new Report1();
            XRLabel Label = report.FindControl("XRLabel35", true) as XRLabel;
            var formulas = await _formulaRepository.GetAllAsync();
            try
            {

                //model = await getDatasource(IdOP, Id);

                if (model == null) return null;
                var m = model.PipeFillingControl.FirstOrDefault(pc => pc.TourNumber == tourNumber);
                foreach (PipeFillingViewModel item in m.PipesList)
                {
                    foreach (var itemx in item.Customers)
                    {
                        if ((item.DistributionBatch == distributionBatch) && (itemx.Id == IdCustomer))
                        {
                            itemx.Folio = itemx.AnalysisReport + itemx.ReviewedDate.Value.ToString("yy");
                            var f1 = formulas.Where(f => f.ProductId == model.ProductId).FirstOrDefault();
                            Label.Text = f1.RegisterCode != null ? f1.RegisterCode : null;
                            pipeFillingControl.TourNumber = tourNumber;
                            var filter = item.Customers.Where(x => x.Id == IdCustomer);
                            item.Customers = filter.ToList();
                            pipeFillingControl.PipesList.Add(item);
                        }
                        if (item.DistributionBatch == distributionBatch)
                        {
                            #region parameters
                            XRTable xrTableParameters = report.FindControl("xrTable1", true) as XRTable;
                            XRTableCell xRTableCellParameterName = xrTableParameters.FindControl("xrTableCell1", true) as XRTableCell;
                            XRTable xrTableValueExpected = report.FindControl("xrTable2", true) as XRTable;
                            XRTableCell xRTableCellValueExpected = xrTableParameters.FindControl("xrTableCell2", true) as XRTableCell;
                            XRTable xrTableValueReal = report.FindControl("xrTable3", true) as XRTable;
                            XRTableCell xRTableCellValueReal = xrTableParameters.FindControl("xrTableCell3", true) as XRTableCell;
                            XRTable xrTableAnalizador = report.FindControl("xrTable4", true) as XRTable;
                            XRTableCell xRTableCellAnalizador = xrTableParameters.FindControl("xrTableCell4", true) as XRTableCell;
                            int numRows = item.FinalAnalysis.Count();
                            for (int i = 0; i < numRows; i++)
                            {
                                XRTableCell rTableCellParameterName = new XRTableCell();
                                rTableCellParameterName.Text = item.FinalAnalysis[i].ParameterName;
                                XRTableRow RowParameterName = new XRTableRow();
                                RowParameterName.Cells.Add(rTableCellParameterName);
                                xrTableParameters.Rows.Add(RowParameterName);

                                XRTableCell rTableCellValueExpected = new XRTableCell();
                                rTableCellValueExpected.Text = item.FinalAnalysis[i].ParameterName == "Identidad" ?
                                      "Positiva" : item.FinalAnalysis[i].ValueExpected;
                                XRTableRow RowValueExpected = new XRTableRow();
                                RowValueExpected.Cells.Add(rTableCellValueExpected);
                                xrTableValueExpected.Rows.Add(RowValueExpected);
                                //string.Format("{0:f2}", float.Parse(item.FinalAnalysis[i].ValueReal.Replace(",", ".")))
                                double myNumber = double.Parse(item.FinalAnalysis[i].ValueReal);
                                double myNumberString = Math.Truncate(myNumber * 100) / 100;
                                XRTableCell rTableCellValueReal = new XRTableCell();
                                rTableCellValueReal.Text = item.FinalAnalysis[i].ParameterName == "Identidad" ?
                                    item.FinalAnalysis[i].ValueReal == "1,0000" ? "Positiva" : "Negativa" :
                                myNumberString.ToString().Replace(",", ".");
                                XRTableRow RowValueReal = new XRTableRow();
                                RowValueReal.Cells.Add(rTableCellValueReal);
                                xrTableValueReal.Rows.Add(RowValueReal);

                                XRTableCell rTableCellAnalizador = new XRTableCell();
                                rTableCellAnalizador.Text = await this.GetAnalizador(item.DistributionBatch,
                                                             item.FinalAnalysis[i].ParameterName);
                                XRLabel LblNumPraxiar = report.FindControl("xrLabel24", true) as XRLabel;
                                LblNumPraxiar.Text = await this.GetTankClient(item.DistributionBatch, item.TourNumber, itemx.Tank);
                                XRTableRow RowValueAnalizador = new XRTableRow();
                                RowValueAnalizador.Cells.Add(rTableCellAnalizador);
                                xrTableAnalizador.Rows.Add(RowValueAnalizador);
                            }

                            xrTableParameters.Rows.FirstRow.Cells.RemoveAt(0);
                            xrTableValueExpected.Rows.FirstRow.Cells.RemoveAt(0);
                            xrTableValueReal.Rows.FirstRow.Cells.RemoveAt(0);
                            xrTableAnalizador.Rows.FirstRow.Cells.RemoveAt(0);

                            #endregion
                        }
                    }
           
                }
                ListPC.Add(pipeFillingControl);
                model.PipeFillingControl = ListPC;
                rptDataSource.Add(model);

                report.DataSource = rptDataSource;

                report.CreateDocument();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetRptCertificate IdCustomer" + IdCustomer + ex);
            }


            return report;
        }

        public async Task<Report1> GetRptCertificate(int IdOP, int Id, string tourNumber, string pipeNumber, string tank, ConditioningOrderViewModel model, string distributionBatch, int CertificateId, int FooterCertificateId)
        {
            List<ConditioningOrderViewModel> rptDataSource = new List<ConditioningOrderViewModel>();

            //ConditioningOrderViewModel model = new ConditioningOrderViewModel();
            PipeFillingControlViewModel pipeFillingControl = new PipeFillingControlViewModel();
            pipeFillingControl.PipesList = new List<PipeFillingViewModel>();
            List<PipeFillingControlViewModel> ListPC = new List<PipeFillingControlViewModel>();
            Report1 report = new Report1();
            XRLabel Label = report.FindControl("XRLabel35", true) as XRLabel;
            XRRichText LabelHeaderOne = report.FindControl("xrRichText1", true) as XRRichText;
            XRRichText LabelLeyendOne = report.FindControl("xrRichText2", true) as XRRichText;
            XRRichText LabelLeyendTwo = report.FindControl("xrRichText3", true) as XRRichText;
            //LabelLeyendTwo.Font = new System.Drawing.Font("Arial", 9);
            XRRichText LabelFooter = report.FindControl("xrRichText4", true) as XRRichText;
            XRPictureBox xRPictureBox = report.FindControl("xrPictureBox1", true) as XRPictureBox;
            var Certificate = (await _leyendsCertificateHistoryRepository.GetAsync(x => x.Id.Equals(CertificateId))).FirstOrDefault();
            var Footer = (await _leyendsFooterCertificateHistoryRepository.GetAsync(x => x.Id.Equals(FooterCertificateId))).FirstOrDefault();
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
            var formulas = await _formulaRepository.GetAllAsync();
            try
            {

                //model = await getDatasource(IdOP, Id);

                if (model == null) return null;
                var m = model.PipeFillingControl.FirstOrDefault(pc => pc.TourNumber == tourNumber);
                foreach (PipeFillingViewModel item in m.PipesList)
                {
                    foreach (var itemx in item.Customers)
                    {
                        if ((item.DistributionBatch == distributionBatch) && (itemx.ConditioningOrderId == Id && itemx.TourNumber == tourNumber && itemx.DistributionBatch == distributionBatch && itemx.Tank == tank))
                        {
                            itemx.Folio = itemx.AnalysisReport;
                            var f1 = formulas.Where(f => f.ProductId == model.ProductId).FirstOrDefault();
                            Label.Text = f1.RegisterCode != null ? f1.RegisterCode : null;
                            pipeFillingControl.TourNumber = tourNumber;
                            var filter = item.Customers.Where(x => x.Id == itemx.Id);
                            item.Customers = filter.ToList();
                            pipeFillingControl.PipesList.Add(item);
                            #region states certificate 
                            XRLabel LblDescriptionState = report.FindControl("xrLabel28", true) as XRLabel;
                            if (itemx.state.HasValue && itemx.state == false)
                            {
                                LblDescriptionState.Text = _resource.GetString("ReRounting").Value;
                                LblDescriptionState.ForeColor = Color.Red;
                            }
                            #endregion
                            if (item.DistributionBatch == distributionBatch)
                            {
                                #region parameters
                                XRTable xrTableParameters = report.FindControl("xrTable1", true) as XRTable;
                                XRTableCell xRTableCellParameterName = xrTableParameters.FindControl("xrTableCell1", true) as XRTableCell;
                                XRTable xrTableValueExpected = report.FindControl("xrTable2", true) as XRTable;
                                XRTableCell xRTableCellValueExpected = xrTableParameters.FindControl("xrTableCell2", true) as XRTableCell;
                                XRTable xrTableValueReal = report.FindControl("xrTable3", true) as XRTable;
                                XRTableCell xRTableCellValueReal = xrTableParameters.FindControl("xrTableCell3", true) as XRTableCell;
                                XRTable xrTableAnalizador = report.FindControl("xrTable4", true) as XRTable;
                                XRTableCell xRTableCellAnalizador = xrTableParameters.FindControl("xrTableCell4", true) as XRTableCell;
                                int numRows = item.FinalAnalysis.Count();
                                for (int i = 0; i < numRows; i++)
                                {
                                    XRTableCell rTableCellParameterName = new XRTableCell();
                                    rTableCellParameterName.Text = item.FinalAnalysis[i].ParameterName;
                                    XRTableRow RowParameterName = new XRTableRow();
                                    RowParameterName.Cells.Add(rTableCellParameterName);
                                    xrTableParameters.Rows.Add(RowParameterName);

                                    XRTableCell rTableCellValueExpected = new XRTableCell();
                                    rTableCellValueExpected.Text = item.FinalAnalysis[i].ParameterName == "Identidad" ?
                                          "Positiva" : item.FinalAnalysis[i].ValueExpected;
                                    XRTableRow RowValueExpected = new XRTableRow();
                                    RowValueExpected.Cells.Add(rTableCellValueExpected);
                                    xrTableValueExpected.Rows.Add(RowValueExpected);
                                    //string.Format("{0:f2}", float.Parse(item.FinalAnalysis[i].ValueReal.Replace(",", ".")))


                                    var plit = item.FinalAnalysis[i].ValueReal.Split(",");
     
                                    decimal myNumber = 0;
                                    if (plit?[1].Length > 2)
                                        myNumber = decimal.Parse(string.Format("{0:f2}", decimal.Parse(plit?[0] + "," + plit?[1].Substring(0,2))));
                                    else
                                        myNumber = decimal.Parse(string.Format("{0:f2}", decimal.Parse(item.FinalAnalysis[i].ValueReal)));

                                    XRTableCell rTableCellValueReal = new XRTableCell();
                                    rTableCellValueReal.Text = item.FinalAnalysis[i].ParameterName == "Identidad" ?
                                        item.FinalAnalysis[i].ValueReal == "1,0000" ? "Positiva" : "Negativa" :
                                    myNumber.ToString().Replace(",", ".");
                                    XRTableRow RowValueReal = new XRTableRow();
                                    RowValueReal.Cells.Add(rTableCellValueReal);
                                    xrTableValueReal.Rows.Add(RowValueReal);

                                    XRTableCell rTableCellAnalizador = new XRTableCell();
                                    rTableCellAnalizador.Text = await this.GetAnalizador(item.DistributionBatch,
                                                                 item.FinalAnalysis[i].ParameterName);
                                    XRLabel LblNumPraxiar = report.FindControl("xrLabel24", true) as XRLabel;
                                    LblNumPraxiar.Text = await this.GetTankClient(item.DistributionBatch, item.TourNumber, itemx.Tank);
                                    XRTableRow RowValueAnalizador = new XRTableRow();
                                    RowValueAnalizador.Cells.Add(rTableCellAnalizador);
                                    xrTableAnalizador.Rows.Add(RowValueAnalizador);
                                }

                                xrTableParameters.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableValueExpected.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableValueReal.Rows.FirstRow.Cells.RemoveAt(0);
                                xrTableAnalizador.Rows.FirstRow.Cells.RemoveAt(0);

                                #endregion
                            }
                        }
                   
                    }
                   
                }
                ListPC.Add(pipeFillingControl);
                model.PipeFillingControl = ListPC;
                rptDataSource.Add(model);

                report.DataSource = rptDataSource;

                report.CreateDocument();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetRptCertificate Tank" + tank + ex);
            }


            return report;
        }
        public async Task<ConditioningOrderViewModel> getDatasource(int IdOP, int Id)
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
                    return null;
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
                                        .Where(x => !string.IsNullOrEmpty(x.BayArea))
                                        .Select(x => new BayAreaItem
                                        {
                                            Index = bayAreaIndex++,
                                            BayArea = x.BayArea,
                                            FillingPump = x.FillingPump,
                                            FillingHose = x.FillingHose
                                        }).ToList();
                model.BayAreaFilter = model.BayAreaList
                                        .Select(x => new SelectListItem { Value = x.Index.ToString(), Text = x.BayArea }).ToList();

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en orden de acondicionamiento " + ex);
                return null;
            }

        }
        public async Task<String> GetAnalizador(string IdLotePipa, string Param)
        {
            String Analizador = string.Empty;
            var AnalizadorDB = _analisisClienteRepository.GetAsync(x => x.ID_LOTEPIPA == IdLotePipa && x.DESC_PARAMETRO == Param).FirstOrDefault();
            if (AnalizadorDB != null)
                Analizador = AnalizadorDB.DESC_ANALIZADOR;
            return Analizador;
        }
        public async Task<String> GetTankClient(string IdLotePipa, string TourNumber, string TankPraxiar)
        {
            String Tank = string.Empty;
            var TankDB = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == IdLotePipa && x.TOURNUMBER == TourNumber && x.TANQUE_CLIENTE == TankPraxiar).FirstOrDefault();
            if (TankDB != null)
                Tank = TankDB.TANQUE_CLIENTE;
            return Tank;
        }
        public async Task<RptChkList> GetRptChkListQuestionsOne(CheckListGeneralViewModel chkGeneral)
        {
            List<RptChkList> retList = new List<RptChkList>();
            List<CheckListVM> lstCheckListVM = new List<CheckListVM>();

            int idOA, checkListId;
            string tourNumber, distributionBatch;


            CheckListVM model = new CheckListVM();
            RptChkList rptCheckList = new RptChkList();

            idOA = chkGeneral.ConditioningOrderId;
            checkListId = chkGeneral.Id;
            tourNumber = chkGeneral.TourNumber;
            distributionBatch = chkGeneral.DistributionBatch;

            model.NumberOrder = idOA;
            model.TourNumber = tourNumber;
            model.DistributionBatch = distributionBatch;
            model.checkListId = checkListId;

            var labelsDb = await _principalService.CheckListRecordLabels(idOA, checkListId);

            model.Localizate = labelsDb.Localizate;
            model.Product = labelsDb.Product;
            if (string.IsNullOrEmpty(labelsDb.Pipe))
                model.Pipe = labelsDb.Alias;
            else
                model.Pipe = labelsDb.Pipe;

            model = await GetRptCheckListVMsStepOne(model);

            model.checkListsCatalog.ForEach(item =>
            {

                switch (item.Verification)
                {
                    case "OK":
                        item.Verification = "Cumple";
                        break;
                    case "NO":
                        item.Verification = "No Cumple";
                        break;
                    default:
                        item.Verification = "NA";
                        break;
                }

            });
            model.checkListsfpCatalog = new List<CheckListVM>();
            model.checkListPipeDictiumAnswers.ForEach(item =>
            {

                switch (item.Verification)
                {
                    case "OK":
                        item.Verification = "Si";
                        break;
                    case "NO":
                        item.Verification = "No";
                        break;
                    default:
                        item.Verification = "";
                        break;
                }
            });

            lstCheckListVM.Add(model);
            rptCheckList.DataSource = lstCheckListVM;
            rptCheckList.CreateDocument();


            return rptCheckList;
        }


        public async Task<CheckListVM> GetRptCheckListVMsStepOne(CheckListVM model)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            try
            {
                ///Init add
                var dictiumDB = _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder
                       && x.TourNumber == model.TourNumber
                       && x.Id == model.checkListId).Result.ToList();
                if (dictiumDB.Any())
                {
                    model.checkListId = dictiumDB.FirstOrDefault().Id;
                }

                //STATUS INIT
                var DBRecord = _checkListPipeRecordAnswerRepository.
                    GetAsync(x => x.Status == CheckListType.Inprogress.Value).Result.Where(y => y.NumOA == model.NumberOrder && y.TourNumber == model.TourNumber
                             && y.CheckListPipeDictiumId == model.checkListId).ToList();



                //LOAD CATALOG INIT
                var questionsDefault = await _principalService.GenerateCheckListCat();
                var DbCatAnswer = new List<CheckListPipeAnswer>();

                DbCatAnswer = _checkListPipeAnswerRepository.
                                    GetAsync(x => x.NumOA == model.NumberOrder
                                    && x.CheckListPipeDictiumId == model.checkListId).Result.Where(y => y.Group == "IV").ToList();
                if (DbCatAnswer.Count == 5)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "IV"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Notify = item.Notify, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsCatalog = UserInfo;
                }
                else
                {
                    model.checkListsCatalog = new List<CheckListVM>();
                }

                var DbCatFPAnswer = _checkListPipeAnswerRepository.
                                  GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                  && x.DistributionBatch == model.DistributionBatch
                                  && x.CheckListPipeDictiumId == model.checkListId).Result.Where(y => y.Group == "FP").ToList();
                if (DbCatFPAnswer.Count == 6)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatFPAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "FP"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsfpCatalog = UserInfo;
                }
                else
                {
                    model.checkListsfpCatalog = new List<CheckListVM>();
                }

                ///BLOQ ELEMENT
                if (model.checkListsCatalog.Where(x => x.Verification != "OK").Count() > 0 && model.checkListsfpCatalog.Where(x => x.Verification != "OK").Count() > 0)
                {
                    model.Style = "pointer-events: none;";
                }

                model.ListUserNotify = await GetRptUsersItemsAsync();

                var recordsDB = (from record in await _checkListPipeRecordAnswerRepository
                                       .GetAsync(x => x.NumOA == model.NumberOrder
                                       && x.CheckListPipeDictiumId == model.checkListId)
                                 select new CheckListPipeRecordAnswer
                                 {
                                     Status = record.Step == CheckListGeneralViewModelCheckListStep.One.Value ? "CL1 - " + record.Status : "CL2 - " + record.Status,
                                     Date = record.Date,
                                     Step = record.Step
                                 });

                model.checkListsRecord = recordsDB.Where(x => x.Step == CheckListGeneralViewModelCheckListStep.One.Value).ToList();

                model.LastStatusRecord = model.checkListsRecord.Count() > 0 ? model.checkListsRecord.Select(record => record.Status).LastOrDefault().Replace("CL1 - ", "") : null;
                model.FlagApproveSC =
                    (model.checkListsRecord.Where(x => x.ApproveSC == "NO").ToList().Count > 0) ? null :
                    (model.checkListsRecord.Where(x => x.ApproveSC == "SI").ToList().Count > 0) ?
                    model.checkListsRecord.Where(x => x.ApproveSC == "SI").LastOrDefault().Reason : "NULLA";
                model.LastDateRecord = model.checkListsRecord.Select(record => record.Date).FirstOrDefault();


                var CommentsDB = (from record in await _checkListPipeCommentsAnswerReepository
                                                  .GetAsync(x => x.NumOA == model.NumberOrder
                                                  && x.CheckListPipeDictiumId == model.checkListId)
                                  select new CheckListPipeCommentsAnswer
                                  {
                                      Group = record.Step == CheckListGeneralViewModelCheckListStep.One.Value ? "CL1 - " + record.Group : "CL2 - " + record.Group,
                                      Author = record.Author,
                                      Date = record.Date,
                                      Comment = record.Comment
                                  });

                model.checkListPipeCommentsAnswers = CommentsDB.OrderBy(x => x.Date).ToList();
                model.CommentIv = (model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Inspección visual de Pipas al recibo")).ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Inspección visual de Pipas al recibo")).LastOrDefault().Comment : null;
                model.CommentFP = (model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Checklist llenado de pipa y verificación de pipas")).ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group.Contains("Checklist llenado de pipa y verificación de pipas")).LastOrDefault().Comment : null;
                model.checkListPipeDictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                var dictium = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder &&
                                x.TourNumber == model.TourNumber && x.DistributionBatch == model.DistributionBatch
                                && x.Id == model.checkListId);
                model.checkListPipeDictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                if (dictium.Any())
                {
                    foreach (var item in dictium)
                    {
                        var dictiumInCompliance = await _checkListPipeRecordAnswerRepository.GetAsync(x => x.CheckListPipeDictiumId == dictium.FirstOrDefault().Id);

                        var stepOne = dictiumInCompliance.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value);
                        stepOne = stepOne.Where(x => x.Step == CheckListGeneralViewModelCheckListStep.One.Value);

                        var stepTwo = dictiumInCompliance.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value);
                        stepTwo = stepTwo.Where(x => x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);

                        if (stepOne.Any())
                        {
                            model.checkListPipeDictiumAnswers.Add(new CheckListPipeDictiumAnswer
                            {
                                Status = stepOne.LastOrDefault().Status == CheckListType.CloseOk.Value ? "CL1 - " + "CUMPLE" : "CL1 - " + "NO CUMPLE",
                                CreatedBy = stepOne.LastOrDefault().CreatedBy + " " + stepOne.LastOrDefault().Date.ToString("yyyy-MM-dd HH:mm"),
                                Comment = item.Comment,
                                Date = stepOne.LastOrDefault().Date
                            });
                        }
                        if (stepTwo.Any())
                        {
                            model.checkListPipeDictiumAnswers.Add(new CheckListPipeDictiumAnswer
                            {
                                Status = stepTwo.LastOrDefault().Status == CheckListType.CloseOk.Value ? "CL2 - " + "CUMPLE" : "CL2 - " + "NO CUMPLE",
                                CreatedBy = stepTwo.LastOrDefault().CreatedBy + stepOne.LastOrDefault().Date.ToString("yyyy-MM-dd HH:mm"),
                                Comment = item.CommentTwo,
                                Date = stepTwo.LastOrDefault().Date
                            });
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al actualizar CheckListPipeAnswer " + model.DistributionBatch + " " + model.TourNumber + " " + model.NumberOrder + ex);
            }

            return model;
        }

        #endregion
        #region helpers
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
            var productsFiltered = (await _generalCatalogRepository.GetAsync(x => x.PlantId == plantId)).Select(x => x.ProductId);

            var response = products.Where(x => productsFiltered.Contains(x.Value)).ToList();

            return response;
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
        #endregion
        public async Task<Step4> BuildTableAnalisys(PipeFillingViewModel pipeFillingViewModel, List<ConditioningOrderViewModel> rptDataSource)
        {
            var step4 = new Step4();
            step4.DataSource = rptDataSource;
            var labelTourNumber = step4.FindControl("xrLabel1", true) as XRLabel;
            labelTourNumber.Text = pipeFillingViewModel.TourNumber;

            var table5 = step4.FindControl("xrTable5", true) as XRTable;
            var tableInfoClientTank = step4.FindControl("xrTable4", true) as XRTable;
            var tableAnalisys = step4.FindControl("xrTable1", true) as XRTable;
            var tableAnalisisInfo = step4.FindControl("xrTable3", true) as XRTable;
            this.FillTable5(ref table5, pipeFillingViewModel);
            this.FillTableInfoClientTank(ref tableInfoClientTank, pipeFillingViewModel);
            this.FillAnalisys(ref tableAnalisys, pipeFillingViewModel);
            this.FillAnalisysInfo(ref tableAnalisisInfo, pipeFillingViewModel, rptDataSource.FirstOrDefault()?.State);
            tableAnalisisInfo.HeightF = 5;
            await step4.CreateDocumentAsync();
            return step4;
        }
        private void FillAnalisys(ref XRTable xRTable, PipeFillingViewModel pipeFillingViewModel)
        {
            var width = 342 / pipeFillingViewModel.InitialAnalysis.Count();
            var cells = pipeFillingViewModel.InitialAnalysis.Select(x => new XRTableCell()
            {
                Text = x.ParameterName,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            var cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.InitialAnalyzedDate.HasValue ? ((DateTime)pipeFillingViewModel.InitialAnalyzedDate).ToString("yyyy-MM-dd HH:mm") : "NA",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 89,
            };
            cells.Add(cell2);
            var cells3 = pipeFillingViewModel.FinalAnalysis.Select(x => new XRTableCell()
            {
                Text = x.ParameterName,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cells.AddRange(cells3);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.FinalAnalyzedDate.HasValue ? ((DateTime)pipeFillingViewModel.FinalAnalyzedDate).ToString("yyyy-MM-dd HH:mm") : "NA",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 89
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.AnalyzedBy,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 99
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.DistributionBatch,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 99
            };
            cells.Add(cell2);
            var row = new XRTableRow();
            row.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row);

            cells = pipeFillingViewModel.InitialAnalysis.Select(x => new XRTableCell()
            {
                Text = x.MeasureUnit,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            cells3 = pipeFillingViewModel.FinalAnalysis.Select(x => new XRTableCell()
            {
                Text = x.MeasureUnit,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cells.AddRange(cells3);
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            var row2 = new XRTableRow();
            row2.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row2);

            cells = pipeFillingViewModel.InitialAnalysis.Select(x => new XRTableCell()
            {
                Text = x.ValueExpected,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            cells3 = pipeFillingViewModel.FinalAnalysis.Select(x => new XRTableCell()
            {
                Text = x.ValueExpected,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cells.AddRange(cells3);
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            var row3 = new XRTableRow();
            row3.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row3);

            cells = pipeFillingViewModel.InitialAnalysis.Select(x => new XRTableCell()
            {
                Text =  GetValueReal( x.ParameterName, x.ValueReal),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            float number = 0;
            cells3 = pipeFillingViewModel.FinalAnalysis.Select(x => new XRTableCell()
            {
                Text = GetValueReal(x.ParameterName, x.ValueReal),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = width
            }).ToList();
            cells.AddRange(cells3);
            cell2 = new XRTableCell()
            {
                WidthF = 89,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                WidthF = 99,
                Visible = false
            };
            cells.Add(cell2);
            var row4 = new XRTableRow();
            row4.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row4);

            if (pipeFillingViewModel.InitialAnalysis.Any(x => !string.IsNullOrEmpty(x.PathFile)) || pipeFillingViewModel.FinalAnalysis.Any(x => !string.IsNullOrEmpty(x.PathFile)))
            {
                cells = pipeFillingViewModel.InitialAnalysis.Select(x => new XRTableCell()
                {
                    Text = !string.IsNullOrEmpty(x.PathFile) ? x.PathFile : string.Empty,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = width
                }).ToList();
                cell2 = new XRTableCell()
                {
                    WidthF = 89,
                    Visible = false
                };
                cells.Add(cell2);
                cells3 = pipeFillingViewModel.FinalAnalysis.Select(x => new XRTableCell()
                {
                    Text = !string.IsNullOrEmpty(x.PathFile) ? x.PathFile : string.Empty,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = width
                }).ToList();
                cells.AddRange(cells3);
                cell2 = new XRTableCell()
                {
                    WidthF = 89,
                    Visible = false
                };
                cells.Add(cell2);
                cell2 = new XRTableCell()
                {
                    WidthF = 99,
                    Visible = false
                };
                cells.Add(cell2);
                cell2 = new XRTableCell()
                {
                    WidthF = 99,
                    Visible = false
                };
                cells.Add(cell2);
                var row5 = new XRTableRow();
                row5.Cells.AddRange(cells.ToArray());
                xRTable.Rows.Add(row5);
                xRTable.HeightF = 80;
            }
            else
                xRTable.HeightF = 70;
        }
        private void FillAnalisysInfo(ref XRTable xRTable, PipeFillingViewModel pipeFillingViewModel, string Status)
        {
            var cells = new List<XRTableCell>();
            var cell1 = new XRTableCell()
            {
                Text = pipeFillingViewModel.DueDate.HasValue ? ((DateTime)pipeFillingViewModel.DueDate).ToString("yyyy-MM-dd") : "NA",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150,
            };
            cells.Add(cell1);
            var checkbox = new XRCheckBox()
            {
                Text = "Si",
                Borders = DevExpress.XtraPrinting.BorderSide.None,
                LocationF = new PointF(25, 10)
            };
            var checkbox2 = new XRCheckBox()
            {
                Text = "No",
                Borders = DevExpress.XtraPrinting.BorderSide.None,
                LocationF = new PointF(75, 10)
            };
            if (pipeFillingViewModel.InCompliance.HasValue && pipeFillingViewModel.InCompliance.Value)
                checkbox.Checked = true;
            else if (pipeFillingViewModel.InCompliance.HasValue && !pipeFillingViewModel.InCompliance.Value)
                checkbox2.Checked = true;
            cell1 = new XRTableCell()
            {
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150
            };
            cell1.Controls.Add(checkbox);
            cell1.Controls.Add(checkbox2);
            cells.Add(cell1);
            cell1 = new XRTableCell()
            {
                Text = pipeFillingViewModel.ReportPNCFolio,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150
            };
            cells.Add(cell1);
            cell1 = new XRTableCell()
            {
                Text = pipeFillingViewModel.ReportPNCNotes,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150
            };
            cells.Add(cell1);
            checkbox = new XRCheckBox()
            {
                Text = "Liberado",
                Borders = DevExpress.XtraPrinting.BorderSide.None,
                LocationF = new PointF(25, 1)
            };
            checkbox2 = new XRCheckBox()
            {
                Text = "Rechazado",
                Borders = DevExpress.XtraPrinting.BorderSide.None,
                LocationF = new PointF(25, 25)
            };
            if (pipeFillingViewModel.IsReleased.HasValue && pipeFillingViewModel.IsReleased.Value)
                checkbox.Checked = true;
            else if (pipeFillingViewModel.IsReleased.HasValue && !pipeFillingViewModel.IsReleased.Value)
                checkbox2.Checked = true;
            cell1 = new XRTableCell()
            {
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150
            };
            cell1.Controls.Add(checkbox);
            cell1.Controls.Add(checkbox2);
            cells.Add(cell1);
            var dateReleased = pipeFillingViewModel.ReleasedDate.HasValue ? ((DateTime)pipeFillingViewModel.ReleasedDate).ToString("yyyy-MM-dd HH:mm") : string.Empty;
            cell1 = new XRTableCell()
            {
                Text = $"{pipeFillingViewModel.ReleasedBy} \n {dateReleased}",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 150
            };
            cells.Add(cell1);

            var row = new XRTableRow();
            row.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row);
        }
        private void FillTable5(ref XRTable xRTable, PipeFillingViewModel pipeFillingViewModel)
        {
            var cells = new List<XRTableCell>();
            var cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.Date.HasValue ? ((DateTime)pipeFillingViewModel.Date).ToString("yyyy-MM-dd") : "NA",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 9),
                WidthF = 203,
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.PipeNumber,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 194
            };
            cells.Add(cell2);
            var row2 = new XRTableRow();
            row2.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row2);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.InitialWeight,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 182
            };
            cells.Add(cell2);
            cell2 = new XRTableCell()
            {
                Text = pipeFillingViewModel.FinalWeight,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 204
            };
            cells.Add(cell2);
            var MeasureUnit = pipeFillingViewModel.FinalWeight.Split(" ").Count() >= 2 ?
                pipeFillingViewModel.FinalWeight.Split(" ")?[1] : null;
            cell2 = new XRTableCell()
            {

                Text = MeasureUnit != null ? pipeFillingViewModel.DiffWeight.ToString() + " " + MeasureUnit : "NA",
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                WidthF = 216
            };
            cells.Add(cell2);
            var row = new XRTableRow();
            row.Font = new System.Drawing.Font("Arial", 9);
            row.Cells.AddRange(cells.ToArray());
            xRTable.Rows.Add(row);

        }
        private void FillTableInfoClientTank(ref XRTable xRTable, PipeFillingViewModel pipeFillingViewModel)
        {
            var ListCells = new List<XRTableCell>();
            var row = new XRTableRow();

            foreach (var x in pipeFillingViewModel.Customers)
            {
                ListCells = new List<XRTableCell>();
                var cell = new XRTableCell();
                XRPictureBox ImageState = new XRPictureBox();
                ImageState.Borders = DevExpress.XtraPrinting.BorderSide.None;
                if (x.state.HasValue && x.state == true)
                {
                    ImageState.ImageUrl = this._config["urlWebApp"] + "/img/waygreenIcon.png";
                    ImageState.HeightF = 40;
                    ImageState.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
   
                }
                else if (x.state.HasValue && x.state == false)
                {
                    ImageState.ImageUrl = this._config["urlWebApp"]  + "/img/wayredIcon.png";
                    ImageState.HeightF = 40;
                    ImageState.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
           
                }
                ///IMG
                cell = new XRTableCell()
                {
                    Text = "",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,

                    WidthF = 100,
                };
                cell.Controls.Add(ImageState);
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = x.Tank,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 129,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = x.Name,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 134,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = x.DeliveryNumber,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 136,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = !string.IsNullOrEmpty(x.AnalysisReport) ?
                           x.ReviewedBy + " " + (x.ReviewedDate.HasValue ? ((DateTime)x.ReviewedDate).ToString("yyyy-MM-dd HH:mm") : string.Empty) : "NA",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 136,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = !string.IsNullOrEmpty(x.AnalysisReport) ? x.AnalysisReport : "NA",
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 140,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = x.EmailsList,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 140,
                };
                ListCells.Add(cell);

                cell = new XRTableCell()
                {
                    Text = x.Notes,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    WidthF = 190,
                };
                ListCells.Add(cell);

                row = new XRTableRow();
                row.Cells.AddRange(ListCells.ToArray());
                xRTable.Rows.Add(row);

            }
        }
        private string GetValueReal(string paramName, string value)
        {
            decimal nVal;
            var nValue = value;
            if (paramName.ToLower().Trim() == "identidad")
            {
                decimal.TryParse((string.Format("{0:f2}", float.Parse(value))).Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out nVal);
                if (nVal == 1)
                    nValue = "Positiva";
                else
                    nValue = "Negativa";
            }
            else
            {
                double myNumber = double.Parse(value);
                double myNumberString = Math.Truncate(myNumber * 100) / 100;
                nValue = myNumberString.ToString();
            }
            return nValue.Replace(",", ".");
        }
    }
}
