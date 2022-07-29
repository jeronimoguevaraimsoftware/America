using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class QuerysGeneralController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPrincipalService _principalService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IGeneralCatalogRepository _generalRepository;
        private readonly ILogger<QuerysGeneralController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuerysGeneralController(UserManager<ApplicationUser> userManager, IPrincipalService principalService, IStringLocalizer<Resource> resource,
        IGeneralCatalogRepository generalCatalogRepository, ILogger<QuerysGeneralController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _principalService = principalService;
            _resource = resource;
            _generalRepository = generalCatalogRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTA_GENERAL)]
        public async Task<IActionResult> Index(String SelectedPlantsFilter, String SelectedProductsFilter, String SelectedTanksFilter,
        String SelectedStatesFilter, string StartDate, string EndDate)
        {

            ConfiguracionUsuarioVM model = new ConfiguracionUsuarioVM();
            List<SelectListItem> Tank = new List<SelectListItem>();
            try
            {
                //fill filters
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                model.ListqueryGeneralModels = new List<QueryGeneralModel>();

                model.SelectedPlantsFilter = string.IsNullOrEmpty(SelectedPlantsFilter) ? new List<string>() : SelectedPlantsFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedProductsFilter = string.IsNullOrEmpty(SelectedProductsFilter) ? new List<string>() : SelectedProductsFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedTanksFilter = string.IsNullOrEmpty(SelectedTanksFilter) ? new List<string>() : SelectedTanksFilter.Trim().Replace(" ", "").Split(",").ToList();
                model.SelectedStatesFilter = string.IsNullOrEmpty(SelectedStatesFilter) ? new List<string>() : SelectedStatesFilter.Trim().Replace(" ", "").Split(",").ToList();

                //init serch
                if (model.SelectedPlantsFilter.Any())
                {
                    var qryGeneral = await _principalService.GetQueryGeneral();
                    var mapped = ObjectMapper.Mapper.Map<IEnumerable<QueryGeneralModel>>(qryGeneral);
                    model.ListqueryGeneralModels = (List<QueryGeneralModel>)mapped;
                }

                //filter by criteria plant
                if (model.SelectedPlantsFilter != null && model.SelectedPlantsFilter.Count > 0)
                {
                    model.ListqueryGeneralModels = (from r in model.ListqueryGeneralModels where model.SelectedPlantsFilter.Contains(r.PlantId) select r).ToList();
                }
                //filter by criteria product
                if (model.SelectedProductsFilter != null && model.SelectedProductsFilter.Count > 0)
                {
                    model.ListqueryGeneralModels = (from r in model.ListqueryGeneralModels where model.SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                //filter by criteria tank
                if (model.SelectedTanksFilter != null && model.SelectedTanksFilter.Count > 0)
                {
                    model.ListqueryGeneralModels = (from r in model.ListqueryGeneralModels where model.SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                //filter by criteria state
                if (model.SelectedStatesFilter != null && model.SelectedStatesFilter.Count > 0)
                {
                    model.ListqueryGeneralModels = (from r in model.ListqueryGeneralModels where SelectedStatesFilter.Contains(r.State) select r).ToList();
                }
                if (StartDate != null && EndDate != null)
                {
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", true).DateTimeFormat;
                    var date = Convert.ToDateTime(StartDate, usDtfi);
                    model.ListqueryGeneralModels = (from r in model.ListqueryGeneralModels.Where(archive => Convert.ToDateTime(StartDate, usDtfi) <= archive.StartDateOP
                                            && archive.StartDateOP <= (Convert.ToDateTime(EndDate, usDtfi)).AddDays(1))
                                                    select r).ToList();
                    var dateEnd = Convert.ToDateTime(EndDate, usDtfi);
                    model.StartDate = date;
                    model.EndDate = dateEnd;
                }

                ViewBag.ShowModal = (Request.Query.Count > 0 && model.ListqueryGeneralModels.Count <= 0);


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }
            return View(model);
        }
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTA_GENERAL)]
        public IActionResult ClearFiltersGeneral()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTA_GENERAL)]
        public async Task<IActionResult> GetPlants()
        {
            var response = await GetPlantsItemsAsync();

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTA_GENERAL)]
        public async Task<IActionResult> GetProduts(string plantId)
        {
            var response = await GetProdutsItemsAsync(plantId);

            return Json(new { Result = "Ok", Data = response });
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTA_GENERAL)]
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
