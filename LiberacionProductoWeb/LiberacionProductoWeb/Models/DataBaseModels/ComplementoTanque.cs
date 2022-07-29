using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ComplementoTanque : Entity, ICloneable
    {
        public string NumeroLoteProduccion { get; set; }
        public int ProductionOrderId { get; set; }
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
        //-----------------------------
        //Relationships
        public ProductionOrder productionOrder { get; set; }
        public ComplementoTanque(int Id,string numeroLoteProduccion, int productionOrderId, DateTime fecha, string hora, string folioTrabajoNoConforme, string folioPNC
            ,string disposicionPNC,string folioControlCambios, string observaciones)
        {
            this.Id = Id;
            this.NumeroLoteProduccion = numeroLoteProduccion;
            this.ProductionOrderId = productionOrderId;
            this.Fecha = fecha;
            this.Hora = hora;  
            this.FolioTrabajoNoConforme = folioTrabajoNoConforme;
            this.FolioPNC = folioPNC;
            this.DisposicionPNC = disposicionPNC;
            this.FolioControlCambios= folioControlCambios;
            this.Observaciones = observaciones;
        }

        public ComplementoTanque()
        {
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as ComplementoTanque;
            var old = objectToCompareOld as ComplementoTanque;
            if (old.NumeroLoteProduccion != current.NumeroLoteProduccion)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - NumeroLoteProduccion",
                    Funcionality = "Complemento Rap tanque",
                    PreviousValue = old.NumeroLoteProduccion,
                    NewValue = current.NumeroLoteProduccion,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.Fecha != current.Fecha)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - Fecha",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.Fecha.ToString(),
                    NewValue = current.Fecha.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.FolioTrabajoNoConforme != current.FolioTrabajoNoConforme)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - Folio Trabajo no conforme",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.FolioTrabajoNoConforme,
                    NewValue = current.FolioTrabajoNoConforme,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.FolioPNC != current.FolioPNC)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - FolioPNC",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.FolioPNC,
                    NewValue = current.FolioPNC,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.DisposicionPNC != current.DisposicionPNC)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - Disposicion de PNC",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.DisposicionPNC,
                    NewValue = current.DisposicionPNC,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.FolioControlCambios != current.FolioControlCambios)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - Folio de control de cambios",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.FolioControlCambios,
                    NewValue = current.FolioControlCambios,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            if (old.Observaciones != current.Observaciones)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "RapTanques",
                    Date = DateTime.Now,
                    Detail = "Complemento Rap tanques- Campo - Folio de control de Observaciones",
                    Funcionality = "Rap tanque complemento",
                    PreviousValue = old.Observaciones,
                    NewValue = current.Observaciones,
                    Method = "UpdateAsync",
                    Plant = current.productionOrder.PlantId,
                    Product = current.productionOrder.ProductId,
                    User = current.productionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.productionOrder.Id
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new ComplementoTanque(this.Id, this.NumeroLoteProduccion, this.ProductionOrderId, this.Fecha, this.Hora, this.FolioTrabajoNoConforme, this.FolioPNC 
            , this.DisposicionPNC, this.FolioControlCambios, this.Observaciones);
        }
    }
}
