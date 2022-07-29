using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ConditioningOrderRepository : Repository<ConditioningOrder>, IConditioningOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public ConditioningOrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
    public class PipelineClearanceOARepository : Repository<PipelineClearanceOA>, IPipelineClearanceOARepository
    {
        private readonly AppDbContext _appDbContext;
        public PipelineClearanceOARepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class ScalesFlowMetersRepository : Repository<ScalesFlowMeters>, IScalesFlowMetersRepository
    {
        private readonly AppDbContext _appDbContext;
        public ScalesFlowMetersRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class AnalyticalEquipamentRepository : Repository<AnalyticalEquipament>, IAnalyticalEquipamentRepository
    {
        private readonly AppDbContext _appDbContext;
        public AnalyticalEquipamentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
    public class EquipmentProcessConditioningRepository : Repository<EquipmentProcessConditioning>, IEquipmentProcessConditioningRepository
    {
        private readonly AppDbContext _appDbContext;
        public EquipmentProcessConditioningRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
    public class PerformanceProcessConditioningRepository : Repository<PerformanceProcessConditioning>, IPerformanceProcessConditioningRepository
    {
        private readonly AppDbContext _appDbContext;
        public PerformanceProcessConditioningRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class PipeFillingControlRepository : Repository<PipeFillingControl>, IPipeFillingControlRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipeFillingControlRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class PipeFillingRepository : Repository<PipeFilling>, IPipeFillingRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipeFillingRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class PipeFillingAnalysisRepository : Repository<PipeFillingAnalysis>, IPipeFillingAnalysisRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipeFillingAnalysisRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class PipeFillingCustomerRepository : Repository<PipeFillingCustomer>, IPipeFillingCustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipeFillingCustomerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<IEnumerable<string>> FindLastFolioByParametersAsync(string plant, string product, string tank)
        {
            return await this._dbContext.PipeFillingCustomers
                .Where(x => x.PlantIdentificador == plant && x.ProductId == product && x.TankId == tank && !string.IsNullOrEmpty(x.Folio))
                .Select(x => x.Folio)
                .ToListAsync();
        }

        public Task<PipeFillingCustomer> GetByParametersAsync(int ConditionOrderId, string tournumber, string distributionBatch, string tank)
        {
            return this._dbContext.PipeFillingCustomers
                .FirstOrDefaultAsync(x => x.ConditioningOrderId == ConditionOrderId && x.TourNumber == tournumber && x.DistributionBatch == distributionBatch && x.Tank == tank);
        }
    }
}
