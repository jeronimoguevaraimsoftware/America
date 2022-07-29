using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class StateRepository : Repository<States>, IStateRepository
    {
        private readonly AppDbContext _appDbContext;
        public StateRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
