using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ProductCatalogRepository : Repository<ProductCatalog>, IProductCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}

