using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class StabilityCatalog : Entity, ICloneable
    {
        public StabilityCatalog()
        {
        }

        public StabilityCatalog(int id, string plantId, string productId, string tankId, string code, string observations, string user, bool status)
        {
            this.Id = id;
            this.PlantId = plantId;
            this.ProductId = productId;
            this.TankId = tankId;
            this.Code = code;
            this.Observations = observations;
            this.User = user;
            this.Status = status;
        }

        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public DateTime StudyDate { get; set; }
        public String Code { get; set; }
        public Boolean Status { get; set; }
        public String Observations { get; set; }
        public String User { get; set; }


        public static StabilityCatalog Create(
            int stabilityId,
            String plantId,
            String productId,
            String tankId,
            DateTime studyDate,
            String code,
            String observations,
            String User,
            Boolean estatus = true
            )
        {
            var entityStabilityCatalog = new StabilityCatalog
            {
                Id = stabilityId,
                PlantId = plantId,
                ProductId = productId,
                TankId = tankId,
                StudyDate = studyDate,
                Code = code,
                Observations = observations,
                Status = estatus
            };
            return entityStabilityCatalog;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as StabilityCatalog;
            var current = objectToCompare as StabilityCatalog;
            if (old.PlantId != this.PlantId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Planta",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.PlantId,
                    NewValue = PlantId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.ProductId != this.ProductId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Producto",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.ProductId,
                    NewValue = ProductId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.TankId != this.TankId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Tanque",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.TankId,
                    NewValue = TankId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.StudyDate != this.StudyDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Fecha de estudio",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.StudyDate.ToString(),
                    NewValue = StudyDate.ToString(),
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Code != this.Code)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Código",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.Code,
                    NewValue = Code,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Observations != this.Observations)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Observaciones",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.Observations,
                    NewValue = Observations,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Status != this.Status)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "StabilityCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Estatus",
                    Funcionality = "Estudio de estabilidad del producto",
                    PreviousValue = old.Status == true ? "SI" : "NO",
                    NewValue = current.Status == true ? "SI" : "NO",
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new StabilityCatalog(this.Id, this.PlantId, this.ProductId, this.TankId, this.Code, this.Observations, this.User, this.Status);
        }
    }
}
