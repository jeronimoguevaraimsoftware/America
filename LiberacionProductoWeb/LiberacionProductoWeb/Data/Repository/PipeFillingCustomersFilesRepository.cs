using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class PipeFillingCustomersFilesRepository: Repository<PipeFillingCustomersFiles>, IPipeFillingCustomersFilesRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipeFillingCustomersFilesRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
