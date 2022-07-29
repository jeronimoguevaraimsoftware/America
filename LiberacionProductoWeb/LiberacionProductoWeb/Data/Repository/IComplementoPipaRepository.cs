using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IComplementoPipaRepository : IRepository<ComplementoPipa>
    {
        Task<ComplementoPipa> FindByDistributionBatchAsync(int conditioningOrderId, string distributionBatch);
    }
}
