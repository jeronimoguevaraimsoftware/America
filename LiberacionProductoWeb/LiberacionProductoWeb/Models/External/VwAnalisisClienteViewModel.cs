using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwAnalisisClienteViewModel
    {
        public string RAZON_SOC { get; set; }
        public string ANALIZADO { get; set; }
        public string IDENTIFICADOR { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string DESC_GRADO { get; set; }
        public string DESC_CERTIFICADO { get; set; }
        public string NOTAS { get; set; }
        public string TOURNUMBER { get; set; }
        public Int16 TRIPNUMBER { get; set; }
        public string SONUMB { get; set; }
        public string PX_TANQUE { get; set; }
        public int ID_PRODPARAM { get; set; }
        public int ID_PARAMETRO { get; set; }
        public string DESC_PARAMETRO { get; set; }
        public string VALOR_LIMITE { get; set; }
        public Decimal ANALISIS_INI { get; set; }
        public Decimal ANALISIS_FIN { get; set; }
        public int ID_UM { get; set; }
        public string DESC_UM { get; set; }
        public string OBSERVACIONES { get; set; }
        public Byte EVALUACION { get; set; }
        public DateTime FEC_ALTA { get; set; }
        public int USR_ALTA { get; set; }
        public string ESPECIFICACION { get; set; }
        public int ID_ANALIZADOR { get; set; }
        public string DESC_ANALIZADOR { get; set; }
        public Decimal LIMITE_INFERIOR { get; set; }
        public string LEYENDA_REPORTE { get; set; }
        public string ID_LOTEPIPA { get; set; }
        public string DESC_PIPA { get; set; }
        public string TRASVASADO { get; set; }
        public string ELABORADO { get; set; }
        public string REPRESENTANTE { get; set; }
        public string PRODUCT_ID { get; set; }
        public string ANALISIS_INICIAL { get; set; }
        public string ANALISIS_FINAL { get; set; }
        public int ID_PLANTA { get; set; }
        public string Unique { get; set; }
        public string PATHFINAL { get; set; }   //AHF
        public string PATHINICIAL { get; set; } //AHF
    }
}
