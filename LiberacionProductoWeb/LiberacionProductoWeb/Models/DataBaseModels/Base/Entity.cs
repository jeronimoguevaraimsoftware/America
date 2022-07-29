using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.DataBaseModels.Base
{
    public abstract class Entity : EntityBase<int>
    {
        public abstract IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null);
    }
}
