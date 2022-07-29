using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.ConfigViewModels
{
    public class UsuariosViewModel: SechToolDistributionBatchVM
    {
        public DatosUsuario Usuario { get; set; }
        public List<DatosUsuario> Usuarios { get; set; }



        //Catalagos
        public List<SelectListItem> Unidades { get; set; }
        public List<SelectListItem> Perfiles { get; set; }

        public List<SelectListItem> PlantsFilter { get; set; }
        public List<SelectListItem> UsersFilter { get; set; }
        public List<SelectListItem> StatusFilter { get; set; }
        public List<SelectListItem> ProfilesFilter { get; set; }

        public List<SelectListItem> ExternalUsersFilter { get; set; }

        //helper controls
        public List<String> SelectedPlantsFilter { get; set; }
        public List<String> SelectedUsersFilter { get; set; }
        public List<String> SelectedStatusFilter { get; set; }
        public List<String> SelectedProfilesFilter { get; set; }




        //mensajes en vista
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }


    }

    public class DatosUsuario
    {
        public String Id { get; set; }
        public String NombreUsuario { get; set; }
        public String MexeUsuario { get; set; }
        public String EmailUsuario { get; set; }
        public String RolesUsuario { get; set; }
        public String PlantasUsuario { get; set; }
        public String EstatusUsuario { get; set; }
        public String ExternalUsuario { get; set; }
        
    }
}
