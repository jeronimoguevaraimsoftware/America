using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.LayoutCertificateViewModels;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LayoutCertificateController : Controller
    {
        private readonly ILayoutCertificateService _LayoutCertificateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPrincipalService _principalService;
        private readonly ILogger<ProductionOrderController> _logger;
        private readonly IStringLocalizer<Resource> _resource;
        public LayoutCertificateController(ILayoutCertificateService layoutCertificateService, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager, IPrincipalService principalService, ILogger<ProductionOrderController> logger, 
        IStringLocalizer<Resource> resource)
        {
            _LayoutCertificateService = layoutCertificateService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _principalService = principalService;
            _logger = logger;
            _resource = resource;
        }
        [Authorize(SecurityConstants.CONSULTAR_LAYOUT_CERTIFICADO)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            LeyendsCertificateVM model = new LeyendsCertificateVM();
            try
            {
                model = await _LayoutCertificateService.GetAllAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutCertificateController index." });
            }
            return View(model);

        }
        [Authorize(SecurityConstants.EDITAR_LAYOUT_CERTIFICADO)]
        [HttpPost]
        public async Task<IActionResult> CrearEditar([FromForm] LeyendsCertificateVM model)
        {
            try
            {
                var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    var files = HttpContext.Request.Form.Files;
                    string FileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(_resource.GetString("PathLogoCertificate").Value);
                    if (!Directory.Exists(upload))
                        Directory.CreateDirectory(upload);
                    var ext = Path.GetExtension(files[0].FileName);
                    ////delete file
                    if ((System.IO.File.Exists(upload + @"\" + model.HeaderTwo)))
                    {
                        System.IO.File.Delete(upload + @"\" + model.HeaderTwo);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(upload, FileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    model.HeaderTwo = FileName + ext;
                    model.NewFile = true;
                }
                else
                {
                    model.HeaderTwo = model.FileName;
                }
                model.User = userInfo;
                await _LayoutCertificateService.AddLayout(model);
            }

            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en CrearEditar " + ex);
                return Json(new { Result = "fail", Message = ex });
            }
            return Json(new { Result = "Ok", Message = "Guardado con éxito" });
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

        [Authorize(SecurityConstants.EDITAR_LAYOUT_CERTIFICADO)]
        [HttpGet]
        public async Task<IActionResult> GetFooter(string PlantsId)
        {
            List<LeyendsFooterCertificateVM> ListLeyendsCertificate = new List<LeyendsFooterCertificateVM>();
            LeyendsCertificateVM model = new LeyendsCertificateVM();
            List<LeyendsFooterCertificateHistoryVM> leyendsFooter = new List<LeyendsFooterCertificateHistoryVM>();
            try
            {
                var sourcePlants = PlantsId?.Split(",")?.ToList();
                foreach (var item in sourcePlants)
                {
                    var info = await _LayoutCertificateService.GetFooter(item);
                    if (info.leyendsFooterCertificateVM != null)
                    {
                        ListLeyendsCertificate.Add(new LeyendsFooterCertificateVM
                        {
                            Footer = info.leyendsFooterCertificateVM.Select(x => x.Footer).FirstOrDefault(),
                            PlantId = info.leyendsFooterCertificateVM.Select(x => x.PlantId).FirstOrDefault(),
                            PlantName = info.leyendsFooterCertificateVM.Select(x => x.PlantName).FirstOrDefault()
                        });
                    }
                }
                model.leyendsFooterCertificateVM = ListLeyendsCertificate;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return Json(new { Result = "Error", Message = "Surgio un error al cargar LayoutCertificateController GetFooter." });
            }
            return PartialView("_Footer", model);
        }
    }
}
