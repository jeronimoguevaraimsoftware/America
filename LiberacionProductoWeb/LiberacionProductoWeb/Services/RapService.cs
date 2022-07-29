using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository.Base.External;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.RAPModels;
using LiberacionProductoWeb.Models.Principal;
using Microsoft.AspNetCore.Mvc.Rendering;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.CheckListViewModels;
using Microsoft.AspNetCore.Identity;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Http;
using LiberacionProductoWeb.Localize;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using System.Globalization;

namespace LiberacionProductoWeb.Services
{
    public class RapService : IRapService
    {
        private AppDbContext _context;
        private readonly IPlantasRepository _plantasRepository;
        private readonly ITanquesRepository _tanquesRepository;
        private readonly IConfiguration _config;
        private readonly IProductoRepository _productoRepository;
        private readonly IDispositionCatalogRepository _dispositionCatalogRepository;
        private readonly IFormulaCatalogRepository _formulaCatalog;
        private readonly IAnalyticsCertsService _analyticsCertsService;
        private readonly IStabilityCatalogRepository _stabilityCatalog;
        private readonly IPrincipalService _principalService;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IComplementoTanqueRepository _complementoTanqueRepository;
        private readonly IComplementoPipaRepository _complementoPipaRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IPipeFillingControlRepository _pipeFillingControlRepository;
        private readonly IPipeFillingRepository _pipeFillingRepository;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IPipelineClearanceOARepository _pipelineClearanceOARepository;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;
        private readonly IEquipmentProcessConditioningRepository _equipmentProcessConditioningRepository;
        private readonly IPipeFillingAnalysisRepository _pipeFillingAnalysisRepository;
        public bool WSMexeFuncionalidad;

        public RapService(AppDbContext context, IPlantasRepository plantasRepository,
        ITanquesRepository tanquesRepository, IProductoRepository productoRepository, IConfiguration config,
        IFormulaCatalogRepository formulaCatalog, IAnalyticsCertsService analyticsCertsService, IPrincipalService principalService,
        IProductionOrderAttributeRepository productionOrderAttributeRepository,
        IDispositionCatalogRepository dispositionCatalogRepository, IProductionOrderRepository productionOrderRepository,
        IBatchDetailsRepository batchDetailsRepository, IComplementoTanqueRepository complementoTanqueRepository,
        IComplementoPipaRepository complementoPipaRepository,
        UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
        IPipeFillingControlRepository pipeFillingControlRepository, IPipelineClearanceRepository pipelineClearanceRepository,
        IPipeFillingRepository pipeFillingRepository, IStringLocalizer<Resource> resource,
        IConditioningOrderRepository conditioningOrderRepository, IPipelineClearanceOARepository pipelineClearanceOARepository,
        IEquipmentProcessConditioningRepository equipmentProcessConditioningRepository,
        IPipeFillingAnalysisRepository pipeFillingAnalysisRepository)
        {
            _context = context;
            _plantasRepository = plantasRepository;
            _tanquesRepository = tanquesRepository;
            _productoRepository = productoRepository;
            _config = config;
            _formulaCatalog = formulaCatalog;
            _principalService = principalService;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _dispositionCatalogRepository = dispositionCatalogRepository;
            _productionOrderRepository = productionOrderRepository;
            bool.TryParse(_config["FlagWSMexe:ServiceApiKey"], out WSMexeFuncionalidad);
            _analyticsCertsService = analyticsCertsService;
            _batchDetailsRepository = batchDetailsRepository;
            _complementoTanqueRepository = complementoTanqueRepository;
            _complementoPipaRepository = complementoPipaRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _resource = resource;
            _pipeFillingControlRepository = pipeFillingControlRepository;
            _pipeFillingRepository = pipeFillingRepository;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _conditioningOrderRepository = conditioningOrderRepository;
            _equipmentProcessConditioningRepository = equipmentProcessConditioningRepository;
            _pipeFillingAnalysisRepository = pipeFillingAnalysisRepository;
            _pipelineClearanceOARepository = pipelineClearanceOARepository;
        }

