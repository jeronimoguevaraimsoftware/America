using LiberacionProductoWeb.Models.IndentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.Mappers
{
    public class PerfilesMapper
    {
        public String Usuario { get; set; }
        public String NombreUsuario { get; set; }
        public String Rol { get; set; }
        public Boolean LogActiveDirectory { get; set; }

        //mensajes en vista
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

    }
}