using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LiberacionProductoWeb.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CatalogsController : Controller
    {
        private readonly IAnalyticsCertsService _analyticsCerts;
        private readonly IGeneralCatalogRepository _generalRepository;
        private readonly IFormulaCatalogRepository _formulaRepository;
        private readonly IProductCatalogRepository _productRepository;
        private readonly IStabilityCatalogRepository _stabilityCatalogRepository;
        private readonly IContainerCatalogRepository _containerCatalogRepository;
        private readonly IDispositionCatalogRepository _dispositionCatalogRepository;
        private readonly IReportAuditTrailService _reportAuditTrailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private AppDbContext _context;
        private readonly int[] plantsByUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrincipalService _principalService;

        public CatalogsController(IAnalyticsCertsService analyticsCertsService,
            AppDbContext context,
            IGeneralCatalogRepository generalCatalogRepository,
            IFormulaCatalogRepository formulaCatalogRepository,
            IProductCatalogRepository productCatalogRepository,
            IStabilityCatalogRepository stabilityCatalogRepository,
            IContainerCatalogRepository containerCatalogRepository,
            IDispositionCatalogRepository dispositionCatalogRepository,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IReportAuditTrailService reportAuditTrailService,
            IPrincipalService principalService)
        {
            _analyticsCerts = analyticsCertsService;
            _context = context;
            _generalRepository = generalCatalogRepository;
            _formulaRepository = formulaCatalogRepository;
            _productRepository = productCatalogRepository;
            _stabilityCatalogRepository = stabilityCatalogRepository;
            _containerCatalogRepository = containerCatalogRepository;
            _dispositionCatalogRepository = dispositionCatalogRepository;
            _reportAuditTrailService = reportAuditTrailService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _principalService = principalService;
            try
            {
                var name = httpContextAccessor.HttpContext.User.Identity.Name;
                var identity = userManager.FindByNameAsync(name);
                if (identity != null)
                {
                    var sourcePlants = identity.Result.PlantaUsuario?.Split(",")?.ToList();
                    if (sourcePlants != null)
                    {
                        var i = 0;
                        plantsByUser = new int[sourcePlants.Count];
                        sourcePlants.ForEach(
                            p =>
                            {
                                plantsByUser[i] = Convert.ToInt32(p);
                                i++;
                            }
                            );
                    }
                }
            }
            catch
            { }

        }



        #region Container Catalog

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_ENVASE_PRIMARIO)]
        public IActionResult ClearFiltersContainer()
        {
            return RedirectToAction("Container");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_ENVASE_PRIMARIO)]
        public async Task<IActionResult> ContainerAsync(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter, List<String> SelectedTanksFilter)
        {
            ContainerViewModel model = new ContainerViewModel();

            //fill filters
            model.PlantsFilter = await GetPlantsItemsAsync();
            model.ProductsFilter = await GetProdutsItemsAsync();
            model.TanksFilter = await GetTanksItemsAsync();


            model.ContainerList = new List<Container>();

            var container = await _containerCatalogRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Container>>(container);

            if (mapped != null)
            {
                foreach (var item in mapped)
                {
                    //fill external catalogs

                    var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                    var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                    var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                    item.PlantId = plantText?.Text;
                    item.ProductId = productText?.Text;
                    item.TankId = tankText?.Text;
                }

            }

            model.ContainerList = (List<Container>)mapped;


            //filter by criteria


            if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                {
                    var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedProductsFilter[i] = txt;
                }


            }

            if (SelectedProductsFilter.Count > 0)
                model.ContainerList = (from r in model.ContainerList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();

            return View(model);
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_ENVASE_PRIMARIO)]
        public async Task<JsonResult> SaveOrEditContainer([FromBody] DtoContainer data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.status == "true";
                        var plants = data.planta?.Trim().Replace(" ", "").Split(",");
                        var products = data.producto?.Trim().Replace(" ", "").Split(",");
                        var tanks = data.tanque?.Trim().Replace(" ", "").Split(",");

                        if (plants != null && products != null && tanks != null)
                        {

                            for (int i = 0; i < plants.Length; i++)
                            {
                                for (int j = 0; j < products.Length; j++)
                                {

                                    for (int w = 0; w < tanks.Length; w++)
                                    {

                                        var entity = ContainerCatalog.Create(
                                                        0,
                                                        plants[i],
                                                        products[j],
                                                        tanks[w],
                                                        data.presentation,
                                                        data.primary,
                                                        userInfo.Id,
                                                        status
                                                        );

                                        //verify unique row
                                        var all = await _containerCatalogRepository.GetAllAsync();
                                        var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                                     a.ProductId == entity.ProductId &&
                                                                     a.TankId == entity.TankId &&
                                                                     a.Presentation == entity.Presentation &&
                                                                     a.PrimaryContainer == entity.PrimaryContainer &&
                                                                     a.Status == entity.Status).FirstOrDefault();

                                        if (entityExist == null)
                                        {
                                            await _containerCatalogRepository.AddAsync(entity);
                                        }
                                        else
                                        {
                                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                        }


                                    }

                                }


                            }

                        }


                    }
                    else
                    {
                        //edit selected row


                        var entity = await _containerCatalogRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new ContainerCatalog();
                        entityClone = (ContainerCatalog)entity.Clone();
                        if (entity != null)
                        {
                            var status = data.status == "true";
                            entity.PrimaryContainer = data.primary;
                            entity.Presentation = data.presentation;
                            entity.PlantId = data.planta;
                            entity.ProductId = data.producto;
                            entity.TankId = data.tanque;
                            entity.Status = status;
                            entity.User = userInfo.Id;
                            var all = await _containerCatalogRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                         a.ProductId == entity.ProductId &&
                                                         a.TankId == entity.TankId &&
                                                         a.Presentation == entity.Presentation &&
                                                         a.PrimaryContainer == entity.PrimaryContainer &&
                                                         a.Status == entity.Status).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }

                            await _containerCatalogRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_ENVASE_PRIMARIO)]
        public async Task<JsonResult> DeleteContainer(String Id)
        {

            try
            {
                var entity = await _containerCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _containerCatalogRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_ENVASE_PRIMARIO)]
        public async Task<JsonResult> GetContainerHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> products = await GetProdutsItemsAsync();
            List<SelectListItem> tanks = await GetTanksItemsAsync();

            var entity = await _containerCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {



                var productsTag = "<select  id='producto' class='form-control' >";

                foreach (var item in products)
                {
                    if (entity.ProductId == item.Value)
                        productsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        productsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                productsTag += "</select>";




                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.Status)
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
                "<td>" + productsTag + "</td>" +
                "<td><input class='form-control' id='presentation' type='text' value='" + entity.Presentation + "'></td>" +
                                "<td><input class='form-control' id='primary' type='text'  value='" + entity.PrimaryContainer + "'></td>" +

                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }



        #endregion

        #region Stability Catalog

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO)]
        public IActionResult ClearFiltersStability()
        {
            return RedirectToAction("Stability");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO)]
        public async Task<IActionResult> StabilityAsync(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter, List<String> SelectedTanksFilter)
        {
            StabilityViewModel model = new StabilityViewModel();

            //fill filters
            model.PlantsFilter = await GetPlantsItemsAsync();
            model.ProductsFilter = await GetProdutsItemsAsync();
            model.TanksFilter = await GetTanksItemsAsync();


            model.StabilityList = new List<StabilityStudy>();

            var stability = await _stabilityCatalogRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StabilityStudy>>(stability);

            if (mapped != null)
            {
                foreach (var item in mapped)
                {
                    //fill external catalogs

                    var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                    var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                    var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                    item.PlantId = plantText?.Text;
                    item.ProductId = productText?.Text;
                    item.TankId = tankText?.Text;
                }

            }

            model.StabilityList = (List<StabilityStudy>)mapped;


            //filter by criteria
            if (SelectedPlantsFilter != null && SelectedPlantsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedPlantsFilter.Count(); i++)
                {
                    var txt = model.PlantsFilter.Where(p => p.Value == SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedPlantsFilter[i] = txt;
                }


            }

            if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                {
                    var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedProductsFilter[i] = txt;
                }


            }

            if (SelectedPlantsFilter.Count > 0 && SelectedProductsFilter.Count > 0)
            {
                model.StabilityList = (from r in model.StabilityList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
            }
            else if (SelectedPlantsFilter.Count > 0)
            {
                model.StabilityList = (from r in model.StabilityList where SelectedPlantsFilter.Contains(r.PlantId) select r).ToList();
            }
            else if (SelectedProductsFilter.Count > 0)
            {
                model.StabilityList = (from r in model.StabilityList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
            }

            return View(model);
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO)]
        public async Task<JsonResult> SaveOrEditStability([FromBody] DtoStabilityStudy data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.status == "true";
                        var plants = data.planta?.Trim().Replace(" ", "").Split(",");
                        var products = data.producto?.Trim().Replace(" ", "").Split(",");
                        var tanks = data.tanque?.Trim().Replace(" ", "").Split(",");

                        if (plants != null && products != null && tanks != null)
                        {

                            for (int i = 0; i < plants.Length; i++)
                            {
                                for (int j = 0; j < products.Length; j++)
                                {

                                    for (int w = 0; w < tanks.Length; w++)
                                    {


                                        DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                                        var resultDate = Convert.ToDateTime(data.date, usDtfi);
                                        var entity = StabilityCatalog.Create(
                                                        0,
                                                        plants[i],
                                                        products[j],
                                                        tanks[w],
                                                        resultDate,
                                                        data.code,
                                                        data.obs,
                                                        userInfo.Id,
                                                        status

                                                        );

                                        if (entity.StudyDate > DateTime.Now)
                                            throw new Exception("La fecha del estudio no puede ser mayor a la fecha actual");



                                        //verify unique row
                                        var all = await _stabilityCatalogRepository.GetAllAsync();
                                        var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                                     a.ProductId == entity.ProductId &&
                                                                     a.TankId == entity.TankId &&
                                                                     a.Code == entity.Code &&
                                                                     a.StudyDate == entity.StudyDate &&
                                                                     a.Observations == entity.Observations &&
                                                                     a.Status == entity.Status).FirstOrDefault();

                                        if (entityExist == null)
                                        {
                                            await _stabilityCatalogRepository.AddAsync(entity);
                                        }
                                        else
                                        {
                                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                        }


                                    }

                                }


                            }

                        }


                    }
                    else
                    {
                        //edit selected row


                        var entity = await _stabilityCatalogRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new StabilityCatalog();
                        entityClone = (StabilityCatalog)entity.Clone();
                        entityClone.StudyDate = entity.StudyDate;
                        if (entity != null)
                        {
                            var status = data.status == "true";
                            entity.Code = data.code;
                            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                            var resultDate = Convert.ToDateTime(data.date, usDtfi);
                            entity.StudyDate = resultDate;

                            if (entity.StudyDate > DateTime.Now)
                                throw new Exception("La fecha del estudio no puede ser mayor a la fecha actual");
                            entity.PlantId = data.planta;
                            entity.Observations = data.obs;
                            entity.ProductId = data.producto;
                            entity.TankId = data.tanque;
                            entity.Status = status;
                            entity.User = userInfo.Id;
                            var all = await _stabilityCatalogRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                         a.ProductId == entity.ProductId &&
                                                         a.TankId == entity.TankId &&
                                                         a.Code == entity.Code &&
                                                         a.Observations == entity.Observations &&
                                                         a.StudyDate == entity.StudyDate &&
                                                         a.Status == entity.Status).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }

                            await _stabilityCatalogRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO)]
        public async Task<JsonResult> DeleteStability(String Id)
        {
            try
            {
                var entity = await _stabilityCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _stabilityCatalogRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });

        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO)]
        public async Task<JsonResult> GetStabilityHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> products = await GetProdutsItemsAsync();
            List<SelectListItem> tanks = await GetTanksItemsAsync();

            var entity = await _stabilityCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {

                //gets information from catalogs
                var plantsTag = "<select id='plants' class='form-control'>";
                foreach (var item in plants)
                {
                    if (entity.PlantId == item.Value)
                        plantsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        plantsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                plantsTag += "</select>";

                var productsTag = "<select  id='producto' class='form-control' >";

                foreach (var item in products)
                {
                    if (entity.ProductId == item.Value)
                        productsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        productsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                productsTag += "</select>";
                var estatusTag = "<select  id='status' class='form-control'>";
                if (entity.Status)
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
                        "<td>" + plantsTag + "</td>" +
                        "<td>" + productsTag + "</td>" +
                "<td><input class='form-control date' id='date'  type='text' value='" + entity.StudyDate.ToString("MM-dd-yyyy") + "'></td>" +
                "<td><input class='form-control' id='code' type='text'  value='" + entity.Code + "'></td>" +
                "<td><input class='form-control' id='obs' type='text'  value='" + entity.Observations + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";
            }
            return Json(new { Result = "Ok", Html = response });
        }


        #endregion

        #region Disposition Catalog
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_DISPOSICION_DE_PNC)]
        public async Task<IActionResult> Disposition()
        {
            DispositionViewModel model = new DispositionViewModel();

            model.DispositionList = new List<Disposition>();

            var container = await _dispositionCatalogRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Disposition>>(container);

            model.DispositionList = (List<Disposition>)mapped;

            return View(model);
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DISPOSICION_DE_PNC)]
        public async Task<JsonResult> SaveOrEditDisposition([FromBody] DtoDisposition data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.status == "true";
                        var entity = DispositionCatalog.Create(
                                                        0,
                                                        data.type,
                                                        userInfo.Id,
                                                        status
                                                        );

                        //verify unique row
                        var all = await _dispositionCatalogRepository.GetAllAsync();
                        var entityExist = all.Where(a => a.DispositionType == entity.DispositionType).FirstOrDefault();
                        if (entityExist == null)
                        {
                            await _dispositionCatalogRepository.AddAsync(entity);
                        }
                        else
                        {
                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                        }
                    }
                    else
                    {
                        //edit selected row


                        var entity = await _dispositionCatalogRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new DispositionCatalog();
                        entityClone = (DispositionCatalog)entity.Clone();
                        if (entity != null)
                        {
                            var status = data.status == "true";
                            entity.Status = status;
                            entity.DispositionType = data.type;
                            entity.User = userInfo.Id;
                            var all = await _dispositionCatalogRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.DispositionType == entity.DispositionType).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }

                            await _dispositionCatalogRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_DISPOSICION_DE_PNC)]
        public async Task<JsonResult> DeleteDisposition(String Id)
        {
            try
            {
                var entity = await _dispositionCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _dispositionCatalogRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });

        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DISPOSICION_DE_PNC)]
        public async Task<JsonResult> GetDispositionHTMLTagsById(string Id)
        {
            String response = String.Empty;


            var entity = await _dispositionCatalogRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {

                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.Status)
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
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.Id + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td><input class='form-control' id='type' type='text' value='" + entity.DispositionType + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }



        #endregion

        #region Product Catalog

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_PRODUCTO)]
        public IActionResult ClearFiltersProduct()
        {
            return RedirectToAction("Product");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_PRODUCTO)]
        public async Task<IActionResult> ProductAsync(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter, List<String> SelectedTanksFilter)
        {
            ProductViewModel model = new ProductViewModel();

            //fill filters
            model.PlantsFilter = await GetPlantsItemsAsync();
            model.ProductsFilter = await GetProdutsItemsAsync();
            model.TanksFilter = await GetTanksItemsAsync();


            model.ProductList = new List<Product>();

            var producto = await _productRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Product>>(producto);

            if (mapped != null)
            {
                foreach (var item in mapped)
                {
                    //fill external catalogs

                    var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                    var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                    var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                    item.PlantId = plantText?.Text;
                    item.ProductId = productText?.Text;
                    item.TankId = tankText?.Text;
                }

            }

            model.ProductList = (List<Product>)mapped;


            //filter by criteria

            if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                {
                    var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedProductsFilter[i] = txt;
                }

            }

            if (SelectedProductsFilter.Count > 0)
                model.ProductList = (from r in model.ProductList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();


            return View(model);
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_PRODUCTO)]
        public async Task<JsonResult> SaveOrEditProduct([FromBody] DtoProduct data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.status == "true";
                        var plants = data.planta?.Trim().Replace(" ", "").Split(",");
                        var products = data.producto?.Trim().Replace(" ", "").Split(",");
                        var tanks = data.tanque?.Trim().Replace(" ", "").Split(",");

                        if (plants != null && products != null && tanks != null)
                        {

                            for (int i = 0; i < plants.Length; i++)
                            {
                                for (int j = 0; j < products.Length; j++)
                                {

                                    for (int w = 0; w < tanks.Length; w++)
                                    {

                                        var entity = ProductCatalog.Create(
                                                        0,
                                                        plants[i],
                                                        products[j],
                                                        tanks[w],
                                                        data.formula,
                                                        data.purity,
                                                        data.presentation,
                                                        userInfo.Id,
                                                        status
                                                        );

                                        //verify unique row
                                        var all = await _productRepository.GetAllAsync();
                                        var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                                     a.ProductId == entity.ProductId &&
                                                                     a.TankId == entity.TankId &&
                                                                     a.FormulaName == entity.FormulaName &&
                                                                     a.Purity == entity.Purity &&
                                                                     a.Presentation == entity.Presentation &&
                                                                     a.Status == entity.Status).FirstOrDefault();

                                        if (entityExist == null)
                                        {
                                            await _productRepository.AddAsync(entity);
                                        }
                                        else
                                        {
                                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                        }


                                    }

                                }


                            }

                        }


                    }
                    else
                    {
                        //edit selected row


                        var entity = await _productRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new ProductCatalog();
                        entityClone = (ProductCatalog)entity.Clone();
                        if (entity != null)
                        {
                            var status = data.status == "true";
                            entity.FormulaName = data.formula;
                            entity.Presentation = data.presentation;
                            entity.PlantId = data.planta;
                            entity.ProductId = data.producto;
                            entity.TankId = data.tanque;
                            entity.Status = status;
                            entity.User = userInfo.Id;
                            entity.Purity = data.purity;
                            var all = await _productRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                         a.ProductId == entity.ProductId &&
                                                         a.TankId == entity.TankId &&
                                                         a.FormulaName == entity.FormulaName &&
                                                         a.Purity == entity.Purity &&
                                                         a.Status == entity.Status).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }

                            await _productRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_PRODUCTO)]
        public async Task<JsonResult> DeleteProduct(String Id)
        {
            try
            {
                var entity = await _productRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _productRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });

        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_DE_PRODUCTO)]
        public async Task<JsonResult> GetProductHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> products = await GetProdutsItemsAsync();
            List<SelectListItem> tanks = await GetTanksItemsAsync();

            var entity = await _productRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {



                var productsTag = "<select  id='producto' class='form-control' >";

                foreach (var item in products)
                {
                    if (entity.ProductId == item.Value)
                        productsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        productsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                productsTag += "</select>";




                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.Status)
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
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.Id + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + productsTag + "</td>" +
                "<td><input class='form-control' id='formula' type='text' value='" + entity.FormulaName + "'></td>" +
                "<td><input class='form-control' id='purity' type='number'  value='" + entity.Purity + "'>%</td>" +
                "<td><input class='form-control' id='presentation' type='text'  value='" + entity.Presentation + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }


        #endregion

        #region Formula Catalog

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_FORMULA_FARMACEUTICA)]
        public IActionResult ClearFiltersFormula()
        {
            return RedirectToAction("Formula");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_FORMULA_FARMACEUTICA)]
        public async Task<IActionResult> FormulaAsync(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter, List<String> SelectedTanksFilter)
        {
            FormulaViewModel model = new FormulaViewModel();

            //fill filters
            model.PlantsFilter = await GetPlantsItemsAsync();
            model.ProductsFilter = await GetProdutsItemsAsync();
            model.TanksFilter = await GetTanksItemsAsync();


            model.FormulaList = new List<Formula>();

            var formula = await _formulaRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Formula>>(formula);

            if (mapped != null)
            {
                foreach (var item in mapped)
                {
                    //fill external catalogs

                    var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                    var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                    var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                    item.PlantId = plantText?.Text;
                    item.ProductId = productText?.Text;
                    item.TankId = tankText?.Text;
                }

            }

            model.FormulaList = (List<Formula>)mapped;


            //filter by criteria


            if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
            {
                for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                {
                    var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                    SelectedProductsFilter[i] = txt;
                }

                model.FormulaList = (from r in model.FormulaList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
            }


            if (SelectedProductsFilter.Count > 0)
                model.FormulaList = (from r in model.FormulaList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();

            return View(model);
        }


        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_FORMULA_FARMACEUTICA)]
        public async Task<JsonResult> SaveOrEditFormula([FromBody] DtoFormula data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.status == "true";
                        var plants = data.planta?.Trim().Replace(" ", "").Split(",");
                        var products = data.producto?.Trim().Replace(" ", "").Split(",");
                        var tanks = data.tanque?.Trim().Replace(" ", "").Split(",");

                        if (plants != null && products != null && tanks != null)
                        {

                            for (int i = 0; i < plants.Length; i++)
                            {
                                for (int j = 0; j < products.Length; j++)
                                {

                                    for (int w = 0; w < tanks.Length; w++)
                                    {

                                        var entity = FormulaCatalog.Create(
                                                        0,
                                                        plants[i],
                                                        products[j],
                                                        tanks[w],
                                                        data.formula,
                                                        data.purity,
                                                        data.presentation,
                                                        data.register,
                                                        userInfo.Id,
                                                        status
                                                        );

                                        //verify unique row
                                        var all = await _formulaRepository.GetAllAsync();
                                        var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                                     a.ProductId == entity.ProductId &&
                                                                     a.TankId == entity.TankId &&
                                                                     a.FormulaName == entity.FormulaName &&
                                                                     a.Purity == entity.Purity &&
                                                                     a.Presentation == entity.Presentation &&
                                                                     a.RegisterCode == entity.RegisterCode &&
                                                                     a.Status == entity.Status).FirstOrDefault();

                                        if (entityExist == null)
                                        {
                                            await _formulaRepository.AddAsync(entity);
                                        }
                                        else
                                        {
                                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                        }


                                    }

                                }


                            }

                        }


                    }
                    else
                    {
                        //edit selected row


                        var entity = await _formulaRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new FormulaCatalog();
                        entityClone = (FormulaCatalog)entity.Clone();
                        if (entity != null)
                        {
                            var status = data.status == "true";
                            entity.FormulaName = data.formula;
                            entity.Presentation = data.presentation;
                            entity.RegisterCode = data.register;
                            entity.PlantId = data.planta;
                            entity.ProductId = data.producto;
                            entity.TankId = data.tanque;
                            entity.Purity = data.purity;
                            entity.Status = status;
                            entity.User = userInfo.Id;
                            var all = await _formulaRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                         a.ProductId == entity.ProductId &&
                                                         a.TankId == entity.TankId &&
                                                         a.FormulaName == entity.FormulaName &&
                                                         a.Purity == entity.Purity &&
                                                         a.RegisterCode == entity.RegisterCode &&
                                                         a.Status == entity.Status).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }

                            await _formulaRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_FORMULA_FARMACEUTICA)]
        public async Task<JsonResult> DeleteFormula(String Id)
        {
            try
            {
                var entity = await _formulaRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _formulaRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });
        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_FORMULA_FARMACEUTICA)]
        public async Task<JsonResult> GetFormulaHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> products = await GetProdutsItemsAsync();
            List<SelectListItem> tanks = await GetTanksItemsAsync();

            var entity = await _formulaRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {



                var productsTag = "<select  id='producto' class='form-control' >";

                foreach (var item in products)
                {
                    if (entity.ProductId == item.Value)
                        productsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        productsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                productsTag += "</select>";




                var estatusTag = "<select  id='status' class='form-control'>";

                if (entity.Status)
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
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1' data-toggle='tooltip' title='Cancelar'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.Id + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + productsTag + "</td>" +
                "<td><input class='form-control' id='formula' type='text' value='" + entity.FormulaName + "'></td>" +
                "<td><input class='form-control' id='purity' type='text'  value='" + entity.Purity + "'></td>" +
                "<td><input class='form-control' id='presentation' type='text'  value='" + entity.Presentation + "'></td>" +
                "<td><input class='form-control' id='register'  type='text'  value='" + entity.RegisterCode + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }


        #endregion

        #region General Catalog
        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_GENERAL)]
        public IActionResult ClearFiltersGeneral()
        {
            return RedirectToAction("General");
        }

        [HttpGet]
        [Authorize(SecurityConstants.CONSULTAR_CATALOGO_GENERAL)]
        public async Task<IActionResult> GeneralAsync(List<String> SelectedPlantsFilter, List<String> SelectedProductsFilter, List<String> SelectedTanksFilter, int option = 0)
        {
            GeneralViewModel model = new GeneralViewModel();

            if (option == 0)
            {
                //fill filters
                model.PlantsFilter = await GetPlantsItemsAsync();
                model.ProductsFilter = await GetProdutsItemsAsync();
                model.TanksFilter = await GetTanksItemsAsync();
                model.VariableClasificationTypes = GetVariableClasificationTypes();
                model.GeneralList = new List<General>();
                var general = await _generalRepository.GetAllAsync();
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<General>>(general);
                if (mapped != null)
                {
                    foreach (var item in mapped)
                    {
                        //fill external catalogs

                        var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                        var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                        var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                        item.PlantId = plantText?.Text;
                        item.ProductId = productText?.Text;
                        item.TankId = tankText?.Text;
                    }

                }

                model.GeneralList = (List<General>)mapped;
                //filter by criteria
                if (SelectedPlantsFilter != null && SelectedPlantsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedPlantsFilter.Count(); i++)
                    {
                        var txt = model.PlantsFilter.Where(p => p.Value == SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedPlantsFilter[i] = txt;
                    }


                }

                if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                    {
                        var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedProductsFilter[i] = txt;

                    }


                }

                if (SelectedTanksFilter != null && SelectedTanksFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedTanksFilter.Count(); i++)
                    {
                        var txt = model.TanksFilter.Where(p => p.Value == SelectedTanksFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedTanksFilter[i] = txt;

                    }


                }

                if (SelectedPlantsFilter.Count > 0 && SelectedProductsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedProductsFilter.Contains(r.ProductId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0 && SelectedProductsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedProductsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedProductsFilter.Contains(r.ProductId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) select r).ToList();
                }
                else if (SelectedProductsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                else if (SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }


                return View(model);
            }
            else
            {
                //fill filters
                model.PlantsFilter = await GetPlantsItemsAsync();
                model.ProductsFilter = await GetProdutsItemsAsync();
                model.TanksFilter = await GetTanksItemsAsync();
                model.VariableClasificationTypes = GetVariableClasificationTypes();
                model.GeneralList = new List<General>();
                var general = await _generalRepository.GetAllAsync();
                var mapped = ObjectMapper.Mapper.Map<IEnumerable<General>>(general);
                if (mapped != null)
                {
                    foreach (var item in mapped)
                    {
                        //fill external catalogs

                        var plantText = model.PlantsFilter.Find(p => p.Value == Convert.ToString(item.PlantId));
                        var productText = model.ProductsFilter.Find(p => p.Value == Convert.ToString(item.ProductId));
                        var tankText = model.TanksFilter.Find(p => p.Value == Convert.ToString(item.TankId));

                        item.PlantId = plantText?.Text;
                        item.ProductId = productText?.Text;
                        item.TankId = tankText?.Text;
                    }

                }

                SelectedPlantsFilter = SelectedPlantsFilter.FirstOrDefault() != null ? SelectedPlantsFilter.FirstOrDefault().Split(",").ToList() : new List<string>();
                SelectedProductsFilter = SelectedProductsFilter.FirstOrDefault() != null ? SelectedProductsFilter.FirstOrDefault().Split(",").ToList() : new List<string>();
                SelectedTanksFilter = SelectedTanksFilter.FirstOrDefault() != null ? SelectedTanksFilter.FirstOrDefault().Split(",").ToList() : new List<string>();

                model.GeneralList = (List<General>)mapped;
                //filter by criteria
                if (SelectedPlantsFilter != null && SelectedPlantsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedPlantsFilter.Count(); i++)
                    {
                        var txt = model.PlantsFilter.Where(p => p.Value == SelectedPlantsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedPlantsFilter[i] = txt;
                    }


                }

                if (SelectedProductsFilter != null && SelectedProductsFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedProductsFilter.Count(); i++)
                    {
                        var txt = model.ProductsFilter.Where(p => p.Value == SelectedProductsFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedProductsFilter[i] = txt;

                    }


                }

                if (SelectedTanksFilter != null && SelectedTanksFilter.Count > 0)
                {
                    for (int i = 0; i < SelectedTanksFilter.Count(); i++)
                    {
                        var txt = model.TanksFilter.Where(p => p.Value == SelectedTanksFilter.ElementAt(i)).FirstOrDefault()?.Text;
                        SelectedTanksFilter[i] = txt;

                    }


                }

                if (SelectedPlantsFilter.Count > 0 && SelectedProductsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedProductsFilter.Contains(r.ProductId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0 && SelectedProductsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedProductsFilter.Count > 0 && SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedProductsFilter.Contains(r.ProductId) && SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }
                else if (SelectedPlantsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedPlantsFilter.Contains(r.PlantId) select r).ToList();
                }
                else if (SelectedProductsFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedProductsFilter.Contains(r.ProductId) select r).ToList();
                }
                else if (SelectedTanksFilter.Count > 0)
                {
                    model.GeneralList = (from r in model.GeneralList where SelectedTanksFilter.Contains(r.TankId) select r).ToList();
                }

                //model.PlantsFilter = model.PlantsFilter.Where(x => SelectedPlantsFilter.Any(y => y.ToString() == x.Text)).ToList();
                //model.ProductsFilter = model.ProductsFilter.Where(x => SelectedProductsFilter.Any(y => y.ToString() == x.Text)).ToList();
                model.ProductsFilter = new List<SelectListItem>();
                var products = new List<SelectListItem>();
                foreach (var item in model.PlantsFilter.Where(x => SelectedPlantsFilter.Any(y => y.ToString() == x.Text)).ToList())
                {
                    var productsDB = (await GetProdutsItemsAsync(item.Value)).ToList();
                    foreach (var itemy in productsDB)
                    {
                        products.Add(new SelectListItem
                        {
                            Text = itemy.Text,
                            Value = itemy.Value
                        });
                    }

                }

                var resultProduct = from o in products
                                    group o by (o.Text, o.Value) into g
                                    select new SelectListItem
                                    {
                                        Text = g.Key.Text,
                                        Value = g.Key.Value

                                    };



                model.ProductsFilter = resultProduct.ToList();
                //get tanks
                model.TanksFilter = new List<SelectListItem>();
                var tanks = new List<SelectListItem>();
                foreach (var item in model.PlantsFilter.Where(x => SelectedPlantsFilter.Any(y => y.ToString() == x.Text)).ToList())
                {
                    foreach (var itemx in model.ProductsFilter.Where(x => SelectedProductsFilter.Any(y => y.ToString() == x.Text)).ToList())
                    {
                        var tanksDB = (await GetTanksItemsAsync(item.Value, itemx.Value)).ToList();
                        foreach (var itemy in tanksDB)
                        {
                            tanks.Add(new SelectListItem
                            {
                                Text = itemy.Text,
                                Value = itemy.Value
                            });
                        }
                    }
                }

                var resultTanks = from o in tanks
                                  group o by (o.Text, o.Value) into g
                                  select new SelectListItem
                                  {
                                      Text = g.Key.Text,
                                      Value = g.Key.Value

                                  };

                model.TanksFilter = resultTanks.ToList();
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_GENERAL)]
        public async Task<JsonResult> SaveOrEditGeneral([FromBody] DtoGeneral data)
        {
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (data != null)
                {
                    if (String.IsNullOrEmpty(data.id))
                    {
                        var status = data.estado == "true";
                        var plants = data.planta?.Trim().Replace(" ", "").Split(",");
                        var products = data.producto?.Trim().Replace(" ", "").Split(",");
                        var tanks = data.tanque?.Trim().Replace(" ", "").Split(",");

                        if (plants != null && products != null && tanks != null)
                        {

                            for (int i = 0; i < plants.Length; i++)
                            {
                                for (int j = 0; j < products.Length; j++)
                                {

                                    for (int w = 0; w < tanks.Length; w++)
                                    {

                                        var entity = GeneralCatalog.Create(
                                                        0,
                                                        plants[i],
                                                        products[j],
                                                        tanks[w],
                                                        data.factor,
                                                        data.area,
                                                        data.etapa,
                                                        data.variable,
                                                        data.espvariable,
                                                        data.liminf,
                                                        data.limsup,
                                                        data.clasificacion,
                                                        data.codigo,
                                                        data.desc,
                                                        data.bascula,
                                                        data.bahia,
                                                        data.bomba,
                                                        data.manguera,
                                                        userInfo.Id,
                                                        status
                                                        );

                                        //verify unique row
                                        var all = await _generalRepository.GetAllAsync();
                                        var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                                     a.ProductId == entity.ProductId &&
                                                                     a.TankId == entity.TankId &&
                                                                     a.ConversionFactor == entity.ConversionFactor &&
                                                                     a.Area == entity.Area &&
                                                                     a.ProcessStep == entity.ProcessStep &&
                                                                     a.Variable == entity.Variable &&
                                                                     a.VariableSpecification == entity.VariableSpecification &&
                                                                     a.LowerLimit == entity.LowerLimit &&
                                                                     a.UpperLimit == entity.UpperLimit &&
                                                                     a.VariableClasification == entity.VariableClasification &&
                                                                     a.CodeTool == entity.CodeTool &&
                                                                     a.DescriptionTool == entity.DescriptionTool &&
                                                                     a.BayArea == entity.BayArea &&
                                                                     a.WeighingMachine == entity.WeighingMachine &&
                                                                     a.FillingHose == entity.FillingHose &&
                                                                     a.FillingPump == entity.FillingPump &&
                                                                     a.Estatus == entity.Estatus).FirstOrDefault();

                                        if (entityExist == null)
                                        {
                                            await _generalRepository.AddAsync(entity);
                                        }
                                        else
                                        {
                                            throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                        }


                                    }

                                }


                            }

                        }


                    }
                    else
                    {
                        //edit selected row


                        var entity = await _generalRepository.GetByIdAsync(Convert.ToInt32(data.id));
                        var entityClone = new GeneralCatalog();
                        entityClone = (GeneralCatalog)entity.Clone();
                        if (entity != null)
                        {
                            entity.Area = data.area;
                            entity.BayArea = data.bahia;
                            entity.CodeTool = data.codigo;
                            entity.ConversionFactor = data.factor;
                            entity.DescriptionTool = data.desc;
                            entity.Estatus = Convert.ToBoolean(data.estado);
                            entity.FillingHose = data.manguera;
                            entity.FillingPump = data.bomba;
                            entity.LowerLimit = data.liminf;
                            entity.PlantId = data.planta;
                            entity.ProcessStep = data.etapa;
                            entity.ProductId = data.producto;
                            entity.TankId = data.tanque;
                            entity.UpperLimit = data.limsup;
                            entity.Variable = data.variable;
                            entity.VariableClasification = data.clasificacion;
                            entity.VariableSpecification = data.espvariable;
                            entity.WeighingMachine = data.bascula;
                            entity.User = userInfo.Id;
                            var all = await _generalRepository.GetAllAsync();
                            var entityExist = all.Where(a => a.PlantId == entity.PlantId &&
                                                         a.ProductId == entity.ProductId &&
                                                         a.TankId == entity.TankId &&
                                                         a.ConversionFactor == entity.ConversionFactor &&
                                                         a.Area == entity.Area &&
                                                         a.ProcessStep == entity.ProcessStep &&
                                                         a.Variable == entity.Variable &&
                                                         a.VariableSpecification == entity.VariableSpecification &&
                                                         a.LowerLimit == entity.LowerLimit &&
                                                         a.UpperLimit == entity.UpperLimit &&
                                                         a.VariableClasification == entity.VariableClasification &&
                                                         a.CodeTool == entity.CodeTool &&
                                                         a.DescriptionTool == entity.DescriptionTool &&
                                                         a.BayArea == entity.BayArea &&
                                                         a.WeighingMachine == entity.WeighingMachine &&
                                                         a.FillingHose == entity.FillingHose &&
                                                         a.FillingPump == entity.FillingPump &&
                                                         a.Estatus == entity.Estatus).FirstOrDefault();

                            if (entityExist != null)
                            {
                                if (entityExist.Id != entity.Id)
                                {
                                    throw new Exception("Los datos ya existen, no es posible agregar el catálogo");
                                }
                            }
                            //IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail> reportAuditTrails;
                            //var dtao = _reportAuditTrailService.Save(reportAuditTrails);

                            await _generalRepository.UpdateAsync(entity, entityClone);
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
        [Authorize(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_GENERAL)]
        public async Task<JsonResult> DeleteGeneral(String Id)
        {
            try
            {
                var entity = await _generalRepository.GetByIdAsync(Convert.ToInt32(Id));
                if (entity != null)
                {
                    await _generalRepository.DeleteAsync(entity);
                }
                else
                {
                    throw new Exception("El registro no existe");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Fail", Message = ex.Message });
            }

            return Json(new { Result = "Ok" });

        }

        [HttpGet]
        [Authorize(SecurityConstants.EDITAR_CATALOGO_GENERAL)]
        public async Task<JsonResult> GetHTMLTagsById(string Id)
        {
            String response = String.Empty;

            List<SelectListItem> plants = await GetPlantsItemsAsync();
            List<SelectListItem> products = await GetProdutsItemsAsync();
            List<SelectListItem> tanks = await GetTanksItemsAsync();

            List<SelectListItem> clasificacionValues = GetVariableClasificationTypes();

            var entity = await _generalRepository.GetByIdAsync(Convert.ToInt32(Id));
            if (entity != null)
            {

                //gets information from catalogs
                var plantsTag = "<select id='plants' class='form-control'>";
                foreach (var item in plants)
                {
                    if (entity.PlantId == item.Value)
                        plantsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        plantsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                plantsTag += "</select>";


                var productsTag = "<select  id='producto' class='form-control' >";

                foreach (var item in products)
                {
                    if (entity.ProductId == item.Value)
                        productsTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        productsTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                productsTag += "</select>";

                var tanksTag = "<select  id='tanque' class='form-control'>";

                foreach (var item in tanks)
                {
                    if (entity.TankId == item.Value)
                        tanksTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        tanksTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                tanksTag += "</select>";


                var estatusTag = "<select  id='estado' class='form-control'>";

                if (entity.Estatus)
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


                var clasificacionTag = "<select  id='clasificacion' class='form-control'>";

                foreach (var item in clasificacionValues)
                {
                    if (entity.VariableClasification == item.Value)
                        clasificacionTag += "<option value='" + item.Value + "' selected >" + item.Text + " </option>";
                    else
                        clasificacionTag += "<option value='" + item.Value + "'>" + item.Text + " </option>";
                }

                clasificacionTag += "</select>";



                response += "<tr>" +
                "<td> <a href='javascript:void(0)' onclick='refresh();return false;'  class=' btn btn-danger btn-xs' data-id='-1'><i class='fa fa-times-circle'></i></a>" +
                        "<a href='javascript:void(0)' onclick='saveOnClick(this);return false;' id='editData' class='save-data btn btn-info btn-xs' data-id='" + entity.Id + "' ><i class='fa fa-save'></i></a> </td>" +
                "<td>" + plantsTag + "</td>" +
                "<td>" + productsTag + "</td>" +
                "<td>" + tanksTag + "</td>" +
                "<td><input class='form-control' id='factor' type='text' value='" + entity.ConversionFactor + "'></td>" +
                "<td><input class='form-control' id='area' type='text'  value='" + entity.Area + "'></td>" +
                "<td><input class='form-control' id='etapa' type='text'  value='" + entity.ProcessStep + "'></td>" +
                "<td><input class='form-control' id='variable'  type='text'  value='" + entity.Variable + "'></td>" +
                "<td><input class='form-control' id='espvariable'  type='text'  value='" + entity.VariableSpecification + "'></td>" +
                "<td><input class='form-control' id='liminf'  type='text'   value='" + entity.LowerLimit + "'></td>" +
                "<td><input class='form-control' id='limsup'  type='text'  value='" + entity.UpperLimit + "'></td>" +
                "<td>" + clasificacionTag + "</td>" +
                "<td><input class='form-control' id='codigo'  type='text'  value='" + entity.CodeTool + "'></td>" +
                "<td><input class='form-control' id='desc'  type='text'  value='" + entity.DescriptionTool + "'></td>" +
                "<td><input class='form-control' id='bascula' type='text'  value='" + entity.WeighingMachine + "'></td>" +
                "<td><input class='form-control' id='bahia'  type='text'  value='" + entity.BayArea + "'></td>" +
                "<td><input class='form-control' id='bomba'  type='text'  value='" + entity.FillingPump + "'></td>" +
                "<td><input class='form-control' id='manguera'  type='text'  value='" + entity.FillingHose + "'></td>" +
                "<td>" + estatusTag + "</td>" +
                "</tr>";


            }


            return Json(new { Result = "Ok", Html = response });
        }
        #endregion


        #region helper methods

        private async Task<List<SelectListItem>> GetPlantsItemsAsync(int id = -1)
        {
            //filter by user


            Dictionary<int, string> plants = await _analyticsCerts.GetPlants();
            List<SelectListItem> response = new List<SelectListItem>();

            foreach (KeyValuePair<int, string> entry in plants)
            {
                if (plantsByUser != null)
                {
                    if (plantsByUser.ToList().Contains(entry.Key))
                    {
                        response.Add(new SelectListItem
                        {
                            Text = entry.Value,
                            Value = Convert.ToString(entry.Key)

                        });
                    }
                }
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetProdutsItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            Dictionary<string, string> productsFiltered = new Dictionary<string, string>();

            //gets products
            if (plantsByUser != null)
            {
                foreach (var item in plantsByUser.ToList())
                {
                    var products = await _analyticsCerts.GetProducts(item);
                    foreach (var p in products)
                    {
                        if (!productsFiltered.ContainsKey(p.Key))
                            productsFiltered.Add(p.Key, p.Value);
                    }
                }
            }


            foreach (KeyValuePair<string, string> entry in productsFiltered)
            {
                response.Add(new SelectListItem
                {
                    Text = entry.Value,
                    Value = Convert.ToString(entry.Key)

                });
            }

            return response;
        }

        private async Task<List<SelectListItem>> GetTanksItemsAsync(int id = -1)
        {
            List<SelectListItem> response = new List<SelectListItem>();
            Dictionary<string, string> tanksFiltered = new Dictionary<string, string>();



            //gets tanks
            if (plantsByUser != null)
            {
                foreach (var item in plantsByUser.ToList())
                {
                    var tanks = await _analyticsCerts.GetTanks(item);
                    foreach (var t in tanks)
                    {
                        response.Add(new SelectListItem
                        {
                            Text = t.Descripcion,
                            Value = t.Descripcion

                        });
                    }
                }
            }

            return response;
        }

        private List<SelectListItem> GetVariableClasificationTypes()
        {
            List<SelectListItem> response = new List<SelectListItem>() {
                new SelectListItem { Text = VariableClasificationType.ControlVariable.Value, Value = VariableClasificationType.ControlVariable.Value },
                new SelectListItem { Text = VariableClasificationType.CriticalParameter.Value, Value = VariableClasificationType.CriticalParameter.Value },
                new SelectListItem { Text = VariableClasificationType.CriticalQualityAttribute.Value, Value = VariableClasificationType.CriticalQualityAttribute.Value }
            };

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
            var productsFiltered = (await _generalRepository.GetAsync(x => plantIds.Contains(x.PlantId))).Select(x => x.ProductId);

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
        #endregion

    }
}
