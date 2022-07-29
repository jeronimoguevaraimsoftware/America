using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwLotesProduccionDetalle
    {
        public int ID_PLANTA { get; set; }
        public string PRODUCT_ID { get; set; }
        public string ID_LOTE { get; set; }
        public string TANQUE { get; set; }
        public string NIVEL_INICIAL { get; set; }
        public string NIVEL_FINAL { get; set; }
        public string COMENTARIOS { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string STATUS { get; set; }
        public string CREADO_POR { get; set; }
        public string PARAMETRO { get; set; }
        public Decimal LIMITE_INFERIOR { get; set; }
        public Decimal LIMITE_SUPERIOR { get; set; }
        public Decimal VALOR_ANALISIS { get; set; }
        public string UNIDAD_DE_MEDIDA { get; set; }
        public string ANALIZADOR { get; set; }
    }
}
