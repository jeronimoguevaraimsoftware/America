using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels.Base;


namespace LiberacionProductoWeb.Data.Repository
{
    public class ReportAuditTrailRepository: Repository<ReportAuditTrail>, IReportAuditTrailRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReportAuditTrailRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}