using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Reports;
using LiberacionProductoWeb.Reports.ConditioningOrder;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{


    public class CheckListController : Controller
    {
        private readonly IPrincipalService _principalService;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICheckListPipeAnswerRepository _checkListPipeAnswerRepository;
        private readonly ICheckListPipeRecordAnswerRepository _checkListPipeRecordAnswerRepository;
        private readonly ICheckListPipeCommentsAnswerReepository _checkListPipeCommentsAnswerReepository;
        private readonly ILogger<CheckListController> _logger;
        private readonly ICheckListPipeDictiumAnswerRepository _checkListPipeDictiumAnswerRepository;
        private readonly IConditioningOrderService _conditioningOrderService;
        private readonly IConditioningOrderRepository _conditioningOrderRepository;

        public CheckListController(IPrincipalService principalService, IStringLocalizer<Resource> resource,
        ILogger<CheckListController> logger, UserManager<ApplicationUser> userManager, ICheckListPipeAnswerRepository checkListPipeAnswerRepository,
          ICheckListPipeRecordAnswerRepository checkListPipeRecordAnswerRepository,
          ICheckListPipeCommentsAnswerReepository checkListPipeCommentsAnswerReepository, ICheckListPipeDictiumAnswerRepository checkListPipeDictiumAnswerRepository,
          IConditioningOrderService conditioningOrderService,
          IConditioningOrderRepository conditioningOrderRepository)
        {
            _principalService = principalService;
            _resource = resource;
            _userManager = userManager;
            _checkListPipeAnswerRepository = checkListPipeAnswerRepository;
            _checkListPipeRecordAnswerRepository = checkListPipeRecordAnswerRepository;
            _checkListPipeCommentsAnswerReepository = checkListPipeCommentsAnswerReepository;
            _logger = logger;
            _checkListPipeDictiumAnswerRepository = checkListPipeDictiumAnswerRepository;
            _conditioningOrderService = conditioningOrderService;
            _conditioningOrderRepository = conditioningOrderRepository;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS)]
        public async Task<IActionResult> Index(int idOA, string tourNumber, string distributionBatch, int checkListId)
        {
            CheckListVM model = new CheckListVM();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                model.NumberOrder = idOA;
                model.TourNumber = tourNumber;
                model.DistributionBatch = distributionBatch;
                model.checkListId = checkListId;
                if (idOA == 0 || string.IsNullOrEmpty(tourNumber) || string.IsNullOrEmpty(distributionBatch))
                {
                    // Should return error since there is no way to create or get a checklist
                    return View(model);
                }

                var conditioningOrderViewModel = await _conditioningOrderRepository.GetByIdAsync(idOA);
                var labelsDb = await _principalService.CheckListRecordLabels(idOA, checkListId);
                model.Localizate = labelsDb.Localizate;
                model.Product = labelsDb.Product;
                model.Pipe = labelsDb.Pipe;
                ///_conditioningOrderService.GetByIdAsync(idOA);

                if (conditioningOrderViewModel == null)
                {
                    // Should return error since there is no way to create or get a checklist
                    return View(model);
                }

                model = await GetCheckListVMs(model);

            }

            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en Index checkListController " + ex);
            }

            return View(model);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS)]
        public async Task<IActionResult> Save([FromForm] CheckListVM CheckListVM)
        {
            CheckListPipeAnswer model = new CheckListPipeAnswer();
            List<CheckListPipeCommentsAnswer> commentsAnswer = new List<CheckListPipeCommentsAnswer>();
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                try
                {

                    ///comment iv
                    if ((CheckListVM.CommentIv != null))
                    {
                        var CommentDB = await _checkListPipeCommentsAnswerReepository
                                         .GetAsync(x => x.NumOA == CheckListVM.NumberOrder
                                                   && x.TourNumber == CheckListVM.TourNumber
                                                   && x.DistributionBatch == CheckListVM.DistributionBatch
                                                   && x.Comment == CheckListVM.CommentIv);
                        if (!CommentDB.Any())
                        {
                            commentsAnswer.Add(new CheckListPipeCommentsAnswer
                            {
                                Author = userInfo.NombreUsuario,
                                Comment = CheckListVM.CommentIv,
                                Date = DateTime.Now,
                                Group = "Inspección visual de Pipas al recibo",
                                NumOA = CheckListVM.NumberOrder,
                                TourNumber = CheckListVM.TourNumber,
                                CheckListPipeDictiumId = CheckListVM.checkListId,
                                DistributionBatch = CheckListVM.DistributionBatch
                            });
                        }
                    }

                    ///comment fp
                    if ((CheckListVM.CommentFP != null))
                    {
                        var CommentDB = await _checkListPipeCommentsAnswerReepository
                                        .GetAsync(x => x.NumOA == CheckListVM.NumberOrder
                                                  && x.TourNumber == CheckListVM.TourNumber
                                                  && x.DistributionBatch == CheckListVM.DistributionBatch
                                                  && x.Comment == CheckListVM.CommentFP);
                        if (!CommentDB.Any())
                        {
                            commentsAnswer.Add(new CheckListPipeCommentsAnswer
                            {
                                Author = userInfo.NombreUsuario,
                                Comment = CheckListVM.CommentFP,
                                Date = DateTime.Now,
                                Group = "Checklist llenado de pipa y verificación de pipas",
                                NumOA = CheckListVM.NumberOrder,
                                TourNumber = CheckListVM.TourNumber,
                                CheckListPipeDictiumId = CheckListVM.checkListId,
                                DistributionBatch = CheckListVM.DistributionBatch
                            });
                        }
                    }

                    if (userInfo.Rol.Contains(SecurityConstants.PERFIL_RESPONSABLE_SANITARIO))
                    {
                        var dictium = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == CheckListVM.NumberOrder
                                                                                        && x.DistributionBatch == CheckListVM.DistributionBatch
                                                                                        && x.TourNumber == CheckListVM.TourNumber && x.Id == CheckListVM.checkListId);

                        foreach (var item in dictium)
                        {
                            var info = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(item.Id);
                            info.Date = DateTime.Now;
                            info.Comment = CheckListVM.DictumComment;
                            if (CheckListVM.CheckDictium1 == "true")
                            {
                                info.Verification = "OK";
                                info.InCompliance = true;
                                info.Status = CheckListType.CloseOk.Value;
                                commentsAnswer.Add(new CheckListPipeCommentsAnswer
                                {
                                    Author = userInfo.NombreUsuario,
                                    Comment = CheckListVM.DictumComment,
                                    Date = DateTime.Now,
                                    Group = "Checklist observación dictamen",
                                    NumOA = CheckListVM.NumberOrder,
                                    TourNumber = CheckListVM.TourNumber,
                                    CheckListPipeDictiumId = CheckListVM.checkListId,
                                    DistributionBatch = CheckListVM.DistributionBatch
                                });
                            }
                            if (CheckListVM.CheckDictium2 == "true")
                            {
                                info.Verification = "NO";
                                info.InCompliance = false;
                                info.Status = CheckListType.CloseNo.Value;
                                if (CheckListVM.DictumComment == null)
                                {
                                    return Json(new { Result = "Fail", Message = "Ingresa un comentario en el dictamen" });
                                }
                                commentsAnswer.Add(new CheckListPipeCommentsAnswer
                                {
                                    Author = userInfo.NombreUsuario,
                                    Comment = CheckListVM.DictumComment,
                                    Date = DateTime.Now,
                                    Group = "Checklist observación dictamen",
                                    NumOA = CheckListVM.NumberOrder,
                                    TourNumber = CheckListVM.TourNumber,
                                    CheckListPipeDictiumId = CheckListVM.checkListId,
                                    DistributionBatch = CheckListVM.DistributionBatch
                                });
                            }
                            var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(info);
                            await AddStatus(CheckListVM);
                        }
                    }


                    if (commentsAnswer.Count > 0)
                        await _checkListPipeCommentsAnswerReepository.AddAsync(commentsAnswer);

                    await SaveAll(CheckListVM, 1);

                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Ocurrio un error en Index checkListController " + ex);
                }
            }
            return Json(new { Result = "Ok", Message = "CheckList guardado con éxito" });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> Cancel([FromForm] CheckListVM CheckListVM)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            try
            {
                var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var DBRecord = (await _checkListPipeRecordAnswerRepository
                                .GetAsync(y => y.NumOA == CheckListVM.NumberOrder && y.TourNumber == CheckListVM.TourNumber
                                && y.DistributionBatch == CheckListVM.DistributionBatch
                                && y.CheckListPipeDictiumId == CheckListVM.checkListId)).LastOrDefault()?.Status;
                if (DBRecord == CheckListType.Inprogress.Value)
                {
                    modelrecord.Status = CheckListType.InCancellation.Value;
                    modelrecord.NumOA = CheckListVM.NumberOrder;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.ApproveSC = "YES";
                    modelrecord.Reason = CheckListVM.ReasonReject;
                    modelrecord.DistributionBatch = CheckListVM.DistributionBatch;
                    modelrecord.TourNumber = CheckListVM.TourNumber;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.CheckListPipeDictiumId = CheckListVM.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);
                    var info = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(CheckListVM.checkListId);
                    info.Status = CheckListType.InCancellation.Value;
                    var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(info);

                    return Json(new { Result = "Ok", Message = "La Solicitud de cancelación ha sido enviada con para su evaluación" });

                }
                else
                {
                    return Json(new { Result = "Fail", Message = "CheckList en estatus incorrecto" });

                }
                //trigered mail
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en Cancel checkListController " + ex);
                return Json(new { Result = "Fail", Message = ex.ToString() });
            }

            //return RedirectToAction("Index", "CheckList");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> CancelApprove([FromForm] CheckListVM CheckListVM)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                var DBRecord = (await _checkListPipeRecordAnswerRepository.GetAsync(x => x.Status == CheckListType.InCancellation.Value
                                                                                && x.NumOA == CheckListVM.NumberOrder
                                                                                && x.TourNumber == CheckListVM.TourNumber
                                                                                && x.DistributionBatch == CheckListVM.DistributionBatch
                                                                                && x.CheckListPipeDictiumId == CheckListVM.checkListId)).LastOrDefault()?.Status;

                if (DBRecord != CheckListType.InCancellation.Value)
                {
                    return Json(new { Result = "Fail", Message = "La Solicitud de cancelación no tiene el status correcto" });

                }
                else
                {
                    modelrecord.Status = CheckListType.Cancelled.Value;
                    modelrecord.NumOA = CheckListVM.NumberOrder;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.TourNumber = CheckListVM.TourNumber;
                    modelrecord.DistributionBatch = CheckListVM.DistributionBatch;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.CheckListPipeDictiumId = CheckListVM.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);
                    var info = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(CheckListVM.checkListId);
                    info.Status = CheckListType.Cancelled.Value;
                    var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(info);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en CancelApprove checkListController " + ex);
                return Json(new { Result = "Fail", Message = ex.ToString() });
            }

            return Json(new { Result = "Ok", Message = "La Solicitud de cancelación ha sido aprobada con éxito" });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> CancelReject([FromForm] CheckListVM CheckListVM)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                var DBRecord = (await _checkListPipeRecordAnswerRepository
                              .GetAsync(y => y.NumOA == CheckListVM.NumberOrder && y.TourNumber == CheckListVM.TourNumber
                               && y.DistributionBatch == CheckListVM.DistributionBatch
                               && y.CheckListPipeDictiumId == CheckListVM.checkListId)).LastOrDefault()?.Status;
                if (DBRecord == CheckListType.Cancelled.Value)
                {

                    return Json(new { Result = "Fail", Message = "La Solicitud de cancelación no tiene el status correcto" });
                }
                else
                {
                    modelrecord.Status = CheckListType.Inprogress.Value;
                    modelrecord.NumOA = CheckListVM.NumberOrder;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.TourNumber = CheckListVM.TourNumber;
                    modelrecord.DistributionBatch = CheckListVM.DistributionBatch;
                    modelrecord.TourNumber = CheckListVM.TourNumber;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.CheckListPipeDictiumId = CheckListVM.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);
                    var info = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(CheckListVM.checkListId);
                    info.Status = CheckListType.Inprogress.Value;
                    var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(info);

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en CancelApprove checkListController " + ex);
                return Json(new { Result = "Fail", Message = ex.ToString() });
            }

            return Json(new { Result = "Ok", Message = "La Solicitud de cancelación ha sido rechazada con éxito" });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<JsonResult> CancelNo([FromForm] CheckListVM CheckListVM)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                var DBRecord = await _checkListPipeRecordAnswerRepository
                              .GetAsync(y => y.NumOA == CheckListVM.NumberOrder && y.TourNumber == CheckListVM.TourNumber
                               && y.DistributionBatch == CheckListVM.DistributionBatch
                               && y.CheckListPipeDictiumId == CheckListVM.checkListId);

                string valCan = DBRecord.Where(x => x.Status == CheckListType.InCancellation.Value).LastOrDefault()?.Status;
                string valCance = DBRecord.Where(x => x.Status == CheckListType.Cancelled.Value).LastOrDefault()?.Status;
                if (valCan != null || valCance != null)
                {
                    return Json(new { Result = "Fail", Message = "La Solicitud de cancelación no tiene el status correcto" });
                }
                else
                {

                    modelrecord.Status = CheckListType.Inprogress.Value;
                    modelrecord.NumOA = CheckListVM.NumberOrder;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.ApproveSC = "NO";
                    modelrecord.Reason = CheckListVM.ReasonReject;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.TourNumber = CheckListVM.TourNumber;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.DistributionBatch = CheckListVM.DistributionBatch;
                    modelrecord.CheckListPipeDictiumId = CheckListVM.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);
                    var info = await _checkListPipeDictiumAnswerRepository.GetByIdAsync(CheckListVM.checkListId);
                    info.Status = CheckListType.Inprogress.Value;
                    var UpdDictium = _checkListPipeDictiumAnswerRepository.UpdateAsync(info);
                    return Json(new { Result = "Ok", Message = "La Solicitud de cancelación ha sido enviada con para su evaluación" });
                }
                //trigered mail
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en Cancel checkListController " + ex);
                return Json(new { Result = "Fail", Message = ex.ToString() });
            }

            //return RedirectToAction("Index", "CheckList");
        }

        private async Task<List<SelectListItem>> GetUsersItemsAsync(int id = -1)
        {

            List<SelectListItem> response = new List<SelectListItem>();
            var usuarios = _userManager.Users;
            response.Add(new SelectListItem { Text = "NA", Value = "0" });
            foreach (var item in usuarios.Where(x => x.Rol == SecurityConstants.PERFIL_RESPONSABLE_SANITARIO))
            {
                response.Add(new SelectListItem
                {
                    Text = item.NombreUsuario,
                    Value = item.NombreUsuario

                });

            }

            return response;
        }

        public async Task SaveAll(CheckListVM CheckListVM, int Action)
        {
            List<CheckListPipeAnswer> listPipeAnswers = new List<CheckListPipeAnswer>();
            try
            {
                //option 1 upd
                var checkListAnswers = await _checkListPipeAnswerRepository.GetAsync(x => x.NumOA == CheckListVM.NumberOrder
                                                                                && x.TourNumber == CheckListVM.TourNumber
                                                                                && x.DistributionBatch == CheckListVM.DistributionBatch
                                                                                && x.CheckListPipeDictiumId == CheckListVM.checkListId);

                if (Action == 1)
                {
                    var DbExist1 = checkListAnswers.Where(x => x.Requirement == "Condiciones de caja de válvulas :\r\nLimpia, libre de objetos con tapones colocados en las conexiones de carga / descarga");
                    foreach (var item in DbExist1)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Description1;
                        info.Action = CheckListVM.Action1;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr1Filter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr1Filter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }

                        if (CheckListVM.Check1 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Check2 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Check3 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }
;
                    var DbExist2 = checkListAnswers.Where(x => x.Requirement == "Estado físico de mangueras y conexiones :\r\nEn un buen estado, sin fracturas o desgaste excesivo, sin evidencia de desprendimiento de rebabas.");
                    foreach (var item in DbExist2)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Description2;
                        info.Action = CheckListVM.Action2;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr2Filter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr2Filter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Check4 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Check5 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Check6 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }
                    var DbExist3 = checkListAnswers.Where(x => x.Requirement == "Seguridad de caja y portamangueras :\r\nPuertas de caja de válvulas en buen estado, cerradas y con candados colocados, portamangueras con tapa en buen estado y pasador colocado.");
                    foreach (var item in DbExist3)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Description3;
                        info.Action = CheckListVM.Action3;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr3Filter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr3Filter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Check7 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Check8 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Check9 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExist4 = checkListAnswers.Where(x => x.Requirement == "Condiciones de portamangueras :\r\nEn un buen estado, limpio, libre de objetos, maguera con tapón colocado.");
                    foreach (var item in DbExist4)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Description4;
                        info.Action = CheckListVM.Action4;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr4Filter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr4Filter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Check10 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Check11 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Check12 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExist5 = checkListAnswers.Where(x => x.Requirement == "Condiciones del personal :\r\nCon equipo de seguridad, adecuado y en buen estado, uniforme en buen estado, libre de roturas y rasgaduras significativas y, en condiciones apropiadas de higiene.");
                    foreach (var item in DbExist5)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Description5;
                        info.Action = CheckListVM.Action5;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr5Filter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr5Filter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Check13 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Check14 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Check15 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }



                    var DbExistFP1 = checkListAnswers.Where(x => x.Requirement == "Purgado de línea de transferencia de acuerdo al procedimiento de llenado de pipas aplicable a la localidad.");
                    foreach (var item in DbExistFP1)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp1;
                        info.Action = CheckListVM.Actionfp1;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr1fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr1fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp1 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp2 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp3 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExistFP2 = checkListAnswers.Where(x => x.Requirement == "Sellos de conexiones para el llenado, en buen estado, sin fracturas o evidencia de desgaste excesivo.");
                    foreach (var item in DbExistFP2)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp2;
                        info.Action = CheckListVM.Actionfp2;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr2fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr2fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp4 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp5 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp6 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExistFP3 = checkListAnswers.Where(x => x.Requirement == "Colocación de sellos foliados en válvula de descarga (cuando aplique).");
                    foreach (var item in DbExistFP3)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp3;
                        info.Action = CheckListVM.Actionfp3;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr3fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr3fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp7 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp8 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp9 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }


                    var DbExistFP4 = checkListAnswers.Where(x => x.Requirement == "Colocación de tapones en conexiones de carga/ descarga y maguera de transferencia de producto.");
                    foreach (var item in DbExistFP4)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp4;
                        info.Action = CheckListVM.Actionfp4;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr4fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr4fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp10 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp11 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp12 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExistFP5 = checkListAnswers.Where(x => x.Requirement == "Colocación de candado(s) en caja de válvulas y, pasador en la tapa del portamangueras.");

                    foreach (var item in DbExistFP5)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp5;
                        info.Action = CheckListVM.Actionfp5;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr5fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr5fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp13 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp14 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp15 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }

                    var DbExistFP6 = checkListAnswers.Where(x => x.Requirement == "Entrega de los certificados de calidad al conductor (cuando aplique).");
                    foreach (var item in DbExistFP6)
                    {
                        var info = await _checkListPipeAnswerRepository.GetByIdAsync(item.Id);
                        info.Description = CheckListVM.Descriptionfp6;
                        info.Action = CheckListVM.Actionfp6;
                        string initFilter = string.Empty;
                        if (CheckListVM.SelectedNotifyUsr6fpFilter != null)
                        {
                            foreach (var itemf in CheckListVM.SelectedNotifyUsr6fpFilter)
                            {
                                initFilter = initFilter + itemf + ",";
                            }
                            info.Notify = initFilter.Remove(initFilter.Length - 1, 1);
                        }
                        if (CheckListVM.Checkfp16 == "true")
                            info.Verification = "OK";
                        if (CheckListVM.Checkfp17 == "true")
                            info.Verification = "NO";
                        if (CheckListVM.Checkfp18 == "true")
                            info.Verification = "NA";

                        info.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(info);
                    }


                    var UpdMassive = await _checkListPipeAnswerRepository.UpdateAsync(listPipeAnswers);
                    UpdMassive.IsTransient();


                }
                else
                {

                    var CreatedDate = DateTime.Now;
                    var DbExist1 = checkListAnswers.Where(x => x.Requirement == "Condiciones de caja de válvulas :\r\nLimpia, libre de objetos con tapones colocados en las conexiones de carga / descarga");
                    if (!DbExist1.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Condiciones de caja de válvulas :" + "\r\n" +
                        "Limpia, libre de objetos con tapones colocados en las conexiones de carga / descarga";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "IV";
                        model.Description = CheckListVM.Description1;
                        model.Notify = "";
                        model.Action = CheckListVM.Action1;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExist2 = checkListAnswers.Where(x => x.Requirement == "Estado físico de mangueras y conexiones :\r\nEn un buen estado, sin fracturas o desgaste excesivo, sin evidencia de desprendimiento de rebabas.");
                    if (!DbExist2.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Estado físico de mangueras y conexiones :" + "\r\n" +
                    "En un buen estado, sin fracturas o desgaste excesivo, sin evidencia de desprendimiento de rebabas.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "IV";
                        model.Description = CheckListVM.Description2;
                        model.Notify = "";
                        model.Action = CheckListVM.Action2;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExist3 = checkListAnswers.Where(x => x.Requirement == "Seguridad de caja y portamangueras :" + "\r\n" +
                        "Puertas de caja de válvulas en buen estado, cerradas y con candados colocados, portamangueras con tapa en buen estado y pasador colocado.");
                    if (!DbExist3.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Seguridad de caja y portamangueras :" + "\r\n" +
                        "Puertas de caja de válvulas en buen estado, cerradas y con candados colocados, portamangueras con tapa en buen estado y pasador colocado.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "IV";
                        model.Description = CheckListVM.Description3;
                        model.Notify = "";
                        model.Action = CheckListVM.Action3;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExist4 = checkListAnswers.Where(x => x.Requirement == "Condiciones de portamangueras :" + "\r\n" +
                        "En un buen estado, limpio, libre de objetos, maguera con tapón colocado.");
                    if (!DbExist4.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Condiciones de portamangueras :" + "\r\n" +
                        "En un buen estado, limpio, libre de objetos, maguera con tapón colocado.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "IV";
                        model.Description = CheckListVM.Description4;
                        model.Notify = "";
                        model.Action = CheckListVM.Action4;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExist5 = checkListAnswers.Where(x => x.Requirement == "Condiciones del personal :" + "\r\n" +
                        "Con equipo de seguridad, adecuado y en buen estado, uniforme en buen estado, libre de roturas y rasgaduras significativas y, en condiciones apropiadas de higiene.");
                    if (!DbExist5.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Condiciones del personal :" + "\r\n" +
                        "Con equipo de seguridad, adecuado y en buen estado, uniforme en buen estado, libre de roturas y rasgaduras significativas y, en condiciones apropiadas de higiene.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "IV";
                        model.Description = CheckListVM.Description5;
                        model.Notify = "";
                        model.Action = CheckListVM.Action5;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExistFP1 = checkListAnswers.Where(x => x.Requirement == "Purgado de línea de transferencia de acuerdo al procedimiento de llenado de pipas aplicable a la localidad.");
                    if (!DbExistFP1.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Purgado de línea de transferencia de acuerdo al procedimiento de llenado de pipas aplicable a la localidad.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp1;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp1;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExistFP2 = checkListAnswers.Where(x => x.Requirement == "Sellos de conexiones para el llenado, en buen estado, sin fracturas o evidencia de desgaste excesivo.");
                    if (!DbExistFP2.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Sellos de conexiones para el llenado, en buen estado, sin fracturas o evidencia de desgaste excesivo.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp2;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp2;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }


                    var DbExistFP3 = checkListAnswers.Where(model => model.Requirement == "Colocación de sellos foliados en válvula de descarga (cuando aplique).");
                    if (!DbExistFP3.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Colocación de sellos foliados en válvula de descarga (cuando aplique).";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp3;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp3;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExistFP4 = checkListAnswers.Where(x => x.Requirement == "Colocación de tapones en conexiones de carga/ descarga y maguera de transferencia de producto.");
                    if (!DbExistFP4.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Colocación de tapones en conexiones de carga/ descarga y maguera de transferencia de producto.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp4;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp4;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExistFP5 = checkListAnswers.Where(x => x.Requirement == "Colocación de candado(s) en caja de válvulas y, pasador en la tapa del portamangueras.");
                    if (!DbExistFP5.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Colocación de candado(s) en caja de válvulas y, pasador en la tapa del portamangueras.";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp5;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp5;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var DbExistFP6 = checkListAnswers.Where(x => x.Requirement == "Entrega de los certificados de calidad al conductor (cuando aplique).");
                    if (!DbExistFP6.Any())
                    {
                        var model = new CheckListPipeAnswer();
                        model.Requirement = "Entrega de los certificados de calidad al conductor (cuando aplique).";
                        model.Verification = "Empty";
                        model.NumOA = CheckListVM.NumberOrder;
                        model.TourNumber = CheckListVM.TourNumber;
                        model.DistributionBatch = CheckListVM.DistributionBatch;
                        model.Group = "FP";
                        model.Description = CheckListVM.Descriptionfp6;
                        model.Notify = "";
                        model.Action = CheckListVM.Actionfp6;
                        model.CreatedDate = CreatedDate;
                        model.CheckListPipeDictiumId = CheckListVM.checkListId;
                        model.PipeNumber = CheckListVM.Pipe;
                        listPipeAnswers.Add(model);
                    }

                    var insertMassive = await _checkListPipeAnswerRepository.AddAsync(listPipeAnswers);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al actualizar CheckListPipeAnswer " + CheckListVM.DistributionBatch + " " + CheckListVM.TourNumber + " " + CheckListVM.NumberOrder + ex);
            }
        }

        async Task<CheckListVM> GetCheckListVMs(CheckListVM model)
        {
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            CheckListPipeDictiumAnswer dictiumAnswer = new CheckListPipeDictiumAnswer();
            try
            {
                ///Init add
                var dictiumDB = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder
                       && x.DistributionBatch == model.DistributionBatch && x.TourNumber == model.TourNumber
                       && x.Id == model.checkListId);
                if (dictiumDB.Any())
                {
                    model.checkListId = dictiumDB.FirstOrDefault().Id;
                }

                //STATUS INIT
                var DBRecord = await _checkListPipeRecordAnswerRepository.
                    GetAsync(x => x.Status == CheckListType.Inprogress.Value && x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                             && x.DistributionBatch == model.DistributionBatch && x.CheckListPipeDictiumId == model.checkListId);
                if (DBRecord.Count == 0)
                {
                    modelrecord.Status = CheckListType.Inprogress.Value;
                    modelrecord.NumOA = model.NumberOrder;
                    modelrecord.DistributionBatch = model.DistributionBatch;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.TourNumber = model.TourNumber;
                    modelrecord.CheckListPipeDictiumId = model.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);

                }


                //LOAD CATALOG INIT
                var questionsDefault = await _principalService.GenerateCheckListCat();
                var DbCatAnswer = await _checkListPipeAnswerRepository.
                                    GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                    && x.DistributionBatch == model.DistributionBatch && x.CheckListPipeDictiumId == model.checkListId && x.Group == "IV");
                if (DbCatAnswer.Count == 5)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "IV"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsCatalog = UserInfo;
                }
                else
                {
                    model.checkListsCatalog = questionsDefault;
                }

                var DbCatFPAnswer = await _checkListPipeAnswerRepository.
                                  GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                  && x.DistributionBatch == model.DistributionBatch && x.CheckListPipeDictiumId == model.checkListId && x.Group == "FP");
                if (DbCatFPAnswer.Count == 6)
                {
                    List<CheckListVM> UserInfo = new List<CheckListVM>();
                    foreach (var item in DbCatFPAnswer)
                    {
                        foreach (var itemx in questionsDefault.Where(x => x.Group == "FP"))
                        {
                            if (itemx.Requirement.Contains(item.Requirement))
                            {
                                UserInfo.Add(new CheckListVM { Requirement = item.Requirement, Verification = item.Verification, Description = item.Description, Action = item.Action, Group = item.Group });
                            }
                        }
                    }
                    model.checkListsfpCatalog = UserInfo;
                }
                else
                {
                    model.checkListsfpCatalog = questionsDefault.Where(x => x.Group == "FP").ToList();
                }
                ///BLOQ ELEMENT
                if (model.checkListsCatalog.Where(x => x.Verification != "OK").Count() > 0 && model.checkListsfpCatalog.Where(x => x.Verification != "OK").Count() > 0)
                {
                    model.Style = "pointer-events: none;";
                }
                model.ListUserNotify = await GetUsersItemsAsync();
                model.checkListsRecord = (await _checkListPipeRecordAnswerRepository
                                        .GetAsync(x => x.NumOA == model.NumberOrder && x.TourNumber == model.TourNumber
                                        && x.DistributionBatch == model.DistributionBatch && x.CheckListPipeDictiumId == model.checkListId)).ToList();
                model.LastStatusRecord = model.checkListsRecord.Select(record => record.Status).LastOrDefault();
                model.FlagApproveSC =
                    (model.checkListsRecord.Where(x => x.ApproveSC == "NO").ToList().Count > 0) ? null :
                    (model.checkListsRecord.Where(x => x.ApproveSC == "SI").ToList().Count > 0) ?
                    model.checkListsRecord.Where(x => x.ApproveSC == "SI").LastOrDefault().Reason : "NULLA";
                model.LastDateRecord = model.checkListsRecord.Select(record => record.Date).LastOrDefault();
                model.checkListPipeCommentsAnswers = (await _checkListPipeCommentsAnswerReepository
                                                    .GetAsync(x => x.NumOA == model.NumberOrder
                                                            && x.TourNumber == model.TourNumber
                                                            && x.DistributionBatch == model.DistributionBatch
                                                            && x.CheckListPipeDictiumId == model.checkListId))
                                                    .ToList();
                model.CommentIv = (model.checkListPipeCommentsAnswers.Where(x => x.Group == "Inspección visual de Pipas al recibo").ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group == "Inspección visual de Pipas al recibo").LastOrDefault().Comment : null;
                model.CommentFP = (model.checkListPipeCommentsAnswers.Where(x => x.Group == "Checklist llenado de pipa y verificación de pipas").ToList().Count > 0) ? model.checkListPipeCommentsAnswers.Where(x => x.Group == "Checklist llenado de pipa y verificación de pipas").LastOrDefault().Comment : null;
                model.checkListPipeDictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                var dictium = await _checkListPipeDictiumAnswerRepository.GetAsync(x => x.NumOA == model.NumberOrder &&
                                x.TourNumber == model.TourNumber && x.DistributionBatch == model.DistributionBatch
                                && x.Id == model.checkListId);
                if (dictium.Count > 0)
                {
                    model.checkListPipeDictiumAnswers = dictium.ToList();
                    model.DictumComment = dictium.Select(x => x.Comment).FirstOrDefault();
                    model.DictumUser = model.checkListsRecord.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value).Any() ?
                                       model.checkListsRecord.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value).Select(record => record.CreatedBy).LastOrDefault() : null;

                    model.DictiumDate = model.checkListsRecord.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value).Any() ?
                                       model.checkListsRecord.Where(x => x.Status == CheckListType.CloseOk.Value || x.Status == CheckListType.CloseNo.Value).Select(record => record.Date).LastOrDefault() : null;
                    if (dictium.FirstOrDefault().Verification != null)
                        model.Style = "pointer-events: none;";

                }
                else
                {
                    List<CheckListPipeDictiumAnswer> ListdictiumAnswers = new List<CheckListPipeDictiumAnswer>();
                    ListdictiumAnswers.Add(new CheckListPipeDictiumAnswer { Verification = "NA", Date = DateTime.Now, Comment = "" });
                    model.checkListPipeDictiumAnswers = ListdictiumAnswers;
                }


            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error al actualizar CheckListPipeAnswer " + model.DistributionBatch + " " + model.TourNumber + " " + model.NumberOrder + ex);
            }

            await SaveAll(model, 2);

            return model;
        }
        async Task<IActionResult> AddStatus(CheckListVM checkListVM)
        {
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ///status
            CheckListPipeRecordAnswer modelrecord = new CheckListPipeRecordAnswer();
            try
            {
                if (checkListVM.CheckDictium1 == "true")
                    modelrecord.Status = CheckListType.CloseOk.Value;
                if (checkListVM.CheckDictium2 == "true")
                    modelrecord.Status = CheckListType.CloseNo.Value;
                if (modelrecord.Status != null)
                {
                    modelrecord.NumOA = checkListVM.NumberOrder;
                    modelrecord.Date = DateTime.Now;
                    modelrecord.ApproveSC = "";
                    modelrecord.Reason = "";
                    modelrecord.CreatedBy = userInfo.NombreUsuario;
                    modelrecord.DistributionBatch = checkListVM.DistributionBatch;
                    modelrecord.TourNumber = checkListVM.TourNumber;
                    modelrecord.CheckListPipeDictiumId = checkListVM.checkListId;
                    var recordSave = await _checkListPipeRecordAnswerRepository.AddAsync(modelrecord);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en AddStatus checkListController " + ex);
                return Json(new { Result = "Fail", Message = ex.ToString() });
            }

            return Ok();

        }

        /*  LSZ Exporta el Checklist A PDF 
            Se utiliza la misma funcionalidad de Index
            TODO Refactorizar: Crear Metodo SobreCargado de Index para traer el modelo desde un solo lado para ambas funcionalidades.
         */

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Authorize(SecurityConstants.EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS)]
        public async Task<IActionResult> ExportCheckListToPDF(int idOA, string tourNumber, string distributionBatch, int checkListId)
        {
            MemoryStream ms = new MemoryStream();
            CheckListVM model = new CheckListVM();
            var userInfo = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            try
            {
                model.NumberOrder = idOA;
                model.TourNumber = tourNumber;
                model.DistributionBatch = distributionBatch;
                model.checkListId = checkListId;

                if (idOA == 0 || string.IsNullOrEmpty(tourNumber) || string.IsNullOrEmpty(distributionBatch))
                {
                    // Should return error since there is no way to create or get a checklist
                    return View(model);
                }

                var conditioningOrderViewModel = await _conditioningOrderRepository.GetByIdAsync(idOA);

                var labelsDb = await _principalService.CheckListRecordLabels(idOA, checkListId);

                model.Localizate = labelsDb.Localizate;

                model.Product = labelsDb.Product;

                model.Pipe = labelsDb.Pipe;
                ///_conditioningOrderService.GetByIdAsync(idOA);

                if (conditioningOrderViewModel == null)
                {
                    // Should return error since there is no way to create or get a checklist
                    //    return View(model);
                    throw new ArgumentNullException("No se encontro conditioningOrderViewModel = null");

                }

                model = await GetCheckListVMs(model);

                model.checkListsCatalog.ForEach(item => {

                    switch (item.Verification)
                    {
                        case "OK":
                            item.Verification = "Cumple";
                            break;
                        case "NO":
                            item.Verification = "No Cumple";
                            break;
                        default:
                            item.Verification = "NA";
                            break;
                    }
                
                });

                model.checkListsfpCatalog.ForEach(item => {

                    switch (item.Verification)
                    {
                        case "OK":
                            item.Verification = "Cumple";
                            break;
                        case "NO":
                            item.Verification = "No Cumple";
                            break;
                        default:
                            item.Verification = "NA";
                            break;
                    }
                });

                model.checkListPipeDictiumAnswers.ForEach(item => {

                    switch (item.Verification)
                    {
                        case "OK":
                            item.Verification = "Si";
                            break;
                        case "NO":
                            item.Verification = "No";
                            break;
                        default:
                            item.Verification = "";
                            break;
                    }
                });


                // LSZ Creacion del reporte ---->

                RptChkList rptCheckList = new RptChkList();
                List<CheckListVM> lstCheckListVM = new List<CheckListVM>();

                lstCheckListVM.Add(model);
                rptCheckList.DataSource = lstCheckListVM;
                rptCheckList.CreateDocument();
                rptCheckList.ExportToPdf(ms);
            }

            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en ExportCheckListToPDF " + ex);
                return BadRequest();
            }


            return File(ms.ToArray(), "application/pdf", string.Format("{0} {1}.{2}", "CL Verificación de pipas", model.DistributionBatch, "pdf").Replace(" - ", "-"));
        }

    }
}
