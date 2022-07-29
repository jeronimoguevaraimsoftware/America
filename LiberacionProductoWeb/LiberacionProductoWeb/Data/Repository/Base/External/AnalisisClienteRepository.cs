using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class AnalisisClienteRepository : ExternalRepository<VwAnalisisCliente>, IAnalisisClienteRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public AnalisisClienteRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
