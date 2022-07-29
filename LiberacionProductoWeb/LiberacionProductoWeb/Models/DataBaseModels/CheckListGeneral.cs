using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class CheckListGeneral : Entity
    {
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ConditioningOrderId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string TourNumber { get; set; }
        public bool? InCompliance { get; set; }
        [Column(TypeName = "varchar(400)")]
        public string Verification { get; set; }
        public string Notes { get; set; }
        public bool Source { get; set; }
        public string Status { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }

    public class CheckListPipeRecordAnswer : Entity
    {

        [Column(TypeName = "varchar(100)")]
        public String Status { get; set; }
        public int NumOA { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(10)")]
        public String ApproveSC { get; set; }
        [Column(TypeName = "varchar(450)")]
        public String Reason { get; set; }
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string TourNumber { get; set; }
        public int CheckListPipeDictiumId { get; set; }
        public int? Step { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }

    public class CheckListPipeCommentsAnswer : Entity
    {
        [Column(TypeName = "varchar(100)")]
        public String Author { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Group { get; set; }
        public int NumOA { get; set; }
        public String Comment { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string TourNumber { get; set; }
        public int CheckListPipeDictiumId { get; set; }
        public int? Step { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }

    public class CheckListPipeDictiumAnswer : Entity
    {
        [Column(TypeName = "varchar(200)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Verification")]
        public String Verification { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(100)")]
        public int NumOA { get; set; }
        public String Comment { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string TourNumber { get; set; }
        public bool? InCompliance { get; set; }
        public bool Source { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Status { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string File { get; set; }
        public int? Step { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "VerificationTwo")]
        public String VerificationTwo { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string StatusTwo { get; set; }
        public String CommentTwo { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string PipeNumber { get; set; }
        public bool? RelationShip { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Alias { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }

    public class CheckListPipeAnswer : Entity
    {
        [Column(TypeName = "varchar(450)")]
        [Display(Name = "Requirement")]
        public String Requirement { get; set; }

        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Verification")]
        public String Verification { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [Column(TypeName = "varchar(400)")]
        [Display(Name = "Notify")]
        public String Notify { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Action")]
        public String Action { get; set; }

        [Column(TypeName = "varchar(300)")]
        [Display(Name = "Group")]
        public String Group { get; set; }

        public int NumOA { get; set; }

        public string TourNumber { get; set; }

        public string PipeNumber { get; set; }

        public string DistributionBatch { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CheckListPipeDictiumId { get; set; }
        public int? Step { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
