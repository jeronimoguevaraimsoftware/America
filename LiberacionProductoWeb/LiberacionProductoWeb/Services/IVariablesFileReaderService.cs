using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IVariablesFileReaderService
    {
        Task<FilterParameters> GetFilterParameterByProductKeyAsync(string ProductKey);
        Task<ProductionOrderViewModel> StorageVariablesAsync(ProductionOrderViewModel model);
        Task<List<CriticalQualityAttributeViewModel>> FillCriticalQualityAttributes(int productionOrderId, List<CriticalQualityAttributeViewModel> criticalQualityAttributeViewModels);
        Task<ProductionOrderViewModel> FillVariablesAsync(ProductionOrderViewModel model);
        Task<ControlVariableViewModel> GetMaxMin(String variables, string max, string min);
    }
}
