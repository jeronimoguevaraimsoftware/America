using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public interface IHistorianRepository : IRepository<ProductionOrderHistorian>
    {
        Task<ProductionOrderHistorian> FindByProductionOrderIdAsync(int id);
    }
    public interface IHistorianInfoTagsPlantRepository
    {
    }
    public interface IHistorianReadingsPlantRepository
    {
        Task<IEnumerable<VariableHistorian>> GetVariablesAsync(string plant, string product, DateTime start, DateTime end);
    }
    public interface IHistorianTagsPlantRepository
    {
    }
}
