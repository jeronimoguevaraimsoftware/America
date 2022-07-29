using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ProductionOrderRepository : Repository<ProductionOrder>, IProductionOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductionOrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public static explicit operator ProductionOrderRepository(Task<ProductionOrderAttribute> v)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductionEquipmentRepository : Repository<ProductionEquipment>, IProductionEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductionEquipmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class MonitoringEquipmentRepository : Repository<MonitoringEquipment>, IMonitoringEquipmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public MonitoringEquipmentRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class PipelineClearanceRepository : Repository<PipelineClearance>, IPipelineClearanceRepository
    {
        private readonly AppDbContext _appDbContext;
        public PipelineClearanceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class ProductionOrderAttributeRepository : Repository<ProductionOrderAttribute>, IProductionOrderAttributeRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductionOrderAttributeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class BatchDetailsRepository : Repository<BatchDetails>, IBatchDetailsRepository
    {
        private readonly AppDbContext _appDbContext;
        public BatchDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class BatchAnalysisRepository : Repository<BatchAnalysis>, IBatchAnalysisRepository
    {
        private readonly AppDbContext _appDbContext;
        public BatchAnalysisRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class HistoryNotesRepository : Repository<HistoryNotes>, IHistoryNotesRepository
    {
        private readonly AppDbContext _appDbContext;
        public HistoryNotesRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class HistoryStatesRepository : Repository<HistoryStates>, IHistoryStatesRepository
    {
        private readonly AppDbContext _appDbContext;
        public HistoryStatesRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
    
}
