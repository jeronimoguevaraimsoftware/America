using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class ProductionOrderHistorianDto
    {
        public string Type { get; set; }
        public IEnumerable<ProductionOrderVariableDto> Variables { get; set; }
    }

    public class ProductionOrderVariableDto
    {
        public string Name { get; set; }
        public IEnumerable<VariableDto> Values { get; set; }
    }
    public class VariableDto
    {
        public DateTime Period { get; set; }
        public float? Value { get; set; }
    }
    public class VariableHistorian
    {
        public DateTime Period { get; set; }
        public float? Value { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
