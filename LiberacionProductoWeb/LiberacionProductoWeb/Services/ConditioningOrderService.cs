using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LiberacionProductoWeb.Models.IndentityModels;
using static Org.BouncyCastle.Math.EC.ECCurve;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using Org.BouncyCastle.Asn1.Pkcs;
using LiberacionProductoWeb.Models.External;
using Microsoft.Extensions.Logging;
using LiberacionProductoWeb.Models.CheckListViewModels;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.IO;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;

namespace LiberacionProductoWeb.Services
{
    public class ConditioningOrderService : IConditioningOrderService
    {
        private AppDbContext _context;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        private readonly IConfiguration _config;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbExternalContext _external;
        private readonly IMonitoringEquipmentRepository _monitoringEquipmentRepository;
        private readonly ILoteDistribuicionRepository _loteDistribuicionRepository;
        private readonly ILoteDistribuicionDetalleRepository _loteDistribuicionDetalleRepository;
        private readonly ILotesDistribuicionClienteRepository _lotesDistribuicionClienteRepository;
        private readonly IEquipmentProcessConditioningRepository _equipmentProcessConditioningRepository;
        private readonly IScalesFlowMetersRepository _scalesFlowMetersRepository;
        private readonly IPipelineClearanceOARepository _pipelineClearanceOARepository;
        private readonly IAnalyticalEquipamentRepository _analyticalEquipamentRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IHistoryStatesRepository _historyStatesRepository;
        private readonly IPipeFillingControlRepository _pipeFillingControlRepository;
        private readonly IPipeFillingRepository _pipeFillingRepository;
        private readonly IPipeFillingAnalysisRepository _pipeFillingAnalysisRepository;
        private readonly ICheckListPipeAnswerRepository _checkListPipeAnswerRepository;
        private readonly IAnalisisClienteRepository _analisisClienteRepository;
        private readonly IPlantasRepository _plantasRepository;
        private readonly IPipeFillingCustomerRepository _pipeFillingCustomerRepository;
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly ILogger<ConditioningOrderService> _logger;
        private readonly IPerformanceProcessConditioningRepository _performanceProcessConditioningRepository;
        private readonly IVariablesFileReaderService _variablesFileReaderService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IPipeFillingCustomersFilesRepository _pipeFillingCustomersFilesRepository;
        public bool WSMexeFuncionalidad;

        public ConditioningOrderService(AppDbContext context,
            IConditioningOrderRepository conditioningOrderRepository,
            IConfiguration config,
            IHttpContextAccessor httpContextAccessor,
            IProductionOrderRepository productionOrderRepository,
            UserManager<ApplicationUser> userManager,
            AppDbExternalContext external,
            IMonitoringEquipmentRepository monitoringEquipmentRepository,
            ILoteDistribuicionRepository loteDistribuicionRepository,
            ILoteDistribuicionDetalleRepository loteDistribuicionDetalleRepository,
            ILotesDistribuicionClienteRepository lotesDistribuicionClienteRepository,
            IEquipmentProcessConditioningRepository equipmentProcessConditioningRepository,
            IScalesFlowMetersRepository scalesFlowMetersRepository,
            IPipelineClearanceOARepository pipelineClearanceOARepository,
            IAnalyticalEquipamentRepository analyticalEquipamentRepository,
            IBatchDetailsRepository batchDetailsRepository,
            IHistoryStatesRepository historyStatesRepository,
            IPipeFillingControlRepository pipeFillingControlRepository,
            IPipeFillingRepository pipeFillingRepository,
            IPipeFillingAnalysisRepository pipeFillingAnalysisRepository,
            ICheckListPipeAnswerRepository checkListPipeAnswerRepository,
            IAnalisisClienteRepository analisisClienteRepository,
            IPlantasRepository plantasRepository,
            IPipeFillingCustomerRepository pipeFillingCustomerRepository,
            ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository,
            ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository,
            ILogger<ConditioningOrderService> logger,
            IPerformanceProcessConditioningRepository performanceProcessConditioningRepository,
            IVariablesFileReaderService variablesFileReaderService,
            IStringLocalizer<Resource> resource,
            IPipeFillingCustomersFilesRepository pipeFillingCustomersFilesRepository
            )
        {
            _context = context;
            _conditioningOrderRepository = conditioningOrderRepository;
            _productionOrderRepository = productionOrderRepository;
            _config = config;
            bool.TryParse(_config["FlagWSMexe:ServiceApiKey"], out WSMexeFuncionalidad);
            _conditioningOrderRepository = conditioningOrderRepository;
            _monitoringEquipmentRepository = monitoringEquipmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _external = external;
            _loteDistribuicionRepository = loteDistribuicionRepository;
            _loteDistribuicionDetalleRepository = loteDistribuicionDetalleRepository;
            _lotesDistribuicionClienteRepository = lotesDistribuicionClienteRepository;
            _equipmentProcessConditioningRepository = equipmentProcessConditioningRepository;
            _scalesFlowMetersRepository = scalesFlowMetersRepository;
            _pipelineClearanceOARepository = pipelineClearanceOARepository;
            _analyticalEquipamentRepository = analyticalEquipamentRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _historyStatesRepository = historyStatesRepository;
            _pipeFillingControlRepository = pipeFillingControlRepository;
            _pipeFillingRepository = pipeFillingRepository;
            _pipeFillingAnalysisRepository = pipeFillingAnalysisRepository;
            _checkListPipeAnswerRepository = checkListPipeAnswerRepository;
            _analisisClienteRepository = analisisClienteRepository;
            _plantasRepository = plantasRepository;
            _pipeFillingCustomerRepository = pipeFillingCustomerRepository;
            _checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _logger = logger;
            _performanceProcessConditioningRepository = performanceProcessConditioningRepository;
            this._variablesFileReaderService = variablesFileReaderService;
            _resource = resource;
            _pipeFillingCustomersFilesRepository = pipeFillingCustomersFilesRepository;
        }


        public async Task<List<ConditioningOrderViewModel>> GetAllAsync()
        {
            var conditioningOrdersDB = await _conditioningOrderRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<List<ConditioningOrderViewModel>>(conditioningOrdersDB);

            return mapped;
        }

        public async Task<List<EquipamentProcessConditioningViewModel>> GetTable4(string tournumber, int Id, DateTime? datePipelineClearance)
        {
            if (tournumber == "")
            {
                return new List<EquipamentProcessConditioningViewModel>();
            }

            try
            {
                var conO = await _conditioningOrderRepository.GetByIdAsync(Id);
                if (conO == null)
                {
                    return new List<EquipamentProcessConditioningViewModel>();
                }

                var op = await _productionOrderRepository.GetByIdAsync(conO.ProductionOrderId);

                List<EquipamentProcessConditioningViewModel> lst = new List<EquipamentProcessConditioningViewModel>();
                if (WSMexeFuncionalidad)
                {
                    ///info linde
                    var plantIden = _plantasRepository.GetAsync(x => x.Id_Planta == Convert.ToInt32(op.PlantId));
                    var ldetalledist = _analisisClienteRepository.GetAsync(x => x.IDENTIFICADOR == plantIden.FirstOrDefault().Identificador
                                                   && x.PRODUCT_ID == op.ProductId
                                                   && x.TOURNUMBER == tournumber && x.FEC_ALTA >= conO.CreatedDate).ToList();

                    //var ldetalledist = _analisisClienteRepository.GetAsync(x => x.IDENTIFICADOR == "M31"
                    //           && x.PRODUCT_ID == "OX"
                    //           && x.TOURNUMBER == tournumber && x.DESC_GRADO == "MEDICINAL" && x.FEC_ALTA >= conO.CreatedDate).ToList();
                    if (ldetalledist == null)
                        ldetalledist = new List<VwAnalisisCliente>();
                    ldetalledist = ldetalledist.Where(x => !string.IsNullOrEmpty(x.DESC_GRADO) && x.DESC_GRADO == "MEDICINAL").ToList();
                    if (!ldetalledist.Any())
                    {
                        return lst;
                    }

                    foreach (var item in ldetalledist.GroupBy(x => x.DESC_PIPA))
                    {
                        //var pipeId = item.FirstOrDefault()?.ID_LOTEPIPA?.Split('-')[3];
                        //if (!lst.Any(x => x.TourNumber == tournumber && x.PipeNumber == pipeId))
                        //{
                        var fechaLoteString = item.FirstOrDefault()?.ID_LOTEPIPA?.Split('-')[2];
                        var fechaLoteMonth = int.Parse(fechaLoteString.Substring(0, 2));
                        var fechaLoteDay = int.Parse(fechaLoteString.Substring(2, 2));
                        var fechaLoteYear = int.Parse(fechaLoteString.Substring(4, 2)) + 2000;

                        var horaLoteString = item.FirstOrDefault()?.ID_LOTEPIPA?.Split('-')[4];
                        var fechaHora = int.Parse(horaLoteString.Substring(0, 2));
                        var fechaMinutos = int.Parse(horaLoteString.Substring(2, 2));

                        var fechaLote = new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
                        fechaLote = fechaLote.AddHours(fechaHora);
                        fechaLote = fechaLote.AddMinutes(fechaMinutos);

                        var equipment = new EquipamentProcessConditioningViewModel
                        {
                            TourNumber = tournumber,
                            PipeNumber = item.Key,
                            DistributionBatchDate = fechaLote
                        };

                        if (item.FirstOrDefault()?.DESC_GRADO != "MEDICINAL")
                        {
                            equipment.ErrorMessage = "NO MEDICINAL";
                        }
                        else if (fechaLote <= datePipelineClearance)
                        {
                            equipment.ErrorMessage = "FECHA INVALIDA";
                        }

                        lst.Add(equipment);
                        //}
                    }

                    return lst;
                }
                else
                {
                    lst = new List<EquipamentProcessConditioningViewModel>
                    {
                        new EquipamentProcessConditioningViewModel { Bay = "1", HoseDownload = "45", Hosefill = "INST-01", Bomb = "RP-01", TourNumber = "123456", PipeNumber = "11111", Notes = "NA" },
                        new EquipamentProcessConditioningViewModel { Bay = "2", HoseDownload = "46", Hosefill = "INST-02", Bomb = "RP-02", TourNumber = "123457", PipeNumber = "11111", Notes = "NA" },
                        new EquipamentProcessConditioningViewModel { Bay = "2", HoseDownload = "46", Hosefill = "INST-02", Bomb = "RP-02", TourNumber = "123457", PipeNumber = "11112", Notes = "NA", ErrorMessage = "NO MEDICINAL" },
                        new EquipamentProcessConditioningViewModel { Bay = "2", HoseDownload = "46", Hosefill = "INST-02", Bomb = "RP-02", TourNumber = "123457", PipeNumber = "11113", Notes = "NA", ErrorMessage = "FECHA INVALIDA" },
                        new EquipamentProcessConditioningViewModel { Bay = "3", HoseDownload = "47", Hosefill = "INST-03", Bomb = "RP-03", TourNumber = "00123458", PipeNumber = "11111", Notes = "NA" },
                        new EquipamentProcessConditioningViewModel { Bay = "1", HoseDownload = "48", Hosefill = "INST-01", Bomb = "RP-02", TourNumber = "123459", PipeNumber = "11111", Notes = "NA" },
                    };
                    return lst.Where(x => x.TourNumber == tournumber).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en obtener Numero de lote " + ex);
                throw ex;
            }

            return new List<EquipamentProcessConditioningViewModel>();
        }

        public async Task<ConditioningOrderViewModel> GetByProductionOrderIdAsync(int Id)
        {
            var model = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == Id)).FirstOrDefault();

            if (model == null)
            {
                return new ConditioningOrderViewModel();
            }

            return await GetByIdAsync(model.Id);
        }

