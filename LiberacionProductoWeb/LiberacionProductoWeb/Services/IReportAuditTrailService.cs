using LiberacionProductoWeb.Models.ConfigViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IReportAuditTrailService
    {
        Task<List<ReportAuditTrailViewModel>> GetReportAuditTrail();
        public void Save(IEnumerable<Models.DataBaseModels.Base.ReportAuditTrail> reportAuditTrails);
    }
}
