using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository.Base.External;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Data.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IUsuariosExtRepository _usuariosExtRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger _logger;
        private AppDbContext _context;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IReportAuditTrailRepository _reportAuditTrailRepository;


        public UsersController(UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IAnalyticsCertsService analyticsCertsService,
            RoleManager<ApplicationRole> roleManager,
            AppDbContext context,
            ILogger<UsersController> logger,
            IEmailSender emailSender,
            IWebHostEnvironment environment,
            IUsuariosExtRepository usuariosExtRepository,
            IReportAuditTrailRepository reportAuditTrailRepository
            )
        {
            _analyticsCerts = analyticsCertsService;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
            _environment = environment;
            _config = config;
            _usuariosExtRepository = usuariosExtRepository;
            _reportAuditTrailRepository = reportAuditTrailRepository;
        }


        #region Users Catalog



        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DE_USUARIOS)]
        public IActionResult ClearFilters()
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> IndexAsync(List<String> SelectedPlantsFilter, List<String> SelectedUsersFilter, List<String> SelectedProfilesFilter, List<String> SelectedStatusFilter)
        {
            UsuariosViewModel model = new UsuariosViewModel();

            //fill filters
            model.PlantsFilter = await GetPlantsItemsAsync();
            model.UsersFilter = await GetUsersItemsAsync();
            model.ProfilesFilter = await GetProfilesItemsAsync();
            model.ExternalUsersFilter = await GetExternalUsers();


            model.Usuarios = GetAllUsers();


            if (model.Usuarios != null)
            {
                foreach (var item in model.Usuarios)
                {
                    //fill external catalogs
                    var plantsId = item.PlantasUsuario?.Trim().Replace(" ", "").Split(",");
                    String[] plantsText = null;
                    if (plantsId != null)
                    {
                        try
                        {
                            plantsText = new String[plantsId.Length];
                            for (int i = 0; i < plantsId.Length; i++)
                            {
                                if (plantsId[i].ToString() != "")
                                {
                                    plantsText[i] = model.PlantsFilter.Find(p => p.Value == plantsId[i])?.Text;
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }

                        item.PlantasUsuario = String.Join(",", plantsText);
                    }

                    var rolesId = item.RolesUsuario?.Split(",");
                    String[] rolesText = null;
                    if (rolesId != null)
                    {
                        rolesText = new String[rolesId.Length];
                        for (int i = 0; i < rolesId.Length; i++)
                        {
                            rolesText[i] = model.ProfilesFilter.Find(p => p.Value == rolesId[i]?.Trim())?.Text;
                        }

                        item.RolesUsuario = String.Join(",", rolesText);
                    }

                }

            }

            List<DatosUsuario> filtro = new List<DatosUsuario>();

            //filter by criteria
            if (SelectedPlantsFilter != null && SelectedPlantsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedPlantsFilter.Count(); i++)
                {
                    var txt = model.PlantsFilter.Where(p => p.Value == SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedPlantsFilter[i] = txt;

                }
            }

            if (SelectedUsersFilter != null && SelectedUsersFilter.Count > 0)
            {
                for (int i = 0; i < SelectedUsersFilter.Count(); i++)
                {
                    var txt = model.UsersFilter.Where(p => p.Value == SelectedUsersFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedUsersFilter[i] = txt;

                }
            }



            List<String> b = new List<String>();

            SelectedStatusFilter.ForEach(z => { b.Add(z); });

            if (SelectedPlantsFilter.Count > 0 || SelectedUsersFilter.Count > 0 || SelectedProfilesFilter.Count > 0 || SelectedStatusFilter.Count > 0)
                model.Usuarios = (from r in model.Usuarios
                                  where SelectedPlantsFilter.Any(s => r.PlantasUsuario.Split(",").Any(o => o == s))
         || SelectedProfilesFilter.Any(s => r.RolesUsuario.Split(",").Any(o => o == s))
         || SelectedUsersFilter.Contains(r.MexeUsuario)
         || b.Contains(r.EstatusUsuario)
                                  select r).ToList();
            return View(model);
        }


        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DE_USUARIOS)]
        public async Task<JsonResult> GetDataById(String Id)
        {
            var userId = Convert.ToUInt64(Id);
            var data = GetExternalUSerData(userId);

            if (data != null)
            {

                return Json(new { Nombre = data.Nombre, Usuario = data.Usuario, Email = data.Email });
            }
            else
            {
                return Json(new { Nombre = data.Nombre, Usuario = data.Usuario, Email = data.Email });
            }

        }



        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_USUARIOS)]
        public async Task<JsonResult> SaveOrEdit([FromBody] DatosUsuario data)
        {
            try
            {
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.Id))
                    {
                        //var status = data.EstatusUsuario == "true";
                        //var plants = data.PlantasUsuario?.Split(",");
                        var profiles = data.RolesUsuario?.Split(",");

                        var all = _userManager.Users;

                        if (all.Where(u => u.MexeUsuario == data.MexeUsuario).FirstOrDefault() != null)
                            throw new Exception("Ya existe un usuario con el mismo MEXE");

                        if (all.Where(u => u.EmailUsuario == data.EmailUsuario).FirstOrDefault() != null)
                            throw new Exception("Ya existe un usuario con el mismo correo");

                        if (all.Where(u => u.NombreUsuario == data.NombreUsuario).FirstOrDefault() != null)
                            throw new Exception("Ya existe un usuario con el mismo nombre");

                        if (String.IsNullOrEmpty(data.RolesUsuario))
                            throw new Exception("Es necesario seleccionar un Rol");

                        if (String.IsNullOrEmpty(data.PlantasUsuario))
                            throw new Exception("Es necesario seleccionar una Planta");


                        ApplicationUser user = new ApplicationUser
                        {
                            UserName = data.MexeUsuario,
                            EmailUsuario = data.EmailUsuario,
                            MexeUsuario = data.MexeUsuario,
                            PlantaUsuario = data.PlantasUsuario,
                            Activo = data.EstatusUsuario == "true",
                            NombreUsuario = data.NombreUsuario,
                            Rol = data.RolesUsuario,
                            ExternalId = Convert.ToInt32(data.ExternalUsuario),
                            LockoutEnd = data.EstatusUsuario == "true" ? null : DateTime.Now.AddYears(1),
                            LockoutEnabled = true
                        };

                        var createResult = await _userManager.CreateAsync(user);


                        if (createResult.Succeeded)
                        {
                            var createdUser = await _userManager.FindByNameAsync(data.MexeUsuario);
                            foreach (var item in profiles)
                            {
                                await _userManager.AddToRoleAsync(createdUser, item.Trim());
                            }

                        }


                    }
                    else
                    {
                        //edit selected row

                        var profiles = data.RolesUsuario?.Split(",");
                        var entity = await _userManager.FindByIdAsync(data.Id);
                        var entityClone = new ApplicationUser();
                        entityClone = (ApplicationUser)entity.Clone();
                        if (entity != null)
                        {
                            entity.UserName = data.MexeUsuario;
                            entity.NombreUsuario = data.NombreUsuario;
                            entity.EmailUsuario = data.EmailUsuario;
                            entity.Activo = data.EstatusUsuario == "true";
                            entity.Rol = data.RolesUsuario;
                            entity.PlantaUsuario = data.PlantasUsuario;
                            entity.MexeUsuario = data.MexeUsuario;
                            entity.AccessFailedCount = 0;
                            entity.LockoutEnd = data.EstatusUsuario == "true" ? null : DateTime.Now.AddYears(1);
                            entity.ExternalId = Convert.ToInt32(data.ExternalUsuario);

                            var all = _userManager.Users;

                            if (all.Where(u => u.MexeUsuario == data.MexeUsuario && u.Id != data.Id).FirstOrDefault() != null)
                                throw new Exception("Ya existe un usuario con el mismo MEXE");

                            if (all.Where(u => u.EmailUsuario == data.EmailUsuario && u.Id != data.Id).FirstOrDefault() != null)
                                throw new Exception("Ya existe un usuario con el mismo correo");

                            if (all.Where(u => u.NombreUsuario == data.NombreUsuario && u.Id != data.Id).FirstOrDefault() != null)
                                throw new Exception("Ya existe un usuario con el mismo nombre");

                        
                            var createResult = await _userManager.UpdateAsync(entity);
                            var difClone = await AuditTrailComparison(entity, entityClone);
                            var ReportAudit = await _reportAuditTrailRepository.AddAsync(difClone);

                            if (createResult.Succeeded)
                            {
                                var createdUser = await _userManager.FindByNameAsync(data.MexeUsuario);
                                foreach (var item in profiles)
                                {
                                    await _userManager.AddToRoleAsync(createdUser, item.Trim());
                                }

                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                //TODO send to log
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }


        [HttpPost]
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_EN_CATALOGO_DE_USUARIOS)]
        public async Task<IActionResult> Delete(String Id)
        {

            var entity = await _userManager.FindByIdAsync(Id);
            if (entity != null)
            {
                //TODO verifica logica de negocio para borrar
                await _userManager.DeleteAsync(entity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_USUARIOS)]
        public async Task<JsonResult> GetHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> profiles = await GetProfilesItemsAsync();
            List<SelectListItem> extUsers = await GetExternalUsers();


            var entity = await _userManager.FindByIdAsync(Id);

            var plantas = entity.PlantaUsuario.Trim().Replace(" ", "").Split(",");
            var roles = String.Join(",", entity.Rol);
            if (entity != null)
            {

                //gets information from catalogs
                var plantsTag = "<select multiple data-style='btn-white' id='plantas' class='selectpicker show-menu-arrow combobox'>";
                foreach (var item in plants)
                {
                    if (plantas.Where(x=>x.Equals(item.Value)).Any())
                        plantsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        plantsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                plantsTag += "</select>";


                var profilesTag = "<select multiple data-style='btn-white'  id='roles' class='selectpicker show-menu-arrow combobox' >";

                foreach (var item in profiles)
                {
                    if (roles.Contains(item.Value))
                        profilesTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        profilesTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                profilesTag += "</select>";


                //users from AD
                var extUsersTag = "<select onChange='onChangeUser(this);' data-style='btn-white' data-live-search='true'  id='extuser' class='selectpicker show-menu-arrow combobox' >";

                foreach (var item in extUsers)
                {
                    if (entity.ExternalId == Convert.ToInt32(item.Value))
                        extUsersTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        extUsersTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                extUsersTag += "</select>";


                var estatusTag = "<select  id='estado' class='form-control'>";

                if (entity.Activo)
                {
                    estatusTag += "<option value='true' selected >Activo</option>";
                    estatusTag += "<option value='false' >Inactivo</option>";
                }
                else
                {
                    estatusTag += "<option value='false' selected >Inactivo</option>";
                    estatusTag += "<option value='true' >Activo</option>";
                }

                estatusTag += "</select>";


                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.Id + "' ><i class='fa fa-save'></i></a> </td>" +
                        "<td>" + extUsersTag + "</td>" +
                        "<td><input class='form-control' id='mexe' type='text' value='" + entity.MexeUsuario + "'></td>" +
                        "<td><input class='form-control' id='correo' type='text' value='" + entity.EmailUsuario + "'></td>" +
                "<td>" + profilesTag + "</td>" +
                "<td>" + plantsTag + "</td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }


        #endregion

        #region Helpers


        private List<DatosUsuario> GetAllUsers()
        {
            var users = _userManager.Users;
            List<DatosUsuario> response = new List<DatosUsuario>();
            if (users != null)
            {

                foreach (var item in users)
                {
                    if (item.UserName == Helpers.SecurityConstants.ADMIN_USER)
                        continue;

                    DatosUsuario user = new DatosUsuario();
                    user.Id = item.Id;
                    user.EmailUsuario = item.EmailUsuario;
                    user.NombreUsuario = item.NombreUsuario;
                    user.MexeUsuario = item.MexeUsuario;
                    user.PlantasUsuario = item.PlantaUsuario;
                    user.RolesUsuario = item.Rol;
                    user.EstatusUsuario = item.Activo == true ? "true" : "false";
                    response.Add(user);
                }
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetPlantsItemsAsync(int id = -1)
        {
            Dictionary<int, string> plants = await _analyticsCerts.GetPlants();
            List<SelectListItem> response = new List<SelectListItem>();

            foreach (KeyValuePair<int, string> entry in plants)
            {
                response.Add(new SelectListItem
                {
                    Text = entry.Value,
                    Value = Convert.ToString(entry.Key)

                });
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetUsersItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            var usuarios = _userManager.Users;

            foreach (var item in usuarios)
            {
                response.Add(new SelectListItem
                {
                    Text = item.MexeUsuario,
                    Value = item.Id

                });

            }

            return response;
        }

        private async Task<List<SelectListItem>> GetProfilesItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            var roles = _roleManager.Roles;

            foreach (var item in roles)
            {
                response.Add(new SelectListItem
                {
                    Text = item.CustomName,
                    Value = item.Name

                });
            }

            return response;
        }


        private async Task<List<SelectListItem>> GetExternalUsers()
        {
            List<SelectListItem> response = new List<SelectListItem>();

            //Obtiene los usuarios externos
            //TODO las propiedades se deben inyectar en una clase
            var isDBAvailable = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);

            if (isDBAvailable)
            {
                var usuarios = _usuariosExtRepository.GetAll()?.ToList();
                foreach (var item in usuarios)
                {
                    response.Add(new SelectListItem
                    {
                        Text = item.Nombre,
                        Value = Convert.ToString(item.No_Emp)
                    });
                }
            }
            else
            {
                //only for test porpouse
                for (int i = 0; i < 30; i++)
                {
                    response.Add(new SelectListItem
                    {
                        Text = "Usuario " + i,
                        Value = Convert.ToString(i)
                    }); ;

                }
            }

            return response;
        }


        private VwUsuarios GetExternalUSerData(UInt64 id)
        {
            VwUsuarios response = new VwUsuarios();

            //Obtiene los usuarios externos
            //TODO las propiedades se deben inyectar en una clase
            var isDBAvailable = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);

            if (isDBAvailable)
            {
                var user = _usuariosExtRepository.GetXDecimal(id);
                return user;
            }
            else
            {
                //only for test porpouse
                response.Nombre = "Nombre Prueba " + id;
                response.Usuario = "MEXE" + id;
                response.Email = "correo " + id + "@test.com";
            }

            return response;
        }

        async Task<IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail>> AuditTrailComparison(ApplicationUser objectToCompare, ApplicationUser objectToCompareOld = null)
        {
            var auditList = new List<LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail>();
            var userInfo = await GetUserId(HttpContext.User.Claims.ToList());
            var old = objectToCompareOld as ApplicationUser;
            var current = objectToCompare as ApplicationUser;
            if (old.NombreUsuario != current.NombreUsuario)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Nombre Usuario",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.NombreUsuario,
                    NewValue = current.NombreUsuario,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            if (old.EmailUsuario != current.EmailUsuario)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Email",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.EmailUsuario,
                    NewValue = current.EmailUsuario,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            if (old.MexeUsuario != current.MexeUsuario)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Usuario",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.MexeUsuario,
                    NewValue = current.MexeUsuario,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            if (old.Rol != current.Rol)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Rol",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.Rol,
                    NewValue = current.Rol,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            if (old.PlantaUsuario != current.PlantaUsuario)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Planta",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.PlantaUsuario,
                    NewValue = current.PlantaUsuario,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            if (old.Activo != current.Activo)
            {
                auditList.Add(new LiberacionProductoWeb.Models.DataBaseModels.Base.ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "UsersController",
                    Date = DateTime.Now,
                    Detail = "Campo - Activo",
                    Funcionality = "Catálogo usuarios",
                    PreviousValue = old.Activo == true ? "SI" + " " + current.MexeUsuario : "NO" + " " + current.MexeUsuario,
                    NewValue = current.Activo == true ? "SI" + " " + current.MexeUsuario : "NO" + " " + current.MexeUsuario,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = userInfo
                });
            }
            return auditList;
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
        #endregion




    }
}
