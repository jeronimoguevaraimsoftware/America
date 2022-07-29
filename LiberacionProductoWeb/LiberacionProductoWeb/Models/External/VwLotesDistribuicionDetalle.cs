using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesDistribuicionDetalle
    {
        public int ID_PLANTA { get; set; }
        public string PRODUCT_ID { get; set; }
        public string ID_LOTEPIPA { get; set; }
        public string TOURNUMBER { get; set; }
        public Int16 TRIPNUMBER { get; set; }
        public string DESC_PARAMETRO { get; set; }
        public string DESC_UM { get; set; }
        public Decimal ANALISIS_INI { get; set; }
        public Decimal ANALISIS_FIN { get; set; }
        public string DESC_ANALIZADOR { get; set; }
        public string ANALISIS_INICIAL { get; set; }
        public string ANALISIS_FINAL { get; set; }
    }
}
