using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LoteDistribuicionDetalleRepository : ExternalRepository<VwLotesDistribuicionDetalle>, ILoteDistribuicionDetalleRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LoteDistribuicionDetalleRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
