using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwProductos
    {
        [Key]
        public int Id_Producto { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Name_Esp { get; set; }
    }
}
