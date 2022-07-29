using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels.Base
{
    public class ReportAuditTrail : Entity
    {
        public ReportAuditTrail()
        {

        }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Controller")]
        public String Controller { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Method")]
        public String Method { get; set; }
        [Column(TypeName = "DateTime")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "User")]
        public String User { get; set; }
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Funcionality")]
        public String Funcionality { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "PreviousValue")]
        public String PreviousValue { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "NewValue")]
        public String NewValue { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Action")]
        public String Action { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Plant")]
        public String Plant { get; set; }
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Product")]
        public String Product { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        [Display(Name = "Detail")]
        public String Detail { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "DistribuitionBatch")]
        [DefaultValue("NA")]
        public String DistribuitionBatch { get; set; }
        public int ProductionOrderId { get; set; }

        public static ReportAuditTrail Create(int ReportId, String controller, String method, DateTime date, String user, String funcionality, String previousValue,
        String newValue, String action, String plant, String product, String detail)
        {
            var EntityreportAudit = new ReportAuditTrail()
            {
                Id = ReportId,
                Controller = controller,
                Method = method,
                Date = date,
                User = user,
                Funcionality = funcionality,
                PreviousValue = previousValue,
                NewValue = newValue,
                Action = action,
                Plant = plant,
                Product = product,
                Detail = detail
            };
            return EntityreportAudit;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }
}
