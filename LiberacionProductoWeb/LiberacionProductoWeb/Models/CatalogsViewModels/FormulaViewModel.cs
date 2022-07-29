using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CatalogsViewModels
{
    public class FormulaViewModel: SechToolDistributionBatchVM
    {
        public Formula Formula { get; set; }
        public List<Formula> FormulaList { get; set; }

        //helper controls
        public List<String> SelectedPlantsFilter { get; set; }
        public List<String> SelectedProductsFilter { get; set; }
        public List<String> SelectedTanksFilter { get; set; }


        //catalogs
        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> ProductsFilter { get; set; }
        public List<SelectListItem> TanksFilter { get; set; }

        //TODO only for test porpouse
        public String PurityReference = "99.5% Min.";

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Formula
    {
        public String Id { get; set; }
        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String FormulaName { get; set; }
        public String Status { get; set; }
        public String Purity { get; set; }
        public String Presentation { get; set; }
        public String RegisterCode { get; set; }

    }

    public class DtoFormula
    {
        public String id { get; set; }
        public String planta { get; set; }
        public String producto { get; set; }
        public String tanque { get; set; }
        public String formula { get; set; }
        public String status { get; set; }
        public String purity { get; set; }
        public String presentation { get; set; }
        public String register { get; set; }
    }

}
