using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class Value
    {
        public string Name { get; set; }
    }

    public class Variable
    {
        public string Type { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Product
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductKey { get; set; }
        public List<Variable> Variables { get; set; }
    }

    public class PlantConfiguration
    {
        public string PlantName { get; set; }
        public string PlantCode { get; set; }
        public string PlantKey { get; set; }
        public string DirectoryPath { get; set; }

        public string SheetName { get; set; }
        public List<Product> Products { get; set; }
    }
}
