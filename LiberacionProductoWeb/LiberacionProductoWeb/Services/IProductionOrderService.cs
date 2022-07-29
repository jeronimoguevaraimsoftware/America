using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;

namespace LiberacionProductoWeb.Services
{
    public interface IProductionOrderService
    {
        Task<IList<ProductionOrder>> GetAllAsync();
        Task<IList<VwLotesProduccionDetalle>> GetLot(string plant, string product, string tank, string productoNombre);
        Task<ProductionOrderViewModel> GetByIdAsync(int Id);
        Task<Boolean> ForReleasedProductionOrder(int ProductionOrderId);
    }
}
