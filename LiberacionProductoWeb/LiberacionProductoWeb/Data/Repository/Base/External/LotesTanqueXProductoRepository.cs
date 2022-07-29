using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LotesTanqueXProductoRepository : ExternalRepository<VwLotesTanqueXProducto>, ILotesTanqueXProductoRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LotesTanqueXProductoRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
