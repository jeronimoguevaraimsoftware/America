using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class TanquesRepository : ExternalRepository<VwTanques>, ITanquesRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public TanquesRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
