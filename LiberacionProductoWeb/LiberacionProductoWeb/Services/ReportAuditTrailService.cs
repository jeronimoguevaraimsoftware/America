using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class ReportAuditTrailService : IReportAuditTrailService
    {
        private readonly IReportAuditTrailRepository _reportAuditTrailRepository;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly IPrincipalService _principalService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        public bool WSMexeFuncionalidad;
        public ReportAuditTrailService(IReportAuditTrailRepository reportAuditTrailRepository, IStringLocalizer<Resource> resource,
            IConfiguration config, IPrincipalService principalService, UserManager<ApplicationUser> userManager)
        {
            _resource = resource;
            _reportAuditTrailRepository = reportAuditTrailRepository;
            _config = config;
            _principalService = principalService;
            bool.TryParse(_config["FlagWSMexe:ServiceApiKey"], out WSMexeFuncionalidad);
            _userManager = userManager;
        }
        public async Task<List<ReportAuditTrailViewModel>> GetReportAuditTrail()
        {

            List<ReportAuditTrailViewModel> reportAuditTrails = new List<ReportAuditTrailViewModel>();

            var plantsDb = await _principalService.GetPlants();
            var productDb = await _principalService.GetProducts();
            var userDb = _userManager.Users;
            var ReportDB = from Report in await _reportAuditTrailRepository.GetAllAsync()
                           select new ReportAuditTrailViewModel
                           {
                               Id = Report.Id,
                               Method = Report.Method,
                               Date = Report.Date,
                               User = userDb.Where(x => x.Id == Report.User).Any() ? userDb.Where(x => x.Id == Report.User).FirstOrDefault().NombreUsuario : _resource.GetString("NoInformation"),
                               Funcionality = Report.Funcionality,
                               PreviousVal = Report.Detail.Contains("Fecha") ? ConvertDateFormat(Report.PreviousValue).Result :
                                              Report.Detail.Contains("Planta") ? GetPlantName(Report.PreviousValue, Report.User, plantsDb).Result : Report.PreviousValue,
                               NewVal = Report.Detail.Contains("Fecha") ? ConvertDateFormat(Report.NewValue).Result :
                                              Report.Detail.Contains("Planta") ? GetPlantName(Report.NewValue, Report.User, plantsDb).Result : Report.NewValue,
                               Action = Report.Action,
                               Plant = Report.Plant != "NA" ? plantsDb.Where(x => x.Value == Report.Plant).FirstOrDefault().Text
                                       : _resource.GetString("NoInformation"),
                               Product = Report.Product != "NA" ? productDb.Where(x => x.Value == Report.Product).FirstOrDefault().Text : _resource.GetString("NoInformation"),
                               Detail = Report.Detail,
                               DistribuitionBatch = Report.DistribuitionBatch,
                               Controller = Report.Controller,
                               ProductionOrderId = Report.ProductionOrderId
                           };
            reportAuditTrails = ReportDB.ToList();

            return reportAuditTrails;
        }

        public void Save(IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail> reportAuditTrails)
        {
            IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail> reportAuditTrail;
            reportAuditTrail = reportAuditTrails;
            reportAuditTrail.Append(new Models.DataBaseModels.Base.ReportAuditTrail
            {
                Controller = "",
                Method = "",
                User = "dsds",
                Funcionality = "Funcio",
                PreviousValue = "cdd",
                NewValue = "",
                Date = DateTime.Now,
                Plant = "Planta 1",
                Product = "Oxygen",
                Action = "catalogs",
                Detail = "Detalle"
            });
            reportAuditTrail.Append(new Models.DataBaseModels.Base.ReportAuditTrail
            {
                Controller = "",
                Method = "",
                User = "dsds",
                Funcionality = "Funcional",
                PreviousValue = "cdd",
                NewValue = "",
                Date = DateTime.Now,
                Plant = "Planta 1",
                Product = "Oxygen",
                Action = "catalogs",
                Detail = "Detalle"
            });
            _reportAuditTrailRepository.AddAsync(reportAuditTrail);

        }
        public async Task<String> ConvertDateFormat(string date)
        {
            DateTime dateConvert = new DateTime();
            string dateString = string.Empty;
            if (!string.IsNullOrEmpty(date))
            {
                dateConvert = DateTime.Parse(date);
                dateString = dateConvert.ToString("yyyy-MM-dd HH:mm");
            }
            return dateString;
        }

        public async Task<String> GetPlantName(string PlantsId, string UserId, List<SelectListItem> selectListItems)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            StringBuilder PlantsName = new StringBuilder();
            var plantsSplit = PlantsId?.Split(",").ToList();
            if (!string.IsNullOrEmpty(PlantsId))
            {

                var info = (from a in selectListItems
                            join b in plantsSplit
                            on a.Value equals b.Trim()
                            select new SelectListItem
                            {
                                Text = a.Text + " ",
                                Value = a.Value
                            }).ToList();

                listItems = info;
            }
            foreach (var item in listItems)
            {
                PlantsName.Append(item.Text + ",");

            }
            return PlantsName.ToString().TrimEnd(',');
        }
    }
}
