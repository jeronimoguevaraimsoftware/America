using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class Activities : Entity
    {
        [Required(ErrorMessage = "Description")]
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        [DefaultValue("true")]
        public Boolean Active { get; set; }
        [Required(ErrorMessage = "Group")]
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Group")]
        public string Group { get; set; }

        public static Activities Create(int StatusId, string description, DateTime creationDate, DateTime updateDate, Boolean active, string group)
        {
            var activities = new Activities
            {
                Id = StatusId,
                Description = description,
                CreationDate = creationDate,
                UpdateDate = updateDate,
                Active = active,
                Group = group

            };
            return activities;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
