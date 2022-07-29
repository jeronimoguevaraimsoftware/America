using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ProductCatalog : Entity
    {
        public ProductCatalog()
        {
        }

        public ProductCatalog(int id, string plantId, string productId, string tankId, string formulaName, string purity, string presentation, bool status)
        {
            Id = id;
            PlantId = plantId;
            ProductId = productId;
            TankId = tankId;
            FormulaName = formulaName;
            Purity = purity;
            Presentation = presentation;
            Status = status;
        }

        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String FormulaName { get; set; }
        public Boolean Status { get; set; }
        public String Purity { get; set; }
        public String Presentation { get; set; }
        public String User { get; set; }



        public static ProductCatalog Create(
            int formulaId,
            String plantId,
            String productId,
            String tankId,
            String formula,
            String purity,
            String presentation,
            String User,
            Boolean estatus = true
            )
        {
            var entityProductCatalog = new ProductCatalog
            {
                Id = formulaId,
                PlantId = plantId,
                ProductId = productId,
                TankId = tankId,
                FormulaName = formula,
                Purity = purity,
                Presentation = presentation,
                Status = estatus
            };
            return entityProductCatalog;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ProductCatalog;
            var current = objectToCompare as ProductCatalog;
            if (old.PlantId != this.PlantId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Planta",
                    Funcionality = "Catálogo Producto",
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
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Producto",
                    Funcionality = "Catálogo Producto",
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
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Tanque",
                    Funcionality = "Catálogo Producto",
                    PreviousValue = old.TankId,
                    NewValue = TankId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.FormulaName != this.FormulaName)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Fórmula farmacéutica",
                    Funcionality = "Catálogo Producto",
                    PreviousValue = old.FormulaName,
                    NewValue = FormulaName,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Purity != this.Purity)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Pureza",
                    Funcionality = "Catálogo Producto",
                    PreviousValue = old.Purity,
                    NewValue = Purity,
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
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Presentación",
                    Funcionality = "Catálogo Producto",
                    PreviousValue = old.Presentation,
                    NewValue = Presentation,
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
                    Controller = "ProductCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Estatus",
                    Funcionality = "Catálogo Producto",
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
            return new ProductCatalog(this.Id, this.PlantId, this.ProductId, this.TankId, this.FormulaName, this.Purity, this.Presentation, this.Status);
        }
    }
}
