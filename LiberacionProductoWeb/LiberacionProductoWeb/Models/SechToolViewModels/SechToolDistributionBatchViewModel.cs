using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.SechToolViewModels
{
    public class SechToolDistributionBatchViewModel
    {
        public string DistributionBatch { get; set; }
        public string Source { get; set; }
        public int ConditioningOrderId { get; set; }
        public int ProductionOrderId { get; set; }
        public int Id { get; set; }
        public string TourNumber { get; set; }
        public string PipeNumber { get; set; }
        public bool? RelationShip { get; set; }
        public string File { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
    }
}
