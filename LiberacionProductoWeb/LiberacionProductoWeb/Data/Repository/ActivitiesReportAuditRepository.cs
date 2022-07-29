using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ActivitiesReportAuditRepository : Repository<ActivitiesReportAudit>, IActivitiesReportAuditRepository
    {
        private readonly AppDbContext _appDbContext;
        public ActivitiesReportAuditRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}