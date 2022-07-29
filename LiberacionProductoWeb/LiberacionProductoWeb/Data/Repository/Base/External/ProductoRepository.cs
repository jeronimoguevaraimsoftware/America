using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class ProductoRepository : ExternalRepository<VwProductos>, IProductoRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public ProductoRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
