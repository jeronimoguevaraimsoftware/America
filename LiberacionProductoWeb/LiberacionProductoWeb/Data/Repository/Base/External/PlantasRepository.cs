using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class PlantasRepository : ExternalRepository<VwPlantas>, IPlantasRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public PlantasRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
