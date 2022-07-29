using LiberacionProductoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using LiberacionProductoWeb.Models.Mappers;
using LiberacionProductoWeb.Models.ConfigViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Helpers;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.Principal;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using LiberacionProductoWeb.Models.SechToolViewModels;
using LiberacionProductoWeb.Data.Repository;

namespace LiberacionProductoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly Services.IUsersLogin _usersLogin;
        public bool WSMexeFuncionalidad;
        private readonly IConfiguration _config;
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IPrincipalService _principalService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGeneralCatalogRepository _generalRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IGeneralCatalogRepository generalCatalogRepository,
        IMapper mapper, Services.IUsersLogin usersLogin, IConfiguration config, IAnalyticsCertsService analyticsCertsService,
        IPrincipalService principalService, IStringLocalizer<Resource> resource, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _usersLogin = usersLogin;
            _config = config;
            WSMexeFuncionalidad = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);
            _analyticsCerts = analyticsCertsService;
            _principalService = principalService;
            _resource = resource;
            _generalRepository = generalCatalogRepository;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
        public async Task<IActionResult> Index(String SelectedPlantsFilter, String SelectedProductsFilter, String SelectedTanksFilter,
        String SelectedActivitiesFilter, String SelectedStatesFilter, string StartDate, string EndDate)
        {
            ConfiguracionUsuarioVM configuracionUsuario = new ConfiguracionUsuarioVM();
            List<SelectListItem> Tank = new List<SelectListItem>();
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                configuracionUsuario.SelectedPlantsFilter = string.IsNullOrEmpty(SelectedPlantsFilter) ? new List<string>() : SelectedPlantsFilter.Trim().Replace(" ", "").Split(",").ToList();
                configuracionUsuario.SelectedProductsFilter = string.IsNullOrEmpty(SelectedProductsFilter) ? new List<string>() : SelectedProductsFilter.Trim().Replace(" ", "").Split(",").ToList();
                configuracionUsuario.SelectedTanksFilter = string.IsNullOrEmpty(SelectedTanksFilter) ? new List<string>() : SelectedTanksFilter.Trim().Replace(" ", "").Split(",").ToList();
                configuracionUsuario.SelectedStatesFilter = string.IsNullOrEmpty(SelectedStatesFilter) ? new List<string>() : SelectedStatesFilter.Trim().Replace(" ", "").Split(",").ToList();
                configuracionUsuario.SelectedActivitiesFilter = string.IsNullOrEmpty(SelectedActivitiesFilter) ? new List<string>() : SelectedActivitiesFilter.Trim().Replace(" ", "").Split(",").ToList();

                var plants = await _principalService.GetPlants();
                var plantsId = userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",");
                var PlantsXuser = await _principalService.GetPlantsXuser(plantsId, plants);

                configuracionUsuario.ListPlants = PlantsXuser;
                // configuracionUsuario.Plant = new Plant();

                // configuracionUsuario.Plant.Id = (PlantsXuser.ToList().Count() == 0) ? 0 : int.Parse((PlantsXuser.FirstOrDefault().Value));
                // configuracionUsuario.Plant.Name = (PlantsXuser.ToList().Count() == 0) ? _resource.GetString("NoInformation") : PlantsXuser.FirstOrDefault().Text;
                var statusTaskFilter = this._config["StatusTaskFilter"];
                var statusTaskFilterList = statusTaskFilter?.Split(",").Select(x => x.Trim()).ToList();
                statusTaskFilterList = statusTaskFilterList != null ? statusTaskFilterList : new List<string>();
                configuracionUsuario.ListpenddingTasks = await GetListPenddingTask(statusTaskFilterList);
                if (userInfo.Rol != SecurityConstants.PERFIL_RESPONSABLE_SANITARIO)
                {
                    configuracionUsuario.ListpenddingTasks = configuracionUsuario.ListpenddingTasks.Where(x => x.Estado != "OP - En Cancelación").ToList();
                }

                var products = await _principalService.GetProductsXPlants(plantsId, PlantsXuser);
                configuracionUsuario.ListProducts = products;
                // configuracionUsuario.Product = new Product();
                // configuracionUsuario.Product.Id = products.FirstOrDefault()?.Value ?? "0";
                // configuracionUsuario.Product.Name = products.FirstOrDefault()?.Text ?? _resource.GetString("NoInformation");

                foreach (var item in products)
                {
                    var tank = await _principalService.GetTanks(item.Value);
                    foreach (var itemtank in tank)
                    {
                        Tank.Add(new SelectListItem { Value = itemtank.Value, Text = itemtank.Text });
                    }
                }

                configuracionUsuario.ListTanks = Tank;
                // configuracionUsuario.Tank = new Tank();
                // configuracionUsuario.Tank.Id = Tank.FirstOrDefault()?.Value;
                // configuracionUsuario.Tank.Name = Tank.FirstOrDefault()?.Text;

                var activities = await _principalService.GetActivities();
                configuracionUsuario.ListActivities = activities;
                configuracionUsuario.Activities = new Activities();
                configuracionUsuario.Activities.Id = (activities.ToList().Count() == 0) ? 0 : int.Parse((activities.FirstOrDefault()?.Value));
                configuracionUsuario.Activities.Name = (activities.ToList().Count() == 0) ? _resource.GetString("NoInformation") : activities.FirstOrDefault()?.Text;

                var states = await _principalService.GetStates();
                configuracionUsuario.ListStates = states.Where(x => !statusTaskFilterList.Contains(x.Text));
                configuracionUsuario.State = new State();
                configuracionUsuario.State.Id = int.Parse(states.FirstOrDefault()?.Value ?? "0");
                configuracionUsuario.State.Name = states.FirstOrDefault()?.Text ?? _resource.GetString("NoInformation");


                //filter by criteria plant
                if (configuracionUsuario.SelectedPlantsFilter != null && configuracionUsuario.SelectedPlantsFilter.Count > 0)
                {
                    for (int i = 0; i < configuracionUsuario.SelectedPlantsFilter.Count(); i++)
                    {
                        var txt = configuracionUsuario.ListPlants.Where(p => p.Value == configuracionUsuario.SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        configuracionUsuario.SelectedPlantsFilter[i] = txt;
                    }
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedPlantsFilter.Contains(r.Planta) select r).ToList();
                }
                if (configuracionUsuario.SelectedPlantsFilter.Count > 0)
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedPlantsFilter.Contains(r.Planta) select r).ToList();
                //filter by criteria product
                if (configuracionUsuario.SelectedProductsFilter != null && configuracionUsuario.SelectedProductsFilter.Count > 0)
                {
                    for (int i = 0; i < configuracionUsuario.SelectedProductsFilter.Count(); i++)
                    {
                        var txt = configuracionUsuario.ListProducts.Where(p => p.Value == configuracionUsuario.SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        configuracionUsuario.SelectedProductsFilter[i] = txt;
                    }
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedProductsFilter.Contains(r.Producto) select r).ToList();
                }
                if (configuracionUsuario.SelectedProductsFilter.Count > 0)
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedProductsFilter.Contains(r.Producto) select r).ToList();
                //filter by criteria tank
                if (configuracionUsuario.SelectedTanksFilter != null && configuracionUsuario.SelectedTanksFilter.Count > 0)
                {
                    for (int i = 0; i < configuracionUsuario.SelectedTanksFilter.Count(); i++)
                    {
                        var txt = configuracionUsuario.ListTanks.Where(p => p.Value == configuracionUsuario.SelectedTanksFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        configuracionUsuario.SelectedTanksFilter[i] = txt;
                    }
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedTanksFilter.Contains(r.Tanque) select r).ToList();
                }
                if (configuracionUsuario.SelectedTanksFilter.Count > 0)
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedTanksFilter.Contains(r.Tanque) select r).ToList();
                //filter by criteria Activities
                if (configuracionUsuario.SelectedActivitiesFilter != null && configuracionUsuario.SelectedActivitiesFilter.Count > 0)
                {
                    for (int i = 0; i < configuracionUsuario.SelectedActivitiesFilter.Count(); i++)
                    {
                        var txt = configuracionUsuario.ListActivities.Where(p => p.Value == configuracionUsuario.SelectedActivitiesFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        configuracionUsuario.SelectedActivitiesFilter[i] = txt;
                    }
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedActivitiesFilter.Contains(r.ActividadACompletar) select r).ToList();
                }
                if (configuracionUsuario.SelectedActivitiesFilter.Count > 0)
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedActivitiesFilter.Contains(r.ActividadACompletar) select r).ToList();
                //filter by criteria state
                if (configuracionUsuario.SelectedStatesFilter != null && configuracionUsuario.SelectedStatesFilter.Count > 0)
                {
                    for (int i = 0; i < configuracionUsuario.SelectedStatesFilter.Count(); i++)
                    {
                        var txt = configuracionUsuario.ListStates.Where(p => p.Value == configuracionUsuario.SelectedStatesFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        configuracionUsuario.SelectedStatesFilter[i] = txt;
                    }
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedStatesFilter.Contains(r.Estado) select r).ToList();
                }
                if (configuracionUsuario.SelectedStatesFilter.Count > 0)
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks where configuracionUsuario.SelectedStatesFilter.Contains(r.Estado) select r).ToList();
                if (StartDate != null && EndDate != null)
                {
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", true).DateTimeFormat;
                    var date = Convert.ToDateTime(StartDate, usDtfi);
                    configuracionUsuario.ListpenddingTasks = (from r in configuracionUsuario.ListpenddingTasks.Where(archive => Convert.ToDateTime(StartDate, usDtfi) <= archive.FechaDeCreacion
                                            && archive.FechaDeCreacion <= (Convert.ToDateTime(EndDate, usDtfi)).AddDays(1))
                                                              select r).ToList();
                    var dateEnd = Convert.ToDateTime(EndDate, usDtfi);
                    configuracionUsuario.StartDate = date;
                    configuracionUsuario.EndDate = dateEnd;
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }


            return View(configuracionUsuario);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
        public async Task<IActionResult> GetTanksXProduct(string productId)
        {
            if (productId != null)
            {
                var tanks = await _principalService.GetTanks(productId);
                if (tanks != null)
                {
                    return Json(tanks);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
        public async Task<IActionResult> GetPenddingTask()
        {
            try
            {
                ///provicional es un dummy
                List<LiberacionProductoWeb.Models.Principal.PenddingTaskModel> penndings = new List<Models.Principal.PenddingTaskModel>();
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var statusTaskFilter = this._config["StatusTaskFilter"];
                var statusTaskFilterList = statusTaskFilter?.Split(",").Select(x => x.Trim()).ToList();
                statusTaskFilterList = statusTaskFilterList != null ? statusTaskFilterList : new List<string>();
                penndings = await _principalService.GetPenddingTasks(userInfo.NombreUsuario, statusTaskFilterList);
                if (userInfo.Rol != SecurityConstants.PERFIL_RESPONSABLE_SANITARIO)
                {
                    penndings = penndings.Where(x => x.Estado != "OP - En Cancelación").ToList();
                }
                return Json(new { data = penndings.OrderByDescending(x => x.FechaDeCreacion) });
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public async Task<List<PenddingTaskModel>> GetListPenddingTask(List<string> filterStatus)
        {
            var tasks = new List<Models.Principal.PenddingTaskModel>();
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                tasks = await _principalService.GetPenddingTasks(userInfo.Id, filterStatus);
                if (userInfo.Rol != SecurityConstants.PERFIL_RESPONSABLE_SANITARIO)
                {
                    tasks = tasks.Where(x => x.Estado != "OP - En Cancelación").ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en GetPenddingTasks " + ex);
            }
            return tasks.OrderByDescending(x=>x.FechaDeCreacion).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> SerchDistributionBatch(string DistributionBatch)
        {
            SechToolDistributionBatchVM model = new SechToolDistributionBatchVM();
            var infoDB = await _principalService.GetDistributions(DistributionBatch);
            if (infoDB.Any())
                model.SechToolDistributions = infoDB;
            return PartialView("_SerchTool", model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
        public async Task<IActionResult> GetPlants()
        {
            var response = await GetPlantsItemsAsync();

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
        public async Task<IActionResult> GetProduts(string plantId)
        {
            var response = await GetProdutsItemsAsync(plantId);

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]        
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
            var productsFiltered = (await _generalRepository.GetAsync(x => plantIds.Contains(x.PlantId) && x.Estatus)).Select(x => x.ProductId);

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
            var tanksFiltered = (await _generalRepository.GetAsync(x => plantIds.Contains(x.PlantId) && productIds.Contains(x.ProductId))).Select(x => x.TankId);

            var response = tanks.Where(x => tanksFiltered.Contains(x.Value)).ToList();

            return response;
        }
    }
}
