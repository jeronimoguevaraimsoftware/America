using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.Properties
{
    public class FilterParameters
    {
        public FilterParameters()
        {
            this.Variables = new List<string>();
        }
        public string ProductKey { get; set; }
        public List<string> Variables { get; set; }
    }
}
