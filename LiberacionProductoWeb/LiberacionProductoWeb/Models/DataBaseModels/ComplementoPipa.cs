using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ComplementoPipa : Entity, ICloneable
    {
        public string NumeroLoteDistribucion { get; set; }
        public int ConditioningOrderId { get; set; }
        public DateTime Fecha { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Hora { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FolioTrabajoNoConforme { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FolioPNC { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string DisposicionPNC { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FolioControlCambios { get; set; }
        public string Observaciones { get; set; }
        public string FolioEventoAdverso { get; set; }
        public string FolioRetiroProductos { get; set; }
        public string NumeroDeDevolucion { get; set; }
        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ComplementoPipa;
            var current = objectToCompare as ComplementoPipa;
            if (old.NumeroLoteDistribucion != current.NumeroLoteDistribucion)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.NumeroLoteDistribucion,
                    NewValue = current.NumeroLoteDistribucion,
                    Method = "UpdateAsync",
                    Plant = current?.ConditioningOrder?.PlantId,
                    Product = current.ConditioningOrder.PlantId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Fecha != current.Fecha)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.Fecha.ToString(),
                    NewValue = current.Fecha.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Hora != current.Hora)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.Hora,
                    NewValue = current.Hora,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FolioTrabajoNoConforme != current.FolioTrabajoNoConforme)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.FolioTrabajoNoConforme,
                    NewValue = current.FolioTrabajoNoConforme,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FolioPNC != current.FolioPNC)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.FolioPNC,
                    NewValue = current.FolioPNC,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DisposicionPNC != current.DisposicionPNC)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.DisposicionPNC,
                    NewValue = current.DisposicionPNC,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FolioControlCambios != current.FolioControlCambios)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.FolioControlCambios,
                    NewValue = current.FolioControlCambios,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Observaciones != current.Observaciones)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.Observaciones,
                    NewValue = current.Observaciones,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FolioEventoAdverso != current.FolioEventoAdverso)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.FolioEventoAdverso,
                    NewValue = current.FolioEventoAdverso,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FolioRetiroProductos != current.FolioRetiroProductos)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.FolioRetiroProductos,
                    NewValue = current.FolioRetiroProductos,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.NumeroDeDevolucion != current.NumeroDeDevolucion)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RAPPipas",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap Pipas",
                    Funcionality = "Complemento Rap Pipas",
                    PreviousValue = old.NumeroDeDevolucion,
                    NewValue = current.NumeroDeDevolucion,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new ComplementoPipa()
            {
                Id = this.Id,
                NumeroLoteDistribucion = this.NumeroLoteDistribucion,
                ConditioningOrderId = this.ConditioningOrderId,
                Fecha = this.Fecha,
                Hora = this.Hora,
                FolioTrabajoNoConforme = this.FolioTrabajoNoConforme,
                FolioPNC = this.FolioPNC,
                DisposicionPNC = this.DisposicionPNC,
                FolioControlCambios = this.FolioControlCambios,
                Observaciones = this.Observaciones,
                FolioEventoAdverso = this.FolioEventoAdverso,
                FolioRetiroProductos = this.FolioRetiroProductos,
                NumeroDeDevolucion = this.NumeroDeDevolucion
            };
        }
    }
}
