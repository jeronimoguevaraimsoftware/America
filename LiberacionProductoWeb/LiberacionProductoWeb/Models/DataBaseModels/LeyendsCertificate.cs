using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class LeyendsCertificate : Entity, ICloneable
    {
        public string HeaderOne { get; set; }
        public string HeaderTwo { get; set; }
        public string LeyendOne { get; set; }
        public string LeyendTwo { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string User { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as LeyendsCertificate;
            var current = objectToCompare as LeyendsCertificate;
            if (old.HeaderOne != current.HeaderOne)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "Encabezado uno",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.HeaderOne,
                    NewValue = current.HeaderOne,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            if (old.HeaderTwo != current.HeaderTwo)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "Encabezado dos logo",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.HeaderTwo,
                    NewValue = current.HeaderTwo,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            if (old.LeyendOne != current.LeyendOne)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "Leyenda uno",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.LeyendOne,
                    NewValue = current.LeyendOne,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            if (old.LeyendTwo != current.LeyendTwo)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "Leyenda dos",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.LeyendTwo,
                    NewValue = current.LeyendTwo,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            if (old.ModifyDate != current.ModifyDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "fecha de modificación",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.ModifyDate.HasValue ? old.ModifyDate.Value.ToString() : null,
                    NewValue = current.ModifyDate.HasValue ? current.ModifyDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }
        public LeyendsCertificate()
        {

        }
        public object Clone()
        {
            return new LeyendsCertificate
            {
                Id = this.Id,
                HeaderOne = this.HeaderOne,
                HeaderTwo = this.HeaderTwo,
                LeyendOne = this.LeyendOne,
                LeyendTwo = this.LeyendTwo,
                User = this.User,
               
            };
        }
    }
    public class LeyendsCertificateHistory: Entity
    {
        public string HeaderOne { get; set; }
        public string HeaderTwo { get; set; }
        public string LeyendOne { get; set; }
        public string LeyendTwo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string User { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            throw new NotImplementedException();
        }
    }
    public class LeyendsFooterCertificate : Entity
    {
        public string PlantId { get; set; }
        public string Footer { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string User { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as LeyendsFooterCertificate;
            var current = objectToCompare as LeyendsFooterCertificate;
            if (old.Footer != current.Footer)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "Pie",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.Footer,
                    NewValue = current.Footer,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            if (old.ModifyDate != current.ModifyDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "Cambios al Certificado de Calidad",
                    Date = DateTime.Now,
                    Detail = "fecha de modificación",
                    Funcionality = "Cambios al Certificado de Calidad - datos generales",
                    PreviousValue = old.ModifyDate.HasValue ? old.ModifyDate.Value.ToString() : null,
                    NewValue = current.ModifyDate.HasValue ? current.ModifyDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = "NA",
                    Product = "NA",
                    User = User,
                    ProductionOrderId = 0
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

    }

    public class LeyendsFooterCertificateHistory : Entity
    {
        public string PlantId { get; set; }
        public string Footer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string User { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            throw new NotImplementedException();
        }
    }
}
