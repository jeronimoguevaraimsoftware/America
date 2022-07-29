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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Helpers;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.RAPModels;
using System.Globalization;
using LiberacionProductoWeb.Models.ConfigViewModels;
using Microsoft.AspNetCore.Http;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class RAPPipasController : Controller
    {
        private readonly ILogger<RAPPipasController> _logger;
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
        private readonly IPipelineClearanceRepository _pipelineClearanceRepository;
        private readonly IPipelineClearanceOARepository _pipelineClearanceOARepository;
        private readonly IComplementoPipaRepository _complementoPipaRepository;
        private readonly IGeneralCatalogRepository _generalCatalogRepository;

        public RAPPipasController(ILogger<RAPPipasController> logger, UserManager<ApplicationUser> userManager,
        IMapper mapper, Services.IUsersLogin usersLogin, IConfiguration config, IAnalyticsCertsService analyticsCertsService,
        IPrincipalService principalService, IRapService rapService, IStringLocalizer<Resource> resource, ILoggerFactory loggerFactory,
        IHttpContextAccessor httpContextAccessor, IPipelineClearanceRepository pipelineClearanceRepository,
        IPipelineClearanceOARepository pipelineClearanceOARepository, IComplementoPipaRepository complementoPipaRepository,
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
            _pipelineClearanceRepository = pipelineClearanceRepository;
            _pipelineClearanceOARepository = pipelineClearanceOARepository;
            _complementoPipaRepository = complementoPipaRepository;
            _generalCatalogRepository = generalCatalogRepository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.CONSULTAR_RAP_PIPAS)]
        public async Task<IActionResult> Index(String SelectedPlantFilter, String SelectedProductFilter, String SelectedTankFilter,
        String SelectedPresentationFilter, String SelectedPurityFilter, String SelectedHealthRegisterFilter,
        String SelectedPharmaceuticalFormFilter, DateTime? StartDate, DateTime? EndDate)
        {
            PipaViewModel pipaModel = new PipaViewModel();
            pipaModel.ListRAPPipas = new List<PipaModel>();

            pipaModel.SelectedPlantFilter = SelectedPlantFilter;
            pipaModel.SelectedProductFilter = SelectedProductFilter;
            pipaModel.SelectedTankFilter = SelectedTankFilter;
            pipaModel.SelectedPresentationFilter = SelectedPresentationFilter;
            pipaModel.SelectedPurityFilter = SelectedPurityFilter;
            pipaModel.SelectedHealthRegisterFilter = SelectedHealthRegisterFilter;
            pipaModel.SelectedPharmaceuticalFormFilter = SelectedPharmaceuticalFormFilter;

            if (string.IsNullOrEmpty(SelectedPlantFilter) || string.IsNullOrEmpty(SelectedProductFilter) || string.IsNullOrEmpty(SelectedTankFilter))
            {
                return View(pipaModel);
            }

            try
            {

                pipaModel.ListRAPPipas = await GetListRapPipas(SelectedPlantFilter, SelectedProductFilter, SelectedTankFilter);
                if (StartDate.HasValue)
                {
                    pipaModel.ListRAPPipas = pipaModel.ListRAPPipas
                                                .Where(x => StartDate.Value <= x.Fecha)
                                                .ToList();
                    pipaModel.StartDate = StartDate;
                }
                if (EndDate.HasValue)
                {
                    pipaModel.ListRAPPipas = pipaModel.ListRAPPipas
                                                .Where(x => x.Fecha <= EndDate.Value.AddDays(1))
                                                .ToList();
                    pipaModel.EndDate = EndDate.Value;
                }

                // "Aprobados= (Cuenta la totalidad de ""No. de lote de distribución"") - (El valor de esta tabla ""Rechazados"").
                pipaModel.Aprobados = pipaModel.ListRAPPipas.Count(s => s.Aprobado == "Sí").ToString();
                // "Aprobados con desviación: Sumar Si; el campo Aprobado es ""SI"" y la columna ""Folio de desviación"" es diferente a ""NA"".
                pipaModel.ConDesviacion = pipaModel.ListRAPPipas.Count(s => s.Aprobado == "Sí" && (!string.IsNullOrEmpty(s.aseguramiento?.FolioInformeDesviacion) && s.aseguramiento.FolioInformeDesviacion != "NA")).ToString();
                // "Rechazados= Cuenta la cantidad de ""NO"" del campo ""Aprobado""
                pipaModel.Rechazados = pipaModel.ListRAPPipas.Count(s => s.Aprobado != "Sí").ToString();
                // "Total = Aprobados + Rechazados"
                pipaModel.Total = pipaModel.ListRAPPipas.Count().ToString();

                pipaModel.ListAnalisisInitial = GetListAnalisis(pipaModel.ListRAPPipas.Select(x => x.aInicial).ToList(), PipeFillingAnalysisType.InitialAnalysis.Value, SelectedProductFilter);
                pipaModel.ListAnalisisFinal = GetListAnalisis(pipaModel.ListRAPPipas.Select(x => x.aFinal).ToList(), PipeFillingAnalysisType.FinalAnalysis.Value, SelectedProductFilter);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorRAPPIPAS"));
                _logger.LogInformation("Ocurrio un error en RAP PIPAS " + ex);
            }
            return View(pipaModel);
        }

        public async Task<List<PipaModel>> GetListRapPipas(string plantId, string productId, string tankId)
        {

            List<PipaModel> raps = new List<PipaModel>();
            try
            {
                raps = await _rapService.GetRapPipas(plantId, productId, tankId);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Ocurrió un error. " + ex.ToString());
            }
            return raps;
        }

        public List<AnalisisPipa> GetListAnalisis(List<List<Analisis>> rapAnalysis, string type, string productId)
        {
            ///provicional es un dummy
            List<AnalisisPipa> analisis = new List<AnalisisPipa>();
            try
            {
                analisis = _rapService.GetAnalisis(rapAnalysis, type, productId);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Ocurrió un error. " + ex.ToString());
            }
            return analisis;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.COMPLEMENTO_DE_RAP_PIPAS)]
        public async Task<IActionResult> Complemento(String SelectedPlantFilter, String SelectedProductFilter, String SelectedTankFilter, DateTime? StartDate, DateTime? EndDate)
        {
            ComplementoViewModel complementoModel = new ComplementoViewModel();

            // Initialize filters
            var plants = await GetPlantsItemsAsync();
            complementoModel.ListPlants = plants;

            var dispositions = await _rapService.DisposicionXplanta(SelectedPlantFilter, plants);
            complementoModel.ListDisposicionPNC = dispositions;

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

            complementoModel.SelectedPlantFilter = SelectedPlantFilter;
            complementoModel.SelectedProductFilter = SelectedProductFilter;
            complementoModel.SelectedTankFilter = SelectedTankFilter;

            if (string.IsNullOrEmpty(SelectedPlantFilter) || string.IsNullOrEmpty(SelectedProductFilter) || string.IsNullOrEmpty(SelectedTankFilter))
            {
                return View(complementoModel);
            }

            try
            {
                complementoModel.Plant = new Models.RAPModels.Plant()
                {
                    Id = (!plants.Any()) ? 0 : int.Parse((plants.FirstOrDefault().Value)),
                    Name = (!plants.Any()) ? _resource.GetString("NoInformation") : plants.FirstOrDefault().Text
                };

                complementoModel.Product = new Models.RAPModels.Product()
                {
                    Id = complementoModel.ListProducts.FirstOrDefault().Value ?? "0",
                    Name = complementoModel.ListProducts.FirstOrDefault().Text ?? _resource.GetString("NoInformation")
                };

                complementoModel.Tank = new Models.RAPModels.Tank()
                {
                    Id = complementoModel.ListTanks.FirstOrDefault().Value ?? "0",
                    Name = complementoModel.ListTanks.FirstOrDefault().Text ?? _resource.GetString("NoInformation")
                };

                //filter by criteria plant, product and tank
                complementoModel.ListPipaComplemento = await GetListRapCompPipas(SelectedPlantFilter, SelectedProductFilter, SelectedTankFilter);

                //filter by criteria fechas inicio y fin
                if (StartDate.HasValue)
                {
                    complementoModel.ListPipaComplemento = complementoModel.ListPipaComplemento
                                                .Where(x => StartDate.Value <= x.Fecha)
                                                .ToList();
                    complementoModel.StartDate = StartDate;
                }
                if (EndDate.HasValue)
                {
                    complementoModel.ListPipaComplemento = complementoModel.ListPipaComplemento
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

        [HttpPost]
        public async Task<IActionResult> Save(List<RapComplemento> model)
        {
            try
            {
                List<ComplementoPipa> complementoPipas = new List<ComplementoPipa>();
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<ComplementoPipa>>(model);
                var complementosClonados = new List<ComplementoPipa>();
                foreach (var obj in mapped)
                {
                    var entity = await _complementoPipaRepository.FindByDistributionBatchAsync(obj.ConditioningOrderId, obj.NumeroLoteDistribucion);
                    if (entity == null)
                    {
                        obj.Fecha = DateTime.Now;
                        obj.Hora = obj.Fecha.Hour.ToString() + ":" + obj.Fecha.Minute.ToString();
                        await _complementoPipaRepository.AddAsync(obj);
                    }
                    else
                    {
                        var clone = (ComplementoPipa)entity.Clone();
                        entity.FolioTrabajoNoConforme = obj.FolioTrabajoNoConforme;
                        entity.FolioPNC = obj.FolioPNC;
                        entity.DisposicionPNC = obj.DisposicionPNC;
                        entity.FolioEventoAdverso = obj.FolioEventoAdverso;
                        entity.NumeroDeDevolucion = obj.NumeroDeDevolucion;
                        entity.FolioRetiroProductos = obj.FolioRetiroProductos;
                        entity.FolioControlCambios = obj.FolioControlCambios;
                        entity.Observaciones = obj.Observaciones;
                        complementoPipas.Add(entity);
                        complementosClonados.Add(clone);
                    }
                }
                var upd = await _complementoPipaRepository.UpdateAsync(complementoPipas, complementosClonados);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en RAP Pipas " + ex);
                return Json(new { Result = "Fail", Message = "Parametro invalido" });
            }
            return Json(new { Result = "Ok", Message = "Complemento guardado con éxito" });
        }

        public async Task<List<RapComplemento>> GetListRapCompPipas(string plantId, string productId, string tankId)
        {
            List<RapComplemento> raps = new List<RapComplemento>();
            try
            {
                raps = await _rapService.GetRapCompPipas(plantId, productId, tankId);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Ocurrió un error. " + ex.ToString());
            }
            return raps;
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

            return Json(new { Result = "Ok", Data = presentaciones });
        }

        [HttpGet]
        public async Task<IActionResult> GetPurezas(string productId)
        {
            var purezas = await _rapService.GetPurezas(productId);

            return Json(new { Result = "Ok", Data = purezas });
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistrosSan(string productId)
        {
            var registro = await _rapService.GetRegistrosSan(productId);

            return Json(new { Result = "Ok", Data = registro });
        }

        [HttpGet]
        public async Task<IActionResult> GetFormFarmGetFormFarm(string productId)
        {
            var formula = await _rapService.GetFormFarm(productId);

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
    }
}
