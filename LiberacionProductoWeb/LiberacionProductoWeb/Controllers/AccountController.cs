using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Models.AccountViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Data;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using LiberacionProductoWeb.Helpers;
using Microsoft.EntityFrameworkCore;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Microsoft.Data.SqlClient;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiberacionProductoWeb.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILoggerFactory _loggerFactory;
        public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger, AppDbContext appDbContext, IConfiguration config, RoleManager<ApplicationRole> roleManager, ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<AccountController>();
            _context = appDbContext;
            _config = config;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            bool Authorizate = false;
            ApplicationRole identityRole = new ApplicationRole();
            try
            {
                if (ModelState.IsValid)
                {
                    bool WSMexeFuncionalidad = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);
                    switch (WSMexeFuncionalidad)
                    {
                        case true:
                            Authorizate = await LoginMexe(model);
                            _logger.LogInformation("usr " + model.Email + " en mexe se autentico con: " + Authorizate.ToString());
                            break;
                        default:
                            Authorizate = true;
                            break;
                    }
                    var User = await _userManager.FindByNameAsync(model.Email);
                    if (User != null)
                    {
                        var lockOut = await _userManager.IsLockedOutAsync(User);
                        if (lockOut)
                        {
                            ModelState.AddModelError("LoginError", "Su usuario ha sido bloqueado, por favor comuníquese con el Administrador de la Aplicación.");
                        }
                        else if (Authorizate)
                        {
                            //load user and roles
                            var roles = User.Rol?.Split(",")?.ToList();
                            ClaimsIdentity claimsIdentity = null;
                            List<Claim> claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.Name, model.Email));
                            claims.Add(new Claim("Id", User.Id));
                            List<Claim> claimsRoles = new List<Claim>();
                            foreach (var r in roles)
                            {
                                var rol = await _roleManager.FindByNameAsync(r.Trim());
                                identityRole.Id = rol.Id;
                                var claimsRole = await _roleManager.GetClaimsAsync(identityRole);
                                claims.Add(new Claim(ClaimTypes.Role, rol.Name));
                                claimsRoles.AddRange(claimsRole);
                            }
                            var claimsPermission = claimsRoles.Select(x => x.Value).ToList().Distinct().Select(x => new Claim(SecurityConstants.PERMISSION, x)).ToList();
                            claims.Union(claimsPermission);
                            claims.AddRange(claimsPermission);
                            claimsIdentity = new ClaimsIdentity(claims, "Login");

                            //register login and clear Attempts
                            await this._userManager.ResetAccessFailedCountAsync(User);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        }
                        else
                        {
                            var countFailed = await FailedAttemptsAsync(User);
                            var isLockOut = await _userManager.IsLockedOutAsync(User);
                            if (isLockOut)
                            {
                                User.Activo = false;
                                ModelState.AddModelError("LoginError", "Su usuario ha sido bloqueado, por favor comuníquese con el Administrador de la Aplicación.");
                            }
                            else
                            {
                                ModelState.AddModelError("LoginError", "Usuario y/o contraseña incorrecta " +
                                       "por favor recuerde que tiene " +
                                       $"{_config["AccessFailedCount:ServiceApiKey"]} intentos para ingresar, luego de esto su usuario será bloqueado." +
                                       $" LLeva {countFailed} intentos fallidos de " +
                                       $"{_config["AccessFailedCount:ServiceApiKey"]}");
                            }
                            User.FechaUltimoIntento = DateTime.Now;
                            await this._userManager.UpdateAsync(User);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("LoginError", "Usuario no dado de alta en la aplicación, comunicate con el administrador");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("roleName"))
                {
                    ModelState.AddModelError("LoginError", "Usuario sin Rol asignado , comunicate con el administrador");
                }
                else
                {
                    ModelState.AddModelError("LoginError", ex.Message);
                }
                _logger.LogWarning("Ocurrió un error inesperado" + ex.ToString());
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("User logged out.");
            return Ok();
        }

        public async Task<int> FailedAttemptsAsync(ApplicationUser user)
        {
            await this._userManager.AccessFailedAsync(user);
            return await this._userManager.GetAccessFailedCountAsync(user);
        }

        public Task<bool> LoginMexe(LoginViewModel model)
        {
            string url = _config["WSMexe:ServiceApiKey"];
            ServiceReferenceMexe.PrxMxServicesSoapClient prxMxServicesSoap = new ServiceReferenceMexe.PrxMxServicesSoapClient(ServiceReferenceMexe.PrxMxServicesSoapClient.EndpointConfiguration.PrxMxServicesSoap, url);
            var AuthorizateWS = prxMxServicesSoap.loginAsync(model.Email.Trim(), model.Password.Trim());
            return AuthorizateWS;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}