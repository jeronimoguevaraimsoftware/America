using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.External
{
    public class VwPlantas
    {
        [Key]
        public int Id_Planta { get; set; }
        public string Descripcion { get; set; }
        public string Identificador { get; set; }


    }
}
