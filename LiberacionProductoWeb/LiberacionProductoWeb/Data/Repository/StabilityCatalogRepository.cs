using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class StabilityCatalogRepository : Repository<StabilityCatalog>, IStabilityCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public StabilityCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}

