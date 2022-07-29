using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

    public class ReportAuditTrailController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPrincipalService _principalService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly ILogger<QuerysFilesController> _logger;
        private readonly IReportAuditTrailService _reportAuditTrailService;

        public ReportAuditTrailController(UserManager<ApplicationUser> userManager, IPrincipalService principalService,
        IStringLocalizer<Resource> resource, ILogger<QuerysFilesController> logger, IReportAuditTrailService reportAuditTrailService)
        {
            _userManager = userManager;
            _principalService = principalService;
            _resource = resource;
            _logger = logger;
            _reportAuditTrailService = reportAuditTrailService;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.CONSULTAR_REPORTE_AUDIT_TRAIL)]
        public async Task<IActionResult> Index(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter,
        string StartDate, string EndDate, List<String> SelectedUserFilter, List<String> SelectedActionFilter)
        {
            ConfiguracionUsuarioVM model = new ConfiguracionUsuarioVM();
            try
            {

                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var plants = await _principalService.GetPlants();
                var plantsId = userInfo.PlantaUsuario?.Trim().Replace(" ", "").Split(",");
                var PlantsXuser = await _principalService.GetPlantsXuser(plantsId, plants);
                //fill filters
                model.ListPlants = PlantsXuser;
                model.ListProducts = await _principalService.GetProductsXPlants(plantsId, PlantsXuser);


                var actions = await _principalService.GetActions();
                model.ListActions = actions;

                var user = await GetUsersItemsAsync();
                model.ListUsers = user;

                model.ListReportAudit = new List<ReportAuditTrailViewModel>();

                if ((SelectedPlantsFilter.Count > 0) || (SelectedProductsFilter.Count > 0) || (SelectedUserFilter.Count > 0) || (SelectedActionFilter.Count > 0) || ((StartDate != null && EndDate != null)))
                {

                    model.ListReportAudit = await _reportAuditTrailService.GetReportAuditTrail();

                }
                //filter by criteria plant
                if (SelectedPlantsFilter != null && SelectedPlantsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedPlantsFilter.Count(); i++)
                    {
                        var txt = model.ListPlants.Where(p => p.Value == SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedPlantsFilter[i] = txt;
                    }
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedPlantsFilter.Contains(r.Plant) select r).ToList();
                }
                //filter by criteria product
                if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                    {
                        var txt = model.ListProducts.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedProductsFilter[i] = txt;
                    }
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedProductsFilter.Contains(r.Product) select r).ToList();
                }
                if (SelectedProductsFilter.Count > 0)
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedProductsFilter.Contains(r.Product) select r).ToList();
                //filter by criteria user
                if (SelectedUserFilter != null && SelectedUserFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedUserFilter.Count(); i++)
                    {
                        var txt = model.ListUsers.Where(p => p.Value == SelectedUserFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedUserFilter[i] = txt;
                    }
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedUserFilter.Contains(r.User) select r).ToList();
                }
                if (SelectedUserFilter.Count > 0)
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedUserFilter.Contains(r.User) select r).ToList();

                //filter by criteria action
                if (SelectedActionFilter != null && SelectedActionFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedActionFilter.Count(); i++)
                    {
                        var txt = model.ListActions.Where(p => p.Value == SelectedActionFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedActionFilter[i] = txt;
                    }
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedActionFilter.Contains(r.Action) select r).ToList();
                }
                if (SelectedActionFilter.Count > 0)
                    model.ListReportAudit = (from r in model.ListReportAudit where SelectedActionFilter.Contains(r.Action) select r).ToList();

                if (StartDate != null && EndDate != null)
                {
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", true).DateTimeFormat;
                    var date = Convert.ToDateTime(StartDate, usDtfi);
                    var FilterDateEnd = Convert.ToDateTime(EndDate, usDtfi).AddHours(23).AddMinutes(59).AddSeconds(59);
                    var FilterDateNew = Convert.ToDateTime(StartDate, usDtfi);

                    model.ListReportAudit = (from r in model.ListReportAudit.
                                             Where(archive => archive.Date >= FilterDateNew
                                            && archive.Date <= FilterDateEnd)
                                             select r).ToList();

                    var dateEnd = Convert.ToDateTime(EndDate, usDtfi);
                    model.StartDate = date;
                    model.EndDate = dateEnd;

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", _resource.GetString("ErrorPrincipal"));
                _logger.LogInformation("Ocurrio un error en principal " + ex);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.CONSULTAR_REPORTE_AUDIT_TRAIL)]
        public IActionResult ClearFilters()
        {
            return RedirectToAction("Index");
        }
        private async Task<List<SelectListItem>> GetUsersItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            var usuarios = _userManager.Users;

            foreach (var item in usuarios)
            {
                response.Add(new SelectListItem
                {
                    Text = item.NombreUsuario,
                    Value = item.MexeUsuario

                });

            }

            return response;
        }
    }
}
