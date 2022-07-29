using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LotesDistribuicionClienteRepository : ExternalRepository<VwLotesDistribuicionCliente>, ILotesDistribuicionClienteRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LotesDistribuicionClienteRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
