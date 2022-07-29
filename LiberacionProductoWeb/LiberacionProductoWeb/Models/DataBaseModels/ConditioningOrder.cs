using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ConditioningOrder : Entity, ICloneable
    {
        public int ProductionOrderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string State { get; set; }
        public int StepSaved { get; set; }
        public string StepSavedDescription { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }
        public string DelegateUser { get; set; }
        public DateTime? EndDate { get; set; }
        public string ReasonReject { get; set; }
        public string Presentation { get; set; }
        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public int? CertificateId { get; set; }
        public int? FooterCertificateId { get; set; }
        public ProductionOrder ProductionOrder { get; set; }
        public ConditioningOrder()
        {

        }

        public ConditioningOrder(int id, DateTime? endDate, string reasonReject, bool? isReleased,
            string releasedNotes, string state, string stepSavedDescription,
            bool? inCompliance, string delegateUser, ProductionOrder productionOrder)
        {
            this.Id = id;
            this.EndDate = endDate;
            this.ReasonReject = reasonReject;
            this.IsReleased = isReleased;
            this.ReleasedNotes = releasedNotes;
            this.State = state;
            this.StepSavedDescription = stepSavedDescription;
            this.DelegateUser = delegateUser;
            this.ProductionOrder = productionOrder;
        }

        public static ConditioningOrder Create(
            int id,
            int productionOrderId,
            string createdBy,
            string presentation,
            string plantId,
            string productId,
            int certificateId,
            int footerCertificateId)
        {
            var entity = new ConditioningOrder
            {
                Id = id,
                State = "OA-En proceso",
                StepSaved = 0,
                StepSavedDescription = "Equipos analíticos",
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                ProductionOrderId = productionOrderId,
                Presentation = presentation,
                PlantId = plantId,
                ProductId = productId,
                CertificateId = certificateId,
                FooterCertificateId = footerCertificateId
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as ConditioningOrder;
            var old = objectToCompareOld as ConditioningOrder;
            if (old != null)
            {
                if (old.EndDate != current.EndDate)
                {
                    if (old.State != current.State)
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 7: Cierre del expediente de lote - Campo - Fecha término",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.EndDate.ToString(),
                            NewValue = current.EndDate.ToString(),
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Orden de acondicionamiento- Campo - Fecha término",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.EndDate.ToString(),
                            NewValue = current.EndDate.ToString(),
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                }
            }
            if (old != null)
            {
                //if (old.ReasonReject != current.ReasonReject)
                //{
                //    auditList.Add(new ReportAuditTrail
                //    {
                //        Action = "Cancelar orden de acondicionamiento",
                //        Controller = "ConditioningOrder",
                //        Date = DateTime.Now,
                //        Detail = "Orden de acondicionamiento - Campo - Motivo cancelación",
                //        Funcionality = "Orden de acondicionamiento",
                //        PreviousValue = current.ReasonReject,
                //        NewValue = old.ReasonReject,
                //        Method = "UpdateAsync",
                //        Plant = current.PlantId,
                //        Product = current.ProductId,
                //        User = current.DelegateUser,
                //        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                //        ProductionOrderId = current.ProductionOrderId
                //    });
                //}
            }
            if (old != null)
            {
                if (old.IsReleased != current.IsReleased)
                {
                    if (old.State != current.State)
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 7: Cierre del expediente de lote - Campo - Liberación",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.IsReleased.HasValue ? old.IsReleased == true ? "SI" : "NO" : null,
                            NewValue = current.IsReleased.HasValue ? current.IsReleased == true ? "SI" : "NO" : null,
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Campo - Liberación",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.IsReleased.ToString(),
                            NewValue = current.IsReleased.ToString(),
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                }
            }
            //if (old != null)
            //{
            //    if (old.State != current.State)
            //    {
            //        if (current.State == ConditioningOrderStatus.Released.Value)
            //        {
            //            auditList.Add(new ReportAuditTrail
            //            {
            //                Action = "Actualizar",
            //                Controller = "ConditioningOrder",
            //                Date = DateTime.Now,
            //                Detail = "Tabla 7: Cierre del expediente de lote - Campo - Estado",
            //                Funcionality = "Orden de acondicionamiento",
            //                PreviousValue = old.State,
            //                NewValue = current.State,
            //                Method = "UpdateAsync",
            //                Plant = current.PlantId,
            //                Product = current.ProductId,
            //                User = current.DelegateUser,
            //                DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
            //                ProductionOrderId = current.ProductionOrderId
            //            });
            //        }
            //        else
            //        {
            //            auditList.Add(new ReportAuditTrail
            //            {
            //                Action = "Actualizar",
            //                Controller = "ConditioningOrder",
            //                Date = DateTime.Now,
            //                Detail = "Orden de acondicionamiento - Campo - Estado",
            //                Funcionality = "Orden de acondicionamiento",
            //                PreviousValue = old.State,
            //                NewValue = current.State,
            //                Method = "UpdateAsync",
            //                Plant = current.PlantId,
            //                Product = current.ProductId,
            //                User = current.CreatedBy,
            //                DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
            //                ProductionOrderId = current.ProductionOrderId
            //            });
            //        }
            //    }
            //}
            if (old != null)
            {
                if (old.ReleasedNotes != current.ReleasedNotes)
                {
                    if (current.State == ConditioningOrderStatus.Released.Value)
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 7: Cierre del expediente de lote - Campo - Observaciones",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.ReleasedNotes,
                            NewValue = current.ReleasedNotes,
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ConditioningOrder",
                            Date = DateTime.Now,
                            Detail = "Orden de acondicionamiento - Campo - Observaciones",
                            Funcionality = "Orden de acondicionamiento",
                            PreviousValue = old.ReleasedNotes,
                            NewValue = current.ReleasedNotes,
                            Method = "UpdateAsync",
                            Plant = current.PlantId,
                            Product = current.ProductId,
                            User = current.DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.ProductionOrderId
                        });
                    }
                }
            }
            if (old != null)
            {
                if (current.State == ConditioningOrderStatus.Released.Value)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Cierre del expediente de lote - Campo - usuario firma",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ReleasedBy,
                        NewValue = current.ReleasedBy,
                        Method = "UpdateAsync",
                        Plant = current.PlantId,
                        Product = current.ProductId,
                        User = current.DelegateUser,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                else
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Orden de acondicionamiento - Campo - usuario firma",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ReleasedBy,
                        NewValue = current.ReleasedBy,
                        Method = "UpdateAsync",
                        Plant = current.PlantId,
                        Product = current.ProductId,
                        User = current.DelegateUser,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }


        public object Clone()
        {
            return new ConditioningOrder
            {
                Id = this.Id,
                StepSaved = this.StepSaved,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                PlantId = this.PlantId,
                ProductId = this.ProductId,
                Presentation = this.Presentation,
                ProductionOrderId = this.ProductionOrderId,
                ReleasedBy = this.ReleasedBy,
                ReleasedDate = this.ReleasedDate,
                EndDate = this.EndDate,
                ReasonReject = this.ReasonReject,
                IsReleased = this.IsReleased,
                ReleasedNotes = this.ReleasedNotes,
                State = this.State,
                StepSavedDescription = this.StepSavedDescription,
                DelegateUser = this.DelegateUser,
                ProductionOrder = this.ProductionOrder
            };
        }
    }
    public class AnalyticalEquipament : Entity, ICloneable
    {
        public AnalyticalEquipament()
        {

        }
        public AnalyticalEquipament(int id, string code, string description, bool? isCalibrated, string notes,
            string reviewedBy, DateTime? reviewedDate, int conditioningOrderId, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.Code = code;
            this.Description = description;
            this.IsCalibrated = isCalibrated;
            this.Notes = notes;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.ConditioningOrderId = conditioningOrderId;
            this.ConditioningOrder = conditioningOrder;

        }

        [Column(TypeName = "varchar(180)")]
        public string Code { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        public bool? IsCalibrated { get; set; }
        [Column(TypeName = "varchar(400)")]
        public string Notes { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ConditioningOrderId { get; set; }

        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as AnalyticalEquipament;
            var old = objectToCompareOld as AnalyticalEquipament;
            if (old.Code != current.Code)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Campo - Tabla 1: Equipos analíticos - Código del equipo",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Code,
                    NewValue = current.Code,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Description != current.Description)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos analíticos - Campo -  Descripción del equipo",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Description,
                    NewValue = current.Description,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.IsCalibrated != current.IsCalibrated)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos analíticos - Campo -  Estado de calibración",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.IsCalibrated.HasValue ? old.IsCalibrated.Value == true ? "SI" : "NO" : null,
                    NewValue = current.IsCalibrated.HasValue ? current.IsCalibrated.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos analíticos - Campo -  Observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos analíticos - Campo -  Revisado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos analíticos - Campo -  Fecha de revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                    NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new AnalyticalEquipament(this.Id, this.Code, this.Description, this.IsCalibrated, this.Notes, this.ReviewedBy,
                this.ReviewedDate, this.ConditioningOrderId, this.ConditioningOrder);
        }
    }

    public class ScalesFlowMeters : Entity, ICloneable
    {
        public ScalesFlowMeters()
        {

        }
        public ScalesFlowMeters(int id, string description, bool? isCalibrated, string reviewedBy,
            DateTime? reviewedDate, int conditioningOrderId, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.Description = description;
            this.IsCalibrated = isCalibrated;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.ConditioningOrderId = conditioningOrderId;
            this.ConditioningOrder = ConditioningOrder;
        }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
        public bool? IsCalibrated { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ConditioningOrderId { get; set; }
        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as ScalesFlowMeters;
            var old = objectToCompareOld as ScalesFlowMeters;
            if (old.Description != current.Description)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Básculas y flujometros - Campo -  Calibración de la báscula / flujometro",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Description,
                    NewValue = current.Description,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.IsCalibrated != current.IsCalibrated)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Básculas y flujometros - Campo -  Estado de calibración",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.IsCalibrated.HasValue ? old.IsCalibrated.Value == true ? "SI" : "NO" : null,
                    NewValue = current.IsCalibrated.HasValue ? current.IsCalibrated.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Básculas y flujometros - Campo -  Revisado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Básculas y flujometros - Campo -  Fecha de revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                    NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new ScalesFlowMeters(this.Id, this.Description, this.IsCalibrated, this.ReviewedBy, this.ReviewedDate,
                this.ConditioningOrderId, this.ConditioningOrder);
        }
    }
    public class EquipmentProcessConditioning : Entity, ICloneable
    {
        public EquipmentProcessConditioning()
        {

        }
        public EquipmentProcessConditioning(int id, string pipeNumber, string tourNumber, string bay, string bomb,
            string hosefill, string hoseDownload, string reviewedBy, DateTime? reviewedDate,
            string notes, int conditioningOrderId, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.PipeNumber = pipeNumber;
            this.TourNumber = tourNumber;
            this.Bay = bay;
            this.Bomb = bomb;
            this.Hosefill = hosefill;
            this.HoseDownload = hoseDownload;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.Notes = notes;
            this.ConditioningOrderId = conditioningOrderId;
            this.ConditioningOrder = ConditioningOrder;
        }

        [Column(TypeName = "varchar(150)")]
        public string TourNumber { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string PipeNumber { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Bay { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Bomb { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Hosefill { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string HoseDownload { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        [Column(TypeName = "varchar(400)")]
        public string Notes { get; set; }
        public int ConditioningOrderId { get; set; }
        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as EquipmentProcessConditioning;
            var old = objectToCompareOld as EquipmentProcessConditioning;
            if (old.PipeNumber != current.PipeNumber)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo -  numero de pipa",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.PipeNumber,
                    NewValue = current.PipeNumber,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.TourNumber != current.TourNumber)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - TourNumber",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.TourNumber,
                    NewValue = current.TourNumber,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Bay != current.Bay)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - bahía",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Bay,
                    NewValue = current.Bay,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Bomb != current.Bomb)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Bomba de llenado",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Bomb,
                    NewValue = current.Bomb,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Hosefill != current.Hosefill)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Manguera de llenado",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Hosefill,
                    NewValue = current.Hosefill,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.HoseDownload != current.HoseDownload)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Manguera de descarga",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.HoseDownload,
                    NewValue = current.HoseDownload,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Revisado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Fecha de revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                    NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 4: Equipos (accesorios) empleados en el proceso de acondicionamiento - Campo - Observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new EquipmentProcessConditioning(this.Id, this.PipeNumber, this.TourNumber, this.Bay, this.Bomb,
                this.Hosefill, this.HoseDownload, this.ReviewedBy, this.ReviewedDate, this.Notes,
                this.ConditioningOrderId, this.ConditioningOrder);
        }
    }

    public class PerformanceProcessConditioning : Entity, ICloneable
    {
        public PerformanceProcessConditioning()
        {

        }
        public PerformanceProcessConditioning(int id, string sizeLote, string totalTons, string difTons,
            string reviewedBy, DateTime? reviewedDate, string notes,
            int conditioningOrderId, string pipeNumber, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.SizeLote = sizeLote;
            this.TotalTons = totalTons;
            this.DifTons = difTons;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.Notes = notes;
            this.ConditioningOrderId = conditioningOrderId;
            this.PipeNumber = pipeNumber;
            this.ConditioningOrder = conditioningOrder;
        }

        [Column(TypeName = "varchar(150)")]
        public string SizeLote { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string TotalTons { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string DifTons { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string Notes { get; set; }
        public int ConditioningOrderId { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string TourNumber { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string PipeNumber { get; set; }
        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as PerformanceProcessConditioning;
            var old = objectToCompareOld as PerformanceProcessConditioning;
            if (old.SizeLote != current.SizeLote)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo - Tamaño del lote",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.SizeLote,
                    NewValue = current.SizeLote,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.TotalTons != current.TotalTons)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo -Cantidad total de producto de grado medicinal llenado en unidades de distribución (Kg)",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.TotalTons,
                    NewValue = current.TotalTons,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DifTons != current.DifTons)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo - Diferencia contra el tamaño del lote",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.DifTons,
                    NewValue = current.DifTons,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo - Revisado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo - Fecha de revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                    NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 6: Rendimiento del proceso de acondicionamiento - Campo - Observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PerformanceProcessConditioning(this.Id, this.SizeLote, this.TotalTons, this.DifTons, this.ReviewedBy, this.ReviewedDate,
                this.Notes, this.ConditioningOrderId, this.PipeNumber, this.ConditioningOrder);
        }
    }
    public class PipelineClearanceOA : Entity, ICloneable
    {

        public PipelineClearanceOA(int Id, int conditioningOrderId, bool? inCompliance, string notes, string reviewedBy,
            DateTime? reviewedDate, string bill, string activitie, string reviewedBySecond,
            DateTime? reviewedDateSecond, string notesSecond, ConditioningOrder conditioningOrder)
        {
            this.Id = Id;
            this.ConditioningOrderId = conditioningOrderId;
            this.InCompliance = inCompliance;
            this.Notes = notes;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.Bill = bill;
            this.Activitie = activitie;
            this.ReviewedBySecond = reviewedBySecond;
            this.ReviewedDateSecond = reviewedDateSecond;
            this.NotesSecond = notesSecond;
            this.ConditioningOrder = conditioningOrder;
        }
        public PipelineClearanceOA()
        {

        }

        public int ConditioningOrderId { get; set; }
        public bool? InCompliance { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string Bill { get; set; }
        public string Activitie { get; set; }
        public string ReviewedBySecond { get; set; }
        public DateTime? ReviewedDateSecond { get; set; }
        public string NotesSecond { get; set; }

        //-----------------------------
        //Relationships
        public ConditioningOrder ConditioningOrder { get; set; }

        public static PipelineClearanceOA Create(
            int id,
            int conditioningOrderId,
            bool? inCompliance,
            string notes,
            string reviewedBy,
            DateTime? reviewedDate,
            string bill,
            string activitie,
            string reviewedBySecond,
            DateTime? reviewedDateSecond
            )
        {
            var entity = new PipelineClearanceOA
            {
                Id = id,
                ConditioningOrderId = conditioningOrderId,
                InCompliance = inCompliance,
                Notes = notes,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate,
                Bill = bill,
                Activitie = activitie,
                ReviewedBySecond = reviewedBySecond,
                ReviewedDateSecond = reviewedDateSecond
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as PipelineClearanceOA;
            var current = objectToCompare as PipelineClearanceOA;
            if (old.InCompliance != current.InCompliance)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Cumple",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI" : "NO" : null,
                    NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Revisdo por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Fecha revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.ToString(),
                    NewValue = current.ReviewedDate.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Bill != current.Bill)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - folio",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Bill,
                    NewValue = current.Bill,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBySecond != current.ReviewedBySecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Revisdo por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBySecond,
                    NewValue = current.ReviewedBySecond,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDateSecond != current.ReviewedDateSecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Fecha revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDateSecond.ToString(),
                    NewValue = current.ReviewedDateSecond.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.NotesSecond != current.NotesSecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Campo - Observaciones secundarias",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.NotesSecond,
                    NewValue = current.NotesSecond,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PipelineClearanceOA(this.Id, this.ConditioningOrderId, this.InCompliance, this.Notes, this.ReviewedBy, this.ReviewedDate,
            this.Bill, this.Activitie, this.ReviewedBySecond, this.ReviewedDateSecond, this.NotesSecond, this.ConditioningOrder);
        }
    }


    public class PipeFillingControl : Entity, ICloneable
    {
        public PipeFillingControl()
        {

        }
        public PipeFillingControl(int id, string tourNumber, int conditioningOrderId,
            List<PipeFilling> pipesList, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.TourNumber = tourNumber;
            this.ConditioningOrderId = conditioningOrderId;
            this.PipesList = pipesList;
            this.ConditioningOrder = conditioningOrder;
        }

        [Column(TypeName = "varchar(50)")]
        public string TourNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        public List<PipeFilling> PipesList { get; set; }
        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as PipeFillingControl;
            var current = objectToCompare as PipeFillingControl;
            if (old.TourNumber != current.TourNumber)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Campo -  Tabla 5: Control de llenado de pipas- TourNumber",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.TourNumber,
                    NewValue = current.TourNumber,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PipeFillingControl(this.Id, this.TourNumber, this.ConditioningOrderId, this.PipesList, this.ConditioningOrder);
        }
    }

    public class PipeFilling : Entity, ICloneable
    {
        public PipeFilling()
        {

        }
        public PipeFilling(int id, string pipeNumber, string checkListStatus, bool? checkListIncompliance,
            DateTime? date, string initialWeight, string finalWeight, decimal diffWeight,
            string analyzedBy, DateTime? analyzedDate, DateTime? initialAnalyzedDate, DateTime? finalAnalyzedDate, DateTime? dueDate,
            string distributionBatch, bool? inCompliance, string reportPNCFolio, string reportPNCNotes, bool? isReleased,
            string releasedBy, DateTime? releasedDate, int pipeFillingControlId, ConditioningOrder conditioningOrder)
        {
            this.Id = id;
            this.PipeNumber = pipeNumber;
            this.CheckListStatus = checkListStatus;
            this.CheckListIncompliance = checkListIncompliance;
            this.Date = date;
            this.InitialWeight = initialWeight;
            this.FinalWeight = finalWeight;
            this.DiffWeight = diffWeight;
            this.AnalyzedBy = analyzedBy;
            this.AnalyzedDate = analyzedDate;
            this.InitialAnalyzedDate = initialAnalyzedDate;
            this.FinalAnalyzedDate = finalAnalyzedDate;
            this.DueDate = dueDate;
            this.DistributionBatch = distributionBatch;
            this.InCompliance = inCompliance;
            this.ReportPNCFolio = reportPNCFolio;
            this.ReportPNCNotes = reportPNCNotes;
            this.IsReleased = isReleased;
            this.ReleasedBy = releasedBy;
            this.ReleasedDate = releasedDate;
            this.PipeFillingControlId = pipeFillingControlId;
            this.ConditioningOrder = ConditioningOrder;
        }

        [Column(TypeName = "varchar(180)")]
        public string PipeNumber { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string CheckListStatus { get; set; }
        public bool? CheckListIncompliance { get; set; }
        public DateTime? Date { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string InitialWeight { get; set; }
        [Column(TypeName = "varchar(180)")]
        public string FinalWeight { get; set; }
        [Column(TypeName = "varchar(180)")]
        public decimal DiffWeight { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string AnalyzedBy { get; set; }
        public DateTime? AnalyzedDate { get; set; }
        public DateTime? InitialAnalyzedDate { get; set; }
        public DateTime? FinalAnalyzedDate { get; set; }
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string DistributionBatch { get; set; }
        public bool? InCompliance { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ReportPNCFolio { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ReportPNCNotes { get; set; }
        public bool? IsReleased { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int PipeFillingControlId { get; set; }
        public ConditioningOrder ConditioningOrder { get; set; }
        public int ConditioningOrderId { get; set; }
        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as PipeFilling;
            var current = objectToCompare as PipeFilling;
            //if (old.CheckListStatus != current.CheckListStatus)
            //{
            //    auditList.Add(new ReportAuditTrail
            //    {
            //        Action = "Actualizar",
            //        Controller = "ConditioningOrder",
            //        Date = DateTime.Now,
            //        Detail = "Tabla 5: Control de llenado de pipas - Campo - CheckList Estatus",
            //        Funcionality = "Orden de acondicionamiento",
            //        PreviousValue = old.CheckListStatus,
            //        NewValue = current.CheckListStatus,
            //        Method = "UpdateAsync",
            //        Plant = current.ConditioningOrder.PlantId,
            //        Product = current.ConditioningOrder.ProductId,
            //        User = current.ConditioningOrder.CreatedBy,
            //        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
            //        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
            //    });
            //}
            if (old.CheckListIncompliance != current.CheckListIncompliance)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - CheckList en cumplimiento",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.CheckListIncompliance.HasValue ? old.CheckListIncompliance.Value.ToString() : null,
                    NewValue = current.CheckListIncompliance.HasValue ? current.CheckListIncompliance.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.AnalyzedBy != current.AnalyzedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Analizado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.AnalyzedBy,
                    NewValue = current.AnalyzedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.AnalyzedDate != current.AnalyzedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Fecha de analizado",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.AnalyzedDate.HasValue ? old.AnalyzedDate.Value.ToString() : null,
                    NewValue = current.AnalyzedDate.HasValue ? current.AnalyzedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Date != current.Date)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Fecha",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Date.HasValue ? old.Date.Value.ToString() : null,
                    NewValue = current.Date.HasValue ? current.Date.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.InitialWeight != current.InitialWeight)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Peso inicial",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.InitialWeight,
                    NewValue = current.InitialWeight,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FinalWeight != current.FinalWeight)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Peso final",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.FinalWeight,
                    NewValue = current.FinalWeight,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DiffWeight != current.DiffWeight)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Cantidad de producto llenado",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.FinalWeight,
                    NewValue = current.FinalWeight,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.InitialAnalyzedDate != current.InitialAnalyzedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Hora de análisis inicial de pipa",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.InitialAnalyzedDate.HasValue ? old.InitialAnalyzedDate.Value.ToString() : null,
                    NewValue = current.InitialAnalyzedDate.HasValue ? current.InitialAnalyzedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.FinalAnalyzedDate != current.FinalAnalyzedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Hora de análisis final de pipa",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.FinalAnalyzedDate.HasValue ? old.FinalAnalyzedDate.Value.ToString() : null,
                    NewValue = current.FinalAnalyzedDate.HasValue ? current.FinalAnalyzedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DistributionBatch != current.DistributionBatch)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Lote de distribución",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.DistributionBatch,
                    NewValue = current.DistributionBatch,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DueDate != current.DueDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Fecha de caducidad",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.DueDate.HasValue ? old.DueDate.Value.ToString() : null,
                    NewValue = current.DueDate.HasValue ? current.DueDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.InCompliance != current.InCompliance)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - En cumplimiento",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI" : "NO" : null,
                    NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReportPNCFolio != current.ReportPNCFolio)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Reporte de PNC/folio",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReportPNCFolio,
                    NewValue = current.ReportPNCFolio,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReportPNCNotes != current.ReportPNCNotes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Reporte de PNC/observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReportPNCNotes,
                    NewValue = current.ReportPNCNotes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.IsReleased != current.IsReleased)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Dictamen liberado/rechazado",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.IsReleased.HasValue ? old.IsReleased.Value == true ? "SI" : "NO" : null,
                    NewValue = current.IsReleased.HasValue ? current.IsReleased.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReleasedBy != current.ReleasedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Liberado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReleasedBy,
                    NewValue = current.ReleasedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReleasedDate != current.ReleasedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Fecha de liberación",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReleasedDate.HasValue ? old.ReleasedDate.Value.ToString() : null,
                    NewValue = current.ReleasedDate.HasValue ? current.ReleasedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PipeFilling(this.Id, this.PipeNumber, this.CheckListStatus, this.CheckListIncompliance, this.Date,
                this.InitialWeight, this.FinalWeight, this.DiffWeight, this.AnalyzedBy, this.AnalyzedDate,this.InitialAnalyzedDate, this.FinalAnalyzedDate,
                this.DueDate, this.DistributionBatch, this.InCompliance, this.ReportPNCFolio, this.ReportPNCNotes, this.IsReleased,
                this.ReleasedBy, this.ReleasedDate, this.PipeFillingControlId, this.ConditioningOrder);
        }
    }

    public class PipeFillingAnalysis : Entity, ICloneable
    {
        public PipeFillingAnalysis()
        {

        }
        public PipeFillingAnalysis(int id, string parameterName, string valueExpected, string valueReal,
            string measureUnit, string type, string distributionBatch,
            string pipeNumber, int conditioningOrderId, string unique, string pathFile, ConditioningOrder conditioningOrder)    //AHF
        {
            this.Id = id;
            this.ParameterName = parameterName;
            this.ValueExpected = valueExpected;
            this.ValueReal = valueReal;
            this.MeasureUnit = measureUnit;
            this.Type = type;
            this.DistributionBatch = distributionBatch;
            this.PipeNumber = pipeNumber;
            this.ConditioningOrderId = conditioningOrderId;
            this.Unique = unique;
            this.PathFile = pathFile;   //AHF
            this.ConditioningOrder = conditioningOrder;
        }

        [Column(TypeName = "varchar(150)")]
        public string ParameterName { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string ValueExpected { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string ValueReal { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string MeasureUnit { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Type { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string PipeNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Unique { get; set; }
        [Column(TypeName = "varchar(300)")]
        public string PathFile { get; set; }    //AHF

        public ConditioningOrder ConditioningOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as PipeFillingAnalysis;
            var current = objectToCompare as PipeFillingAnalysis;
            if (current.Type == PipeFillingAnalysisType.InitialAnalysis.Value)
            {
                if (old.ValueExpected != current.ValueExpected)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis inicial Valor esperado",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ValueExpected,
                        NewValue = current.ValueExpected,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
                if (old.ValueReal != current.ValueReal)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis inicial Valor real",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ValueReal,
                        NewValue = current.ValueReal,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
                if (old.MeasureUnit != current.MeasureUnit)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis inicial unidad de medida",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.MeasureUnit,
                        NewValue = current.MeasureUnit,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
            }
            else
            {
                if (old.ValueExpected != current.ValueExpected)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis final Valor esperado",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ValueExpected,
                        NewValue = current.ValueExpected,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
                if (old.ValueReal != current.ValueReal)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis final Valor real",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.ValueReal,
                        NewValue = current.ValueReal,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
                if (old.MeasureUnit != current.MeasureUnit)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ConditioningOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Control de llenado de pipas - Campo - Análisis final unidad de medida",
                        Funcionality = "Orden de acondicionamiento",
                        PreviousValue = old.MeasureUnit,
                        NewValue = current.MeasureUnit,
                        Method = "UpdateAsync",
                        Plant = current.ConditioningOrder.PlantId,
                        Product = current.ConditioningOrder.ProductId,
                        User = current.ConditioningOrder.CreatedBy,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                    });
                }
            }
            return new List<ReportAuditTrail>();
        }

        public object Clone()
        {
            return new PipeFillingAnalysis(this.Id, this.ParameterName, this.ValueExpected, this.ValueReal,
                this.MeasureUnit, this.Type, this.DistributionBatch, this.PipeNumber,
                this.ConditioningOrderId, this.Unique, this.PathFile, this.ConditioningOrder); //AHF
        }
    }
    public class PipeFillingCustomer : Entity, ICloneable
    {
        public PipeFillingCustomer()
        {

        }
        public PipeFillingCustomer(int id, string tank, string name, string deliveryNumber, string reviewedBy,
            DateTime? reviewedDate, string analysisReport, string emailsList, string notes,
            bool? inCompliance, string distributionBatch, string tourNumber, int conditioningOrderId,
            string plantIdentificador, string productId, string tankId, string folio, ConditioningOrder conditioningOrder, bool? EmailListSend)
        {
            this.Id = id;
            this.Tank = tank;
            this.Name = name;
            this.DeliveryNumber = deliveryNumber;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.AnalysisReport = analysisReport;
            this.EmailsList = emailsList;
            this.Notes = notes;
            this.InCompliance = inCompliance;
            this.DistributionBatch = distributionBatch;
            this.TourNumber = tourNumber;
            this.ConditioningOrderId = conditioningOrderId;
            this.PlantIdentificador = plantIdentificador;
            this.ProductId = productId;
            this.TankId = tankId;
            this.Folio = folio;
            this.ConditioningOrder = conditioningOrder;
            this.EmailListSend = EmailListSend;
        }

        [Column(TypeName = "varchar(150)")]
        public string Tank { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string DeliveryNumber { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string AnalysisReport { get; set; }
        public string EmailsList { get; set; }
        public string Notes { get; set; }
        public bool? InCompliance { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string DistributionBatch { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string TourNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string PlantIdentificador { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ProductId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string TankId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Folio { get; set; }
        public ConditioningOrder ConditioningOrder { get; set; }
        public bool? EmailListSend { get; set; }

        public bool? State { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var current = objectToCompare as PipeFillingCustomer;
            var old = objectToCompareOld as PipeFillingCustomer;
            if (old.Tank != current.Tank)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Tanque",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Tank,
                    NewValue = current.Tank,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Name != current.Name)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Nombre del cliente",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Name,
                    NewValue = current.Name,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.InCompliance != current.InCompliance)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo -- En cumplimiento",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI" : "NO" : null,
                    NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Revisado por",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Fecha de revisión",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                    NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.EmailsList != current.EmailsList)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Enviar por correo electrónico el certificado de análisis COC-4",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.EmailsList,
                    NewValue = current.EmailsList,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Observaciones",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.DeliveryNumber != current.DeliveryNumber)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - numero entrega",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.DeliveryNumber,
                    NewValue = current.DeliveryNumber,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            if (old.Folio != current.Folio)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ConditioningOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 5: Control de llenado de pipas - Campo - Folio",
                    Funcionality = "Orden de acondicionamiento",
                    PreviousValue = old.Folio,
                    NewValue = current.Folio,
                    Method = "UpdateAsync",
                    Plant = current.ConditioningOrder.PlantId,
                    Product = current.ConditioningOrder.ProductId,
                    User = current.ConditioningOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                    ProductionOrderId = current.ConditioningOrder.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PipeFillingCustomer(this.Id, this.Tank, this.Name, this.DeliveryNumber, this.ReviewedBy, this.ReviewedDate, this.AnalysisReport,
                this.EmailsList, this.Notes, this.InCompliance, this.DistributionBatch, this.TourNumber, this.ConditioningOrderId, this.PlantIdentificador,
                this.ProductId, this.TankId, this.Folio, this.ConditioningOrder, this.EmailListSend);
        }
    }

    public class PipeFillingAnalysisType
    {
        private PipeFillingAnalysisType(string value) { Value = value; }

        public string Value { get; private set; }

        public static PipeFillingAnalysisType InitialAnalysis { get { return new PipeFillingAnalysisType("InitialAnalysis"); } }
        public static PipeFillingAnalysisType FinalAnalysis { get { return new PipeFillingAnalysisType("FinalAnalysis"); } }

    }

}