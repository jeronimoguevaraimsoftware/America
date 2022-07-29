using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.Principal
{
    public class QueryGeneralModel
    {
        public int Id { get; set; }
        public DateTime? StartDateOP { get; set; }
        public string NoLotProd { get; set; }
        public string Tank { get; set; }
        public string TankId { get; set; }
        public DateTime? StartDateOA { get; set; }
        public string NoLotDist { get; set; }
        public string CheckList { get; set; }
        public string NoTankClient { get; set; }
        public string NameClient { get; set; }
        public string AnalysisReport { get; set; }
        public string Comments { get; set; }
        public string Plant { get; set; }
        public string PlantId { get; set; }
        public string Product { get; set; }
        public string ProductId { get; set; }
        public string State { get; set; }
        public int ProductionOrderId { get; set; }
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string PipeNumber { get; set; }
        public string distributionBatch { get; set; }
    }
}
