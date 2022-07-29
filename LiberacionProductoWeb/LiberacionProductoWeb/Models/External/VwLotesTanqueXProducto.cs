using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesTanqueXProducto
    {
        public int ID_PLANTA { get; set; }
        public string PRODUCT_ID { get; set; }
        public DateTime FEC_ALTA { get; set; }
        public string DESC_TANQUE { get; set; }
        public string ID_LOTE { get; set; }
        public string NIVEL_INICIAL { get; set; }
        public string NIVEL_FINAL { get; set; }
        public string COMENTARIOS { get; set; }
        public string STATUS_LOTE { get; set; }
        public string STATUS_CANCELADO { get; set; }
        public string COMENTARIO_CANCELA { get; set; }
    }
}