        public async Task<ConditioningOrderViewModel> GetByIdAsync(int Id, bool refresh = false, string user = null)
        {
            var ObjectDB = await _conditioningOrderRepository.GetByIdAsync(Id);
            if (ObjectDB == null)
            {
                return null;
            }
            var mapped = ObjectMapper.Mapper.Map<ConditioningOrderViewModel>(ObjectDB);
            mapped.State = mapped.State;
            mapped.CertificateId = ObjectDB.CertificateId;
            mapped.FooterCertificateId = ObjectDB.FooterCertificateId;
            var op = await _productionOrderRepository.GetByIdAsync(mapped.ProductionOrderId);
            mapped.PlantId = ObjectDB.PlantId;
            mapped.ProductId = ObjectDB.ProductId;
            var catproductos = _context.productCatalogs.Where(x => x.ProductId == op.ProductId).FirstOrDefault();
            var fechacaducidad = Convert.ToDateTime(op.CreatedDate);
            fechacaducidad = fechacaducidad.AddDays(90);
            var lote = _context.BatchDetails.Where(x => x.ProductionOrderId == op.Id).FirstOrDefault();

            mapped.ContainerPrimary = "Pipa";
            mapped.CreateDate = DateTime.Now;
            mapped.DueDate = fechacaducidad;
            mapped.LotProd = lote.Number;
            mapped.Presentation = ObjectDB.Presentation ?? catproductos.Presentation;
            mapped.AnalyticEquipmentList = await GetTable1(mapped);
            mapped.ScalesFlowMeters = await ScalesFlowMeters(mapped);
            mapped.ScalesflowsList = await GetTable2(mapped);
            mapped.EquipamentProcessesList = await GetTable4(mapped);
            mapped.PipeClearancesList = await GetPipeClearanceOAViewModels(mapped);

            mapped.ObservationsList = GetObservationHistories(mapped);
            mapped.StatesList = await GetStatesHistories(mapped);
            //val Cancelled botton open OA
            var Cancelled = mapped.StatesList.Where(x => x.State == ConditioningOrderStatus.Cancelled.Value).FirstOrDefault()?.Date;
            mapped.IsCancelled = Cancelled.HasValue ? Cancelled <= DateTime.Now.AddDays(int.Parse(_config["DaysIsCancelled"])) : false;
            mapped.ShowPanelSteps = "panel-collapse collapse overflow-hidden";

            mapped.PipelineClearanceOA = mapped.PipeClearancesList.Where(x => x.InCompliance.HasValue && x.InCompliance.Value).FirstOrDefault();
            if (mapped.PipelineClearanceOA == null)
            {
                var hasPendingReview = mapped.PipeClearancesList.Where(x => x.InCompliance.HasValue && !x.InCompliance.Value && (string.IsNullOrEmpty(x.ReviewedBySecond) || !x.ReviewedDateSecond.HasValue)).Any();

                mapped.PipelineClearanceOA = new PipeClearanceOAViewModel
                {
                    HasPendingReview = hasPendingReview
                };
            }

            mapped.PipeFillingControl = await GetPipeFillingControls(mapped, refresh, user);
            mapped.PerformanceList = await GetPerformanceProcessConditionings(mapped);

            if (WSMexeFuncionalidad)
            {
                var plantas = _external.LPM_VW_PLANTAS.Where(x => x.Id_Planta == Convert.ToInt32(op.PlantId)).FirstOrDefault();
                var producto = _external.LPM_VW_PRODUCTOS.Where(x => x.Product_Id == op.ProductId).FirstOrDefault();
                var tanque = _external.LPM_VW_TANQUES.Where(x => x.Descripcion == op.TankId).FirstOrDefault();

                mapped.Plant = plantas.Descripcion;
                mapped.Product = producto.Product_Name;
                mapped.Tank = tanque.Descripcion;
                ///
                mapped.Location = plantas.Identificador;
                mapped.ProductName = producto.Product_Id;

            }
            else
            {
                mapped.Plant = op.PlantId;
                mapped.Product = op.ProductId;
                mapped.Tank = op.TankId;
                mapped.Location = "1";
                mapped.ProductName = "OX";
            }

            return mapped;
        }

        public async Task<ConditioningOrderViewModel> AddAsync(ConditioningOrder entity)
        {
            var save = await _conditioningOrderRepository.AddAsync(entity);
            var mapped = ObjectMapper.Mapper.Map<ConditioningOrderViewModel>(save);
            return mapped;
        }

        public async Task<List<EquipamentProcessConditioningViewModel>> GetPipesXTournumber(int Id)
        {
            List<EquipamentProcessConditioningViewModel> lista = new List<EquipamentProcessConditioningViewModel>();

            try
            {
                EquipamentProcessConditioningViewModel eq = new EquipamentProcessConditioningViewModel();
                var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == Id);
                foreach (var item in epc)
                {
                    eq.TourNumber = item.TourNumber;
                    var epc2 = await _equipmentProcessConditioningRepository.GetAsync(x => x.TourNumber == eq.TourNumber);
                    if (epc2.Count > 0)
                    {
                        foreach (var itemx in epc2)
                        {
                            if (item.TourNumber == itemx.TourNumber)
                            {
                                lista.Add(new EquipamentProcessConditioningViewModel
                                {
                                    TourNumber = item.TourNumber,
                                    PipeNumber = item.PipeNumber.ToString(),
                                    ReviewedBy = itemx.ReviewedBy,
                                    ReviewedDate = itemx.ReviewedDate,
                                    Notes = itemx.Notes,

                                });
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en obtener Numero de lote " + ex);
            }

            return lista;
        }

        private async Task<List<PipeFillingControlViewModel>> GetPipeFillingControls(ConditioningOrderViewModel model, bool refresh = false, string user = null)
        {
            List<PipeFillingControlViewModel> listpipeFillingControls = new List<PipeFillingControlViewModel>();
            var pipeFillins = new List<PipeFillingViewModel>();
            try
            {
                var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
                var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                var tournumberList = epc.Select(x => x.TourNumber).Distinct();

                var ldist = new List<VwLotesDistribuicion>();
                var ldetalledistViewModel = new List<VwAnalisisClienteViewModel>();
                var ldetalledistViewModelTemp = new List<VwAnalisisClienteViewModel>();
                if (WSMexeFuncionalidad)
                {
                }
                else
                {
                    var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                    if (ldetalledistDB.Any())
                    {
                        var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                        if (control.Any())
                        {
                            foreach (var itemz in control)
                            {

                                var pipeFilling = await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == itemz.Id);
                                foreach (var itemy in pipeFilling)
                                {
                                    foreach (var item in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                                    {
                                        foreach (var itemx in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                                        {
                                            if (item.ParameterName == itemx.ParameterName && item.Unique == itemx.Unique)
                                            {

                                                ldetalledistViewModel.Add(
                                                    new VwAnalisisClienteViewModel
                                                    {
                                                        ANALISIS_FIN = decimal.Parse(itemx.ValueReal),
                                                        ANALISIS_FINAL = item.ValueExpected,
                                                        ANALISIS_INI = decimal.Parse(item.ValueReal),
                                                        ANALISIS_INICIAL = itemx.ValueExpected,
                                                        DESC_ANALIZADOR = "",
                                                        DESC_PARAMETRO = item.ParameterName,
                                                        DESC_UM = itemx.MeasureUnit,
                                                        ID_LOTEPIPA = item.DistributionBatch,
                                                        ESPECIFICACION = itemx.ValueExpected,
                                                        ID_PLANTA = Convert.ToInt32(op.PlantId),
                                                        PRODUCT_ID = op.ProductId,
                                                        TOURNUMBER = itemz.TourNumber,
                                                        TRIPNUMBER = 123,
                                                    });

                                            }
                                        }
                                    }
                                    ldist.Add(new VwLotesDistribuicion
                                    {
                                        ID_LOTEPIPA = itemy.DistributionBatch,
                                        PESO_INI = itemy.InitialWeight,
                                        PESO_FIN = itemy.FinalWeight,
                                        NOMBRE = itemy.AnalyzedBy,
                                        FECHA_INI = itemy.InitialAnalyzedDate,
                                        FECHA_FIN = itemy.FinalAnalyzedDate,
                                        ID_LOTE = itemy.DistributionBatch?.Split('-')[3]
                                    });
                                }
                            }
                        }
                    }
                    else
                    {

                        ldetalledistViewModelTemp = new List<VwAnalisisClienteViewModel> {
                            // Tour Number 123456 - Pipe PRXM458
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5",

                            },
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Humedad",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Identidad",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "CO",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "CO2",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "O2",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            // Tour Number 123456 - Pipe PRXM459
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM459-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            // Tour Number 123456 - Pipe PRXM460
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM460-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123456",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            // Tour Number 123457 - Pipe PRXM458
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123457",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            // Tour Number 123458 - Pipe PRXM458
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "00123458",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            },
                            // Tour Number 123459 - Pipe PRXM458
                            new VwAnalisisClienteViewModel {
                                ANALISIS_FIN = 99.9m,
                                ANALISIS_FINAL = ">= 99.5",
                                ANALISIS_INI = 99.8m,
                                ANALISIS_INICIAL = ">= 99.5",
                                DESC_ANALIZADOR = "",
                                DESC_PARAMETRO = "Pureza",
                                DESC_UM = "% v/v",
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                ID_PLANTA = 1,
                                PRODUCT_ID = "O1",
                                TOURNUMBER = "123459",
                                TRIPNUMBER = 123,
                                ESPECIFICACION = ">= 99.5"
                            }
                        };

                        foreach (var item in ldetalledistViewModelTemp)
                        {
                            Guid Unique = Guid.NewGuid();
                            ldetalledistViewModel.Add(new VwAnalisisClienteViewModel
                            {
                                ANALISIS_FIN = item.ANALISIS_FIN,
                                ANALISIS_FINAL = item.ANALISIS_FINAL,
                                ANALISIS_INI = item.ANALISIS_INI,
                                ANALISIS_INICIAL = item.ANALISIS_INICIAL,
                                DESC_ANALIZADOR = item.DESC_ANALIZADOR,
                                DESC_PARAMETRO = item.DESC_PARAMETRO,
                                DESC_UM = item.DESC_UM,
                                ID_LOTEPIPA = item.ID_LOTEPIPA,
                                ID_PLANTA = item.ID_PLANTA,
                                PRODUCT_ID = item.PRODUCT_ID,
                                TOURNUMBER = item.TOURNUMBER,
                                TRIPNUMBER = item.TRIPNUMBER,
                                ESPECIFICACION = item.ESPECIFICACION,
                                Unique = Unique.ToString()
                            });
                        }

                        ldist = new List<VwLotesDistribuicion> {
                            new VwLotesDistribuicion {
                                ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                                PESO_INI = "3.5",
                                PESO_FIN = "18.9",
                                NOMBRE = "Martha Elena Herrera",
                                FECHA_INI = DateTime.Now.AddDays(-1),
                                FECHA_FIN = DateTime.Now.AddHours(-5),
                                ID_LOTE = "OX-M31"
                            },
                            new VwLotesDistribuicion {
                                ID_LOTEPIPA = "OX-M31-032620-PRXM459-0107",
                                PESO_INI = "3.5",
                                PESO_FIN = "18.9",
                                NOMBRE = "Martha Elena Herrera",
                                FECHA_INI = DateTime.Now.AddDays(-1),
                                FECHA_FIN = DateTime.Now.AddHours(-5),
                                ID_LOTE = "OX-M31"
                            },
                            new VwLotesDistribuicion {
                                ID_LOTEPIPA = "OX-M31-032620-PRXM460-0107",
                                PESO_INI = "3.5",
                                PESO_FIN = "18.9",
                                NOMBRE = "Martha Elena Herrera",
                                FECHA_INI = DateTime.Now.AddDays(-1),
                                FECHA_FIN = DateTime.Now.AddHours(-5),
                                ID_LOTE = "OX-M31"
                            }
                        };
                    }
                }
                if (!refresh)
                    pipeFillins = (await GetAnalisisCliente(model.Id, op.ProductId, op.PlantId, model.CreatedDate, tournumberList.ToList())).ToList();
                else
                    pipeFillins = (await GetAnalisisClienteUpdated(model.Id, op.ProductId, op.PlantId, model.CreatedDate, tournumberList.ToList(), user, op.TankId)).ToList();
                foreach (var tournumber in tournumberList)
                {
                    var pipeDetailsList = pipeFillins.Where(x => x.TourNumber == tournumber).ToList();

                    listpipeFillingControls.Add(new PipeFillingControlViewModel
                    {
                        TourNumber = tournumber,
                        PipesList = pipeDetailsList
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetPipeFillingControls " + ex);
            }

            return listpipeFillingControls;
        }

        private async Task<List<PipeFillingCustomerViewModel>> GetCustomersByPipeNumber(string pipeNumber, ConditioningOrderViewModel model, string tournumber)
        {
            var customers = new List<PipeFillingCustomerViewModel>();

            if (WSMexeFuncionalidad)
            {
                var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id && x.TourNumber == tournumber);
                if (control.Any())
                {
                    foreach (var itemz in control)
                    {

                        var info = (await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == model.Id && x.DistributionBatch.Contains(pipeNumber) && x.TourNumber == tournumber))
                            .Select(x => new PipeFillingCustomerViewModel
                            {
                                Id = x.Id,
                                Tank = x.Tank,
                                Name = x.Name,
                                DeliveryNumber = x.DeliveryNumber,
                                Notes = x.Notes,
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                EmailsList = x.EmailsList,
                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio : null,
                                InCompliance = x.InCompliance != null ? true : false,
                                PipeFillingControlId = itemz.Id,
                                Folio = x.Folio
                            }).ToList();

                        customers.AddRange(info);

                        if (!info.Any())
                        {
                            var customersPerPipe = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == pipeNumber)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0
                                            }).ToList();

                            customers.AddRange(customersPerPipe);
                        }
                    }
                }
                else
                {
                    var customersPerPipe = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == pipeNumber)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0
                                            }).ToList();

