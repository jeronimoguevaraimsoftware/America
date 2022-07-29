using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class ProductionOrderAttributeViewModel
    {
        public int Id { get; set; }
        public int ProductionOrderId { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string Variable { get; set; }
        public string ChartPath { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string AvgValue { get; set; }
        public bool? InCompliance { get; set; }
        public string DeviationReportFolio { get; set; }
        public string DeviationReportNotes { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public ProductionOrderAttributeType Type { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }
    }
}
