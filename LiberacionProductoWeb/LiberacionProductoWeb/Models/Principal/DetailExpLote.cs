using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.Principal
{
    public class DetailExpLote 
    {
        public List<DetailOA> detailOA { get; set; }
        public List<DetailOP> detailOP { get; set; }
        public List<DetailDistributionBatch> detailDB { get; set; }
        
    }

    public class DetailOA
    {
        public string State { get; set; }
        public string CalibrationAnalyticalEquipment { get; set; }
        public string CalibrationScalelEquipment { get; set; }
        public string LineBreak { get; set; }
        public string Accessories { get; set; }
        public string TotalProduct { get; set; }
        public string InitialChromotogram { get; set; }
        public string FinalChromotogram { get; set; }
        public string ProductId { get; set; }
        public int ConditioningOrder { get; set; }
    }

    public class DetailOP
    {
        public string State { get; set; }
        public string ReasonCancellation { get; set; }
        public string CalibrationStatus { get; set; }
        public string LineBreak { get; set; }
        public string StageMonitoring { get; set; }
        public string Chromotogram { get; set; }
        public string Lotification { get; set; }
        public string ProductId { get; set; }
        public int ProductionOrder { get; set; }
    }

    public class DetailDistributionBatch
    {
        public string DistributionBatch { get; set; }
        public List<DetailDistributionBatchItem> Items { get; set; }
        public int ProductionOrderId { get; set; }
        public int ConditioningOrderId { get; set; }
        public string ProductId { get; set; }
   
    }

    public class DetailDistributionBatchItem
    {
        public string TourNumber { get; set; }
        public string OrderNumber { get; set; }
        public string PipeNumber { get; set; }
        public string ChecklistStatus { get; set; }
        public string InitialAnalysis { get; set; }
        public string InitialChromatogram { get; set; }
        public string FinalAnalysis { get; set; }
        public string FinalChromatogram { get; set; }
        public string CustomerTank { get; set; }
        public string CustomerName { get; set; }
        public string AnalysisReport { get; set; }
        public string CheckListId { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int Id { get; set; }

        public string DistributionBatch { get; set; }
    }
}
