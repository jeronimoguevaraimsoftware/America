using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class HistorianRepository : Repository<ProductionOrderHistorian>, IHistorianRepository
    {
        private readonly AppDbContext _appDbContext;
        public HistorianRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public Task<ProductionOrderHistorian> FindByProductionOrderIdAsync(int id)
        {
            return this._appDbContext.ProductionOrderHistorian.FirstOrDefaultAsync(x => x.ProductionOrderId == id);
        }
    }

    public class HistorianInfoTagsPlantRepository : IHistorianInfoTagsPlantRepository
    {
        private readonly AppDbContext _appDbContext;
        public HistorianInfoTagsPlantRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class HistorianReadingsPlantRepository : IHistorianReadingsPlantRepository
    {
        private readonly AppDbContext _appDbContext;
        public HistorianReadingsPlantRepository(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<IEnumerable<VariableHistorian>> GetVariablesAsync(string plant, string product, DateTime start, DateTime end)
        {
            return await (from hinfo in this._appDbContext.HistorianInfoTagsPlants
                          join htags in this._appDbContext.HistorianTagsPlants on hinfo.TagName equals htags.TagName
                          join hreading in this._appDbContext.HistorianReadingsPlants on htags.TagRid equals hreading.TagRid
                          where hinfo.PlantId == plant && hinfo.Product == product && hreading.ReadingDateTime <= end && hreading.ReadingDateTime >= start
                          select new VariableHistorian()
                          {
                              Name = htags.TagPhysical,
                              Period = hreading.ReadingDateTime,
                              Value = hreading.ReadingValue,
                              Type = hinfo.Type
                          }).ToListAsync();
        }
    }

    public class HistorianTagsPlantRepository : IHistorianTagsPlantRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public HistorianTagsPlantRepository(AppDbExternalContext dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
