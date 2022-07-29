using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IReportAuditTrailRepository : IRepository<ReportAuditTrail>
    {
    }
}
