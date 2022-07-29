using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LiberacionProductoWeb.Models.RAPModels
{
    public class PipaViewModel : SechToolDistributionBatchVM
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
        public String SelectedPresentationFilter { get; set; }
        public String SelectedPurityFilter { get; set; }
        public String SelectedHealthRegisterFilter { get; set; }
        public String SelectedPharmaceuticalFormFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public PipaModel rappipas { get; set; }
        public List<PipaModel> ListRAPPipas { get; set; }


        //catalogs
        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> ProductsFilter { get; set; }
        public List<SelectListItem> TanksFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

        //Total de lotes revisados
        public string Aprobados { get; set; }
        public string ConDesviacion { get; set; }
        public string Rechazados { get; set; }
        public string Total { get; set; }

        //Estudio de estabilidad del producto	
        public EstudioEstabilidad estudioEstabilidad { get; set; }

        //Parámetros críticos de proceso				
        public List<AnalisisPipa> ListAnalisisInitial { get; set; }
        public List<AnalisisPipa> ListAnalisisFinal { get; set; }

    }
    public class AnalisisPipa
    {
        public string Etapa { get; set; }
        public string Parametro { get; set; }
        public string Especificacion { get; set; }
        public string LSC { get; set; }
        public string LIC { get; set; }
        public string ValorMax { get; set; }
        public string ValorProm { get; set; }
        public string ValorMin { get; set; }
        public string DesvEstandar { get; set; }
    }

    public class PipaModel
    {
        public int Id { get; set; }
        public int ProductionOrderId { get; set; }
        public string Plant { get; set; }
        public string Product { get; set; }
        public string Tank { get; set; }
        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public string TankId { get; set; }
        public DateTime StartDateExp { get; set; }
        public DateTime StartDateProd { get; set; }
        public string Presentation { get; set; }
        public string Purity { get; set; }
        public string HealthRegister { get; set; }
        public string PharmaceuticalForm { get; set; }

        public DateTime? Fecha { get; set; }
        public string Hora { get; set; }
        public string NumeroLoteProduccion { get; set; }
        public string NumeroLoteDistribucion { get; set; }
        public DateTime? FechaCaducidad { get; set; }

        public List<string> InitialAnalysisParamsNames { get; set; }
        public List<string> FinalAnalysisParamsNames { get; set; }
        public List<Analisis> aInicial { get; set; }
        public List<Analisis> aFinal { get; set; }

        public AseguramientoAcon aseguramiento { get; set; }

        public string Observaciones { get; set; }
        public string Aprobado { get; set; }
    }

    public class AseguramientoAcon
    {
        public string FolioInformeDesviacion { get; set; }
        public string FolioTrabajoNoCOnforme { get; set; }
        public string FolioPNC { get; set; }
        public string DisposicionPNC { get; set; }
        public string DisposicionPNCText { get; set; }
        public string FolioEventoAdverso { get; set; }
        public string NumeroDeDevolucion { get; set; }
        public string FolioRetiroProductos { get; set; }
        public string FolioDeControlDeCambios { get; set; }
    }

    public class Analisis
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string Value { get; set; }
        public string DistributionBatch { get; set; }
        public string PipeNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        public string Specification { get; set; }
    }

}
