using System;
using System.Collections.Generic;
using System.Linq;
using LiberacionProductoWeb.Models.DataBaseModels.Base;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class GeneralCatalog : Entity, ICloneable
    {
        public GeneralCatalog()
        {
        }

        public GeneralCatalog(int Id, string PlantId, string ProductId, string TankId, string ConversionFactor, string Area, string ProcessStep, string Variable,
                              string VariableSpecification, string LowerLimit, string UpperLimit, string VariableClasification, string CodeTool, string DescriptionTool,
                              string WeighingMachine, string BayArea, string FillingPump, string FillingHose, bool Estatus)
        {
            this.Id = Id;
            this.PlantId = PlantId;
            this.ProductId = ProductId;
            this.TankId = TankId;
            this.ConversionFactor = ConversionFactor;
            this.Area = Area;
            this.ProcessStep = ProcessStep;
            this.Variable = Variable;
            this.VariableSpecification = VariableSpecification;
            this.LowerLimit = LowerLimit;
            this.UpperLimit = UpperLimit;
            this.VariableClasification = VariableClasification;
            this.CodeTool = CodeTool;
            this.DescriptionTool = DescriptionTool;
            this.WeighingMachine = WeighingMachine;
            this.BayArea = BayArea;
            this.FillingPump = FillingPump;
            this.FillingHose = FillingHose;
            this.Estatus = Estatus;
        }


        public String PlantId { get; set; }
        public String ProductId { get; set; }
        public String TankId { get; set; }
        public String ConversionFactor { get; set; }
        public String Area { get; set; }
        public String ProcessStep { get; set; }
        public String Variable { get; set; }
        public String VariableSpecification { get; set; }
        public String LowerLimit { get; set; }
        public String UpperLimit { get; set; }
        public String VariableClasification { get; set; }
        public String CodeTool { get; set; }
        public String DescriptionTool { get; set; }
        public String WeighingMachine { get; set; }
        public String BayArea { get; set; }
        public String FillingPump { get; set; }
        public String FillingHose { get; set; }
        public Boolean Estatus { get; set; }
        public String? User { get; set; }

        public static GeneralCatalog Create(
            int generalId,
            String plantId,
            String productId,
            String tankId,
            String conversionFactor,
            String area,
            String processStep,
            String variable,
            String variableSpecification,
            String lowerLimit,
            String upperLimit,
            String variableClasification,
            String codeTool,
            String descriptionTool,
            String weighingMachine,
            String bayArea,
            String fillingPump,
            String fillingHose,
            String User,
            Boolean estatus = true
            )
        {
            var entityGeneralCatalog = new GeneralCatalog()
            {
                Id = generalId,
                PlantId = plantId,
                ProductId = productId,
                TankId = tankId,
                ConversionFactor = conversionFactor,
                Area = area,
                ProcessStep = processStep,
                Variable = variable,
                VariableSpecification = variableSpecification,
                LowerLimit = lowerLimit,
                UpperLimit = upperLimit,
                VariableClasification = variableClasification,
                CodeTool = codeTool,
                DescriptionTool = descriptionTool,
                WeighingMachine = weighingMachine,
                BayArea = bayArea,
                FillingPump = fillingPump,
                FillingHose = fillingHose,
                Estatus = estatus,
                User = User
            };
            return entityGeneralCatalog;
        }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            var auditList = new List<ReportAuditTrail>();
            var old = objectToCompareOld as GeneralCatalog;
            var current = objectToCompare as GeneralCatalog;
            if (old.PlantId != this.PlantId)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Planta",
                    Funcionality = "Catálogo general",
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
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Producto",
                    Funcionality = "Catálogo general",
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
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Tanque",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.TankId,
                    NewValue = TankId,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.ConversionFactor != this.ConversionFactor)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Factor de conversión del tanque",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.ConversionFactor,
                    NewValue = ConversionFactor,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Area != this.Area)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Area",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.Area,
                    NewValue = Area,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.ProcessStep != this.ProcessStep)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Etapa de Proceso/Equipo utilizado",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.ProcessStep,
                    NewValue = ProcessStep,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Variable != this.Variable)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Variable",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.Variable,
                    NewValue = Variable,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.VariableSpecification != this.VariableSpecification)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Especificación de la variable ",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.VariableSpecification,
                    NewValue = VariableSpecification,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.LowerLimit != this.LowerLimit)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Límite inferior de variable, parámetro o atributo",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.LowerLimit,
                    NewValue = LowerLimit,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.UpperLimit != this.UpperLimit)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Límite superior de variable, parámetro o atributo",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.UpperLimit,
                    NewValue = UpperLimit,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.VariableClasification != this.VariableClasification)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Clasificación de la variable",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.VariableClasification,
                    NewValue = VariableClasification,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.CodeTool != this.CodeTool)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Código del equipo",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.CodeTool,
                    NewValue = CodeTool,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.DescriptionTool != this.DescriptionTool)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Descripción del equipo",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.DescriptionTool,
                    NewValue = DescriptionTool,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.WeighingMachine != this.WeighingMachine)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Báscula",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.WeighingMachine,
                    NewValue = WeighingMachine,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.BayArea != this.BayArea)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Bahía",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.BayArea,
                    NewValue = BayArea,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.FillingPump != this.FillingPump)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Bomba de llenado",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.FillingPump,
                    NewValue = FillingPump,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.FillingHose != this.FillingHose)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Manguera de llenado",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.FillingHose,
                    NewValue = FillingHose,
                    Method = "UpdateAsync",
                    Plant = PlantId,
                    Product = ProductId,
                    User = User,
                });
            }
            if (old.Estatus != this.Estatus)
            {
                auditList.Add(new ReportAuditTrail
                {
                    Action = "Modificación",
                    Controller = "GeneralCatalog",
                    Date = DateTime.Now,
                    Detail = "Campo - Estatus",
                    Funcionality = "Catálogo general",
                    PreviousValue = old.Estatus == true ? "SI" : "NO",
                    NewValue = current.Estatus == true ? "SI" : "NO",
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
            return new GeneralCatalog(this.Id, this.PlantId, this.ProductId, this.TankId, this.ConversionFactor, this.Area, this.ProcessStep, this.Variable,
                              this.VariableSpecification, this.LowerLimit, this.UpperLimit, this.VariableClasification, this.CodeTool, this.DescriptionTool,
                              this.WeighingMachine, this.BayArea, this.FillingPump, this.FillingHose, this.Estatus);
        }



    }
}
