using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.AccountViewModels;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using LiberacionProductoWeb.Data.Repository;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ProfilesController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;
        private AppDbContext _context;
        private readonly IEmailSender _emailSender;
        private ISecurityService _securityService;
        private readonly IReportAuditTrailRepository _reportAuditTrailRepository;
        private readonly IConfiguration _config;
        public ProfilesController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context,
            ILogger<UsersController> logger,
            IEmailSender emailSender,
            ISecurityService securityService,
            IReportAuditTrailRepository reportAuditTrailRepository,
            IConfiguration config
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
            _securityService = securityService;
            _reportAuditTrailRepository = reportAuditTrailRepository;
            _config = config;
        }


        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DE_ROLES)]
        public IActionResult ClearFilters()
        {
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DE_ROLES)]
        public async Task<IActionResult> Index(List<String> SelectedRoleFilter, List<String> SelectedFunctionsFilter)
        {

            var roleModel = new PerfilesViewModel();

            try
            {
                roleModel.RoleList = await GetAllRoles();
                roleModel.Sections = await GetAllPermissions();



                roleModel.RoleFilterControl = new List<SelectListItem>();
                roleModel.FunctionsFilterControl = new List<SelectListItem>();

                if (roleModel.RoleList != null)
                {
                    roleModel.RoleList.ForEach(

                        r =>
                        {
                            roleModel.RoleFilterControl.Add(new SelectListItem
                            {
                                Text = r.Name,
                                Value = r.Id,
                                Selected = SelectedRoleFilter.Contains(r.Id)
                            }) ;
                        }

                        );
                }

                if (roleModel.FunctionsFilterControl != null)
                {
                    roleModel.Sections.ToList().ForEach(

                        f =>
                        {
                            f.Permissions?.ToList().ForEach(

                                p =>
                                {
                                    roleModel.FunctionsFilterControl.Add(
                                    new SelectListItem
                                    {
                                        Text = p.Text,
                                        Value = p.Key,
                                        Selected = SelectedFunctionsFilter.Contains(p.Key)

                                    }

                                    );


                                }
                                );
                        }

                        );
                }

                //if we have serach criteria, need filter data
                if (SelectedRoleFilter != null && SelectedRoleFilter.Count > 0)
                {
                    roleModel.RoleList = (from r in roleModel.RoleList where SelectedRoleFilter.Contains(r.Id) select r).ToList();
                }

                if (SelectedFunctionsFilter != null && SelectedFunctionsFilter.Count > 0)
                {

                    for (int i = 0; i < roleModel.Sections?.Count; i++)
                    {
                        roleModel.Sections.ElementAt(i).Permissions =
                            (from p in roleModel.Sections.ElementAt(i).Permissions
                             where SelectedFunctionsFilter.Contains(p.Key)
                             select p).ToList();
                    }
                }



            }
            catch (Exception ex)
            {
                roleModel.MensajeError = "Ocurrio un error: " + ex.Message;
            }

            return View(roleModel);
        }



        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_ROLES)]
        public async Task<IActionResult> Index(PerfilesViewModel model)
        {

            model.Sections = await GetAllPermissions();
            model.RoleFilterControl = new List<SelectListItem>();
            model.FunctionsFilterControl = new List<SelectListItem>();

            //TODO obtener los mensajes de archivos de recursos
            if (!ModelState.IsValid)
            {
                model.MensajeError = $"Por favor verifica los datos";
                return View(model);
            }

            try
            {
                


                //Create Role
                if (String.IsNullOrEmpty(model.Id))
                {
                    var role = new ApplicationRole(model.Name, model.Name);
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        foreach (string permission in model.SelectedPermissions)
                        {
                            await _roleManager.AddClaimAsync(role, new Claim(SecurityConstants.PERMISSION, permission));
                        }

                       
                        model.RoleList = await GetAllRoles();
                        model.MensajeInfo = $"Perfil agregado con éxito";
                    }

                }
                else
                {
                    //edit role
                    var role = await _roleManager.FindByIdAsync(model.Id);

                    if (UsersExistsInRole(role.Name))
                    {
                        model.RoleList = await GetAllRoles();
                        model.MensajeError = $"No es posible actualizar el rol, debido a que tiene usuarios asignados";
                    }
                    else if(role != null)
                    {
                        role.CustomName = model.Name;
                        var result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded)
                        {
                            model.RoleList = await GetAllRoles();
                            model.MensajeInfo = $"Perfil actualizado con éxito";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                model.RoleList = await GetAllRoles();
                model.MensajeError = $"Ocurrio un error {ex.Message} ";
            }


            if (model.RoleList != null)
            {
                model.RoleList.ForEach(
                    r =>
                    {
                        model.RoleFilterControl.Add(new SelectListItem
                        {
                            Text = r.Name,
                            Value = r.Id

                        });
                    }

                    );
            }

            if (model.FunctionsFilterControl != null)
            {
                model.Sections.ToList().ForEach(
                    f =>
                    {
                        f.Permissions?.ToList().ForEach(
                            p =>
                            {
                                model.FunctionsFilterControl.Add(
                                new SelectListItem
                                {
                                    Text = p.Text,
                                    Value = p.Key

                                }

                                );
                            }
                            );
                    }

                    );
            }




            return View(model);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_ROLES)]
        public async Task<IActionResult> SetPermission(string id)
        {

            string[] vals = id.Split(",");
            if(vals != null && vals.Count() == 2)
            {
                var role = await _roleManager.FindByIdAsync(vals[0]);
                if (role != null)
                {
                    //change permission
                    IList<Claim> claims = await _roleManager.GetClaimsAsync(role);
                    IList<String> actualPermissions = claims.Select(c => c.Value).ToList();
                    var sections = await GetAllPermissions();
                    try
                    {
                        if (actualPermissions.Contains(vals[1]))
                        {
                            foreach (Claim permission in claims)
                            {
                                if (vals[1].Equals(permission.Value))
                                {
                                    await _roleManager.RemoveClaimAsync(role, permission);
                                    var report = await AuditTrailComparison(role.Name, sections.SelectMany(x => x.Permissions).Where(x => x.Key == permission.Value).Select(x => x.Text).FirstOrDefault(), true);
                                    var ReportDb = _reportAuditTrailRepository.AddAsync(report);
                                }
                            }
                        }
                        else
                        {
                            await _roleManager.AddClaimAsync(role, new Claim(SecurityConstants.PERMISSION, vals[1]));
                            var report = await AuditTrailComparison(role.Name, sections.SelectMany(x => x.Permissions).Where(x => x.Key == vals[1]).Select(x => x.Text).FirstOrDefault(), false);
                            var ReportDb = _reportAuditTrailRepository.AddAsync(report);
                        }
                        return Ok();

                    }
                    catch
                    {
                        //TODO send to log
                        return BadRequest();
                    }
                    
                }
                else
                    return NotFound();

            }
            else
            {
                return BadRequest();
            }

        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(SecurityConstants.ELIMINAR_ROL_DEL_CATALOGO_DE_ROLES)]
        public async Task<IActionResult> DeleteProfile(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);
            var rolesDelegate = this._config["rolesDelegate"];
            var rolesDelegateList = rolesDelegate?.Split(",").Select(x => x.Trim()).ToList();

            if(rolesDelegateList.Contains(id))
                return Json(new { Result = "Fail", Message = "El rol no se puede eliminar"});

            if (role.Name == SecurityConstants.PERFIL_ADMIN)
                return Json(new { Result = "Fail", Message = "El usuario es administrador"});



            if (UsersExistsInRole(role.Name))
                return Json(new { Result = "Fail", Message = "No es posible eliminar el rol, ya que tiene usuarios asignados" });


            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return Ok();
                else
                    return Json(new { Result = "Fail", Message = "No es posible eliminar el rol, ocurrió un error" });
            }
            else
                return NotFound();

        }

        private bool UsersExistsInRole(string name)
        {
            var users = _userManager.Users;

            if(users != null)
            {
                foreach (var item in users)
                {
                    if (item.Rol?.Split(",").Where(r => r.Equals(name)).FirstOrDefault() != null)
                        return true;
                }
            }

            return false;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DE_ROLES)]
        public async Task<IActionResult> GetProfile(string id)
        {
            try
            {
                var modelReturn = new PerfilesViewModel();

                var role = await _roleManager.FindByIdAsync(id);


                if (role != null)
                {
                    modelReturn.Name = role.CustomName;
                    modelReturn.Id = role.Id;
                    IList<Claim> permissions = await _roleManager.GetClaimsAsync(role);

                    if (permissions != null && permissions.Count() > 0)
                    {
                        modelReturn.SelectedPermissions = permissions.Select(claim => claim.Value).ToList();
                    }

                    modelReturn.Sections = await GetAllPermissions();

                    return Ok(modelReturn);
                }
                else
                {
                    return NotFound();
                }

            }
            catch 
            {
                return BadRequest();
            }

        }



        #region catalogs




        #endregion



        #region security

        private async Task<IList<SectionData>> GetAllPermissions()
        {
            return await _securityService.GetAllPermissionsAsync();
        }

        private async Task<List<RoleData>> GetAllRoles()
        {
            var roleItems = new List<RoleData>();
            var roles = _roleManager.Roles.ToList();

            foreach (ApplicationRole role in roles)
            {
                var item = new RoleData();
                item.Name = role.CustomName;
                item.Id = role.Id;

                //agrega los permisos
                IList<Claim> permissions = await _roleManager.GetClaimsAsync(role);

                if (permissions != null && permissions.Count() > 0)
                {
                    item.ListaPermisos = permissions.Select(claim => claim.Value).ToList();
                }


                roleItems.Add(item);
            }

            return roleItems;
        }

        #endregion


        async Task<IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail>> AuditTrailComparison(string role, string permission, bool on)
        {
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var auditList = new List<LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail>();
            if (on)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProfilesController",
                    Date = DateTime.Now,
                    Detail = "Campo - rol",
                    Funcionality = "Catálogo roles",
                    PreviousValue = "Apagado " + role + " " + " " + permission,
                    NewValue = "Encendido "  + role + " " + " " + permission,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo.Id
                });
            }
            else
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProfilesController",
                    Date = DateTime.Now,
                    Detail = "Campo - rol",
                    Funcionality = "Catálogo roles",
                    PreviousValue = "Encendido " + role + " " + " " + permission,
                    NewValue = "Apagado " + role + " " + " " + permission,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo.Id
                });
            }
            return auditList;
        }

    }
}
