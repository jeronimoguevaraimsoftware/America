using LiberacionProductoWeb.Models.External;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class LotesProduccionRepository: ExternalRepository<VwLotesProduccion>, ILotesProduccionRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public LotesProduccionRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

      
    }
}

