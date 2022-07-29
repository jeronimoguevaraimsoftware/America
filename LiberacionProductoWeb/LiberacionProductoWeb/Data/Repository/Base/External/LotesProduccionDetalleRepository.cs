using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LotesProduccionDetalleRepository: ExternalRepository<VwLotesProduccionDetalle>, ILotesProduccionDetalleRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LotesProduccionDetalleRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        //public async Task<IReadOnlyList<T>> GetAllAsync()
        //{
        //    return await _appDbContext.Set<VwLotesProduccionDetalle>().ToListAsync();
        //}
        //public IQueryable<TEntity> GetAll()
        //{
        //    return _appDbContext.Set<TEntity>().ToList();
        //}
    }
}
