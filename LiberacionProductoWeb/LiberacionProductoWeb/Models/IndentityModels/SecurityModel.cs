using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.IndentityModels
{
    public class SectionData
    {
        public String Section { get; set; }
        public IList<PermissionData> Permissions { get; set; }

    }

    public class PermissionData
    {
        public String Key { get; set; }
        public String Text { get; set; }

    }
}
