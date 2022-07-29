using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.Principal;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.ConfigViewModels
{
    public class ConfiguracionUsuarioVM: SechToolDistributionBatchVM
    {
        public Plant Plant { get; set; }
        public IEnumerable<SelectListItem> ListPlants { get; set; }
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ListProducts { get; set; }
        public Tank Tank { get; set; }
        public IEnumerable<SelectListItem> ListTanks { get; set; }
        public Activities Activities { get; set; }
        public IEnumerable<SelectListItem> ListActivities { get; set; }
        public State State { get; set; }
        public IEnumerable<SelectListItem> ListStates { get; set; }
        public ReportAuditTrailViewModel ReportAuditTrail { get; set; }
        public List<ReportAuditTrailViewModel> ListReportAudit { get; set; }
        public Action Action { get; set; }
        public IEnumerable<SelectListItem> ListActions { get; set; }

        public IEnumerable<SelectListItem> ListUsers { get; set; }

        //helper controls

        public List<QueryFilesModel> ListqueryFilesModels { get; set; }
        public List<QueryGeneralModel> ListqueryGeneralModels { get; set; }
        public List<General> GeneralList { get; set; }
        public List<PenddingTaskModel> ListpenddingTasks { get; set; }
        public List<String> SelectedPlantsFilter { get; set; }
        public List<String> SelectedProductsFilter { get; set; }
        public List<String> SelectedTanksFilter { get; set; }
        public List<String> SelectedStatesFilter { get; set; }
        public List<String> SelectedActivitiesFilter { get; set; }
        public List<String> SelectedActionFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<String> SelectedUserFilter { get; set; }

        //mensajes en vista
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

    }

    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class Tank
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Activities
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class State
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class ReportAuditTrailViewModel
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Funcionality { get; set; }
        public string PreviousVal { get; set; }
        public string NewVal { get; set; }
        public string Action { get; set; }
        public string Detail { get; set; }

        public string Plant { get; set; }
        public string Product { get; set; }
        public string DistribuitionBatch { get; set; }
        public int ProductionOrderId { get; set; }
        public string Controller { get; set; }
    }

    public class Action
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
