using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwTanques
    {
   
        public int Id_Tanque {get; set;}
        public string Descripcion { get; set; }
        public int Id_Planta { get; set; }
        public string Product_Id { get; set; }
    }
}
