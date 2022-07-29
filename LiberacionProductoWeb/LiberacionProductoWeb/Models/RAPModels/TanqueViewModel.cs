using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LiberacionProductoWeb.Models.RAPModels
{
    public class TanqueViewModel : SechToolDistributionBatchVM
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

        public Tanque Tanque { get; set; }
        public List<Tanque> TanqueList { get; set; }

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

        public TanqueModel raptanques { get; set; }
        public List<TanqueModel> ListRAPTanques { get; set; }

        public TanqueViewModel()
        {
            estudioEstabilidad = new EstudioEstabilidad();
            ListVariables = new List<VariablesTanque>();
            ListParametros = new List<VariablesTanque>();
            ListAtributos = new List<VariablesTanque>();
        }

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

        //Variables: mayor - menor  - prom				
        public List<VariablesTanque> ListVariables { get; set; }
        //Parametros: mayor - menor  - prom				
        public List<VariablesTanque> ListParametros { get; set; }
        //Atributos: mayor - menor  - prom				
        public List<VariablesTanque> ListAtributos { get; set; }
    }

    public class VariablesTanque
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
        public int Count { get; set; }
        public double Sum { get; set; }
        public List<double> Values { get; set; }
        public List<string> BatchesNotInCompliance { get; set; }
    }

    public class Tanque
    {
        public String Id { get; set; }
        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String ConversionFactor { get; set; }
        public String Area { get; set; }
        public String ProcessStep { get; set; }
        public String Variable { get; set; }
        public String VariableSpecification { get; set; }
        public String LowerLimit { get; set; }
        public String UpperLimit { get; set; }
        public String VariableClasification { get; set; }
        public String CodeTool { get; set; }
        public String DescriptionTool { get; set; }
        public String WeighingMachine { get; set; }
        public String BayArea { get; set; }
        public String FillingPump { get; set; }
        public String FillingHose { get; set; }
        public String Estatus { get; set; }
    }

    public class TanqueModel
    {

        public TanqueModel()
        {
            ControlVariables = new List<ControlVariableViewModel>();
            CriticalParameters = new List<CriticalParameterViewModel>();
            CriticalQualityAttributes = new List<CriticalQualityAttributeViewModel>();
            BatchDetails = new BatchDetailsViewModel();
        }

        public int Id { get; set; }
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

        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string NumeroLoteProduccion { get; set; }
        public string Dictamen { get; set; }

        public List<ControlVariableViewModel> ControlVariables { get; set; }
        public List<CriticalParameterViewModel> CriticalParameters { get; set; }
        public List<CriticalQualityAttributeViewModel> CriticalQualityAttributes { get; set; }
        public BatchDetailsViewModel BatchDetails { get; set; }
        public PipelineClearanceViewModel PipelineClearance { get; set; }


        public string FolioDesviacion { get; set; }
        public string FolioTrabajo { get; set; }
        public string FolioPNC { get; set; }
        public string DisposicionPNC { get; set; }
        public string DisposicionPNCText { get; set; }
        public string FolioControl { get; set; }
        public string Observaciones { get; set; }
    }


    public class DtoRap
    {
        public String id { get; set; }
        public String planta { get; set; }
        public String producto { get; set; }
        public String tanque { get; set; }
        public String factor { get; set; }
        public String area { get; set; }
        public String etapa { get; set; }
        public String variable { get; set; }
        public String espvariable { get; set; }
        public String liminf { get; set; }
        public String limsup { get; set; }
        public String clasificacion { get; set; }
        public String codigo { get; set; }
        public String desc { get; set; }
        public String bascula { get; set; }
        public String bahia { get; set; }
        public String bomba { get; set; }
        public String manguera { get; set; }
        public String estado { get; set; }
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

    public class Presentation
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Purity
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class HealthRegister
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class PharmaceuticalForm
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
    public class EstudioEstabilidad
    {
        public string AnioEstudio { get; set; }
        public string Estado { get; set; }
        public string Codigo { get; set; }
        public string Observaciones { get; set; }

    }

    public class VariablesControlProceso
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

    public class ParametrosCriticosProceso
    {

        public string Etapa { get; set; }
        public string Parametro { get; set; }
        public string Especificacion { get; set; }
        public string LSC { get; set; }
        public string LIC { get; set; }
        public string ValMax { get; set; }
        public string ValProm { get; set; }
        public string ValMin { get; set; }
        public string DesvEstandar { get; set; }

    }

    public class AtributosCriticosCalidad
    {
        public string Etapa { get; set; }
        public string Parametro { get; set; }
        public string Especificacion { get; set; }
        public string LSC { get; set; }
        public string LIC { get; set; }
        public string ValMax { get; set; }
        public string ValProm { get; set; }
        public string ValMin { get; set; }
        public string DesvEstandar { get; set; }

    }
}
