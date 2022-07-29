using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class DispositionCatalogRepository : Repository<DispositionCatalog>, IDispositionCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public DispositionCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}

