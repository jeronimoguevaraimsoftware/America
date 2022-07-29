using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels.Base
{
    public class ActivitiesReportAudit : Entity
    {
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Value { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
