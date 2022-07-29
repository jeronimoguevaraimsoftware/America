using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class DispositionCatalog : Entity
    {
        public DispositionCatalog()
        {
        }

        public DispositionCatalog(string dispositionType, bool status, string user)
        {
            DispositionType = dispositionType;
            Status = status;
            User = user;
        }

        public String DispositionType { get; set; }
        public Boolean Status { get; set; }
        public String? User { get; set; }

        public static DispositionCatalog Create(
            int dispositionId,
            String disposition,
            String user,
            Boolean estatus = true
            )
        {
            var dispositionCatalog = new DispositionCatalog
            {
                Id = dispositionId,
                DispositionType = disposition,
                Status = estatus

            };
            return dispositionCatalog;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as DispositionCatalog;
            var current = objectToCompare as DispositionCatalog;
            if (old.DispositionType != this.DispositionType)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "DispositionCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Disposición de PNC",
                    Funcionality = "Catálogo disposición de PNC",
                    PreviousValue = old.DispositionType,
                    NewValue = DispositionType,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                });
            }
            if (old.Status != this.Status)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "DispositionCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Estatus",
                    Funcionality = "Catálogo disposición de PNC",
                    PreviousValue = old.Status == true ? "SI" : "NO",
                    NewValue = current.Status == true ? "SI" : "NO",
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                });
            }

            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new DispositionCatalog(this.DispositionType, this.Status, this.User);
        }
    }
}
