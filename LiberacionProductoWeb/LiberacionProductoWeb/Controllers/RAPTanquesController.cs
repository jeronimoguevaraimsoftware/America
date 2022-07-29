using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using LiberacionProductoWeb.Models.ConfigViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Helpers;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.RAPModels;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class RAPTanquesController : Controller
    {
        private readonly ILogger<RAPTanquesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly Services.IUsersLogin _usersLogin;
        public bool WSMexeFuncionalidad;
        private readonly IConfiguration _config;
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IPrincipalService _principalService;
        private readonly IRapService _rapService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductionOrderAttributeRepository _productionOrderAttributeRepository;
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IBatchDetailsRepository _batchDetailsRepository;
        private readonly IBatchAnalysisRepository _batchAnalysisRepository;
        private readonly IStabilityCatalogRepository _stabilityCatalogRepository;
        private readonly IComplementoTanqueRepository _complementoTanqueRepository;
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;

        public RAPTanquesController(ILogger<RAPTanquesController> logger, UserManager<ApplicationUser> userManager,
        IMapper mapper, Services.IUsersLogin usersLogin, IConfiguration config, IAnalyticsCertsService analyticsCertsService,
        IPrincipalService principalService, IRapService rapService, IStringLocalizer<Resource> resource,
        ILoggerFactory loggerFactory, IProductionOrderAttributeRepository productionOrderAttributeRepository,
        IProductionOrderRepository productionOrderRepository, IHttpContextAccessor httpContextAccessor,
        IBatchAnalysisRepository batchAnalysisRepository, IBatchDetailsRepository batchDetailsRepository,
        IStabilityCatalogRepository stabilityCatalogRepository,
        IComplementoTanqueRepository complementoTanqueRepository,
        IPipelineClearanceRepository pipelineClearanceRepository,
        IGeneralCatalogRepository generalCatalogRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _usersLogin = usersLogin;
            _config = config;
            WSMexeFuncionalidad = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);
            _analyticsCerts = analyticsCertsService;
            _principalService = principalService;
            _rapService = rapService;
            _resource = resource;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
            _productionOrderAttributeRepository = productionOrderAttributeRepository;
            _productionOrderRepository = productionOrderRepository;
            _batchDetailsRepository = batchDetailsRepository;
            _batchAnalysisRepository = batchAnalysisRepository;
            _stabilityCatalogRepository = stabilityCatalogRepository;
            _complementoTanqueRepository = complementoTanqueRepository;
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _generalCatalogRepository = generalCatalogRepository;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.CONSULTAR_RAP_TANQUES)]
        public async Task<IActionResult> Index(String SelectedPlantFilter, String SelectedProductFilter, String SelectedTankFilter,
        String SelectedPresentationFilter, String SelectedPurityFilter, String SelectedHealthRegisterFilter,
        String SelectedPharmaceuticalFormFilter, DateTime? StartDate, DateTime? EndDate)
        {

            TanqueViewModel tanqueModel = new TanqueViewModel();
            ConfiguracionUsuarioVM tanqueModelconfiguracionUsuario = new ConfiguracionUsuarioVM();

            // Initialize filters
            // var plants = await GetPlantsItemsAsync();
            // tanqueModel.ListPlants = plants;

            // if (!string.IsNullOrEmpty(SelectedPlantFilter))
            // {
            //     var products = await GetProdutsItemsAsync(SelectedPlantFilter);
            //     tanqueModel.ListProducts = products;

            //     if (!string.IsNullOrEmpty(SelectedProductFilter))
            //     {
            //         var tanks = await GetTanksItemsAsync(SelectedPlantFilter, SelectedProductFilter);
            //         tanqueModel.ListTanks = tanks;

            //         var presentaciones = await _rapService.GetPresentaciones(SelectedPlantFilter, SelectedProductFilter);
            //         tanqueModel.ListPresentation = presentaciones;

            //         var purezas = await _rapService.GetPurezas(SelectedPlantFilter, SelectedProductFilter);
            //         tanqueModel.ListPurity = purezas;

            //         var registro = await _rapService.GetRegistrosSan(SelectedPlantFilter, SelectedProductFilter);
            //         tanqueModel.ListHealthRegister = registro;

            //         var formula = await _rapService.GetFormFarm(SelectedPlantFilter, SelectedProductFilter);
            //         tanqueModel.ListPharmaceuticalForm = formula;
            //     }
            // }

            tanqueModel.ListRAPTanques = new List<TanqueModel>();

            tanqueModel.SelectedPlantFilter = SelectedPlantFilter;
            tanqueModel.SelectedProductFilter = SelectedProductFilter;
            tanqueModel.SelectedTankFilter = SelectedTankFilter;
            tanqueModel.SelectedPresentationFilter = SelectedPresentationFilter;
            tanqueModel.SelectedPurityFilter = SelectedPurityFilter;
            tanqueModel.SelectedHealthRegisterFilter = SelectedHealthRegisterFilter;
            tanqueModel.SelectedPharmaceuticalFormFilter = SelectedPharmaceuticalFormFilter;

            if (string.IsNullOrEmpty(SelectedPlantFilter) || string.IsNullOrEmpty(SelectedProductFilter) || string.IsNullOrEmpty(SelectedTankFilter))
            {
                return View(tanqueModel);
            }

            try
            {
                //fill filters
                // tanqueModel.Plant = new Models.RAPModels.Plant()
                // {
                //     Id = (!plants.Any()) ? 0 : int.Parse((plants.FirstOrDefault().Value)),
                //     Name = (!plants.Any()) ? _resource.GetString("NoInformation") : plants.FirstOrDefault().Text
                // };

                //filter by criteria plant, product and tank
                var raptanques = await _rapService.GetRapTanques(SelectedPlantFilter, SelectedProductFilter, SelectedTankFilter);
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<TanqueModel>>(raptanques);
                tanqueModel.ListRAPTanques = (List<TanqueModel>)mapped;

                //filter by criteria presentation
                // if (SelectedPresentationFilter != null && SelectedPresentationFilter.Any(x => !string.IsNullOrEmpty(x)))
                // {
                //     tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques.Where(x => SelectedPresentationFilter.Contains(x.Presentation)).ToList();
                // }

                // filter by criteria pureza
                // if (SelectedPurityFilter != null && SelectedPurityFilter.Any(x => !string.IsNullOrEmpty(x)))
                // {
                //     tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques.Where(x => SelectedPurityFilter.Contains(x.Purity)).ToList();
                // }

                // filter by criteria registro sanitario
                // if (SelectedHealthRegisterFilter != null && SelectedHealthRegisterFilter.Any(x => !string.IsNullOrEmpty(x)))
                // {
                //     tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques.Where(x => SelectedHealthRegisterFilter.Contains(x.HealthRegister)).ToList();
                // }

                // filter by criteria forma farmaceutica
                // if (SelectedPharmaceuticalFormFilter != null && SelectedPharmaceuticalFormFilter.Any(x => !string.IsNullOrEmpty(x)))
                // {
                //     tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques.Where(x => SelectedPharmaceuticalFormFilter.Contains(x.PharmaceuticalForm)).ToList();
                // }

                //filter by criteria date
                if (StartDate.HasValue)
                {
                    tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques
                                                .Where(x => StartDate.Value <= x.Fecha)
                                                .ToList();
                    tanqueModel.StartDate = StartDate;
                }
                if (EndDate.HasValue)
                {
                    tanqueModel.ListRAPTanques = tanqueModel.ListRAPTanques
                                                .Where(x => x.Fecha <= EndDate.Value.AddDays(1))
                                                .ToList();
                    tanqueModel.EndDate = EndDate;
                }

                var generalCatalogFilter = await _generalCatalogRepository.GetAsync(x => x.PlantId == SelectedPlantFilter && x.ProductId == SelectedProductFilter && x.TankId == SelectedTankFilter);

                foreach (var item in tanqueModel.ListRAPTanques)
                {
                    var opAttributesDb = await _productionOrderAttributeRepository.GetAsync(x => x.ProductionOrderId == item.Id);
                    var opAttributes = from x in opAttributesDb
                                       select new ProductionOrderAttribute
                                       {
                                           Area = x.Area,
                                           AvgValue = x.AvgValue,
                                           MaxValue = x.MaxValue,
                                           MinValue = x.MinValue,
                                           ChartPath = x.ChartPath,
                                           InCompliance = x.InCompliance,
                                           Description = x.Description,
                                           Variable = x.Variable,
                                           Specification = x.Specification,
                                           DeviationReportFolio = x.DeviationReportFolio,
                                           DeviationReportNotes = x.DeviationReportNotes,
                                           ProductionOrder = x.ProductionOrder,
                                           ReviewedDate = x.ReviewedDate,
                                           ReviewedBy = x.ReviewedBy,
                                           Notes = x.Notes,
                                           ProductionOrderId = x.ProductionOrderId,
                                           Type = x.Type
                                       };

                    var controlVariablesList = opAttributes.Where(x => x.Type == ProductionOrderAttributeType.ControlVariable);
                    item.ControlVariables = controlVariablesList.Select(x =>
                    {
                        return new ControlVariableViewModel
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
                            VariableCode = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.CodeTool,
                            LowLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.LowerLimit,
                            TopLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.UpperLimit
                        };
                    }).ToList();
                    var controlVariablesNames = item.ControlVariables.Select(x => x.Variable).Distinct();
                    foreach (var paramName in controlVariablesNames)
                    {
                        var specification = item.ControlVariables.Where(x => x.Variable == paramName).Select(x => x.Specification).FirstOrDefault();
                        var phase = item.ControlVariables.Where(x => x.Variable == paramName).Select(x => x.Description).FirstOrDefault();

                        var values = item.ControlVariables.Where(x => x.Variable == paramName).Select(x =>
                        {
                            var avgValue = x.AvgValue.Replace(',', '.');
                            double val;
                            double.TryParse(avgValue, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                            return val;
                        });

                        var max = values.Max();
                        var min = values.Min();

                        var count = values.Count();
                        var sum = values.Sum();

                        var variable = tanqueModel.ListVariables.Where(x => x.Etapa == phase
                                                                    && x.Parametro?.Split('/')[0].TrimEnd() == paramName?.Split('/')[0].TrimEnd())
                                                                .SingleOrDefault();
                        if (variable != null)
                        {
                            double valMax;
                            double valMin;
                            double.TryParse(variable.ValorMax.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMax);
                            double.TryParse(variable.ValorMin.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMin);
                            if (valMax < max)
                            {
                                variable.ValorMax = max.ToString().Replace(',', '.');
                            }
                            if (valMin > min)
                            {
                                variable.ValorMin = min.ToString().Replace(',', '.');
                            }

                            variable.Count += count;
                            variable.Sum += sum;

                            variable.Values.AddRange(values);
                            variable.ValorProm = (string.Format("{0:0.##}", variable.Values.Average())).Replace(',', '.');
                            variable.DesvEstandar = string.Format("{0:0.##}", StandardDeviation(variable.Values)).Replace(',', '.');
                        }
                        else
                        {
                            var lic = item.ControlVariables.Where(x => x.Variable == paramName).Select(x => x.LowLimit).FirstOrDefault();
                            var lsc = item.ControlVariables.Where(x => x.Variable == paramName).Select(x => x.TopLimit).FirstOrDefault();

                            tanqueModel.ListVariables.Add(new VariablesTanque
                            {
                                Etapa = phase,
                                Parametro = paramName + (!string.IsNullOrEmpty(specification) ? " / " + specification : ""),
                                Especificacion = specification,
                                LIC = lic,
                                LSC = lsc,
                                ValorMax = string.Format("{0:0.##}", max).Replace(',', '.'),
                                ValorMin = string.Format("{0:0.##}", min).Replace(',', '.'),
                                Count = count,
                                Sum = sum,
                                Values = values.ToList(),
                                ValorProm = string.Format("{0:0.##}", (sum / count)).Replace(',', '.'),
                                DesvEstandar = string.Format("{0:0.##}", StandardDeviation(values)).Replace(',', '.'),
                            });
                        }
                    }

                    var criticalParametersList = opAttributes.Where(x => x.Type == ProductionOrderAttributeType.CriticalParameter);
                    item.CriticalParameters = criticalParametersList.Select(x =>
                    {
                        return new CriticalParameterViewModel
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
                            VariableCode = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.CodeTool,
                            LowLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.LowerLimit,
                            TopLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.UpperLimit
                        };
                    }).ToList();
                    var criticalParametersNames = item.CriticalParameters.Select(x => x.Parameter).Distinct();
                    foreach (var paramName in criticalParametersNames)
                    {
                        var specification = item.CriticalParameters.Where(x => x.Parameter == paramName).Select(x => x.Specification).FirstOrDefault();
                        var phase = item.CriticalParameters.Where(x => x.Parameter == paramName).Select(x => x.Description).FirstOrDefault();

                        var values = item.CriticalParameters.Where(x => x.Parameter == paramName).Select(x =>
                        {
                            var avgValue = x.AvgValue.Replace(',', '.');
                            double val;
                            double.TryParse(avgValue, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                            return val;
                        });

                        var max = values.Max();
                        var min = values.Min();

                        var count = values.Count();
                        var sum = values.Sum();

                        var parametro = tanqueModel.ListParametros.Where(x => x.Etapa == phase
                                                                        && x.Parametro?.Split('/')[0].TrimEnd() == paramName?.Split('/')[0].TrimEnd())
                                                                    .SingleOrDefault();
                        if (parametro != null)
                        {
                            double valMax;
                            double valMin;
                            double.TryParse(parametro.ValorMax.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMax);
                            double.TryParse(parametro.ValorMin.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMin);
                            if (valMax < max)
                            {
                                parametro.ValorMax = max.ToString().Replace(',', '.');
                            }
                            if (valMin > min)
                            {
                                parametro.ValorMin = min.ToString().Replace(',', '.');
                            }

                            parametro.Count += count;
                            parametro.Sum += sum;

                            parametro.Values.AddRange(values);
                            parametro.ValorProm = string.Format("{0:0.##}", parametro.Values.Average()).Replace(',', '.');
                            parametro.DesvEstandar = string.Format("{0:0.##}", StandardDeviation(parametro.Values)).Replace(',', '.');
                        }
                        else
                        {
                            var lic = item.CriticalParameters.Where(x => x.Parameter == paramName).Select(x => x.LowLimit).FirstOrDefault();
                            var lsc = item.CriticalParameters.Where(x => x.Parameter == paramName).Select(x => x.TopLimit).FirstOrDefault();

                            var avg = (sum > 0 && count > 0) ? (sum / count) : 0;
                            var std = (sum != 0 && count > 0) ? Math.Sqrt((Math.Pow(sum - avg, 2)) / (count - 1)) : 0;

                            tanqueModel.ListParametros.Add(new VariablesTanque
                            {
                                Etapa = phase,
                                Parametro = paramName + (!string.IsNullOrEmpty(specification) ? " / " + specification : ""),
                                Especificacion = specification,
                                LIC = lic,
                                LSC = lsc,
                                ValorMax = string.Format("{0:0.##}", max).Replace(',', '.'),
                                ValorMin = string.Format("{0:0.##}", min).Replace(',', '.'),
                                Count = count,
                                Sum = sum,
                                Values = values.ToList(),
                                ValorProm = string.Format("{0:0.##}", (sum / count)).Replace(',', '.'),
                                DesvEstandar = string.Format("{0:0.##}", StandardDeviation(values)).Replace(',', '.'),
                            });
                        }
                    }

                    var criticalQualityAttributesList = opAttributes.Where(x => x.Type == ProductionOrderAttributeType.CriticalQualityAttribute);
                    item.CriticalQualityAttributes = criticalQualityAttributesList.Select(x =>
                    {
                        return new CriticalQualityAttributeViewModel
                        {
                            Id = x.Id,
                            ProductionOrderId = x.ProductionOrderId,
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
                            VariableCode = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.CodeTool,
                            LowLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.LowerLimit,
                            TopLimit = generalCatalogFilter
                                            .Where(g => g.Variable == x.Variable && g.Area == x.Area).FirstOrDefault()?.UpperLimit
                        };
                    }).ToList();
                    var criticalQualityAttributesNames = item.CriticalQualityAttributes.Select(x => x.Attribute).Distinct();
                    foreach (var paramName in criticalQualityAttributesNames)
                    {
                        var specification = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x => x.Specification).FirstOrDefault();
                        var phase = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x => x.Description).FirstOrDefault();

                        var values = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x =>
                        {
                            var avgValue = x.AvgValue.Replace(',', '.');
                            double val;
                            double.TryParse(avgValue, NumberStyles.Any, CultureInfo.InvariantCulture, out val);
                            return val;
                        });

                        var max = values.Max();
                        var min = values.Min();

                        var count = values.Count();
                        var sum = values.Sum();

                        var attribute = tanqueModel.ListAtributos.Where(x => x.Etapa == phase
                                                                    && x.Parametro?.Split('/')[0].TrimEnd() == paramName?.Split('/')[0].TrimEnd())
                                                                .SingleOrDefault();
                        if (attribute != null)
                        {
                            if (!string.IsNullOrEmpty(attribute.Parametro) && attribute.Parametro.Contains("Identidad"))
                            {
                                var anyOp = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x => x.ProductionOrderId).FirstOrDefault();
                                var op = await _productionOrderRepository.GetByIdAsync(anyOp);
                                attribute.Especificacion = (op.ProductId != "AR") ? "Positiva" : "Negativa";

                                var ordersNotInCompliance = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName && x.InCompliance != true).Select(x => x.ProductionOrderId);
                                var batchesNotInCompliance = await _batchDetailsRepository.GetAsync(x => x.Number != null && ordersNotInCompliance.Contains(x.ProductionOrderId));
                                attribute.BatchesNotInCompliance = batchesNotInCompliance.Select(x => x.Number.Trim()).ToList();
                            }

                            double valMax;
                            double valMin;
                            double.TryParse(attribute.ValorMax.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMax);
                            double.TryParse(attribute.ValorMin.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valMin);

                            if (valMax < max)
                            {
                                attribute.ValorMax = max.ToString().Replace(',', '.');
                            }
                            if (valMin > min)
                            {
                                attribute.ValorMin = min.ToString().Replace(',', '.');
                            }

                            attribute.Count += count;
                            attribute.Sum += sum;

                            attribute.Values.AddRange(values);
                            attribute.ValorProm = string.Format("{0:0.##}", attribute.Values.Average()).Replace(',', '.');
                            attribute.DesvEstandar = string.Format("{0:0.##}", StandardDeviation(attribute.Values)).Replace(',', '.');
                        }
                        else
                        {
                            var lic = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x => x.LowLimit).FirstOrDefault();
                            var lsc = item.CriticalQualityAttributes.Where(x => x.Attribute == paramName).Select(x => x.TopLimit).FirstOrDefault();

                            var avg = (sum > 0 && count > 0) ? (sum / count) : 0;
                            var std = (sum != 0 && count > 0) ? Math.Sqrt((Math.Pow(sum - avg, 2)) / (count - 1)) : 0;

                            tanqueModel.ListAtributos.Add(new VariablesTanque
                            {
                                Etapa = phase,
                                Parametro = paramName + (!string.IsNullOrEmpty(specification) ? " / " + specification : ""),
                                Especificacion = !string.IsNullOrEmpty(paramName) && paramName.Contains("Identidad") ? "Positiva" : specification,
                                LIC = lic,
                                LSC = lsc,
                                ValorMax = string.Format("{0:0.##}", max).Replace(',', '.'),
                                ValorMin = string.Format("{0:0.##}", min).Replace(',', '.'),
                                Count = count,
                                Sum = sum,
                                Values = values.ToList(),
                                ValorProm = string.Format("{0:0.##}", (sum / count)).Replace(',', '.'),
                                DesvEstandar = string.Format("{0:0.##}", StandardDeviation(values)).Replace(',', '.'),
                            });
                        }
                    }
                }

                //CALCULA EL: Total de lotes revisados		

                // "Aprobados= (Cuenta la totalidad de ""No. de lote de distribución"") - (El valor de esta tabla ""Rechazados"").
                tanqueModel.Aprobados = tanqueModel.ListRAPTanques.Count(s => s.Dictamen == "Sí" && !string.IsNullOrEmpty(s.FolioDesviacion) && s.FolioDesviacion == "NA").ToString();
                // "Aprobados con desviación: Sumar Si; el campo Aprobado es ""SI"" y la columna ""Folio de desviación"" es diferente a ""NA"".
                tanqueModel.ConDesviacion = tanqueModel.ListRAPTanques.Where(x => x.Dictamen == "Sí" && !string.IsNullOrEmpty(x.FolioDesviacion) && x.FolioDesviacion != "NA").Count().ToString();
                // "Rechazados= Cuenta la cantidad de ""NO"" del campo ""Aprobado""
                tanqueModel.Rechazados = tanqueModel.ListRAPTanques.Count(s => s.Dictamen != "Sí").ToString();
                // "Total = Aprobados + Rechazados"
                tanqueModel.Total = tanqueModel.ListRAPTanques.Count().ToString();

                //Va por los datos del Estudio de estabilidad del producto		
                var stabilityStudy = (await _stabilityCatalogRepository.GetAsync(x => x.PlantId == tanqueModel.SelectedPlantFilter)).FirstOrDefault();
                if (stabilityStudy != null)
                {
                    tanqueModel.estudioEstabilidad = new EstudioEstabilidad
                    {
                        AnioEstudio = stabilityStudy.StudyDate.ToString("yyyy-MM-dd"),
                        Estado = stabilityStudy.Status == true ? "Activo" : "NA",
                        Codigo = stabilityStudy.Code,
                        Observaciones = stabilityStudy.Observations
                    };
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }
            return View(tanqueModel);
        }

        public async Task<List<TanqueModel>> GetListRapTanques(string plantId, string productId, string tankId)
        {
            List<TanqueModel> raps = new List<TanqueModel>();

            try
            {
                raps = await _rapService.GetRapTanques(plantId, productId, tankId);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Ocurrió un error. " + ex.ToString());
            }
            return raps;
        }

        public async Task<List<RapComplemento>> GetListRapCompTanques(string plantId, string productId, string tankId)
        {
            List<RapComplemento> raps = new List<RapComplemento>();
            try
            {
                raps = await _rapService.GetRapCompTanques(plantId, productId, tankId);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Ocurrió un error. " + ex.ToString());
            }
            return raps;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.COMPLEMENTO_DE_RAP_TANQUES)]
        public async Task<IActionResult> Complemento(String SelectedPlantFilter, String SelectedProductFilter, String SelectedTankFilter, DateTime? StartDate, DateTime? EndDate)
        {
            ComplementoViewModel complementoModel = new ComplementoViewModel();
            ConfiguracionUsuarioVM tanqueModelconfiguracionUsuario = new ConfiguracionUsuarioVM();

            // Initialize filters
            var plants = await GetPlantsItemsAsync();
            complementoModel.ListPlants = plants;

            if (!string.IsNullOrEmpty(SelectedPlantFilter))
            {
                var products = await GetProdutsItemsAsync(SelectedPlantFilter);
                complementoModel.ListProducts = products;

                if (!string.IsNullOrEmpty(SelectedProductFilter))
                {
                    var tanks = await GetTanksItemsAsync(SelectedPlantFilter, SelectedProductFilter);
                    complementoModel.ListTanks = tanks;
                }
            }

            var dispositions = await _rapService.DisposicionXplanta(SelectedPlantFilter, plants);
            complementoModel.ListDisposicionPNC = dispositions;

            var presentaciones = await _rapService.GetPresentaciones(SelectedProductFilter);
            complementoModel.ListPresentation = presentaciones;

            complementoModel.ListTanqueComplemento = new List<RapComplemento>();

            complementoModel.SelectedPlantFilter = SelectedPlantFilter;
            complementoModel.SelectedProductFilter = SelectedProductFilter;
            complementoModel.SelectedTankFilter = SelectedTankFilter;

            if (string.IsNullOrEmpty(SelectedPlantFilter) || string.IsNullOrEmpty(SelectedProductFilter) || string.IsNullOrEmpty(SelectedTankFilter))
            {
                return View(complementoModel);
            }

            try
            {

                //fill filters
                complementoModel.Plant = new Models.RAPModels.Plant()
                {
                    Id = (!plants.Any()) ? 0 : int.Parse((plants.FirstOrDefault().Value)),
                    Name = (!plants.Any()) ? _resource.GetString("NoInformation") : plants.FirstOrDefault().Text
                };

                //filter by criteria plant, product and tank
                var rapcomptanques = await _rapService.GetRapCompTanques(SelectedPlantFilter, SelectedProductFilter, SelectedTankFilter);
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<RapComplemento>>(rapcomptanques);
                complementoModel.ListTanqueComplemento = (List<RapComplemento>)mapped;

                //filter by criteria date
                if (StartDate.HasValue)
                {
                    complementoModel.ListTanqueComplemento = complementoModel.ListTanqueComplemento
                                                .Where(x => StartDate.Value <= x.Fecha)
                                                .ToList();
                    complementoModel.StartDate = StartDate;
                }
                if (EndDate.HasValue)
                {
                    complementoModel.ListTanqueComplemento = complementoModel.ListTanqueComplemento
                                                .Where(x => x.Fecha <= EndDate.Value.AddDays(1))
                                                .ToList();
                    complementoModel.EndDate = EndDate;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorRAPTanques"));
                _logger.LogInformation("Ocurrio un error en RAP Tanques " + ex);
            }


            return View(complementoModel);

        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_RAP_TANQUES)]
        public IActionResult ClearFiltersRap()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_RAP_TANQUES)]
        public IActionResult ClearFiltersRapComplemento()
        {
            return RedirectToAction("Complemento");
        }

        [HttpPost]
        public async Task<IActionResult> Save(List<RapComplemento> model)
        {
            try
            {
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<ComplementoTanque>>(model);
                foreach (var obj in mapped)
                {
                    var entity = (await _complementoTanqueRepository.GetAsync(x => x.ProductionOrderId == obj.ProductionOrderId)).FirstOrDefault();
                    var entityClone = new ComplementoTanque();
                    if (entity == null || entity.Id == 0)
                    {
                        
                        await _complementoTanqueRepository.AddAsync(obj);
                    }
                    else
                    {
                        var Op = (await _productionOrderRepository.GetAsync(x=>x.Id == obj.ProductionOrderId)).FirstOrDefault();
                        entity.productionOrder = Op;
                        entityClone = (ComplementoTanque)entity.Clone();
                        entity.FolioTrabajoNoConforme = obj.FolioTrabajoNoConforme;
                        entity.FolioPNC = obj.FolioPNC;
                        entity.DisposicionPNC = obj.DisposicionPNC;
                        entity.FolioControlCambios = obj.FolioControlCambios;
                        entity.Observaciones = obj.Observaciones;
                        await _complementoTanqueRepository.UpdateAsync(entity, entityClone);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en RAP Tanques " + ex);
                return Json(new { Result = "Fail", Message = "Paramtro invalido" });
            }
            return Json(new { Result = "Ok", Message = "Complemento guardado con éxito" });
        }

        [HttpGet]
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
        public async Task<IActionResult> GetPresentaciones(string productId)
        {
            var presentaciones = await _rapService.GetPresentaciones(productId);
            // tanqueModel.ListPresentation = presentaciones;

            return Json(new { Result = "Ok", Data = presentaciones });
        }

        [HttpGet]
        public async Task<IActionResult> GetPurezas(string productId)
        {
            var purezas = await _rapService.GetPurezas(productId);
            // tanqueModel.ListPurity = purezas;

            return Json(new { Result = "Ok", Data = purezas });
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistrosSan(string productId)
        {
            var registro = await _rapService.GetRegistrosSan(productId);
            // tanqueModel.ListHealthRegister = registro;

            return Json(new { Result = "Ok", Data = registro });
        }

        [HttpGet]
        public async Task<IActionResult> GetFormFarmGetFormFarm(string productId)
        {
            var formula = await _rapService.GetFormFarm(productId);
            // tanqueModel.ListPharmaceuticalForm = formula;

            return Json(new { Result = "Ok", Data = formula });
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
            if (!plantsByUser.Contains(plantId))
            {
                return new List<SelectListItem>();
            }

            //gets products
            var products = await _principalService.GetProducts();
            var productsFiltered = (await _generalCatalogRepository.GetAsync(x => x.PlantId == plantId && x.Estatus)).Select(x => x.ProductId);

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
