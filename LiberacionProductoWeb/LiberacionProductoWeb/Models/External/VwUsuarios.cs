using System;
using System.ComponentModel.DataAnnotations;

namespace LiberacionProductoWeb.Models.External
{
    public class VwUsuarios
    {
        [Key]
        public UInt64 No_Emp { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
