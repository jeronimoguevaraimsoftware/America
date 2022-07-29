using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwDetalleXLoteTanque
    {

        public string ID_LOTE { get; set; }
        public int ID_PLANTA { get; set; }
        public string DESC_PARAMETRO { get; set; }
        public UInt64 VALOR_LIMITE_INF { get; set; }
        public UInt64 VALOR_LIMITE_SUP { get; set; }
        public UInt64 VALOR_ANALISIS { get; set; }
        public string DESC_UM { get; set; }
        public string DESC_ANALIZADOR { get; set; }
        public UInt64 LIMITE_INFERIOR { get; set; }
        public string LEYENDA_REPORTE { get; set; }

    }
}
