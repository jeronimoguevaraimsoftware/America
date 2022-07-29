using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ContainerCatalog : Entity, ICloneable
    {
        public ContainerCatalog(int id, string plantId, string productId, string tankId, string presentation, string primaryContainer, bool status)
        {
            this.Id = id;
            this.PlantId = plantId;
            this.ProductId = productId;
            this.TankId = tankId;
            this.Presentation = presentation;
            this.PrimaryContainer = primaryContainer;
            this.Status = status;
        }

        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String Presentation { get; set; }
        public String PrimaryContainer { get; set; }
        public Boolean Status { get; set; }
        public String User { get; set; }

        public ContainerCatalog()
        {

        }

        public static ContainerCatalog Create(
            int containerId,
            String plantId,
            String productId,
            String tankId,
            String presentation,
            String primary,
            String User,
            Boolean estatus = true
            )
        {
            var entityContainerCatalog = new ContainerCatalog
            {
                Id = containerId,
                PlantId = plantId,
                ProductId = productId,
                TankId = tankId,
                Presentation = presentation,
                PrimaryContainer = primary,
                Status = estatus
            };
            return entityContainerCatalog;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ContainerCatalog;
            var current = objectToCompare as ContainerCatalog;
            if (old.PlantId != this.PlantId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Planta",
                    Funcionality = "Catálogo de envase primario y presentación",
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
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Producto",
                    Funcionality = "Catálogo de envase primario y presentación",
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
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Tanque",
                    Funcionality = "Catálogo de envase primario y presentación",
                    PreviousValue = old.TankId,
                    NewValue = TankId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Presentation != this.Presentation)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Presentacion",
                    Funcionality = "Catálogo de envase primario y presentación",
                    PreviousValue = old.Presentation,
                    NewValue = Presentation,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.PrimaryContainer != this.PrimaryContainer)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Envase primario",
                    Funcionality = "Catálogo de envase primario y presentación",
                    PreviousValue = old.PrimaryContainer,
                    NewValue = PrimaryContainer,
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
                    Controller = "ContainerCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Estatus",
                    Funcionality = "Catálogo de envase primario y presentación",
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
            return new ContainerCatalog(this.Id, this.PlantId, this.ProductId, this.TankId, this.Presentation, this.PrimaryContainer, this.Status);
        }
    }
}
