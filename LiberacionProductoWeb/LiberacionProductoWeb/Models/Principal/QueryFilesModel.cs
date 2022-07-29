using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.Principal
{
    public class QueryFilesModel
    {
        public int Id { get; set; }
        public string Plant { get; set; }
        public string PlantId { get; set; }
        public string State { get; set; }
        public string NoLotProd { get; set; }
        public string Product { get; set; }
        public string ProductId { get; set; }
        public string Tank { get; set; }
        public string TankId { get; set; }
        public DateTime? StartDateExp { get; set; }
        public DateTime? StartDateProd { get; set; }
        public string StartTimeProd { get; set; }
        public DateTime? EndDateProd { get; set; }
        public string EndTimeProd { get; set; }
        public string SizeLot { get; set; }
        public string Dictum { get; set; }
        public String Comments { get; set; }
        public string Presentation { get; set; }
        public string Purity { get; set; }
        public string HealthRegister { get; set; }
        public string PharmaceuticalForm { get; set; }

        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string NumeroLoteProduccion { get; set; }
        public string Dictamen { get; set; }

        public List<String> VariablesControlProceso { get; set; }
        public List<String> ParametrosCriticosProceso { get; set; }
        public List<String> AtributosCriticosCalidad { get; set; }
        public List<String> AseguramientoCalidadFabricacion { get; set; }
        public string Observaciones { get; set; }
        public string Certified { get; set; }
    }
}
