using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class Plant : Entity
    {
        public String ExternalCode { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Boolean Active { get; set; }

        public static Plant Create(int plantId, string name, DateTime creationDate, DateTime updateDate, Boolean active, string description = null, string externalCode = null)
        {
            var plant = new Plant
            {
                Id = plantId,
                Active = active,
                CreationDate = creationDate,
                Description = description,
                ExternalCode = externalCode,
                Name = name,
                UpdateDate = updateDate
            };
            return plant;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