        public List<SelectListItem> GetPlants()
        {
            List<SelectListItem> plants = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var plantas = _plantasRepository.GetAll();
                foreach (var item in plantas)
                {
                    plants.Add(new SelectListItem { Value = item.Id_Planta.ToString(), Text = item.Descripcion });
                }
            }
            else
            {

                plants.Add(new SelectListItem { Value = "1", Text = "Planta 1" });
                plants.Add(new SelectListItem { Value = "2", Text = "Planta 2" });
                plants.Add(new SelectListItem { Value = "3", Text = "Planta 3" });
                plants.Add(new SelectListItem { Value = "4", Text = "Planta 4" });
                plants.Add(new SelectListItem { Value = "5", Text = "Planta 5" });

            }
            return plants;
        }

        public List<SelectListItem> GetProducts()
        {
            List<SelectListItem> products = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var productos = _productoRepository.GetAll();
                foreach (var item in productos)
                {
                    products.Add(new SelectListItem { Value = item.Product_Id, Text = item.Product_Name });
                }
            }
            else
            {
                products.Add(new SelectListItem { Value = "OX", Text = _resource.GetString("Oxygen") });
                products.Add(new SelectListItem { Value = "NI", Text = _resource.GetString("Nitrogen") });
            }
            return products;
        }

        public async Task<List<SelectListItem>> GetDispositions()
        {
            List<SelectListItem> dispositions = new List<SelectListItem>();
            if (WSMexeFuncionalidad)
            {
                var disposicion = await _dispositionCatalogRepository.GetAsync(x => x.Status == true);
                foreach (var item in disposicion)
                {
                    dispositions.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.DispositionType });
                }
            }
            else
            {
                var disposicion = await _dispositionCatalogRepository.GetAsync(x => x.Status == true);
                foreach (var item in disposicion)
                {
                    dispositions.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.DispositionType });
                }
            }
            return dispositions;
        }

        public async Task<List<TanqueModel>> GetRapTanques(string plantId, string productId, string tankId)
        {
            var plants = GetPlants();
            var products = GetProducts();

            var opList = await _productionOrderRepository.GetAsync(x => x.PlantId == plantId && x.ProductId == productId && x.TankId == tankId && x.IsReleased.HasValue);

            var rapTanques = new List<TanqueModel>();
            foreach (var op in opList)
            {
                var batch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == op.Id && !string.IsNullOrEmpty(x.Number))).FirstOrDefault();
                var tankComplement = (await _complementoTanqueRepository.GetAsync(x => x.ProductionOrderId == op.Id)).FirstOrDefault();
                var opVariables = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == op.Id);
                var folio = string.Join(',', opVariables.Where(x => !string.IsNullOrEmpty(x.DeviationReportFolio) && x.DeviationReportFolio != "NA").Select(x => x.DeviationReportFolio).Distinct());

                var dispositions = await DisposicionXplanta(op.PlantId, plants);

                rapTanques.Add(new TanqueModel
                {
                    Id = op.Id,
                    Plant = plants.Where(pl => pl.Value == op.PlantId).SingleOrDefault().Text,
                    PlantId = op.PlantId,
                    Product = products.Where(pr => pr.Value == op.ProductId).SingleOrDefault().Text,
                    ProductId = op.ProductId,
                    Tank = op.TankId,
                    TankId = op.TankId,
                    Presentation = "NA",
                    Purity = op.Purity,
                    HealthRegister = "NA",
                    PharmaceuticalForm = "NA",
                    Fecha = op.CreatedDate.Value,
                    Hora = op.CreatedDate.HasValue ? op.CreatedDate.Value.ToString("HH:mm") : string.Empty,
                    NumeroLoteProduccion = batch?.Number ?? "NA",
                    Dictamen = op.IsReleased.Value == true ? "Sí" : "No",
                    BatchDetails = new BatchDetailsViewModel
                    {
                        Number = batch?.Number,
                        Tank = batch?.Tank,
                        Level = batch?.Level ?? 0,
                        Size = batch?.Size ?? 0,
                        AnalyzedBy = batch?.AnalyzedBy ?? "NA",
                        AnalyzedDate = batch?.AnalyzedDate,
                        InCompliance = batch?.InCompliance,
                        NotInComplianceFolio = batch?.NotInComplianceFolio ?? "NA",
                        NotInComplianceNotes = batch?.NotInComplianceNotes ?? "NA",
                        IsReleased = batch?.IsReleased,
                        ReleasedBy = batch?.ReleasedBy ?? "NA",
                        ReleasedDate = batch?.ReleasedDate,
                        ReleasedNotes = batch?.ReleasedNotes ?? "NA"
                    },
                    FolioPNC = batch?.NotInComplianceFolio ?? "NA",
                    DisposicionPNC = tankComplement?.DisposicionPNC ?? "NA",
                    DisposicionPNCText = dispositions.Where(x => x.Value == tankComplement?.DisposicionPNC).Any() ?
                                dispositions.Where(x => x.Value == tankComplement?.DisposicionPNC).Select(x => x.Text).FirstOrDefault() : null,
                    FolioControl = tankComplement?.FolioControlCambios ?? "NA",
                    Observaciones = tankComplement?.Observaciones ?? "NA",
                    FolioTrabajo = tankComplement?.FolioTrabajoNoConforme ?? "NA",
                    FolioDesviacion = !string.IsNullOrEmpty(folio) ? folio : "NA",
                });
            };

            return rapTanques;
        }

        public async Task<List<SelectListItem>> GetPresentaciones(string productId)
        {
            List<SelectListItem> Presentaciones = new List<SelectListItem>();
            List<SelectListItem> Presentacionesfilter = new List<SelectListItem>();

            var presentaciones = await _formulaCatalog.GetAsync(x => x.Status == true && x.ProductId == productId);
            foreach (var item in presentaciones)
            {
                Presentaciones.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Presentation });
            }
            //Presentacionesfilter = Presentaciones.Distinct().ToList();
            IEnumerable<SelectListItem> ldidList = Presentaciones.Select(c => new SelectListItem
            {
                Text = c.Text.ToString(),
            }).Distinct(new SelectListItemComparer());
            foreach (var item2 in ldidList)
            {
                Presentacionesfilter.Add(new SelectListItem { Text = item2.Text });
            }

            return Presentacionesfilter;
        }

        public class SelectListItemComparer : IEqualityComparer<SelectListItem>
        {
            public bool Equals(SelectListItem x, SelectListItem y)
            {
                return x.Text == y.Text && x.Value == y.Value;
            }

            public int GetHashCode(SelectListItem item)
            {
                int hashText = item.Text == null ? 0 : item.Text.GetHashCode();
                int hashValue = item.Value == null ? 0 : item.Value.GetHashCode();
                return hashText ^ hashValue;
            }
        }

        public async Task<List<SelectListItem>> GetPurezas(string productId)
        {
            List<SelectListItem> Purezas = new List<SelectListItem>();
            List<SelectListItem> Purezasfilter = new List<SelectListItem>();
            var purezas = await _formulaCatalog.GetAsync(x => x.Status == true && x.ProductId == productId);
            foreach (var item in purezas)
            {
                Purezas.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Purity });
            }
            IEnumerable<SelectListItem> ldidList = Purezas.Select(c => new SelectListItem
            {
                Text = c.Text.ToString(),
            }).Distinct(new SelectListItemComparer());
            foreach (var item2 in ldidList)
            {
                Purezasfilter.Add(new SelectListItem { Text = item2.Text });
            }
            return Purezasfilter;
        }

        public async Task<List<SelectListItem>> GetRegistrosSan(string productId)
        {
            List<SelectListItem> Registro = new List<SelectListItem>();
            List<SelectListItem> Registrofilter = new List<SelectListItem>();
            var registro = await _formulaCatalog.GetAsync(x => x.Status == true && x.ProductId == productId);
            foreach (var item in registro)
            {
                Registro.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.RegisterCode });
            }
            IEnumerable<SelectListItem> ldidList = Registro.Select(c => new SelectListItem
            {
                Text = c.Text.ToString(),
            }).Distinct(new SelectListItemComparer());
            foreach (var item2 in ldidList)
            {
                Registrofilter.Add(new SelectListItem { Text = item2.Text });
            }
            return Registrofilter;
        }

        public async Task<List<SelectListItem>> GetFormFarm(string productId)
        {
            List<SelectListItem> Formula = new List<SelectListItem>();
            List<SelectListItem> Formulafilter = new List<SelectListItem>();
            var formula = await _formulaCatalog.GetAsync(x => x.Status == true && x.ProductId == productId);
            foreach (var item in formula)
            {
                Formula.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.FormulaName });
            }
            IEnumerable<SelectListItem> ldidList = Formula.Select(c => new SelectListItem
            {
                Text = c.Text.ToString(),
            }).Distinct(new SelectListItemComparer());
            foreach (var item2 in ldidList)
            {
                Formulafilter.Add(new SelectListItem { Text = item2.Text });
            }
            return Formulafilter;
        }

        public async Task<EstudioEstabilidad> GetDatosEstudio(int id)
        {
            var est = await _stabilityCatalog.GetAllAsync();
            var record = est.Where(x => x.PlantId.Equals(id)).Select(x => new EstudioEstabilidad
            {
                AnioEstudio = x.StudyDate.ToString("yyyy-MM-dd HH:mm"),
                Estado = x.Status.ToString(),
                Codigo = x.Code,
                Observaciones = x.Observations
            }).FirstOrDefault();

            return record;
        }

        public async Task<List<PipaModel>> GetRapPipas(string plantId, string productId, string tankId)
        {
            var rapPipas = new List<PipaModel>();
            var oaList = new List<ConditioningOrder>();

            var opList = await _productionOrderRepository.GetAsync(x => x.PlantId == plantId && x.ProductId == productId && x.TankId == tankId);

            var plants = GetPlants();
            var products = GetProducts();

            foreach (var opItem in opList)
            {
                var oaItem = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == opItem.Id)).FirstOrDefault();
                var opBatch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == opItem.Id)).FirstOrDefault();

                if (oaItem != null)
                {
                    var opPipelineOA = (await _pipelineClearanceOARepository.GetAsync(x => x.ConditioningOrderId == opItem.Id && x.InCompliance.HasValue && !x.InCompliance.Value)).ToList();
                    var pipeAnalysis = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id);
                    var pipeControl = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id);
                    var pipeComplement = (await _complementoPipaRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id)).FirstOrDefault();

                    var initialAnalysisParamsNames = new List<string>();
                    var finalAnalysisParamsNames = new List<string>();

                    var dispositions = await DisposicionXplanta(opItem.PlantId, plants);

                    foreach (var pipeControlItem in pipeControl)
                    {
                        var pipeFilling = (await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == pipeControlItem.Id)).FirstOrDefault();

                        foreach (var analysis in pipeAnalysis.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value))
                        {
                            if (!initialAnalysisParamsNames.Contains(analysis.ParameterName))
                            {
                                initialAnalysisParamsNames.Add(analysis.ParameterName);
                            }
                        }

                        foreach (var analysis in pipeAnalysis.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value))
                        {
                            if (!finalAnalysisParamsNames.Contains(analysis.ParameterName))
                            {
                                finalAnalysisParamsNames.Add(analysis.ParameterName);
                            }
                        }

                        rapPipas.Add(new PipaModel
                        {
                            Id = oaItem.Id,
                            ProductionOrderId = opItem.Id,
                            Plant = plants.Where(pl => pl.Value == opItem.PlantId).SingleOrDefault().Text,
                            PlantId = opItem.PlantId,
                            Product = products.Where(pr => pr.Value == opItem.ProductId).SingleOrDefault().Text,
                            ProductId = opItem.ProductId,
                            Tank = opItem.TankId,
                            TankId = opItem.TankId,
                            Fecha = pipeFilling.FinalAnalyzedDate,
                            Hora = pipeFilling.FinalAnalyzedDate.HasValue ? pipeFilling.FinalAnalyzedDate.Value.ToShortTimeString() : string.Empty,
                            FechaCaducidad = pipeFilling.DueDate,
                            NumeroLoteProduccion = opBatch?.Number,
                            NumeroLoteDistribucion = pipeFilling.DistributionBatch,
                            InitialAnalysisParamsNames = initialAnalysisParamsNames,
                            FinalAnalysisParamsNames = finalAnalysisParamsNames,
                            aInicial = pipeAnalysis.Where(x => x.Type == PipeFillingAnalysisType.InitialAnalysis.Value && x.DistributionBatch == pipeFilling.DistributionBatch)
                                                .Select(x => new Analisis
                                                {
                                                    Id = x.Id,
                                                    ParameterName = x.ParameterName,
                                                    DistributionBatch = x.DistributionBatch,
                                                    ConditioningOrderId = x.ConditioningOrderId,
                                                    Value = x.ValueReal.Replace(',', '.'),
                                                    Specification = x.ValueExpected,
                                                    PipeNumber = x.PipeNumber
                                                }).OrderBy(x => x.ParameterName).ToList(),
                            aFinal = pipeAnalysis.Where(x => x.Type == PipeFillingAnalysisType.FinalAnalysis.Value && x.DistributionBatch == pipeFilling.DistributionBatch)
                                                .Select(x => new Analisis
                                                {
                                                    Id = x.Id,
                                                    ParameterName = x.ParameterName,
                                                    DistributionBatch = x.DistributionBatch,
                                                    ConditioningOrderId = x.ConditioningOrderId,
                                                    Value = x.ValueReal.Replace(',', '.'),
                                                    Specification = x.ValueExpected,
                                                    PipeNumber = x.PipeNumber
                                                }).OrderBy(x => x.ParameterName).ToList(),
                            aseguramiento = new AseguramientoAcon
                            {
                                FolioInformeDesviacion = opPipelineOA.Any() ? opPipelineOA.Select(x => x.Bill).FirstOrDefault() : "NA",
                                DisposicionPNC = pipeComplement?.DisposicionPNC ?? "NA",
                                DisposicionPNCText = dispositions.Where(x => x.Value == pipeComplement?.DisposicionPNC).Any() ?
                                    dispositions.Where(x => x.Value == pipeComplement?.DisposicionPNC).Select(x => x.Text).FirstOrDefault() : "NA",
                                FolioDeControlDeCambios = pipeComplement?.FolioControlCambios ?? "NA",
                                FolioEventoAdverso = pipeComplement?.FolioEventoAdverso ?? "NA",
                                FolioPNC = pipeFilling.ReportPNCFolio ?? "NA",
                                FolioRetiroProductos = pipeComplement?.FolioRetiroProductos ?? "NA",
                                FolioTrabajoNoCOnforme = pipeComplement?.FolioTrabajoNoConforme ?? "NA",
                                NumeroDeDevolucion = pipeComplement?.NumeroDeDevolucion ?? "NA"
                            },
                            Observaciones = pipeComplement?.Observaciones ?? "NA",
                            Aprobado = (pipeFilling.IsReleased.HasValue && pipeFilling.IsReleased.Value) ? "Sí" : "No",
                        });
                    }
                }
            }

            return rapPipas;
        }

        public async Task<List<RapComplemento>> GetRapCompPipas(string plantId, string productId, string tankId)
        {
            var rapPipas = new List<RapComplemento>();
            var oaList = new List<ConditioningOrder>();

            var opList = await _productionOrderRepository.GetAsync(x => x.PlantId == plantId && x.ProductId == productId && x.TankId == tankId);

            var plants = GetPlants();
            var products = GetProducts();
            var dispositions = await DisposicionXplanta(plantId, plants);
            foreach (var opItem in opList)
            {
                var oaItem = (await _conditioningOrderRepository.GetAsync(x => x.ProductionOrderId == opItem.Id)).FirstOrDefault();
                var opPipeline = (await _pipelineClearanceRepository.GetAsync(x => x.ProductionOrderId == opItem.Id && x.InCompliance.HasValue && !x.InCompliance.Value)).ToList();
                var opBatch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == opItem.Id)).FirstOrDefault();

                if (oaItem != null)
                {
                    var pipeAnalysis = await _pipeFillingAnalysisRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id);
                    var pipeControl = await _pipeFillingControlRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id);
                    var pipeComplement = (await _complementoPipaRepository.GetAsync(x => x.ConditioningOrderId == oaItem.Id)).FirstOrDefault();

                    foreach (var pipeControlItem in pipeControl)
                    {
                        var pipeFilling = (await _pipeFillingRepository.GetAsync(x => x.PipeFillingControlId == pipeControlItem.Id)).FirstOrDefault();
                        if (pipeFilling != null)
                        {
                            rapPipas.Add(new RapComplemento
                            {
                                Id = 0,
                                ConditioningOrderId = oaItem.Id,
                                Fecha = pipeFilling.FinalAnalyzedDate,
                                Hora = pipeFilling.FinalAnalyzedDate.HasValue ? pipeFilling.FinalAnalyzedDate.Value.ToString("HH:mm") : string.Empty,
                                NumeroLoteProduccion = opBatch?.Number,
                                NumeroLoteDistribucion = pipeFilling.DistributionBatch,
                                DisposicionPNC = pipeComplement?.DisposicionPNC ?? "NA",
                                DisposicionPNCText = dispositions.Where(x => x.Value == pipeComplement?.DisposicionPNC).Any() ?
                                dispositions.Where(x => x.Value == pipeComplement?.DisposicionPNC).Select(x => x.Text).FirstOrDefault() : "NA",
                                FolioControlCambios = pipeComplement?.FolioControlCambios ?? "NA",
                                FolioEventoAdverso = pipeComplement?.FolioEventoAdverso ?? "NA",
                                FolioPNC = pipeFilling.ReportPNCFolio ?? "NA",
                                FolioRetiroProductos = pipeComplement?.FolioRetiroProductos ?? "NA",
                                FolioTrabajoNoConforme = pipeComplement?.FolioTrabajoNoConforme ?? "NA",
                                NumeroDeDevolucion = pipeComplement?.NumeroDeDevolucion ?? "NA",
                                Observaciones = pipeComplement?.Observaciones ?? "NA"
                            });
                        }
                    }
                }
            }

            return rapPipas;
        }

        public async Task<List<RapComplemento>> GetRapCompTanques(string plantId, string productId, string tankId)
        {
            var plants = GetPlants();
            var products = GetProducts();
            var disposicion = await GetDispositions();
            var dispositions = await DisposicionXplanta(plantId, plants);
            var opList = await _productionOrderRepository.GetAsync(x => x.PlantId == plantId && x.ProductId == productId && x.TankId == tankId && x.IsReleased.HasValue);

            var rapTanques = new List<RapComplemento>();
            foreach (var op in opList)
            {
                var batch = (await _batchDetailsRepository.GetAsync(x => x.ProductionOrderId == op.Id && !string.IsNullOrEmpty(x.Number))).FirstOrDefault();
                var tankComplement = (await _complementoTanqueRepository.GetAsync(x => x.ProductionOrderId == op.Id)).FirstOrDefault();

                rapTanques.Add(new RapComplemento
                {
                    Id = (tankComplement != null) ? tankComplement.Id : 0,
                    ProductionOrderId = op.Id,
                    Plant = plants.Where(pl => pl.Value == op.PlantId).SingleOrDefault().Text,
                    PlantId = op.PlantId,
                    Product = products.Where(pr => pr.Value == op.ProductId).SingleOrDefault().Text,
                    ProductId = op.ProductId,
                    Tank = op.TankId,
                    TankId = op.TankId,
                    Fecha = op.CreatedDate.Value,
                    Hora = op.CreatedDate.HasValue ? op.CreatedDate.Value.ToString("HH:mm") : string.Empty,
                    NumeroLoteProduccion = batch?.Number ?? "NA",
                    FolioPNC = batch?.NotInComplianceFolio ?? "NA",
                    DisposicionPNC = tankComplement?.DisposicionPNC ?? "NA",
                    DisposicionPNCText = dispositions.Where(x => x.Value == tankComplement?.DisposicionPNC).Any() ?
                        dispositions.Where(x => x.Value == tankComplement?.DisposicionPNC).Select(x => x.Text).FirstOrDefault() : "NA",
                    Observaciones = tankComplement?.Observaciones ?? "NA",
                    FolioControlCambios = tankComplement?.FolioControlCambios ?? "NA",
                    FolioTrabajoNoConforme = tankComplement?.FolioTrabajoNoConforme ?? "NA",
                });
            };

            return rapTanques;
        }

        public List<AnalisisPipa> GetAnalisis(List<List<Analisis>> rapAnalisys, string type, string productId)
        {
            var initialAnalysisParams = new List<Analisis>();
            foreach (var analysis in rapAnalisys)
            {
                initialAnalysisParams.AddRange(analysis);
            }

            var initialAnalysisParamsNames = initialAnalysisParams.Select(x => x.ParameterName).Distinct();

            List<AnalisisPipa> pcc = new List<AnalisisPipa>();

            var limits = new List<(string Product, string Variable, string LowLimit, string TopLimit)>
            {
                (Product: "OX", Variable: "Pureza Mínima", LowLimit: "99.50%", TopLimit: "100%"),
                (Product: "OX", Variable: "Humedad", LowLimit: "0 ppm", TopLimit: "67 ppm"),
                (Product: "OX", Variable: "Identidad", LowLimit: "NA", TopLimit: "NA"),
                (Product: "OX", Variable: "Monóxido de Carbono", LowLimit: "0 ppm", TopLimit: "5 ppm"),
                (Product: "OX", Variable: "Bióxido de Carbono", LowLimit: "0 ppm", TopLimit: "300 ppm"),
                (Product: "OX", Variable: "Oxígeno", LowLimit: "NA", TopLimit: "NA"),
                (Product: "OX", Variable: "N", LowLimit: "NA", TopLimit: "NA"),
                (Product: "OX", Variable: "CH", LowLimit: "NA", TopLimit: "NA"),
                (Product: "OX", Variable: "NO2", LowLimit: "NA", TopLimit: "NA"),
                (Product: "NI", Variable: "Pureza Mínima", LowLimit: "99.50%", TopLimit: "100%"),
                (Product: "NI", Variable: "Humedad", "0 ppm", TopLimit: "67 ppm"),
                (Product: "NI", Variable: "Identidad", LowLimit: "NA", TopLimit: "NA"),
                (Product: "NI", Variable: "Monóxido de Carbono", LowLimit: "0 ppm", TopLimit: "5 ppm"),
                (Product: "NI", Variable: "Bióxido de Carbono", LowLimit: "0 ppm", TopLimit: "300 ppm"),
                (Product: "NI", Variable: "Oxígeno", LowLimit: "0 ppm", TopLimit: "50 ppm"),
                (Product: "NI", Variable: "N", LowLimit: "NA", TopLimit: "NA"),
                (Product: "NI", Variable: "CH", LowLimit: "NA", TopLimit: "NA"),
                (Product: "NI", Variable: "NO2", LowLimit: "NA", TopLimit: "NA"),
                (Product: "AR", Variable: "Pureza Mínima", LowLimit: "99.995%", TopLimit: "100%"),
                (Product: "AR", Variable: "Humedad", "0 ppm", TopLimit: "10 ppm"),
                (Product: "AR", Variable: "Identidad", LowLimit: "NA", TopLimit: "NA"),
                (Product: "AR", Variable: "Monóxido de Carbono", LowLimit: "NA", TopLimit: "NA"),
                (Product: "AR", Variable: "Bióxido de Carbono", LowLimit: "NA", TopLimit: "NA"),
                (Product: "AR", Variable: "Oxígeno", LowLimit: "0 ppm", TopLimit: "5 ppm"),
                (Product: "AR", Variable: "N", LowLimit: "0 ppm", TopLimit: "5 ppm"),
                (Product: "AR", Variable: "CH", LowLimit: "0 ppm", TopLimit: "5 ppm"),
                (Product: "AR", Variable: "NO2", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "Pureza Mínima", LowLimit: "99.50%", TopLimit: "100%"),
                (Product: "CD", Variable: "Humedad", "0 ppm", TopLimit: "67 ppm"),
                (Product: "CD", Variable: "Identidad", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "Monóxido de Carbono", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "Bióxido de Carbono", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "Oxígeno", LowLimit: "NA", TopLimit: ""),
                (Product: "CD", Variable: "N", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "CH", LowLimit: "NA", TopLimit: "NA"),
                (Product: "CD", Variable: "NO2", LowLimit: "0 ppm", TopLimit: "2 ppm"),
                (Product: "CD", Variable: "NO+NO2", LowLimit: "0 ppm", TopLimit: "2 ppm"),
            };

            foreach (var paramName in initialAnalysisParamsNames)
            {
                var lic = limits.Where(x => x.Product == productId && x.Variable.ToLower().Equals(paramName.ToLower())).Select(x => x.LowLimit).FirstOrDefault();
                var lsc = limits.Where(x => x.Product == productId && x.Variable.ToLower().Equals(paramName.ToLower())).Select(x => x.TopLimit).FirstOrDefault();

                var specification = initialAnalysisParams.Where(x => x.ParameterName == paramName).Select(x => x.Specification).FirstOrDefault();

                var values = initialAnalysisParams.Where(x => x.ParameterName == paramName).Select(x =>
                {
                    var valueText = x.Value.Replace(',', '.');
                    double val;
                    double.TryParse(valueText, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                    return val;
                });

                var count = values.Count();
                var sum = values.Sum();
                var avg = values.Average();
                var sumStd = values.Select(x => { return x > 0 ? Math.Pow(x - avg, 2) : 0; }).Sum();
                var std = StandardDeviation(values);
                var max = values.Max();
                var min = values.Min();

                pcc.Add(new AnalisisPipa
                {
                    Etapa = type,
                    Parametro = paramName,
                    Especificacion = specification,
                    LIC = lic,
                    LSC = lsc,
                    ValorMax = string.Format("{0:0.##}", max).Replace(',', '.'),
                    ValorMin = string.Format("{0:0.##}", min).Replace(',', '.'),
                    ValorProm = sum > 0 ? string.Format("{0:0.##}", (sum / count)).Replace(',', '.') : "",
                    DesvEstandar = std.ToString() == "NaN" ? "NA" : (std != 0) ? string.Format("{0:0.##}", std).Replace(',', '.') : "No existe",
                });
            }

            return pcc;

        }

        public async Task<List<SelectListItem>> DisposicionXplanta(string plantId, List<SelectListItem> DisposicionPNC)
        {
            List<SelectListItem> dpnc = new List<SelectListItem>();

            var disposicion = await _dispositionCatalogRepository.GetAsync(x => x.Status == true);
            foreach (var item in disposicion)
            {
                dpnc.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.DispositionType });
                // Console.Write("valor de disp -> " + item.DispositionType + "\n");

            }

            return dpnc;


        }

        private double StandardDeviation(IEnumerable<double> sequence)
        {
            try
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                return Math.Sqrt((sum) / (sequence.Count() - 1));
            }
            catch
            {
                return 0;
            }
        }
    }
}