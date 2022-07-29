using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesDistribuicionCliente
    {
        public string ID_LOTEPIPA { get; set; }
        public string TOURNUMBER { get; set; }
        public Byte TRIPNUMBER { get; set; }
        public string NO_ORDEN_ENTREGA { get; set; }
        public string TANQUE_CLIENTE { get; set; }
        public string CLIENTE { get; set; }
        public DateTime FEC_ALTA { get; set; }
        public string CALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public int ID_CERTIFICADO { get; set; }
        public string DESC_CERTIFICADO { get; set; }
        public int ID_GRADO { get; set; }
        public string DESC_GRADO { get; set; }
        public string NVO_LINDENO { get; set; }
    }
}
