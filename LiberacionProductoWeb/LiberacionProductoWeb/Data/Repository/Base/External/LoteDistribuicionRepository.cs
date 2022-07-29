using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LoteDistribuicionRepository : ExternalRepository<VwLotesDistribuicion>, ILoteDistribuicionRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LoteDistribuicionRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
