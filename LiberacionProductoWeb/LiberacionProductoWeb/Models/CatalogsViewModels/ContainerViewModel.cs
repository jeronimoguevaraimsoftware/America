using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CatalogsViewModels
{
    public class ContainerViewModel: SechToolDistributionBatchVM
    {

        public Container Container { get; set; }
        public List<Container> ContainerList { get; set; }

        //helper controls
        public List<String> SelectedPlantsFilter { get; set; }
        public List<String> SelectedProductsFilter { get; set; }
        public List<String> SelectedTanksFilter { get; set; }


        //catalogs
        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> ProductsFilter { get; set; }
        public List<SelectListItem> TanksFilter { get; set; }


        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

    }

    public class Container
    {
        public String Id { get; set; }
        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String Presentation { get; set; }
        public String PrimaryContainer { get; set; }
        public Boolean Status { get; set; }
    }

    public class DtoContainer
    {
        public String id { get; set; }
        public String planta { get; set; }
        public String producto { get; set; }
        public String tanque { get; set; }
        public String presentation { get; set; }
        public String status { get; set; }
        public String primary { get; set; }
    }
}
