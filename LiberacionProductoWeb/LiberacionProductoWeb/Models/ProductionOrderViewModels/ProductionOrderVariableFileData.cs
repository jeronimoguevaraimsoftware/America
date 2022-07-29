using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{

   public class ProductionOrderVariableFileData
   {
        public String VariableType { get; set; }
        public String VariableCode { get; set; }
        public Decimal CurrentValue { get; set; }
        public Decimal MinValue { get; set; }
        public Decimal MaxValue { get; set; }
        public Decimal AvgValue { get; set; }
        public Dictionary<DateTime, Decimal> HistoricalData { get; set; }
        public String HistoricalDataToGraph { get; set; }
        public Boolean ContainsDataInFile { get; set; }
        public Boolean ContainsErrorInFile { get; set; }
        public int ColumnIndexInFile { get; set; }
        public int ColumnFechaIndexInFile { get; set; } //MEG
        public String ColumnNameInFile { get; set; }

  
   }

    public class HistoricalToGrap
    {
        public String Period { get; set; }
        public decimal Value { get; set; }
    }

}
