using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesDistribuicion
    {
        public int ID_PLANTA { get; set; }
        public string PRODUCT_ID { get; set; }
        public string ID_LOTEPIPA { get; set; }
        public string DESC_PIPA { get; set; }
        public string PESO_INI { get; set; }
        public string PESO_FIN { get; set; }
        public string COMENTARIOS { get; set; }
        public DateTime FEC_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string ID_LOTE { get; set; }
        public string STATUS_CANCELADO { get; set; }
        public string COMENTARIO_CANCELA { get; set; }
        public string DESC_CARROTQ { get; set; }
        public string PTA_TRASVASE { get; set; }
        public string LOTE_ORIGEN { get; set; }
        public string COP { get; set; }
        public DateTime? FECHA_INI { get; set; }
        public DateTime? FECHA_FIN { get; set; }

    }
}
