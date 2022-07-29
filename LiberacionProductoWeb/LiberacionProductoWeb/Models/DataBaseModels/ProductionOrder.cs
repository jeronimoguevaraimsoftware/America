using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ProductionOrder : Entity, ICloneable
    {
        public ProductionOrder()
        {

        }

        public ProductionOrder(int id, DateTime? endDate, string reasonReject, bool? isReleased, string releasedNotes, string state, string stepSavedDescription, bool? inCompliance, string delegateUser)
        {
            this.Id = id;
            this.EndDate = endDate;
            this.ReasonReject = reasonReject;
            this.IsReleased = isReleased;
            this.ReleasedNotes = releasedNotes;
            this.State = state;
            this.StepSavedDescription = stepSavedDescription;
            this.InCompliance = inCompliance;
            this.DelegateUser = delegateUser;
        }

        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public string TankId { get; set; }
        public string Purity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }
        public string State { get; set; }
        public int StepSaved { get; set; }
        public string StepSavedDescription { get; set; }
        public bool? InCompliance { get; set; }
        public string DelegateUser { get; set; }
        public DateTime? EndDate { get; set; }
        public string ReasonReject { get; set; }
        //public PipelineClearance PipelineClearance { get; set; }

        public static ProductionOrder Create(
            int id,
            string plantId,
            string productId,
            String tankId,
            string purity,
            string createdBy,
            string stepSavedDescription,
            bool inCompliance,
            string delegateUser)
        {
            var entity = new ProductionOrder
            {
                Id = id,
                PlantId = plantId,
                ProductId = productId,
                TankId = tankId,
                Purity = !string.IsNullOrEmpty(purity) ? purity : null,
                State = "OP-En proceso",
                StepSaved = 0,
                CreatedBy = createdBy,
                CreatedDate = DateTime.Now,
                StepSavedDescription = stepSavedDescription,
                InCompliance = inCompliance,
                DelegateUser = delegateUser
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();

            var current = objectToCompare as ProductionOrder;
            var old = objectToCompareOld as ProductionOrder;
            if (old != null)
            {
                if (old.EndDate != current.EndDate)
                {
                    if (old.State != current.State)
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 9: Rendimiento del proceso y Liberación del producto - Campo - Fecha término",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.EndDate.ToString(),
                            NewValue = current.EndDate.ToString(),
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Orden de producción- Campo - Fecha término",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.EndDate.ToString(),
                            NewValue = current.EndDate.ToString(),
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
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
                //        Action = "Cancelar orden de producción",
                //        Controller = "ProductionOrder",
                //        Date = DateTime.Now,
                //        Detail = "Orden de producción - Campo - Motivo cancelación",
                //        Funcionality = "Orden de producción",
                //        PreviousValue = current.ReasonReject,
                //        NewValue = old.ReasonReject,
                //        Method = "UpdateAsync",
                //        Plant = PlantId,
                //        Product = ProductId,
                //        User = DelegateUser,
                //        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                //        ProductionOrderId = current.Id
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
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 9: Rendimiento del proceso y Liberación del producto - Campo - Liberación",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.IsReleased.HasValue ? old.IsReleased == true ? "SI" : "NO" : null,
                            NewValue = current.IsReleased.HasValue ? current.IsReleased == true ? "SI" : "NO" : null,
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Campo - Liberación",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.IsReleased.ToString(),
                            NewValue = current.IsReleased.ToString(),
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
                        });
                    }
                }
            }
            //if (old != null)
            //{
            //    if (old.State != current.State)
            //    {
            //        if (current.State == ProductionOrderStatus.Released.Value)
            //        {
            //            auditList.Add(new ReportAuditTrail
            //            {
            //                Action = "Actualizar",
            //                Controller = "ProductionOrder",
            //                Date = DateTime.Now,
            //                Detail = "Tabla 9: Rendimiento del proceso y Liberación del producto - Campo - Estado",
            //                Funcionality = "Orden de producción",
            //                PreviousValue = old.State,
            //                NewValue = current.State,
            //                Method = "UpdateAsync",
            //                Plant = PlantId,
            //                Product = ProductId,
            //                User = DelegateUser,
            //                DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
            //                ProductionOrderId = current.Id
            //            });
            //        }
            //        else
            //        {
            //            auditList.Add(new ReportAuditTrail
            //            {
            //                Action = "Actualizar",
            //                Controller = "ProductionOrder",
            //                Date = DateTime.Now,
            //                Detail = "Orden de producción - Campo - Estado",
            //                Funcionality = "Orden de producción",
            //                PreviousValue = old.State,
            //                NewValue = current.State,
            //                Method = "UpdateAsync",
            //                Plant = PlantId,
            //                Product = ProductId,
            //                User = DelegateUser,
            //                DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
            //                ProductionOrderId = current.Id
            //            });
            //        }
            //    }
            //}

            if (old != null)
            {
                if (old.ReleasedNotes != current.ReleasedNotes)
                {
                    if (current.State == ProductionOrderStatus.Released.Value)
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Tabla 9: Rendimiento del proceso y Liberación del producto - Campo - Observaciones",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.ReleasedNotes,
                            NewValue = current.ReleasedNotes,
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
                        });
                    }
                    else
                    {
                        auditList.Add(new ReportAuditTrail
                        {
                            Action = "Modificación",
                            Controller = "ProductionOrder",
                            Date = DateTime.Now,
                            Detail = "Orden de producción - Campo - Observaciones",
                            Funcionality = "Orden de producción",
                            PreviousValue = old.ReleasedNotes,
                            NewValue = current.ReleasedNotes,
                            Method = "UpdateAsync",
                            Plant = PlantId,
                            Product = ProductId,
                            User = DelegateUser,
                            DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                            ProductionOrderId = current.Id
                        });
                    }
                }
            }

            if (old != null)
            {
                if (current.State == ProductionOrderStatus.Released.Value)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 9: Rendimiento del proceso y Liberación del producto - Campo - usuario firma",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReleasedBy,
                        NewValue = current.ReleasedBy,
                        Method = "UpdateAsync",
                        Plant = PlantId,
                        Product = ProductId,
                        User = DelegateUser,
                        DistribuitionBatch = !string.IsNullOrEmpty(DistribuitionBatch) ? DistribuitionBatch : null,
                        ProductionOrderId = current.Id
                    });
                }

            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new ProductionOrder
            {
                Id = this.Id,
                CreatedBy = this.CreatedBy,
                CreatedDate = this.CreatedDate,
                StepSaved = this.StepSaved,
                PlantId = this.PlantId,
                ProductId = this.ProductId,
                Purity = this.Purity,
                ReleasedBy = this.ReleasedBy,
                ReleasedDate = this.ReleasedDate,
                TankId = this.TankId,
                EndDate = this.EndDate,
                ReasonReject = this.ReasonReject,
                IsReleased = this.IsReleased,
                ReleasedNotes = this.ReleasedNotes,
                State = this.State,
                StepSavedDescription = this.StepSavedDescription,
                InCompliance = this.InCompliance,
                DelegateUser = this.DelegateUser
            };
        }
    }

    public class ProductionEquipment : Entity, ICloneable
    {
        public ProductionEquipment()
        {

        }
        public ProductionEquipment(int id, int productionOrderId, bool? isAvailable, string reviewedBy, DateTime? reviewedDate, ProductionOrder productionOrder)
        {
            this.Id = id;
            this.ProductionOrderId = productionOrderId;
            this.IsAvailable = isAvailable;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.ProductionOrder = productionOrder;
        }

        public int ProductionOrderId { get; set; }
        public bool? IsAvailable { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }

        public static ProductionEquipment Create(
            int id,
            int productionOrderId,
            bool? isAvailable,
            string reviewedBy,
            DateTime? reviewedDate
            )
        {
            var entity = new ProductionEquipment
            {
                Id = id,
                ProductionOrderId = productionOrderId,
                IsAvailable = isAvailable,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ProductionEquipment;
            var current = objectToCompare as ProductionEquipment;
            if (old.IsAvailable != current.IsAvailable)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos de producción - Campo - Equipos de proceso - Disponible",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.IsAvailable.HasValue ?
                                    old.IsAvailable.Value == true ? "SI " : "NO" : null,
                    NewValue = current.IsAvailable.HasValue ?
                                    current.IsAvailable.Value == true ? "SI " : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos de producción - Campo - Equipos de proceso - Revisado por",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 1: Equipos de producción - Campo - Equipos de proceso - Fecha revisión",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedDate.ToString(),
                    NewValue = current.ReviewedDate.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }

            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new ProductionEquipment(this.Id, this.ProductionOrderId, this.IsAvailable, this.ReviewedBy, this.ReviewedDate, this.ProductionOrder);
        }
    }

    public class MonitoringEquipment : Entity, ICloneable
    {
        public MonitoringEquipment(int id, int productionOrderId, string code, string description, bool? isCalibrated, string notes,
            string reviewedBy, DateTime? reviewedDate, ProductionOrder productionOrder)
        {
            this.Id = id;
            this.ProductionOrderId = productionOrderId;
            this.Code = code;
            this.Description = description;
            this.IsCalibrated = isCalibrated;
            this.Notes = notes;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.ProductionOrder = productionOrder;
        }
        public MonitoringEquipment()
        {

        }
        public int ProductionOrderId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? IsCalibrated { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }

        public static MonitoringEquipment Create(
            int id,
            int productionOrderId,
            string code,
            string description,
            bool? isCalibrated,
            string notes,
            string reviewedBy,
            DateTime? reviewedDate
            )
        {
            var entity = new MonitoringEquipment
            {
                Id = id,
                ProductionOrderId = productionOrderId,
                Code = code,
                Description = description,
                IsCalibrated = isCalibrated,
                Notes = notes,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as MonitoringEquipment;
            var current = objectToCompare as MonitoringEquipment;
            if (old.Code != current.Code)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Código",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Code,
                    NewValue = current.Code,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Description != current.Description)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Descripción",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Description,
                    NewValue = current.Description,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.IsCalibrated != current.IsCalibrated)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Calibrado",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.IsCalibrated.HasValue ? old.IsCalibrated.Value == true ? "SI " : "NO" : null,
                    NewValue = current.IsCalibrated.HasValue ? current.IsCalibrated.Value == true ? "SI " : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Observaciones",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Revisado por",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 2: Equipos de monitoreo - Campo - Equipos de monitoreo - Fecha revisión",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedDate.ToString(),
                    NewValue = current.ReviewedDate.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new MonitoringEquipment
            {
                Id = this.Id,
                ProductionOrderId = this.ProductionOrderId,
                Code = this.Code,
                Description = this.Description,
                IsCalibrated = this.IsCalibrated,
                Notes = this.Notes,
                ReviewedBy = this.ReviewedBy,
                ReviewedDate = this.ReviewedDate,
                ProductionOrder = this.ProductionOrder
            };
        }
    }

    public class PipelineClearance : Entity, ICloneable
    {
        public PipelineClearance(int Id, int productionOrderId, bool? inCompliance, string notes, string reviewedBy, DateTime? reviewedDate,
            string bill, string activitie, string reviewedBySecond, DateTime? reviewedDateSecond,
            string notesSecond, DateTime? productionStartedDate, DateTime? productionEndDate, ProductionOrder productionOrder)
        {
            this.Id = Id;
            this.ProductionOrderId = productionOrderId;
            this.InCompliance = inCompliance;
            this.Notes = notes;
            this.ReviewedBy = reviewedBy;
            this.ReviewedDate = reviewedDate;
            this.Bill = bill;
            this.Activitie = activitie;
            this.ReviewedBySecond = reviewedBySecond;
            this.ReviewedDateSecond = reviewedDateSecond;
            this.NotesSecond = notesSecond;
            this.ProductionStartedDate = productionStartedDate;
            this.ProductionOrder = productionOrder;
            this.ProductionEndDate = productionEndDate;
        }
        public PipelineClearance()
        {

        }
        public int ProductionOrderId { get; set; }
        public bool? InCompliance { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string Bill { get; set; }
        public string Activitie { get; set; }
        public string ReviewedBySecond { get; set; }
        public DateTime? ReviewedDateSecond { get; set; }
        public string NotesSecond { get; set; }
        public DateTime? ProductionStartedDate { get; set; }
        public DateTime? ProductionEndDate { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }

        public static PipelineClearance Create(
            int id,
            int productionOrderId,
            bool? inCompliance,
            string notes,
            string reviewedBy,
            DateTime? reviewedDate,
            string bill,
            string activitie,
            string reviewedBySecond,
            DateTime? reviewedDateSecond,
            DateTime? productionStartedDate,
            DateTime? productionEndDate
            )
        {
            var entity = new PipelineClearance
            {
                Id = id,
                ProductionOrderId = productionOrderId,
                InCompliance = inCompliance,
                Notes = notes,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate,
                Bill = bill,
                Activitie = activitie,
                ReviewedBySecond = reviewedBySecond,
                ReviewedDateSecond = reviewedDateSecond,
                ProductionStartedDate = productionStartedDate,
                ProductionEndDate = productionEndDate
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as PipelineClearance;
            var current = objectToCompare as PipelineClearance;
            if (old.InCompliance != current.InCompliance)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea Campo - Cumple",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI " : "NO" : null,
                    NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI " : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedBy != current.ReviewedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea Campo - Revisado por",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedBy,
                    NewValue = current.ReviewedBy,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedDate != current.ReviewedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea Campo - Despeje de línea  - Fecha revisión",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedDate.ToString(),
                    NewValue = current.ReviewedDate.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Bill != current.Bill)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de líne - Campo - Despeje de línea  - folio",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Bill,
                    NewValue = current.Bill,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Notes != current.Notes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de líne - Campo - Despeje de línea  - Observaciones",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Notes,
                    NewValue = current.Notes,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedBySecond != current.ReviewedBySecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Revisado por",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedBySecond,
                    NewValue = current.ReviewedBySecond,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReviewedDateSecond != current.ReviewedDateSecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Fecha revisión",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReviewedDateSecond.ToString(),
                    NewValue = current.ReviewedDateSecond.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.NotesSecond != current.NotesSecond)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Observaciones secundarias",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.NotesSecond,
                    NewValue = current.NotesSecond,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ProductionEndDate != current.ProductionEndDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 3: Despeje de línea - Observaciones secundarias - Fecha revisión",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ProductionEndDate?.ToString(),
                    NewValue = current.ProductionEndDate?.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new PipelineClearance(this.Id, this.ProductionOrderId, this.InCompliance, this.Notes, this.ReviewedBy, this.ReviewedDate, this.Bill,
                this.Activitie, this.ReviewedBySecond, this.ReviewedDateSecond, this.NotesSecond, this.ProductionStartedDate, this.ProductionEndDate, this.ProductionOrder);
        }
    }

    public class ProductionOrderAttribute : Entity
    {
        public int ProductionOrderId { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string Variable { get; set; }
        public string Specification { get; set; }
        public string ChartPath { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string AvgValue { get; set; }
        public bool? InCompliance { get; set; }
        public string DeviationReportFolio { get; set; }
        public string DeviationReportNotes { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public ProductionOrderAttributeType Type { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }

        public static ProductionOrderAttribute Create(
            int id,
            int productionOrderId,
            string area,
            string description,
            string variable,
            string specification,
            string chartPath,
            string maxValue,
            string minValue,
            string avgValue,
            string deviationReportFolio,
            string deviationReportNotes,
            bool? inCompliance,
            string notes,
            string reviewedBy,
            DateTime? reviewedDate,
            ProductionOrderAttributeType type
            )
        {
            var entity = new ProductionOrderAttribute
            {
                Id = id,
                ProductionOrderId = productionOrderId,
                Area = area,
                Description = description,
                Variable = variable,
                Specification = specification,
                ChartPath = chartPath,
                MaxValue = maxValue,
                MinValue = minValue,
                AvgValue = avgValue,
                DeviationReportFolio = deviationReportFolio,
                DeviationReportNotes = deviationReportNotes,
                InCompliance = inCompliance,
                Notes = notes,
                ReviewedBy = reviewedBy,
                ReviewedDate = reviewedDate,
                Type = type,
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as ProductionOrderAttribute;
            var current = objectToCompare as ProductionOrderAttribute;
            if (current.Type == ProductionOrderAttributeType.ControlVariable)
            {
                if (old.Area != current.Area)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Area",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Area,
                        NewValue = current.Area,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Description != current.Description)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso- Campo - Variables de control en proceso - Descripción",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Description,
                        NewValue = current.Description,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Variable != current.Variable)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Variable",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Variable,
                        NewValue = current.Variable,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Specification != current.Specification)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Especificación",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Specification,
                        NewValue = current.Specification,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MaxValue != current.MaxValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Valor máximo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MaxValue,
                        NewValue = current.MaxValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MinValue != current.MinValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Valor mínimo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MinValue,
                        NewValue = current.MinValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.AvgValue != current.AvgValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Valor promedio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.AvgValue,
                        NewValue = current.AvgValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.InCompliance != current.InCompliance)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Cumple",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI " : "NO" : null,
                        NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI " : "NO" : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportFolio != current.DeviationReportFolio)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Informe de desviación folio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportFolio,
                        NewValue = current.DeviationReportFolio,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportNotes != current.DeviationReportNotes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso- Campo - Variables de control en proceso - Informe de desviación Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportNotes,
                        NewValue = current.DeviationReportNotes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedBy != current.ReviewedBy)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Revisado por",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedBy,
                        NewValue = current.ReviewedBy,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedDate != current.ReviewedDate)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Fecha de revisión",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                        NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Notes != current.Notes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 5: Variables de control en proceso - Campo - Variables de control en proceso - Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Notes,
                        NewValue = current.Notes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
            }
            if (current.Type == ProductionOrderAttributeType.CriticalQualityAttribute)
            {
                if (old.Area != current.Area)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Area",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Area,
                        NewValue = current.Area,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Description != current.Description)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Descripción",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Description,
                        NewValue = current.Description,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Variable != current.Variable)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad -Campo - Parámetros críticos de proceso - Variable",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Variable,
                        NewValue = current.Variable,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Specification != current.Specification)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad -Campo - Parámetros críticos de proceso - Especificación",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Specification,
                        NewValue = current.Specification,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MaxValue != current.MaxValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad- Campo - Parámetros críticos de proceso - Valor máximo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MaxValue,
                        NewValue = current.MaxValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MinValue != current.MinValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad -Campo - Parámetros críticos de proceso - Valor mínimo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MinValue,
                        NewValue = current.MinValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.AvgValue != current.AvgValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Valor promedio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.AvgValue,
                        NewValue = current.AvgValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.InCompliance != current.InCompliance)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Cumple",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI" : "NO" : null,
                        NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI" : "NO" : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportFolio != current.DeviationReportFolio)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Informe de desviación folio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportFolio,
                        NewValue = current.DeviationReportFolio,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportNotes != current.DeviationReportNotes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Informe de desviación Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportNotes,
                        NewValue = current.DeviationReportNotes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedBy != current.ReviewedBy)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Revisado por",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedBy,
                        NewValue = current.ReviewedBy,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedDate != current.ReviewedDate)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Fecha de revisión",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                        NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Notes != current.Notes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 7: Atributos críticos de calidad - Campo - Parámetros críticos de proceso - Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Notes,
                        NewValue = current.Notes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
            }
            if (current.Type == ProductionOrderAttributeType.CriticalParameter)
            {
                if (old.Area != current.Area)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Area",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Area,
                        NewValue = current.Area,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Description != current.Description)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Descripción",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Description,
                        NewValue = current.Description,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Variable != current.Variable)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Variable",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Variable,
                        NewValue = current.Variable,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Specification != current.Specification)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Especificación",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Specification,
                        NewValue = current.Specification,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MaxValue != current.MaxValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Valor máximo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MaxValue,
                        NewValue = current.MaxValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.MinValue != current.MinValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Valor mínimo",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.MinValue,
                        NewValue = current.MinValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.AvgValue != current.AvgValue)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Valor promedio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.AvgValue,
                        NewValue = current.AvgValue,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.InCompliance != current.InCompliance)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Cumple",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.InCompliance.HasValue ? old.InCompliance.Value == true ? "SI" : "NO" : null,
                        NewValue = current.InCompliance.HasValue ? current.InCompliance.Value == true ? "SI" : "NO" : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportFolio != current.DeviationReportFolio)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Informe de desviación folio",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportFolio,
                        NewValue = current.DeviationReportFolio,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.DeviationReportNotes != current.DeviationReportNotes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Informe de desviación Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.DeviationReportNotes,
                        NewValue = current.DeviationReportNotes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedBy != current.ReviewedBy)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Revisado por",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedBy,
                        NewValue = current.ReviewedBy,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.ReviewedDate != current.ReviewedDate)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Fecha de revisión",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.ReviewedDate.HasValue ? old.ReviewedDate.Value.ToString() : null,
                        NewValue = current.ReviewedDate.HasValue ? current.ReviewedDate.Value.ToString() : null,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
                if (old.Notes != current.Notes)
                {
                    auditList.Add(new ReportAuditTrail
                    {
                        Action = "Modificación",
                        Controller = "ProductionOrder",
                        Date = DateTime.Now,
                        Detail = "Tabla 6: Parámetros críticos de proceso - Campo - Atributos críticos de calidad - Observaciones",
                        Funcionality = "Orden de producción",
                        PreviousValue = old.Notes,
                        NewValue = current.Notes,
                        Method = "UpdateAsync",
                        Plant = current.ProductionOrder.PlantId,
                        Product = current.ProductionOrder.ProductId,
                        User = current.ProductionOrder.CreatedBy,
                        ProductionOrderId = current.ProductionOrderId
                    });
                }
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }
    }

    public class BatchDetails : Entity, ICloneable
    {
        public BatchDetails()
        {

        }
        public BatchDetails(int id, string number, decimal level, decimal size, string tank, DateTime? productionDate,
            string analyzedBy, DateTime? analyzedDate, bool? inCompliance, string notInComplianceFolio, bool? isReleased,
            string releasedBy, DateTime? releasedDate, string releasedNotes, List<BatchAnalysis> batchAnalysis)
        {
            this.Id = id;
            this.Number = number;
            this.Level = level;
            this.Size = size;
            this.Tank = tank;
            this.ProductionDate = productionDate;
            this.AnalyzedBy = analyzedBy;
            this.AnalyzedDate = analyzedDate;
            this.InCompliance = inCompliance;
            this.NotInComplianceFolio = notInComplianceFolio;
            this.IsReleased = isReleased;
            this.ReleasedBy = releasedBy;
            this.ReleasedDate = releasedDate;
            this.ReleasedNotes = releasedNotes;
            this.BatchAnalysis = batchAnalysis;
        }

        public int ProductionOrderId { get; set; }
        public string Number { get; set; }
        public decimal Level { get; set; }
        public decimal Size { get; set; }
        public string Tank { get; set; }
        public DateTime? ProductionDate { get; set; }
        public string AnalyzedBy { get; set; }
        public DateTime? AnalyzedDate { get; set; }
        public bool? InCompliance { get; set; }
        public string NotInComplianceFolio { get; set; }
        public string NotInComplianceNotes { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }

        //-----------------------------
        //Relationships
        public ProductionOrder ProductionOrder { get; set; }
        public List<BatchAnalysis> BatchAnalysis { get; set; }

        public static BatchDetails Create(
            int id,
            int productionOrderId,
            string number,
            decimal level,
            decimal size,
            string tank,
            DateTime? productionDate,
            string analyzedBy,
            DateTime? analyzedDate,
            bool? inCompliance,
            string notInComplianceFolio,
            string notInComplianceNotes,
            bool? isReleased,
            string releasedBy,
            DateTime? releasedDate,
            string releasedNotes,
            List<BatchAnalysis> batchAnalyses
            )
        {
            var entity = new BatchDetails
            {
                Id = id,
                ProductionOrderId = productionOrderId,
                Number = number,
                Level = level,
                Size = size,
                Tank = tank,
                ProductionDate = productionDate,
                AnalyzedBy = analyzedBy,
                AnalyzedDate = analyzedDate,
                InCompliance = inCompliance,
                NotInComplianceFolio = notInComplianceFolio,
                NotInComplianceNotes = notInComplianceNotes,
                IsReleased = isReleased,
                ReleasedBy = releasedBy,
                ReleasedDate = releasedDate,
                ReleasedNotes = releasedNotes,
                BatchAnalysis = batchAnalyses
            };
            return entity;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as BatchDetails;
            var current = objectToCompare as BatchDetails;
            if (old.IsReleased != current.IsReleased)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo - Producto conforme",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.IsReleased.HasValue ? old.IsReleased.Value == true ? "SI" : "NO" : null,
                    NewValue = current.IsReleased.HasValue ? current.IsReleased.Value == true ? "SI" : "NO" : null,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReleasedBy != current.ReleasedBy)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo -  Analizado por",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReleasedBy,
                    NewValue = current.ReleasedBy,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReleasedDate != current.ReleasedDate)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo -  Fecha y hora del análisis",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReleasedDate.HasValue ? old.ReleasedDate.Value.ToString() : null,
                    NewValue = current.ReleasedDate.HasValue ? current.ReleasedDate.Value.ToString() : null,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.ReleasedNotes != current.ReleasedNotes)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo -  Reporte de producto no conforme Observaciones",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.ReleasedNotes,
                    NewValue = current.ReleasedNotes,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.NotInComplianceFolio != current.NotInComplianceFolio)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo -  Reporte de producto no conforme Folio",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.NotInComplianceFolio,
                    NewValue = current.NotInComplianceFolio,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Number.Trim() != current.Number.Trim())
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo - Número de lote de producción",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Number.Trim(),
                    NewValue = current.Number.Trim(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Tank != current.Tank)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo - Número o código del tanque",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Tank,
                    NewValue = current.Tank,
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (old.Size != current.Size)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8: Lotificación y análisis de producto - Campo - Tamaño del lote",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Size.ToString(),
                    NewValue = current.Size.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            if (current.BatchAnalysis != null)
            {
                if (old.BatchAnalysis != current.BatchAnalysis)
                {
                    foreach (var item in current.BatchAnalysis)
                    {
                        if (old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName) && !x.LowerLimit.Equals(item.LowerLimit)).Any())
                        {
                            auditList.Add(new ReportAuditTrail
                            {
                                Action = "Modificación",
                                Controller = "ProductionOrder",
                                Date = DateTime.Now,
                                Detail = "Tabla 8:  Lotificación y análisis de producto - Campo - Análisis " + item.ParameterName + " límite inferior",
                                Funcionality = "Orden de producción",
                                PreviousValue = old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().LowerLimit,
                                NewValue = current.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().LowerLimit,
                                Method = "UpdateAsync",
                                Plant = current.ProductionOrder.PlantId,
                                Product = current.ProductionOrder.ProductId,
                                User = current.ProductionOrder.CreatedBy,
                                DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                                ProductionOrderId = current.ProductionOrderId
                            });

                          
                        }
                        if (old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName) && !x.UpperLimit.Equals(item.UpperLimit)).Any())
                        {
                            auditList.Add(new ReportAuditTrail
                            {
                                Action = "Modificación",
                                Controller = "ProductionOrder",
                                Date = DateTime.Now,
                                Detail = "Tabla 8:  Lotificación y análisis de producto - Campo - Análisis " + item.ParameterName + " límite superior",
                                Funcionality = "Orden de producción",
                                PreviousValue = old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().UpperLimit,
                                NewValue = current.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().UpperLimit,
                                Method = "UpdateAsync",
                                Plant = current.ProductionOrder.PlantId,
                                Product = current.ProductionOrder.ProductId,
                                User = current.ProductionOrder.CreatedBy,
                                DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                                ProductionOrderId = current.ProductionOrderId
                            });
                        }
                        if (old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName) && !x.Value.Equals(item.Value)).Any())
                        {
                            auditList.Add(new ReportAuditTrail
                            {
                                Action = "Modificación",
                                Controller = "ProductionOrder",
                                Date = DateTime.Now,
                                Detail = "Tabla 8:  Lotificación y análisis de producto - Campo - Análisis " + item.ParameterName + " límite valor",
                                Funcionality = "Orden de producción",
                                PreviousValue = old.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().Value,
                                NewValue = current.BatchAnalysis.Where(x => x.ParameterName.Equals(item.ParameterName)).FirstOrDefault().Value,
                                Method = "UpdateAsync",
                                Plant = current.ProductionOrder.PlantId,
                                Product = current.ProductionOrder.ProductId,
                                User = current.ProductionOrder.CreatedBy,
                                DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                                ProductionOrderId = current.ProductionOrderId
                            });
                        }
                    }

                }
            }
            if (old.Level != current.Level)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "ProductionOrder",
                    Date = DateTime.Now,
                    Detail = "Tabla 8:  Lotificación y análisis de producto - Campo - Nivel del tanque",
                    Funcionality = "Orden de producción",
                    PreviousValue = old.Level.ToString(),
                    NewValue = current.Level.ToString(),
                    Method = "UpdateAsync",
                    Plant = current.ProductionOrder.PlantId,
                    Product = current.ProductionOrder.ProductId,
                    User = current.ProductionOrder.CreatedBy,
                    DistribuitionBatch = !string.IsNullOrEmpty(current.Number) ? current.Number : null,
                    ProductionOrderId = current.ProductionOrderId
                });
            }
            return auditList.Where(x => !string.IsNullOrEmpty(x.PreviousValue?.Trim())).ToList();
        }

        public object Clone()
        {
            return new BatchDetails(this.Id, this.Number, this.Level, this.Size, this.Tank, this.ProductionDate, this.AnalyzedBy, this.AnalyzedDate, this.InCompliance,
                this.NotInComplianceFolio, this.IsReleased, this.ReleasedBy, this.ReleasedDate, this.ReleasedNotes, this.BatchAnalysis);
        }
    }

    public class BatchAnalysis : Entity, ICloneable
    {
        public BatchAnalysis()
        {

        }
        public BatchAnalysis(int id, int batchDetailsId, string parameterName, string lowerLimit, string upperLimit, string value, string measureUnit)
        {
            this.Id = id;
            this.BatchDetailsId = batchDetailsId;
            this.ParameterName = parameterName;
            this.LowerLimit = lowerLimit;
            this.UpperLimit = upperLimit;
            this.Value = value;
            this.MeasureUnit = measureUnit;
        }

        public int BatchDetailsId { get; set; }
        public string ParameterName { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public string Value { get; set; }
        public string MeasureUnit { get; set; }


        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }

        public object Clone()
        {
            return new BatchAnalysis(this.Id, this.BatchDetailsId, this.ParameterName, this.LowerLimit, this.UpperLimit, this.Value, this.MeasureUnit);
        }
    }

    public enum ProductionOrderAttributeType
    {
        ControlVariable,
        CriticalParameter,
        CriticalQualityAttribute
    }
}