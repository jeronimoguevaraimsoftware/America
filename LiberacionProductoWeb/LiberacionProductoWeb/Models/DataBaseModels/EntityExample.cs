using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class EntityExample : Entity
    {
        public EntityExample()
        {
        }

        public string CategoryName { get; set; }
        public string Description { get; set; }

        public static EntityExample Create(int categoryId, string name, string description = null)
        {
            var entityExample = new EntityExample
            {
                Id = categoryId,
                CategoryName = name,
                Description = description
            };
            return entityExample;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
