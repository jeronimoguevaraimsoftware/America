using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesProduccion
    {
        public int ID_PLANTA { get; set; }
        public string PRODUCT_ID { get; set; }
        public string ID_LOTE { get; set; }
        public string TANQUECARGA { get; set; }
    }
}
