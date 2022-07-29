using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.SechToolViewModels;

namespace LiberacionProductoWeb.Models.Principal
{
    public class SummaryOrderViewModel: SechToolDistributionBatchVM
    {
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public ConditioningOrderViewModel ConditioningOrder { get; set; }
    }
}