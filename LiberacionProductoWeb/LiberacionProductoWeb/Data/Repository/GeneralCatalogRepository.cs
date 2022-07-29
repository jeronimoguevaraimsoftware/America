using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class GeneralCatalogRepository : Repository<GeneralCatalog>, IGeneralCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public GeneralCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
