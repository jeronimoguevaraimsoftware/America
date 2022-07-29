using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LiberacionProductoWeb.Models.CatalogsViewModels
{
    public class GeneralViewModel: SechToolDistributionBatchVM
    {
        public General General { get; set; }
        public List<General> GeneralList { get; set; }

        //helper controls
        public List<String> SelectedPlantsFilter { get; set; }
        public List<String> SelectedProductsFilter { get; set; }
        public List<String> SelectedTanksFilter { get; set; }


        //catalogs
        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> ProductsFilter { get; set; }
        public List<SelectListItem> TanksFilter { get; set; }
        public List<SelectListItem> VariableClasificationTypes { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class General
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


    public class DtoGeneral
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

    public class VariableClasificationType
    {
        private VariableClasificationType(string value) { Value = value; }

        public string Value { get; private set; }

        public static VariableClasificationType ControlVariable { get { return new VariableClasificationType("Variable de control de proceso"); } }
        public static VariableClasificationType CriticalParameter { get { return new VariableClasificationType("Parámetro crítico de proceso"); } }
        public static VariableClasificationType CriticalQualityAttribute { get { return new VariableClasificationType("Atributo crítico de calidad"); } }
    }
}
