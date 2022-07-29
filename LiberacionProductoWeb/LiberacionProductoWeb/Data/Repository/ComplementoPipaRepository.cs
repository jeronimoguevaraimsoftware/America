using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ComplementoPipaRepository : Repository<ComplementoPipa>, IComplementoPipaRepository
    {
        private readonly AppDbContext _appDbContext;
        public ComplementoPipaRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public Task<ComplementoPipa> FindByDistributionBatchAsync(int conditioningOrderId, string distributionBatch)
        {
            return this._appDbContext.ComplementoPipas
                .Include(x => x.ConditioningOrder)
                .FirstOrDefaultAsync(x => x.ConditioningOrderId == conditioningOrderId && x.NumeroLoteDistribucion == distributionBatch);
        }
    }
}