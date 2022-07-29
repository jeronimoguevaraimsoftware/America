using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LiberacionProductoWeb.Models.RAPModels
{
    public class ComplementoViewModel: SechToolDistributionBatchVM
    {

        public Plant Plant { get; set; }
        public IEnumerable<SelectListItem> ListPlants { get; set; }
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> ListProducts { get; set; }
        public Tank Tank { get; set; }
        public IEnumerable<SelectListItem> ListTanks { get; set; }

        public IEnumerable<SelectListItem> ListPresentation { get; set; }
        public Presentation presentation { get; set; }
        public IEnumerable<SelectListItem> ListPurity { get; set; }
        public Purity purity { get; set; }
        public IEnumerable<SelectListItem> ListHealthRegister { get; set; }
        public HealthRegister healthRegister { get; set; }
        public IEnumerable<SelectListItem> ListPharmaceuticalForm { get; set; }
        public HealthRegister pharmaceuticalForm { get; set; }


        //helper controls
        public String SelectedPlantFilter { get; set; }
        public String SelectedProductFilter { get; set; }
        public String SelectedTankFilter { get; set; }
        public List<String> SelectedPresentationFilter { get; set; }
        public List<String> SelectedPurityFilter { get; set; }
        public List<String> SelectedHealthRegisterFilter { get; set; }
        public List<String> SelectedPharmaceuticalFormFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //catalogs
        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> ProductsFilter { get; set; }
        public List<SelectListItem> TanksFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }


        public RapComplemento rapComplemento { get; set; }
        public List<RapComplemento> ListTanqueComplemento { get; set; }
        public List<RapComplemento> ListPipaComplemento { get; set; }

        public DisposicionPNC disposicionPNC { get; set; }
        public IEnumerable<SelectListItem> ListDisposicionPNC { get; set; }



    }
    public class DisposicionPNC
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class RapComplemento
    {
        public int Id { get; set; }
        public string Plant { get; set; }
        public string Product { get; set; }
        public string Tank { get; set; }
        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public string TankId { get; set; }
        public DateTime StartDateExp { get; set; }
        public DateTime StartDateProd { get; set; }
        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
        public string NumeroLoteProduccion { get; set; }
        public string FolioTrabajoNoConforme { get; set; }
        public string FolioPNC { get; set; }
        public string DisposicionPNC { get; set; }
        public string DisposicionPNCText { get; set; }
        public string FolioControlCambios { get; set; }
        public string Observaciones { get; set; }

        public string NumeroLoteDistribucion { get; set; }
        public string FolioEventoAdverso { get; set; }
        public string NumeroDeDevolucion { get; set; }
        public string FolioRetiroProductos { get; set; }
        public int ProductionOrderId { get; set; }
        public int ConditioningOrderId { get; set; }        
    }

}
