using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ContainerCatalogRepository : Repository<ContainerCatalog>, IContainerCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContainerCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}