                    customers.AddRange(customersPerPipe);
                }
            }
            else
            {
                var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                if (control.Any())
                {
                    foreach (var itemz in control)
                    {
                        var info = (await _pipeFillingCustomerRepository.GetAsync(x => x.DistributionBatch.Contains(pipeNumber)
                         && x.TourNumber == itemz.TourNumber))
                            .Select(x => new PipeFillingCustomerViewModel
                            {
                                Tank = x.Tank,
                                Name = x.Name,
                                DeliveryNumber = x.DeliveryNumber,
                                Notes = x.Notes,
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                EmailsList = x.EmailsList,
                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio : null,
                                InCompliance = x.InCompliance != null ? true : false,
                                PipeFillingControlId = itemz.Id
                            }).ToList();

                        customers.AddRange(info);

                    }
                }
                else
                {
                    customers = new List<PipeFillingCustomerViewModel> {
                    new PipeFillingCustomerViewModel {
                        Tank = "M31-LOX-01",
                        Name = "Instituto Mexicano del Seguro Social",
                        DeliveryNumber = "123456",
                        InCompliance = true,
                        TankId = "LOX-01",
                        PlantIdentificador="M31",
                        ProductId = "OX",
                        PipeFillingControlId = 0
                    }
                };
                }
            }

            return customers;
        }

        private async Task<List<PipeFillingCustomerViewModel>> GetCustomers(ConditioningOrderViewModel model)
        {
            var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
            var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            var tournumberList = epc.Select(x => x.TourNumber).Distinct();

            var ldetalledist = new List<VwLotesDistribuicionDetalle>();
            if (WSMexeFuncionalidad)
            {
                var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                var pipeControlDB = (await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id)).FirstOrDefault()?.Id;
                if (ldetalledistDB.Any())
                {
                    var pipeFilling = await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == pipeControlDB);
                    foreach (var itemy in pipeFilling)
                    {
                        foreach (var item in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                        {
                            foreach (var itemx in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                            {
                                if (item.ParameterName == itemx.ParameterName)
                                {

                                    ldetalledist.Add(
                                        new VwLotesDistribuicionDetalle
                                        {
                                            ANALISIS_FIN = decimal.Parse(itemx.ValueReal),
                                            ANALISIS_FINAL = item.ValueExpected,
                                            ANALISIS_INI = decimal.Parse(item.ValueReal),
                                            ANALISIS_INICIAL = itemx.ValueExpected,
                                            DESC_ANALIZADOR = "",
                                            DESC_PARAMETRO = item.ParameterName,
                                            DESC_UM = itemx.MeasureUnit,
                                            ID_LOTEPIPA = item.DistributionBatch,
                                            ID_PLANTA = Convert.ToInt32(op.PlantId),
                                            PRODUCT_ID = op.ProductId,
                                            TOURNUMBER = "123456",
                                            TRIPNUMBER = 123,
                                        });

                                }
                            }
                        }

                    }
                }
                else
                {
                    ldetalledist = _loteDistribuicionDetalleRepository.GetAsync(x => x.ID_PLANTA == Convert.ToInt32(op.PlantId)
                                                            && x.PRODUCT_ID == op.ProductId
                                                            && tournumberList.Contains(x.TOURNUMBER))
                                                .ToList();
                }
            }
            else
            {

                var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                var pipeControlDB = (await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id)).FirstOrDefault()?.Id;
                if (ldetalledistDB.Any())
                {
                    var pipeFilling = await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == pipeControlDB);
                    foreach (var itemy in pipeFilling)
                    {
                        foreach (var item in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                        {
                            foreach (var itemx in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value && x.PipeNumber == itemy.PipeNumber))
                            {
                                if (item.ParameterName == itemx.ParameterName)
                                {

                                    ldetalledist.Add(
                                        new VwLotesDistribuicionDetalle
                                        {
                                            ANALISIS_FIN = decimal.Parse(itemx.ValueReal),
                                            ANALISIS_FINAL = item.ValueExpected,
                                            ANALISIS_INI = decimal.Parse(item.ValueReal),
                                            ANALISIS_INICIAL = itemx.ValueExpected,
                                            DESC_ANALIZADOR = "",
                                            DESC_PARAMETRO = item.ParameterName,
                                            DESC_UM = itemx.MeasureUnit,
                                            ID_LOTEPIPA = item.DistributionBatch,
                                            ID_PLANTA = Convert.ToInt32(op.PlantId),
                                            PRODUCT_ID = op.ProductId,
                                            TOURNUMBER = "123456",
                                            TRIPNUMBER = 123,
                                        });

                                }
                            }
                        }

                    }
                }
                else
                {
                    ldetalledist = new List<VwLotesDistribuicionDetalle> {
                        // Tour Number 123456 - Pipe PRXM458
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Humedad",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Identidad",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "CO",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "CO2",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "O2",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        // Tour Number 123456 - Pipe PRXM459
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM459-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        // Tour Number 123456 - Pipe PRXM460
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM460-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123456",
                            TRIPNUMBER = 123,
                        },
                        // Tour Number 123457 - Pipe PRXM458
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123457",
                            TRIPNUMBER = 123,
                        },
                        // Tour Number 123458 - Pipe PRXM458
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "00123458",
                            TRIPNUMBER = 123,
                        },
                        // Tour Number 123459 - Pipe PRXM458
                        new VwLotesDistribuicionDetalle {
                            ANALISIS_FIN = 99.9m,
                            ANALISIS_FINAL = ">= 99.5",
                            ANALISIS_INI = 99.8m,
                            ANALISIS_INICIAL = ">= 99.5",
                            DESC_ANALIZADOR = "",
                            DESC_PARAMETRO = "Pureza",
                            DESC_UM = "% v/v",
                            ID_LOTEPIPA = "OX-M31-032620-PRXM458-0107",
                            ID_PLANTA = 1,
                            PRODUCT_ID = "O1",
                            TOURNUMBER = "123459",
                            TRIPNUMBER = 123,
                        }
                    };
                }
            }

            var customers = new List<PipeFillingCustomerViewModel>();
            foreach (var tournumber in tournumberList)
            {
                var pipeList = ldetalledist.Where(x => x.TOURNUMBER == tournumber).Select(x => x.ID_LOTEPIPA).Distinct();

                var pipeDetailsList = new List<PipeFillingViewModel>();
                foreach (var pipe in pipeList)
                {
                    if (WSMexeFuncionalidad)
                    {
                        var customersPerPipe = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == pipe)
                                                    .Select(x => new PipeFillingCustomerViewModel
                                                    {
                                                        Tank = x.TANQUE_CLIENTE,
                                                        Name = x.CLIENTE,
                                                        DeliveryNumber = x.NO_ORDEN_ENTREGA
                                                    }).ToList();

                        customers.AddRange(customersPerPipe);
                    }
                    else
                    {
                        customers = new List<PipeFillingCustomerViewModel> {
                            new PipeFillingCustomerViewModel {
                                Tank = "M31-LOX-01",
                                Name = "Instituto Mexicano del Seguro Social"
                            }
                        };
                    }
                }
            }

            return customers;
        }

        private async Task<List<AnalyticEquipmentViewModel>> GetTable1(ConditioningOrderViewModel model)
        {
            List<AnalyticEquipmentViewModel> analyticEquipmentList = new List<AnalyticEquipmentViewModel>();

            var analyticalEquipamentDB = await _analyticalEquipamentRepository.GetAsync(x => x.ConditioningOrderId == model.Id);

            if (analyticalEquipamentDB != null && analyticalEquipamentDB.Any())
            {
                analyticEquipmentList = ObjectMapper.Mapper.Map<List<AnalyticEquipmentViewModel>>(analyticalEquipamentDB);
            }
            else
            {
                var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);

                var general = _context.generalCatalogs.Where(x =>
                    x.PlantId == op.PlantId
                    && x.ProductId == op.ProductId
                    && x.TankId == op.TankId
                    && x.VariableClasification == VariableClasificationType.CriticalQualityAttribute.Value).ToList();

                List<MonitoringEquipmentViewModel> lsmonitoringEquipment = new List<MonitoringEquipmentViewModel>();
                foreach (var item in general)
                {
                    var monitoringEquipmentEntity = await _monitoringEquipmentRepository.GetAsync(x => x.ProductionOrderId == model.ProductionOrderId);
                    if (monitoringEquipmentEntity.Any())
                    {
                        foreach (var itemx in monitoringEquipmentEntity)
                        {
                            if (item.CodeTool == itemx.Code && item.DescriptionTool == item.DescriptionTool)
                            {
                                analyticEquipmentList.Add(new AnalyticEquipmentViewModel
                                {
                                    Code = item.CodeTool,
                                    Description = item.DescriptionTool
                                });
                            }
                        }

                    }
                }
                ///delete duplicates
                var result = from o in analyticEquipmentList
                             group o by (o.Code, o.Description) into g
                             select new AnalyticEquipmentViewModel
                             {
                                 Code = g.Key.Code,
                                 Description = g.Key.Description

                             };
                analyticEquipmentList = result.ToList();
            }
            return analyticEquipmentList;
        }

        public async Task<List<ScalesflowsViewModel>> GetTable2(ConditioningOrderViewModel model)
        {
            List<ScalesflowsViewModel> scaleflowsList = new List<ScalesflowsViewModel>();

            var scaleflowsDB = await _scalesFlowMetersRepository.GetAsync(x => x.ConditioningOrderId == model.Id);

            if (scaleflowsDB != null && scaleflowsDB.Any())
            {
                scaleflowsList = ObjectMapper.Mapper.Map<List<ScalesflowsViewModel>>(scaleflowsDB);
            }
            //else
            //{
            //    var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
            //    var general = _context.generalCatalogs.Where(x => x.PlantId == op.PlantId && x.ProductId == op.ProductId && x.WeighingMachine != "").ToList();

            //    foreach (var item in general.Where(x => x.WeighingMachine != null))
            //    {
            //        ScalesflowsViewModel scale = new ScalesflowsViewModel();
            //        scale.Description = item.WeighingMachine;

            //        scaleflowsList.Add(scale);
            //    }
            //}

            return scaleflowsList;
        }

        private async Task<List<SelectListItem>> ScalesFlowMeters(ConditioningOrderViewModel model)
        {
            List<SelectListItem> scaleflowsList = new List<SelectListItem>();
            var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
            var general = _context.generalCatalogs.Where(x => x.PlantId == op.PlantId && x.ProductId == op.ProductId && x.WeighingMachine != "").ToList();

            foreach (var item in general.Where(x => x.WeighingMachine != null))
            {
                SelectListItem scale = new SelectListItem();
                scale.Text = item.WeighingMachine;
                scale.Value = item.WeighingMachine;
                scaleflowsList.Add(scale);
            }

            return scaleflowsList;
        }

        private async Task<List<EquipamentProcessConditioningViewModel>> GetTable4(ConditioningOrderViewModel model)
        {
            var equipamentsList = new List<EquipamentProcessConditioningViewModel>();

            var equipamentsDB = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            if (equipamentsDB != null && equipamentsDB.Any())
            {
                equipamentsList = ObjectMapper.Mapper.Map<List<EquipamentProcessConditioningViewModel>>(equipamentsDB);
            }

            return equipamentsList;
        }

        private async Task<List<PerformanceProcessConditioningViewModel>> GetPerformanceProcessConditionings(ConditioningOrderViewModel model)
        {
            List<PerformanceProcessConditioningViewModel> lstperformanceProcessConditionings = new List<PerformanceProcessConditioningViewModel>();
            if (WSMexeFuncionalidad)
            {
                var performanceDB = await _performanceProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                if (performanceDB.Any())
                {
                    foreach (var item in performanceDB)
                    {
                        lstperformanceProcessConditionings.Add(new PerformanceProcessConditioningViewModel
                        {
                            TourNumber = item.TourNumber,
                            PipeNumber = item.PipeNumber,
                            SizeLote = item.SizeLote,
                            TotalTons = item.TotalTons,
                            DifTons = item.DifTons,
                            ReviewedBy = item.ReviewedBy,
                            ReviewedDate = item.ReviewedDate,
                            Notes = item.Notes
                        });
                    }
                }
                else
                {
                    var performanceItem = new PerformanceProcessConditioningViewModel();
                    var totalDiffWeight = 0m;

                    var batchDetails = await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == model.ProductionOrderId);

                    foreach (var item in batchDetails)
                    {
                        foreach (var itemx in model.PipeFillingControl)
                        {
                            foreach (var itemy in itemx.PipesList)
                            {
                                totalDiffWeight += itemy.DiffWeight;
                                var difTons = item.Size - totalDiffWeight;

                                performanceItem.TourNumber = itemx.TourNumber;
                                performanceItem.PipeNumber = itemy.PipeNumber;
                                performanceItem.TotalTons = totalDiffWeight.ToString();
                                performanceItem.DifTons = difTons.ToString();
                                performanceItem.Notes = "NA";
                            }
                        }
                        performanceItem.SizeLote = item.Size.ToString();
                    }

                    lstperformanceProcessConditionings = new List<PerformanceProcessConditioningViewModel>();
                    lstperformanceProcessConditionings.Add(performanceItem);
                }

                return lstperformanceProcessConditionings;
            }
            else
            {
                var performanceDB = await _performanceProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                if (performanceDB.Any())
                {
                    foreach (var item in performanceDB)
                    {
                        lstperformanceProcessConditionings.Add(new PerformanceProcessConditioningViewModel
                        {
                            TourNumber = item.TourNumber,
                            PipeNumber = item.PipeNumber,
                            SizeLote = item.SizeLote,
                            TotalTons = item.TotalTons,
                            DifTons = item.DifTons,
                            ReviewedBy = item.ReviewedBy,
                            ReviewedDate = item.ReviewedDate,
                            Notes = item.Notes
                        });
                    }
                }
                else
                {
                    lstperformanceProcessConditionings.Add(new PerformanceProcessConditioningViewModel
                    {
                        TourNumber = "123456",
                        PipeNumber = "123",
                        SizeLote = "356,18",
                        TotalTons = "15,14",
                        DifTons = "340,78",
                        Notes = "NA"

                    });
                }

                return lstperformanceProcessConditionings;
            }
        }

        private List<ObservationHistoryViewModel> GetObservationHistories(ConditioningOrderViewModel model)
        {
            List<ObservationHistoryViewModel> lstobservationHistories = new List<ObservationHistoryViewModel>();
            lstobservationHistories.Add(new ObservationHistoryViewModel
            {
                Section = "Despeje de Línea",
                Author = "Juan Antonio Zamora",
                Date = DateTime.Now,
                Notes = "NA"
            });

            return lstobservationHistories;
        }

        private async Task<List<StatesHistoryViewModel>> GetStatesHistories(ConditioningOrderViewModel model)
        {
            List<StatesHistoryViewModel> lststatesHistories = new List<StatesHistoryViewModel>();
            var userDb = _userManager.Users;
            var historyDB = from states in await _historyStatesRepository.GetAsync(x => x.ProductionOrderId == model.Id && x.Type == HistoryStateType.OrdenAcondicionamiento.Value)
                            select new HistoryStates
                            {
                                ProductionOrderId = states.ProductionOrderId,
                                User = userDb.Where(x => x.Id == states.User).Select(x => x.NombreUsuario).FirstOrDefault(),
                                Date = states.Date,
                                State = states.State,
                                Type = states.Type
                            };
            if (historyDB.Any())
            {
                var validStatus = new List<string>()
                {
                    ConditioningOrderStatus.InProgress.Value,
                    ConditioningOrderStatus.Released.Value
                };
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<StatesHistoryViewModel>>(historyDB.Where(x => validStatus.Contains(x.State)));
                return mapped.OrderBy(x => x.Date).ToList();
            }
            return lststatesHistories;
        }

        private async Task<List<PipeClearanceOAViewModel>> GetPipeClearanceOAViewModels(ConditioningOrderViewModel model)
        {
            List<PipeClearanceOAViewModel> lstpipeClearanceOAViewModels = new List<PipeClearanceOAViewModel>();

            var pipeClearanceOA = await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == model.Id);

            if (pipeClearanceOA != null)
            {
                lstpipeClearanceOAViewModels = pipeClearanceOA.Select(x => new PipeClearanceOAViewModel
                {
                    Id = x.Id,
                    ConditioningOrderId = x.ConditioningOrderId,
                    Bill = x.Bill,
                    Notes = x.Notes,
                    ReviewedBy = x.ReviewedBy,
                    ReviewedDate = x.ReviewedDate,
                    InCompliance = x.InCompliance,
                    Activitie = x.Activitie,
                }).ToList();
            }

            return lstpipeClearanceOAViewModels;
        }

        private Decimal DiffWeight(string InitialWeight, string FinalWeight)
        {
            int DifWeight = 0;
            try
            {
                if (InitialWeight != null && FinalWeight != null)
                {
                    string ini = string.Empty, fin = string.Empty;
                    int IniInt = 0, finInt = 0;
                    ini = InitialWeight;
                    ini = ini.ToUpper().Replace("PULG", "").Trim();
                    ini = ini.ToUpper().Replace("KG", "").Trim();
                    ini = ini.ToUpper().Replace("PSIG", "").Trim();

                    fin = FinalWeight;
                    fin = fin.ToUpper().Replace("PULG", "").Trim();
                    fin = fin.ToUpper().Replace("KG", "").Trim();
                    fin = fin.ToUpper().Replace("PSIG", "").Trim();

                    int.TryParse(ini, out IniInt);
                    int.TryParse(fin, out finInt);
                    DifWeight = finInt - IniInt;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al calcular DiffWeight " + ex);
            }
            return DifWeight;
        }

        private bool? InCompliance(string IdLotePipa)
        {
            bool? InComplianceResult = null;
            if (!string.IsNullOrEmpty(IdLotePipa))
            {
                if (WSMexeFuncionalidad)
                {
                    var DbInfo = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == IdLotePipa).Any() ?
                                           _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == IdLotePipa).ToList() : null;
                    if (DbInfo != null)
                        InComplianceResult = DbInfo.FirstOrDefault().CALIDAD == "CUMPLE" ? true : false;
                }
                else
                {
                    InComplianceResult = true;
                }
            }
            return InComplianceResult;
        }

        public async Task<string> GetBillAsignature(ConditioningOrderViewModel model)
        {
            _logger.LogInformation("Genera folio para la orden de acondicionamiento" + model.Id);
            var customer = await this._pipeFillingCustomerRepository.GetByParametersAsync(model.Id, model.TourNumber, model.DeliveryNumber, model.Name);
            if (string.IsNullOrEmpty(customer.Folio))
            {
                var folios = await this._pipeFillingCustomerRepository.FindLastFolioByParametersAsync(model.Location, model.Product, model.Tank);
                var maxFolio = folios != null && folios.Any() ? folios.Select(x => int.Parse(x)).Max() : 0;
                customer.Folio = (++maxFolio).ToString().PadLeft(4, '0');
                _logger.LogInformation("Genera folio customer.Folio " + customer.Folio + " " + model.Id + " " + model.TourNumber + " " + model.DeliveryNumber + " " + model.Name);
                DateTime date = DateTime.Now;
                string lastTwoDigitsOfYear = date.ToString("yy");
                customer.AnalysisReport = model.Location + "-" + model.Product + "-" + model.Tank + "-" + customer.Folio + lastTwoDigitsOfYear;
                await this._pipeFillingCustomerRepository.UpdateAsync(customer);
            }
            else
            {
                _logger.LogInformation("ya tenia folio   customer.Folio " + customer.Folio + " " + model.Id + " " + model.TourNumber + " " + model.DeliveryNumber + " " + model.Name);
            }
            return customer.Folio;
        }

        async Task IConditioningOrderService.DeleteTourNumber(string number, int id)
        {
            try
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Tournumber",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 30,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = number
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@ConditioningOrderId",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Size = 30,
                            Value = id
                        }};
                await _context.Database.ExecuteSqlRawAsync("EXECUTE [dbo].[SpDltPipeFillingControls] @Tournumber, @ConditioningOrderId", param);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al ejecutar SpDltPipeFillingControls " + ex);
            }

        }

        private async Task<(List<VwLotesDistribuicion> vwLotesDistribuicions, List<VwAnalisisClienteViewModel> vwAnalisisClienteViewModels)> GetAnalisisCliente(ConditioningOrderViewModel model)
        {
            var ldetalledistViewModel = new List<VwAnalisisClienteViewModel>();
            var ldist = new List<VwLotesDistribuicion>();
            var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            var op = await _productionOrderRepository.GetByIdAsync(model.ProductionOrderId);
            var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
            foreach (var itemw in epc)
            {
                var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == model.Id && x.TourNumber == itemw.TourNumber);
                if (control.Any())
                {
                    foreach (var itemz in control)
                    {
                        var pipeFilling = await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == itemz.Id);
                        foreach (var itemy in pipeFilling)
                        {
                            foreach (var item in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value && x.PipeNumber == itemy.PipeNumber && x.DistributionBatch == itemy.DistributionBatch))
                            {
                                foreach (var itemx in ldetalledistDB.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value && x.PipeNumber == itemy.PipeNumber && x.DistributionBatch == itemy.DistributionBatch))
                                {
                                    if (item.ParameterName == itemx.ParameterName && item.Unique == itemx.Unique)
                                    {

                                        ldetalledistViewModel.Add(
                                            new VwAnalisisClienteViewModel
                                            {
                                                ANALISIS_FIN = decimal.Parse(itemx.ValueReal),
                                                ANALISIS_FINAL = item.ValueExpected,
                                                ANALISIS_INI = decimal.Parse(item.ValueReal),
                                                ANALISIS_INICIAL = itemx.ValueExpected,
                                                DESC_ANALIZADOR = "",
                                                DESC_PARAMETRO = item.ParameterName,
                                                DESC_UM = itemx.MeasureUnit,
                                                ID_LOTEPIPA = item.DistributionBatch,
                                                ESPECIFICACION = itemx.ValueExpected,
                                                ID_PLANTA = Convert.ToInt32(op.PlantId),
                                                PRODUCT_ID = op.ProductId,
                                                TOURNUMBER = itemz.TourNumber,
                                                TRIPNUMBER = 123,
                                                Unique = item.Unique,
                                                PATHFINAL = System.IO.Path.GetFileName(itemx.PathFile), //AHF
                                                PATHINICIAL = System.IO.Path.GetFileName(item.PathFile) //AHF
                                            });

                                    }
                                }
                            }
                            ldist.Add(new VwLotesDistribuicion
                            {
                                ID_LOTEPIPA = itemy.DistributionBatch,
                                PESO_INI = itemy.InitialWeight,
                                PESO_FIN = itemy.FinalWeight,
                                NOMBRE = itemy.AnalyzedBy,
                                FECHA_INI = itemy.InitialAnalyzedDate,
                                FECHA_FIN = itemy.FinalAnalyzedDate,
                                ID_LOTE = itemy.DistributionBatch?.Split('-')[3]
                            });
                        }
                    }
                }
                else
                {
                    var pipelineClearance = (await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == model.Id && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();

                    var plantIden = _plantasRepository.GetAsync(x => x.Id_Planta == Convert.ToInt32(op.PlantId));
                    var ldetalledist = new List<VwAnalisisCliente>();

                    var ldetalledistViewModelDB = _analisisClienteRepository.GetAsync(x => x.IDENTIFICADOR == plantIden.FirstOrDefault().Identificador
                                                            && x.PRODUCT_ID == op.ProductId && x.DESC_GRADO == "MEDICINAL"
                                                            && x.TOURNUMBER == itemw.TourNumber && x.FEC_ALTA >= model.CreatedDate)
                                                .ToList();
                    foreach (var item in ldetalledistViewModelDB)
                    {
                        var fechaLoteString = item.ID_LOTEPIPA?.Split('-')[2];
                        var fechaLoteMonth = int.Parse(fechaLoteString.Substring(0, 2));
                        var fechaLoteDay = int.Parse(fechaLoteString.Substring(2, 2));
                        var fechaLoteYear = int.Parse(fechaLoteString.Substring(4, 2)) + 2000;

                        var horaLoteString = item.ID_LOTEPIPA?.Split('-')[4];
                        var fechaHora = int.Parse(horaLoteString.Substring(0, 2));
                        var fechaMinutos = int.Parse(horaLoteString.Substring(2, 2));

                        var fechaLote = new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
                        fechaLote = fechaLote.AddHours(fechaHora);
                        fechaLote = fechaLote.AddMinutes(fechaMinutos);

                        if (pipelineClearance?.ReviewedDate != null && fechaLote > pipelineClearance?.ReviewedDate.Value)
                        {
                            ldetalledist.Add(item);
                        }
                    };

                    if (ldetalledist.Any())
                    {
                        var distribuicionsDb = _loteDistribuicionRepository.GetAsync(x => x.ID_PLANTA == Convert.ToInt32(op.PlantId)
                        && x.PRODUCT_ID == op.ProductId && x.ID_LOTEPIPA == ldetalledist.FirstOrDefault().ID_LOTEPIPA).ToList();
                        foreach (var item in distribuicionsDb)
                        {
                            ldist.Add(new VwLotesDistribuicion
                            {
                                ID_PLANTA = item.ID_PLANTA,
                                ID_LOTE = item.ID_LOTE,
                                ID_LOTEPIPA = item.ID_LOTEPIPA,
                                PRODUCT_ID = item.PRODUCT_ID,
                                DESC_CARROTQ = item.DESC_CARROTQ,
                                DESC_PIPA = item.DESC_PIPA,
                                NOMBRE = item.NOMBRE,
                                FECHA_INI = item.FECHA_INI,
                                FECHA_FIN = item.FECHA_FIN,
                                FEC_ALTA = item.FEC_ALTA,
                                PESO_INI = item.PESO_INI,
                                PESO_FIN = item.PESO_FIN,
                                COMENTARIOS = item.COMENTARIOS,
                                COMENTARIO_CANCELA = item.COMENTARIO_CANCELA,
                                COP = item.COP,
                                LOTE_ORIGEN = item.LOTE_ORIGEN,
                                STATUS_CANCELADO = item.STATUS_CANCELADO,
                                PTA_TRASVASE = item.PTA_TRASVASE
                            });
                        }
                    }
                    //var mapped = ObjectMapper.Mapper.Map<List<VwAnalisisClienteViewModel>>(ldetalledist);
                    //////group delete duplicate 
                    var result = from o in ldetalledist
                                 group o by (o.DESC_PARAMETRO, o.ANALISIS_INI, o.ANALISIS_FIN, o.DESC_ANALIZADOR,
                                            o.DESC_UM, o.ID_LOTEPIPA, o.IDENTIFICADOR, o.PRODUCT_ID, o.TOURNUMBER, o.TRIPNUMBER, o.ESPECIFICACION) into g
                                 select new VwAnalisisClienteViewModel
                                 {
                                     DESC_PARAMETRO = g.Key.DESC_PARAMETRO,
                                     ANALISIS_INI = g.Key.ANALISIS_INI,
                                     ANALISIS_FIN = g.Key.ANALISIS_FIN,
                                     DESC_ANALIZADOR = g.Key.DESC_ANALIZADOR,
                                     DESC_UM = g.Key.DESC_UM,
                                     ID_LOTEPIPA = g.Key.ID_LOTEPIPA,
                                     IDENTIFICADOR = g.Key.IDENTIFICADOR,
                                     PRODUCT_ID = g.Key.PRODUCT_ID,
                                     TOURNUMBER = g.Key.TOURNUMBER,
                                     TRIPNUMBER = g.Key.TRIPNUMBER,
                                     ESPECIFICACION = g.Key.ESPECIFICACION,
                                 };
                    foreach (var item in result)
                    {
                        Guid Unique = Guid.NewGuid();
                        ldetalledistViewModel.Add(new VwAnalisisClienteViewModel
                        {
                            ANALISIS_FIN = item.ANALISIS_FIN,
                            ANALISIS_FINAL = item.ANALISIS_FINAL,
                            ANALISIS_INI = item.ANALISIS_INI,
                            ANALISIS_INICIAL = item.ANALISIS_INICIAL,
                            DESC_ANALIZADOR = item.DESC_ANALIZADOR,
                            DESC_PARAMETRO = item.DESC_PARAMETRO,
                            DESC_UM = item.DESC_UM,
                            ID_LOTEPIPA = item.ID_LOTEPIPA,
                            ID_PLANTA = item.ID_PLANTA,
                            PRODUCT_ID = item.PRODUCT_ID,
                            TOURNUMBER = item.TOURNUMBER,
                            TRIPNUMBER = item.TRIPNUMBER,
                            ESPECIFICACION = item.ESPECIFICACION,
                            Unique = Unique.ToString()
                        });
                    }
                }
            }

            return new(ldist, ldetalledistViewModel);
        }
        public async Task<IEnumerable<PipeFillingViewModel>> GetAnalisisCliente(int ConditioningOrderId, string ProductId, string PlantId, DateTime? CreatedDate, List<string> Tournumbers)
        {
            var pipeFillingViewModel = new List<PipeFillingViewModel>();
            var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId);
            var controls = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && Tournumbers.Contains(x.TourNumber));
            var linde = Tournumbers.Where(x => !controls.Any(y => y.TourNumber == x)).ToList();
            var local = Tournumbers.Where(x => controls.Any(y => y.TourNumber == x)).ToList();
            if (local.Any())
            {
                var controlIds = controls.Where(x => local.Contains(x.TourNumber)).Select(x => x.Id).ToList();
                var pipeFilling = await _pipeFillingRepository.GetAsync(x => controlIds.Contains(x.PipeFillingControlId));
                var pipeFillingCustomers = await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && local.Contains(x.TourNumber));
                var pipeFillingCustomersFiles = await _pipeFillingCustomersFilesRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && local.Contains(x.TourNumber) && x.State == true);
                var analisysDto = (from pipes in pipeFilling
                                   join ldetalle in ldetalledistDB on new { x = pipes.PipeNumber, y = pipes.DistributionBatch } equals new { x = ldetalle.PipeNumber, y = ldetalle.DistributionBatch }
                                   group new { ldetalle, pipes } by new
                                   {
                                       pipes.PipeNumber,
                                       pipes.DistributionBatch
                                   } into gcs
                                   select new PipeFillingViewModel
                                   {
                                       TourNumber = controls.Where(x => x.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(x => x.TourNumber).FirstOrDefault(),
                                       PipeNumber = gcs.FirstOrDefault().ldetalle.PipeNumber,
                                       Date = gcs.FirstOrDefault().pipes.Date,
                                       InitialWeight = gcs.FirstOrDefault().pipes.InitialWeight,
                                       FinalWeight = gcs.FirstOrDefault().pipes.FinalWeight,
                                       DiffWeight = DiffWeight(gcs.FirstOrDefault().pipes.InitialWeight, gcs.FirstOrDefault().pipes.FinalWeight),
                                       InitialAnalysis = gcs.Select(x => x.ldetalle).Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value).Select(x => new PipeFillingAnalysisViewModel
                                       {
                                           ParameterName = x.ParameterName,
                                           MeasureUnit = x.MeasureUnit,
                                           ValueExpected = x.ValueExpected,
                                           ValueReal = x.ValueReal,
                                           Type = x.Type,
                                           Unique = x.Unique,
                                           PathFile = x.PathFile
                                       }).OrderBy(x => x.ParameterName).ToList(),
                                       FinalAnalysis = gcs.Select(x => x.ldetalle).Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value).Select(x => new PipeFillingAnalysisViewModel
                                       {
                                           ParameterName = x.ParameterName,
                                           MeasureUnit = x.MeasureUnit,
                                           ValueExpected = x.ValueExpected,
                                           ValueReal = x.ValueReal,
                                           Type = x.Type,
                                           Unique = x.Unique,
                                           PathFile = x.PathFile
                                       }).OrderBy(x => x.ParameterName).ToList(),
                                       AnalyzedBy = gcs.FirstOrDefault().pipes.AnalyzedBy,
                                       AnalyzedDate = gcs.FirstOrDefault().pipes.AnalyzedDate,
                                       InitialAnalyzedDate = gcs.FirstOrDefault().pipes.InitialAnalyzedDate,
                                       FinalAnalyzedDate = gcs.FirstOrDefault().pipes.FinalAnalyzedDate,
                                       DueDate = gcs.FirstOrDefault().pipes.DueDate,
                                       DistributionBatch = gcs.FirstOrDefault().pipes.DistributionBatch,
                                       InCompliance = gcs.FirstOrDefault().pipes.InCompliance,
                                       ReportPNCFolio = gcs.FirstOrDefault().pipes.ReportPNCFolio,
                                       ReportPNCNotes = gcs.FirstOrDefault().pipes.ReportPNCNotes,
                                       ReleasedBy = gcs.FirstOrDefault().pipes.ReleasedBy,
                                       ReleasedDate = gcs.FirstOrDefault().pipes.ReleasedDate,
                                       IsReleased = gcs.FirstOrDefault().pipes.IsReleased,
                                       Customers = pipeFillingCustomers.Where(x => x.TourNumber == controls
                                            .Where(y => y.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(y => y.TourNumber).FirstOrDefault() && x.DistributionBatch == gcs.FirstOrDefault().pipes.DistributionBatch)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.Tank,
                                                Name = x.Name,
                                                DeliveryNumber = x.DeliveryNumber,
                                                Notes = x.Notes,
                                                ReviewedBy = x.ReviewedBy,
                                                ReviewedDate = x.ReviewedDate,
                                                EmailsList = x.EmailsList,
                                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio + (x.ReviewedDate.HasValue ? ((DateTime)x.ReviewedDate).ToString("yy") : "") : null,
                                                InCompliance = x.InCompliance != null ? true : false,
                                                PipeFillingControlId = gcs.FirstOrDefault().pipes.Id,
                                                Id = x.Id,
                                                EmailsListSend = x.EmailListSend,
                                                TourNumber = x.TourNumber,
                                                DistributionBatch = x.DistributionBatch,
                                                ConditioningOrderId = x.ConditioningOrderId,
                                                Folio = x.Folio,
                                                state = x.State

                                            }).ToList(),
                                       CustomersFiles = pipeFillingCustomersFiles.Where(x => x.TourNumber == controls
                                            .Where(y => y.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(y => y.TourNumber).FirstOrDefault() && x.DistributionBatch == gcs.FirstOrDefault().pipes.DistributionBatch)
                                            .Select(x => new PipeFillingCustomersFilesViewModel
                                            {
                                                Id = x.Id,
                                                ReviewedBy = x.ReviewedBy,
                                                ReviewedDate = x.ReviewedDate,
                                                DistributionBatch = x.DistributionBatch,
                                                TourNumber = x.TourNumber,
                                                PipeNumber = x.PipeNumber,
                                                ConditioningOrderId = x.ConditioningOrderId,
                                                state = x.State,
                                                Tank = x.Tank,
                                                FileName = x.FileName,  
                                                FileNameOrigin = x.FileNameOrigin,  

                                            }).ToList()
                                   }).ToList();
                var distributionBaths = pipeFilling.Select(x => x.DistributionBatch).ToList();
                var pipesd = pipeFilling.Select(x => x.PipeNumber).ToList();
                var checklistsList = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == ConditioningOrderId && Tournumbers.Contains(x.TourNumber) && pipesd.Contains(x.PipeNumber) && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);
                //var filtered = checklistsList.Where(y => y.StatusTwo != CheckListType.CloseNo.Value);
                //filtered = filtered.Where(y => y.StatusTwo != CheckListType.CloseOk.Value);
                analisysDto.ForEach(x => { updateCheckList(ref x, checklistsList.Where(y => y.TourNumber == x.TourNumber && y.PipeNumber == x.PipeNumber).ToList()); });
                pipeFillingViewModel.AddRange(analisysDto);
                foreach (var item in pipeFillingCustomers)
                {
                    this._logger.LogInformation("PipeFillingCustomers " + item.Tank + "|" + item.Name + "|" + item.DeliveryNumber + "|"  +"tournumber" + item.TourNumber + "|" + "ConditioningOrderId " + ConditioningOrderId);
                }
            }
            if (linde.Any())
            {
                var pipelineClearance = (await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
                var plantIden = await _plantasRepository.GetAsync(x => x.Id_Planta == Convert.ToInt32(PlantId)).ToListAsync();
                var ldetalledistViewModelDB = _analisisClienteRepository
                    .GetAsync(x => x.IDENTIFICADOR == plantIden.FirstOrDefault().Identificador && x.PRODUCT_ID == ProductId && x.DESC_GRADO == "MEDICINAL" && linde.Contains(x.TOURNUMBER) && x.FEC_ALTA >= CreatedDate)
                    .ToList();
                var ldetalledist = new List<VwAnalisisCliente>();
                var filterVariables = await this._variablesFileReaderService.GetFilterParameterByProductKeyAsync(ProductId);
                var variables = filterVariables?.Variables.Select(x => x.Trim().ToUpper());
                foreach (var item in ldetalledistViewModelDB)
                {
                    if (variables == null || (variables != null && !variables.Contains(item.DESC_PARAMETRO.Trim().ToUpper())))
                    {
                        var fechaLoteString = item.ID_LOTEPIPA?.Split('-')[2];
                        if (!string.IsNullOrEmpty(fechaLoteString))
                        {
                            var fechaLoteMonth = int.Parse(fechaLoteString.Substring(0, 2));
                            var fechaLoteDay = int.Parse(fechaLoteString.Substring(2, 2));
                            var fechaLoteYear = int.Parse(fechaLoteString.Substring(4, 2)) + 2000;
                            var horaLoteString = item.ID_LOTEPIPA?.Split('-')[4];
                            var fechaHora = int.Parse(horaLoteString.Substring(0, 2));
                            var fechaMinutos = int.Parse(horaLoteString.Substring(2, 2));
                            var fechaLote = new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
                            fechaLote = fechaLote.AddHours(fechaHora);
                            fechaLote = fechaLote.AddMinutes(fechaMinutos);
                            if (pipelineClearance?.ReviewedDate != null && fechaLote > pipelineClearance?.ReviewedDate.Value)
                            {
                                item.Unique = Guid.NewGuid().ToString();
                                ldetalledist.Add(item);
                            }
                        }
                    }
                }
                if (ldetalledist.Any())
                {
                    var pipeNumbersLinde = ldetalledist.Select(x => x.ID_LOTEPIPA).Distinct().ToList();
                    var customersPerPipe = await _lotesDistribuicionClienteRepository.GetAsync(x => pipeNumbersLinde.Contains(x.ID_LOTEPIPA)).ToListAsync();
                    var lotePipaIds = ldetalledist.Select(x => x.ID_LOTEPIPA).Distinct().ToList();
                    var distribuicionsDb = _loteDistribuicionRepository.GetAsync(x => x.ID_PLANTA == Convert.ToInt32(PlantId) && x.PRODUCT_ID == ProductId && lotePipaIds.Contains(x.ID_LOTEPIPA)).ToList();
                    var analysisTemp = (from analisys in ldetalledist
                                        join pipes in distribuicionsDb on analisys.ID_LOTEPIPA equals pipes.ID_LOTEPIPA
                                        group new { analisys, pipes } by new
                                        {
                                            pipes.DESC_PIPA,
                                            pipes.ID_LOTEPIPA
                                        } into gcs
                                        select new PipeFillingViewModel
                                        {
                                            TourNumber = gcs.FirstOrDefault().analisys.TOURNUMBER,
                                            PipeNumber = gcs.FirstOrDefault().analisys.DESC_PIPA,
                                            Date = getLoteDate(gcs.FirstOrDefault().pipes.ID_LOTEPIPA),
                                            InitialWeight = gcs.FirstOrDefault().pipes.PESO_INI,
                                            FinalWeight = gcs.FirstOrDefault().pipes.PESO_FIN,
                                            DiffWeight = DiffWeight(gcs.FirstOrDefault().pipes.PESO_INI, gcs.FirstOrDefault().pipes.PESO_FIN),
                                            InitialAnalysis = gcs.Select(x => x.analisys).GroupBy(x => new
                                            {
                                                x.DESC_PARAMETRO,
                                                x.ANALISIS_INI,
                                                x.ANALISIS_FIN,
                                                x.DESC_ANALIZADOR,
                                                x.DESC_UM,
                                                x.ID_LOTEPIPA,
                                                x.IDENTIFICADOR,
                                                x.PRODUCT_ID,
                                                x.TOURNUMBER,
                                                x.TRIPNUMBER,
                                                x.ESPECIFICACION
                                            }).Select(x => new PipeFillingAnalysisViewModel
                                            {
                                                ParameterName = x.FirstOrDefault().DESC_PARAMETRO,
                                                MeasureUnit = x.FirstOrDefault().DESC_UM,
                                                ValueExpected = x.FirstOrDefault().ESPECIFICACION,
                                                ValueReal = x.FirstOrDefault().ANALISIS_INI.ToString(),
                                                Type = PipeFillingAnalysisType.InitialAnalysis.Value,
                                                Unique = x.FirstOrDefault().Unique
                                            }).OrderBy(x => x.ParameterName).ToList(),
                                            FinalAnalysis = gcs.Select(x => x.analisys).GroupBy(x => new
                                            {
                                                x.DESC_PARAMETRO,
                                                x.ANALISIS_INI,
                                                x.ANALISIS_FIN,
                                                x.DESC_ANALIZADOR,
                                                x.DESC_UM,
                                                x.ID_LOTEPIPA,
                                                x.IDENTIFICADOR,
                                                x.PRODUCT_ID,
                                                x.TOURNUMBER,
                                                x.TRIPNUMBER,
                                                x.ESPECIFICACION
                                            }).Select(x => new PipeFillingAnalysisViewModel
                                            {
                                                ParameterName = x.FirstOrDefault().DESC_PARAMETRO,
                                                MeasureUnit = x.FirstOrDefault().DESC_UM,
                                                ValueExpected = x.FirstOrDefault().ESPECIFICACION,
                                                ValueReal = x.FirstOrDefault().ANALISIS_FIN.ToString(),
                                                Type = PipeFillingAnalysisType.FinalAnalysis.Value,
                                                Unique = x.FirstOrDefault().Unique
                                            }).OrderBy(x => x.ParameterName).ToList(),
                                            AnalyzedBy = gcs.FirstOrDefault().pipes.NOMBRE,
                                            AnalyzedDate = gcs.FirstOrDefault().pipes.FEC_ALTA,
                                            InitialAnalyzedDate = gcs.FirstOrDefault().pipes.FECHA_INI,
                                            FinalAnalyzedDate = gcs.FirstOrDefault().pipes.FECHA_FIN,
                                            DueDate = computeDueDate(gcs.FirstOrDefault().pipes.ID_LOTEPIPA),
                                            DistributionBatch = gcs.FirstOrDefault().pipes.ID_LOTEPIPA,
                                            Customers = customersPerPipe.Where(x => x.ID_LOTEPIPA == gcs.Key.ID_LOTEPIPA)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0,
                                                state = null
                                            }).ToList()
                                        }).ToList();
                    pipeFillingViewModel.AddRange(analysisTemp);
                    foreach (var item in customersPerPipe)
                    {
                        this._logger.LogInformation("PipeFillingCustomers " + item.TANQUE_CLIENTE + "|" + item.CLIENTE + "|" + item.NO_ORDEN_ENTREGA + "tournumber" + item.TOURNUMBER + "|"  + "ConditioningOrderId " + ConditioningOrderId);
                    }
                }
            }
            var totalpipeIds = pipeFillingViewModel.Select(x => x.DistributionBatch).ToList();
            var compliance = await _lotesDistribuicionClienteRepository.GetAsync(x => totalpipeIds.Contains(x.ID_LOTEPIPA) && x.FEC_ALTA >= CreatedDate).ToListAsync();
            foreach (var item in pipeFillingViewModel)
            {
                var inCompliance = compliance.FirstOrDefault(x => x.ID_LOTEPIPA == item.DistributionBatch);
                item.InCompliance = inCompliance != null ? inCompliance.CALIDAD == "CUMPLE" ? true : false : null;
            }
            return pipeFillingViewModel;
        }

        public async Task<IEnumerable<PipeFillingViewModel>> GetAnalisisClienteUpdated(int ConditioningOrderId, string ProductId, string PlantId, DateTime? CreatedDate, List<string> Tournumbers, string user, string TankId)
        {
            var pipeFillingViewModel = new List<PipeFillingViewModel>();
            var customers = new List<PipeFillingCustomer>();
            var pipeCustomerToUpdate = new List<PipeCustomerToUpdate>();
            var conditioningOrder = (await _conditioningOrderRepository.GetByIdAsync(ConditioningOrderId));
            var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId);
            var controls = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && Tournumbers.Contains(x.TourNumber));
            var linde = Tournumbers.Where(x => !controls.Any(y => y.TourNumber == x)).ToList();
            var local = Tournumbers.Where(x => controls.Any(y => y.TourNumber == x)).ToList();
            var customersLinde = new List<PipeFillingCustomerViewModel>();
            var customersDB = new List<PipeFillingCustomerViewModel>();
            var customersDBClone = new List<PipeFillingCustomerViewModel>();
            var plantIden = await _plantasRepository.GetAsync(x => x.Id_Planta == Convert.ToInt32(PlantId)).ToListAsync();
            if (local.Any())
            {
                var controlIds = controls.Where(x => local.Contains(x.TourNumber)).Select(x => x.Id).ToList();
                var pipeFilling = await _pipeFillingRepository.GetAsync(x => controlIds.Contains(x.PipeFillingControlId));
                await GetCustomersLinde(ConditioningOrderId, local, plantIden.FirstOrDefault().Identificador, ProductId, TankId);
                var pipeFillingCustomers = (await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && local.Contains(x.TourNumber))).ToList();
                var pipeFillingCustomersFiles = await _pipeFillingCustomersFilesRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && local.Contains(x.TourNumber) && x.State == true);
                var analisysDto = (from pipes in pipeFilling
                                   join ldetalle in ldetalledistDB on new { x = pipes.PipeNumber, y = pipes.DistributionBatch } equals new { x = ldetalle.PipeNumber, y = ldetalle.DistributionBatch }
                                   group new { ldetalle, pipes } by new
                                   {
                                       pipes.PipeNumber,
                                       pipes.DistributionBatch
                                   } into gcs
                                   select new PipeFillingViewModel
                                   {
                                       TourNumber = controls.Where(x => x.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(x => x.TourNumber).FirstOrDefault(),
                                       PipeNumber = gcs.FirstOrDefault().ldetalle.PipeNumber,
                                       Date = gcs.FirstOrDefault().pipes.Date,
                                       InitialWeight = gcs.FirstOrDefault().pipes.InitialWeight,
                                       FinalWeight = gcs.FirstOrDefault().pipes.FinalWeight,
                                       DiffWeight = DiffWeight(gcs.FirstOrDefault().pipes.InitialWeight, gcs.FirstOrDefault().pipes.FinalWeight),
                                       InitialAnalysis = gcs.Select(x => x.ldetalle).Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value).Select(x => new PipeFillingAnalysisViewModel
                                       {
                                           ParameterName = x.ParameterName,
                                           MeasureUnit = x.MeasureUnit,
                                           ValueExpected = x.ValueExpected,
                                           ValueReal = x.ValueReal,
                                           Type = x.Type,
                                           Unique = x.Unique,
                                           PathFile = x.PathFile
                                       }).OrderBy(x => x.ParameterName).ToList(),
                                       FinalAnalysis = gcs.Select(x => x.ldetalle).Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value).Select(x => new PipeFillingAnalysisViewModel
                                       {
                                           ParameterName = x.ParameterName,
                                           MeasureUnit = x.MeasureUnit,
                                           ValueExpected = x.ValueExpected,
                                           ValueReal = x.ValueReal,
                                           Type = x.Type,
                                           Unique = x.Unique,
                                           PathFile = x.PathFile
                                       }).OrderBy(x => x.ParameterName).ToList(),
                                       AnalyzedBy = gcs.FirstOrDefault().pipes.AnalyzedBy,
                                       AnalyzedDate = gcs.FirstOrDefault().pipes.AnalyzedDate,
                                       InitialAnalyzedDate = gcs.FirstOrDefault().pipes.InitialAnalyzedDate,
                                       FinalAnalyzedDate = gcs.FirstOrDefault().pipes.FinalAnalyzedDate,
                                       DueDate = gcs.FirstOrDefault().pipes.DueDate,
                                       DistributionBatch = gcs.FirstOrDefault().pipes.DistributionBatch,
                                       InCompliance = gcs.FirstOrDefault().pipes.InCompliance,
                                       ReportPNCFolio = gcs.FirstOrDefault().pipes.ReportPNCFolio,
                                       ReportPNCNotes = gcs.FirstOrDefault().pipes.ReportPNCNotes,
                                       ReleasedBy = gcs.FirstOrDefault().pipes.ReleasedBy,
                                       ReleasedDate = gcs.FirstOrDefault().pipes.ReleasedDate,
                                       IsReleased = gcs.FirstOrDefault().pipes.IsReleased,
                                       Customers = pipeFillingCustomers.Where(x => x.TourNumber == controls
                                            .Where(y => y.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(y => y.TourNumber).FirstOrDefault() && x.DistributionBatch == gcs.FirstOrDefault().pipes.DistributionBatch)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.Tank,
                                                Name = x.Name,
                                                DeliveryNumber = x.DeliveryNumber,
                                                Notes = x.Notes,
                                                ReviewedBy = x.ReviewedBy,
                                                ReviewedDate = x.ReviewedDate,
                                                EmailsList = x.EmailsList,
                                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio + (x.ReviewedDate.HasValue ? ((DateTime)x.ReviewedDate).ToString("yy") : "") : null,
                                                InCompliance = x.InCompliance != null ? true : false,
                                                PipeFillingControlId = gcs.FirstOrDefault().pipes.Id,
                                                Id = x.Id,
                                                Folio = x.Folio,
                                                DistributionBatch = x.DistributionBatch,
                                                TourNumber = x.TourNumber,
                                                state = x.State
                                            }).ToList(),
                                       CustomersFiles = pipeFillingCustomersFiles.Where(x => x.TourNumber == controls
                                            .Where(y => y.Id == gcs.FirstOrDefault().pipes.PipeFillingControlId).Select(y => y.TourNumber).FirstOrDefault() && x.DistributionBatch == gcs.FirstOrDefault().pipes.DistributionBatch)
                                            .Select(x => new PipeFillingCustomersFilesViewModel
                                            {
                                                Id = x.Id,
                                                ReviewedBy = x.ReviewedBy,
                                                ReviewedDate = x.ReviewedDate,
                                                DistributionBatch = x.DistributionBatch,
                                                TourNumber = x.TourNumber,
                                                PipeNumber = x.PipeNumber,
                                                ConditioningOrderId = x.ConditioningOrderId,
                                                state = x.State,
                                                Tank = x.Tank,
                                                FileName = x.FileName,
                                                FileNameOrigin = x.FileNameOrigin,

                                            }).ToList()
                                   }).ToList();
                var distributionBaths = pipeFilling.Select(x => x.DistributionBatch).ToList();
                var pipesd = pipeFilling.Select(x => x.PipeNumber).ToList();
                var checklistsList = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == ConditioningOrderId && Tournumbers.Contains(x.TourNumber) && pipesd.Contains(x.PipeNumber) && x.Step == CheckListGeneralViewModelCheckListStep.Two.Value);
                analisysDto.ForEach(x => { updateCheckList(ref x, checklistsList.Where(y => y.TourNumber == x.TourNumber && y.PipeNumber == x.PipeNumber).ToList()); });
                pipeFillingViewModel.AddRange(analisysDto);
                pipeFillingViewModel.ForEach(x => customersDB.AddRange(x.Customers));
                customersDBClone = Clone(customersDB);

            }
            if (linde.Any())
            {
                var pipelineClearance = (await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && x.InCompliance.HasValue && x.InCompliance.Value)).FirstOrDefault();
  
                var ldetalledistViewModelDB = _analisisClienteRepository
                    .GetAsync(x => x.IDENTIFICADOR == plantIden.FirstOrDefault().Identificador && x.PRODUCT_ID == ProductId && x.DESC_GRADO == "MEDICINAL" && linde.Contains(x.TOURNUMBER) && x.FEC_ALTA >= CreatedDate)
                    .ToList();
                var ldetalledist = new List<VwAnalisisCliente>();
                var filterVariables = await this._variablesFileReaderService.GetFilterParameterByProductKeyAsync(ProductId);
                var variables = filterVariables?.Variables.Select(x => x.Trim().ToUpper());
                foreach (var item in ldetalledistViewModelDB)
                {
                    if (variables == null || (variables != null && !variables.Contains(item.DESC_PARAMETRO.Trim().ToUpper())))
                    {
                        var fechaLoteString = item.ID_LOTEPIPA?.Split('-')[2];
                        if (!string.IsNullOrEmpty(fechaLoteString))
                        {
                            var fechaLoteMonth = int.Parse(fechaLoteString.Substring(0, 2));
                            var fechaLoteDay = int.Parse(fechaLoteString.Substring(2, 2));
                            var fechaLoteYear = int.Parse(fechaLoteString.Substring(4, 2)) + 2000;
                            var horaLoteString = item.ID_LOTEPIPA?.Split('-')[4];
                            var fechaHora = int.Parse(horaLoteString.Substring(0, 2));
                            var fechaMinutos = int.Parse(horaLoteString.Substring(2, 2));
                            var fechaLote = new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
                            fechaLote = fechaLote.AddHours(fechaHora);
                            fechaLote = fechaLote.AddMinutes(fechaMinutos);
                            if (pipelineClearance?.ReviewedDate != null && fechaLote > pipelineClearance?.ReviewedDate.Value)
                            {
                                item.Unique = Guid.NewGuid().ToString();
                                ldetalledist.Add(item);
                            }
                        }
                    }
                }
                if (ldetalledist.Any())
                {
                    var pipeNumbersLinde = ldetalledist.Select(x => x.ID_LOTEPIPA).Distinct().ToList();
                    var customersPerPipe = await _lotesDistribuicionClienteRepository.GetAsync(x => pipeNumbersLinde.Contains(x.ID_LOTEPIPA)).ToListAsync();
                    var lotePipaIds = ldetalledist.Select(x => x.ID_LOTEPIPA).Distinct().ToList();
                    var distribuicionsDb = _loteDistribuicionRepository.GetAsync(x => x.ID_PLANTA == Convert.ToInt32(PlantId) && x.PRODUCT_ID == ProductId && lotePipaIds.Contains(x.ID_LOTEPIPA)).ToList();
                    var analysisTemp = (from analisys in ldetalledist
                                        join pipes in distribuicionsDb on analisys.ID_LOTEPIPA equals pipes.ID_LOTEPIPA
                                        group new { analisys, pipes } by new
                                        {
                                            pipes.DESC_PIPA,
                                            pipes.ID_LOTEPIPA
                                        } into gcs
                                        select new PipeFillingViewModel
                                        {
                                            TourNumber = gcs.FirstOrDefault().analisys.TOURNUMBER,
                                            PipeNumber = gcs.FirstOrDefault().analisys.DESC_PIPA,
                                            Date = getLoteDate(gcs.FirstOrDefault().pipes.ID_LOTEPIPA),
                                            InitialWeight = gcs.FirstOrDefault().pipes.PESO_INI,
                                            FinalWeight = gcs.FirstOrDefault().pipes.PESO_FIN,
                                            DiffWeight = DiffWeight(gcs.FirstOrDefault().pipes.PESO_INI, gcs.FirstOrDefault().pipes.PESO_FIN),
                                            InitialAnalysis = gcs.Select(x => x.analisys).GroupBy(x => new
                                            {
                                                x.DESC_PARAMETRO,
                                                x.ANALISIS_INI,
                                                x.ANALISIS_FIN,
                                                x.DESC_ANALIZADOR,
                                                x.DESC_UM,
                                                x.ID_LOTEPIPA,
                                                x.IDENTIFICADOR,
                                                x.PRODUCT_ID,
                                                x.TOURNUMBER,
                                                x.TRIPNUMBER,
                                                x.ESPECIFICACION
                                            }).Select(x => new PipeFillingAnalysisViewModel
                                            {
                                                ParameterName = x.FirstOrDefault().DESC_PARAMETRO,
                                                MeasureUnit = x.FirstOrDefault().DESC_UM,
                                                ValueExpected = x.FirstOrDefault().ESPECIFICACION,
                                                ValueReal = x.FirstOrDefault().ANALISIS_INI.ToString(),
                                                Type = PipeFillingAnalysisType.InitialAnalysis.Value,
                                                Unique = x.FirstOrDefault().Unique
                                            }).OrderBy(x => x.ParameterName).ToList(),
                                            FinalAnalysis = gcs.Select(x => x.analisys).GroupBy(x => new
                                            {
                                                x.DESC_PARAMETRO,
                                                x.ANALISIS_INI,
                                                x.ANALISIS_FIN,
                                                x.DESC_ANALIZADOR,
                                                x.DESC_UM,
                                                x.ID_LOTEPIPA,
                                                x.IDENTIFICADOR,
                                                x.PRODUCT_ID,
                                                x.TOURNUMBER,
                                                x.TRIPNUMBER,
                                                x.ESPECIFICACION
                                            }).Select(x => new PipeFillingAnalysisViewModel
                                            {
                                                ParameterName = x.FirstOrDefault().DESC_PARAMETRO,
                                                MeasureUnit = x.FirstOrDefault().DESC_UM,
                                                ValueExpected = x.FirstOrDefault().ESPECIFICACION,
                                                ValueReal = x.FirstOrDefault().ANALISIS_FIN.ToString(),
                                                Type = PipeFillingAnalysisType.FinalAnalysis.Value,
                                                Unique = x.FirstOrDefault().Unique
                                            }).OrderBy(x => x.ParameterName).ToList(),
                                            AnalyzedBy = gcs.FirstOrDefault().pipes.NOMBRE,
                                            AnalyzedDate = gcs.FirstOrDefault().pipes.FEC_ALTA,
                                            InitialAnalyzedDate = gcs.FirstOrDefault().pipes.FECHA_INI,
                                            FinalAnalyzedDate = gcs.FirstOrDefault().pipes.FECHA_FIN,
                                            DueDate = computeDueDate(gcs.FirstOrDefault().pipes.ID_LOTEPIPA),
                                            DistributionBatch = gcs.FirstOrDefault().pipes.ID_LOTEPIPA,
                                            Customers = customersPerPipe.Where(x => x.ID_LOTEPIPA == gcs.Key.ID_LOTEPIPA)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0,
                                                DistributionBatch = x.ID_LOTEPIPA,
                                                TourNumber = x.TOURNUMBER,
                                                state = null
                                            }).ToList()
                                        }).ToList();
                    pipeFillingViewModel.AddRange(analysisTemp);
                    pipeFillingViewModel.ForEach(x => customersLinde.AddRange(x.Customers));
                    foreach (var item in customersPerPipe)
                    {
                        this._logger.LogInformation("PipeFillingCustomers " + item.TANQUE_CLIENTE + "|" + item.CLIENTE + "|" + item.NO_ORDEN_ENTREGA + "tournumber" + item.TOURNUMBER +  "|" + "ConditioningOrderId " + ConditioningOrderId);
                    }
                }
            }
            var totalpipeIds = pipeFillingViewModel.Select(x => x.DistributionBatch).ToList();
            var compliance = await _lotesDistribuicionClienteRepository.GetAsync(x => totalpipeIds.Contains(x.ID_LOTEPIPA) && x.FEC_ALTA >= CreatedDate).ToListAsync();
            foreach (var item in pipeFillingViewModel)
            {
                var inCompliance = compliance.FirstOrDefault(x => x.ID_LOTEPIPA == item.DistributionBatch);
                item.InCompliance = inCompliance != null ? inCompliance.CALIDAD == "CUMPLE" ? true : false : null;
            }
            return pipeFillingViewModel;
        }
        public async Task<IEnumerable<StatesHistoryViewModel>> GetLogStatusOrderByIdAsync(int Id)
        {
            var userDb = _userManager.Users;
            var validStatus = new List<string>()
                {
                    ConditioningOrderStatus.InProgress.Value,
                    ConditioningOrderStatus.Released.Value
                };
            var result = await _historyStatesRepository.GetAsync(x => x.ProductionOrderId == Id && x.Type == HistoryStateType.OrdenAcondicionamiento.Value && validStatus.Contains(x.State));
            var historyDB = result?.Select(x => new HistoryStates
            {
                ProductionOrderId = x.ProductionOrderId,
                User = userDb.Where(y => y.Id == x.User).Select(x => x.NombreUsuario).FirstOrDefault(),
                Date = x.Date,
                State = x.State,
                Type = x.Type
            }).OrderBy(x => x.Date);
            if (historyDB != null && historyDB.Any())
            {
                return ObjectMapper.Mapper.Map<IEnumerable<StatesHistoryViewModel>>(historyDB.Where(x => validStatus.Contains(x.State)));
            }
            return new List<StatesHistoryViewModel>();
        }

        private DateTime getLoteDate(string LotePipaId)
        {
            var fechaLoteString = LotePipaId.Split('-')[2];
            var fechaLoteMonth = int.Parse(fechaLoteString?.Substring(0, 2));
            var fechaLoteDay = int.Parse(fechaLoteString?.Substring(2, 2));
            var fechaLoteYear = int.Parse(fechaLoteString?.Substring(4, 2)) + 2000;
            return new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
        }
        private DateTime computeDueDate(string LotePipaId)
        {
            var fechaLoteString = LotePipaId.Split('-')[2];
            var fechaLoteMonth = int.Parse(fechaLoteString?.Substring(0, 2));
            var fechaLoteDay = int.Parse(fechaLoteString?.Substring(2, 2));
            var fechaLoteYear = int.Parse(fechaLoteString?.Substring(4, 2)) + 2000;
            var horaLoteString = LotePipaId.Split('-')[4];
            var fechaHora = int.Parse(horaLoteString.Substring(0, 2));
            var fechaMinutos = int.Parse(horaLoteString.Substring(2, 2));
            var fechaLote = new DateTime(fechaLoteYear, fechaLoteMonth, fechaLoteDay);
            fechaLote = fechaLote.AddHours(fechaHora);
            fechaLote = fechaLote.AddMinutes(fechaMinutos);
            return fechaLote.AddMonths(3);
        }
        private void updateCheckList(ref PipeFillingViewModel analisys, List<CheckListPipeDictiumAnswer> checklistsList)
        {
            var checklist = checklistsList.OrderByDescending(x => x.Date).FirstOrDefault();
            if (checklist != null)
            {
                analisys.CheckListId = checklist.Id;
                analisys.CheckListStatus = checklist.StatusTwo;
                analisys.CheckListIncompliance = checklist.InCompliance;
                analisys.CheckListSource = checklist.Source;
            }
        }
        private async Task<List<PipeFillingCustomerViewModel>> GetCustomersByPipeNumber(string pipeNumber, int ConditioningOrderId, string tournumber)
        {
            var customers = new List<PipeFillingCustomerViewModel>();

            if (WSMexeFuncionalidad)
            {
                var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && x.TourNumber == tournumber);
                if (control.Any())
                {
                    foreach (var itemz in control)
                    {

                        var info = (await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && x.DistributionBatch.Contains(pipeNumber) && x.TourNumber == tournumber))
                            .Select(x => new PipeFillingCustomerViewModel
                            {
                                Tank = x.Tank,
                                Name = x.Name,
                                DeliveryNumber = x.DeliveryNumber,
                                Notes = x.Notes,
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                EmailsList = x.EmailsList,
                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio : null,
                                InCompliance = x.InCompliance != null ? true : false,
                                PipeFillingControlId = itemz.Id,
                                Id = x.Id
                            }).ToList();

                        customers.AddRange(info);

                        if (!info.Any())
                        {
                            var customersPerPipe = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == pipeNumber)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0
                                            }).ToList();

                            customers.AddRange(customersPerPipe);
                        }
                    }
                }
                else
                {
                    var customersPerPipe = _lotesDistribuicionClienteRepository.GetAsync(x => x.ID_LOTEPIPA == pipeNumber)
                                            .Select(x => new PipeFillingCustomerViewModel
                                            {
                                                Tank = x.TANQUE_CLIENTE,
                                                Name = x.CLIENTE,
                                                DeliveryNumber = x.NO_ORDEN_ENTREGA,
                                                InCompliance = x.CALIDAD != null ? true : false,
                                                PipeFillingControlId = 0
                                            }).ToList();

                    customers.AddRange(customersPerPipe);
                }
            }
            else
            {
                var control = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId);
                if (control.Any())
                {
                    foreach (var itemz in control)
                    {
                        var info = (await _pipeFillingCustomerRepository.GetAsync(x => x.DistributionBatch.Contains(pipeNumber)
                         && x.TourNumber == itemz.TourNumber))
                            .Select(x => new PipeFillingCustomerViewModel
                            {
                                Tank = x.Tank,
                                Name = x.Name,
                                DeliveryNumber = x.DeliveryNumber,
                                Notes = x.Notes,
                                ReviewedBy = x.ReviewedBy,
                                ReviewedDate = x.ReviewedDate,
                                EmailsList = x.EmailsList,
                                AnalysisReport = x.Folio != null ? x.PlantIdentificador + "-" + x.ProductId + "-" + x.TankId + "-" + x.Folio : null,
                                InCompliance = x.InCompliance != null ? true : false,
                                PipeFillingControlId = itemz.Id
                            }).ToList();

                        customers.AddRange(info);

                    }
                }
                else
                {
                    customers = new List<PipeFillingCustomerViewModel> {
                    new PipeFillingCustomerViewModel {
                        Tank = "M31-LOX-01",
                        Name = "Instituto Mexicano del Seguro Social",
                        DeliveryNumber = "123456",
                        InCompliance = true,
                        TankId = "LOX-01",
                        PlantIdentificador="M31",
                        ProductId = "OX",
                        PipeFillingControlId = 0
                    }
                };
                }
            }

            return customers;
        }

        #region checklist 
        public async Task<List<PipeFillingControlViewModel>> GetPipeFillingControlsXChecklist(ConditioningOrderViewModel model)
        {
            List<PipeFillingControlViewModel> listpipeFillingControls = new List<PipeFillingControlViewModel>();
            try
            {
                var oa = await _conditioningOrderRepository.GetByIdAsync(model.Id);
                var epc = await _equipmentProcessConditioningRepository.GetAsync(x => x.ConditioningOrderId == model.Id);
                var tournumberList = epc.Select(x => x.TourNumber).Distinct();
                var pipeFillins = await GetAnalisisCliente(model.Id, oa.ProductId.ToString(), oa.PlantId, oa.CreatedDate, tournumberList.ToList());
                foreach (var tournumber in tournumberList)
                {
                    var pipeDetailsList = pipeFillins.Where(x => x.TourNumber == tournumber).ToList();

                    listpipeFillingControls.Add(new PipeFillingControlViewModel
                    {
                        TourNumber = tournumber,
                        PipesList = pipeDetailsList
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetPipeFillingControls " + ex);
            }

            return listpipeFillingControls.ToList();
        }
        public async Task<StoredProcedureResult> UpdateRelChecklistTourNumberPipe(ConditioningOrderViewModel model)
        {
            StoredProcedureResult storedProcedureResult = new StoredProcedureResult();
            foreach (var item in model.CheckListRelationShip)
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ConditioningOrderId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Value = model.Id
                        },
                        new SqlParameter() {
                            ParameterName = "@Tournumber",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 30,
                            Value = item.TourNumber
                        },
                        new SqlParameter() {
                            ParameterName = "@Pipenumber",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 30,
                            Value = item.PipeNumber.Split('-')[3]
                        },
                        new SqlParameter() {
                            ParameterName = "@distributionBatch",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 30,
                            Value = item.PipeNumber
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@Id",
                            SqlDbType = System.Data.SqlDbType.Int,
                            Value =item.Id
                        }
                };
                var upd = _context.StoredProcedureResults.
                            FromSqlRaw("[dbo].[SpUpdRelCheckListTourNumberPipe] @ConditioningOrderId, @Tournumber, @Pipenumber, @Id, @distributionBatch", param).ToList();



            }
            return new StoredProcedureResult();
        }
        public async Task<StoredProcedureResult> UpdateRelChecklistTourNumberPipe(int idOA, string tourNumber, string distributionBatch, int checkListId, string pipeNumber)
        {
            StoredProcedureResult storedProcedureResult = new StoredProcedureResult();
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ConditioningOrderId",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Value = idOA
                },
                new SqlParameter() {
                    ParameterName = "@Tournumber",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Value = tourNumber
                },
                new SqlParameter() {
                    ParameterName = "@Pipenumber",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Value = pipeNumber
                },
                new SqlParameter()
                {
                    ParameterName = "@Id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = checkListId
                },
                new SqlParameter()
                {
                    ParameterName = "@distributionBatch",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Value = distributionBatch
                },
                new SqlParameter()
                {
                    ParameterName = "@option",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = 1
                }};
            var upd = _context.StoredProcedureResults.
                        FromSqlRaw("[dbo].[SpUpdRelCheckListTourNumberPipe] @ConditioningOrderId, @Tournumber, @Pipenumber, @Id, @distributionBatch", param).ToList();


            return new StoredProcedureResult();
        }

        public async Task<StoredProcedureResult> DeleteChecklist(int checkListId)
        {
            StoredProcedureResult storedProcedureResult = new StoredProcedureResult();
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Id",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Value = checkListId
                }};
            var upd = _context.StoredProcedureResults.
                        FromSqlRaw("[dbo].[SpDltCheckList] @Id", param).ToList();
            return new StoredProcedureResult();
        }
        #endregion
        List<T> Clone<T>(IEnumerable<T> oldList)
        {
            List<T> newList = new List<T>();
            return newList = new List<T>(oldList);
        }

        public async Task<StoredProcedureResult> UpdateCustomers(string tourNumber, string distributionBatch, string user, string values, string plant, string product)
        {
            StoredProcedureResult storedProcedureResult = new StoredProcedureResult();
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@DistribuitionBatch",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Value = distributionBatch
                },
                new SqlParameter() {
                    ParameterName = "@Tournumber",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Value = tourNumber
                },
                new SqlParameter() {
                    ParameterName = "@CreatedBy",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 50,
                    Value = user
                },
                new SqlParameter()
                {
                    ParameterName = "@Values",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = values
                },
                new SqlParameter()
                {
                    ParameterName = "@Plant",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Value = plant
                },
                new SqlParameter()
                {
                    ParameterName = "@Product",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Value = product
                }};
            var upd = _context.StoredProcedureResults.
                       FromSqlRaw("[dbo].[SpUpdPipeCustomers] @DistribuitionBatch, @Tournumber, @CreatedBy, @Values, @Plant,@Product ", param).ToList();


            return new StoredProcedureResult();
        }

        public async Task<ConditioningOrderViewModel> GetBasicInfoConditioningOrder(int Id)
        {
            var op = await _productionOrderRepository.GetByIdAsync(Id);
            var ObjectDB = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == op.Id)).FirstOrDefault();
            if (ObjectDB == null)
            {
                return null;
            }
            var mapped = ObjectMapper.Mapper.Map<ConditioningOrderViewModel>(ObjectDB);
            mapped.State = mapped.State;

            mapped.PlantId = ObjectDB.PlantId;
            mapped.ProductId = ObjectDB.ProductId;
            var catproductos = _context.productCatalogs.Where(x => x.ProductId == op.ProductId).FirstOrDefault();
            var fechacaducidad = Convert.ToDateTime(op.CreatedDate);
            fechacaducidad = fechacaducidad.AddDays(90);
            var lote = _context.BatchDetails.Where(x => x.ProductionOrderId == op.Id).FirstOrDefault();

            mapped.ContainerPrimary = "Pipa";
            mapped.CreateDate = DateTime.Now;
            mapped.DueDate = fechacaducidad;
            mapped.LotProd = lote.Number;
            mapped.Presentation = ObjectDB.Presentation ?? catproductos.Presentation;

            mapped.ShowPanelSteps = "panel-collapse collapse overflow-hidden";

            if (WSMexeFuncionalidad)
            {
                var plantas = _external.LPM_VW_PLANTAS.Where(x => x.Id_Planta == Convert.ToInt32(op.PlantId)).FirstOrDefault();
                var producto = _external.LPM_VW_PRODUCTOS.Where(x => x.Product_Id == op.ProductId).FirstOrDefault();
                var tanque = _external.LPM_VW_TANQUES.Where(x => x.Descripcion == op.TankId).FirstOrDefault();

                mapped.Plant = plantas.Descripcion;
                mapped.Product = producto.Product_Name;
                mapped.Tank = tanque.Descripcion;
                ///
                mapped.Location = plantas.Identificador;
                mapped.ProductName = producto.Product_Id;

            }
            else
            {
                mapped.Plant = op.PlantId;
                mapped.Product = op.ProductId;
                mapped.Tank = op.TankId;
                mapped.Location = "1";
                mapped.ProductName = "OX";
            }

            return mapped;
        }

        private async Task<List<PipeFillingCustomerViewModel>> GetCustomersLinde(int ConditioningOrderId, List<String> local, string PlantId, string ProductId, string TankId)
        {
            var pipeFillingCustomers = new List<PipeFillingCustomer>();
            var CustomersAdd = new List<PipeFillingCustomerViewModel>();
            var ldetalledistDB = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId);
            var distributionBatch = ldetalledistDB.Select(x => x.DistributionBatch).Distinct().ToList();
            var customersPerPipe = await _lotesDistribuicionClienteRepository.GetAsync(x => distributionBatch.Contains(x.ID_LOTEPIPA)).ToListAsync();
            pipeFillingCustomers  = (await _pipeFillingCustomerRepository.GetAsync(x => x.ConditioningOrderId == ConditioningOrderId && local.Contains(x.TourNumber) && x.State != false)).ToList();
            var CustomerUpdate = customersPerPipe.Where(x => !string.IsNullOrEmpty(x.NVO_LINDENO));
            foreach (var item in CustomerUpdate)
            {
                var info = pipeFillingCustomers.FirstOrDefault(x => x.DistributionBatch == item.ID_LOTEPIPA && x.Tank == item.TANQUE_CLIENTE);
                if(info != null)
                {
                    info.State = false;
                    await _pipeFillingCustomerRepository.UpdateAsync(info);
                }
                var Customers = customersPerPipe.FirstOrDefault(x => x.TANQUE_CLIENTE == item.NVO_LINDENO);
                if (Customers != null && info != null)
                {
                    CustomersAdd.Add(new PipeFillingCustomerViewModel
                    {
                        Tank = Customers.TANQUE_CLIENTE,
                        Name = Customers.CLIENTE,
                        DeliveryNumber = Customers.NO_ORDEN_ENTREGA,
                        InCompliance = Customers.CALIDAD != null ? true : false,
                        PipeFillingControlId = 0,
                        DistributionBatch = Customers.ID_LOTEPIPA,
                        TourNumber = Customers.TOURNUMBER,
                        ConditioningOrderId = ConditioningOrderId,
                        PlantIdentificador = PlantId,
                        ProductId = ProductId,
                        TankId = TankId,
                        state = true
                    });
                }
            }
            var mappedCustomers = ObjectMapper.Mapper.Map<IEnumerable<PipeFillingCustomer>>(CustomersAdd.Distinct());
            await _pipeFillingCustomerRepository.AddAsync(mappedCustomers);
            return CustomersAdd;
        }

        public async Task<PipeFillingCustomersFiles> SaveCustomersFiles(int ConditioningOrderId, string tourNumber, string distributionBatch, string user, IFormFileCollection files, string tank, string pipeNumber)
        {
            var pipeCustomers = new PipeFillingCustomersFiles();
            string FileName = Guid.NewGuid().ToString();
            var upload = Path.Combine(_resource.GetString("PathPipeFillingCustomersFiles").Value);
            if (!Directory.Exists(upload))
                Directory.CreateDirectory(upload);
            var ext = Path.GetExtension(files[0].FileName);
            using (var fileStreams = new FileStream(Path.Combine(upload, FileName + ext), FileMode.Create))
            {
                files[0].CopyTo(fileStreams);
            }
            pipeCustomers = PipeFillingCustomersFiles.Create(
                user,
                distributionBatch,
                tourNumber,
                pipeNumber,
                ConditioningOrderId,
                tank, 
                FileName + ext,
                files[0].FileName);
            await _pipeFillingCustomersFilesRepository.AddAsync(pipeCustomers);
            return pipeCustomers;
        }
        public async Task<bool> ValidateIsReleaseTourNumberPipe(string tourNumber, int conditioningOrderId)
        {
            var exist = new bool();
            var pf = new List<PipeFilling>();
            var pc = (await _pipeFillingControlRepository.GetAsync(x => x.TourNumber == tourNumber 
                        && x.ConditioningOrderId == conditioningOrderId)).FirstOrDefault();
            if(pc != null)
                pf = (await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == pc.Id)).Where(x=>x.IsReleased == true).ToList();

            exist = pf.Any(); 
           return exist;    
        }
    }
}
