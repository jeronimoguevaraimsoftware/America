using System;

namespace LiberacionProductoWeb.Models.Principal
{
    public class PenddingTaskModel
    {
        public int Id { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public string NumeroLoteProduccion { get; set; }
        public string Planta { get; set; }
        public string Producto { get; set; }
        public string Tanque { get; set; }
        public string ActividadACompletar { get; set; }
        public string Estado { get; set; }
        public string MotivoCancelacion { get; set; }
        public string CreadoPor { get; set; }
        public string Responsable { get; set; }
        public int ProductionOrderId { get; set; }
    }
}
