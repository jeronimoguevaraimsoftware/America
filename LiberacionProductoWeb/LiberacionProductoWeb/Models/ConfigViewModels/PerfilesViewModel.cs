using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.Mappers;
using LiberacionProductoWeb.Models.SechToolViewModels;

namespace LiberacionProductoWeb.Models.ConfigViewModels
{
    public class PerfilesViewModel: SechToolDistributionBatchVM
    {
        public String Name { get; set; }

        public String Id { get; set; }

        public List<RoleData> RoleList { get; set; }

        public IList<SectionData> Sections { get; set; }

        public IList<String> SelectedPermissions { get; set; }

        public PerfilesViewModel()
        {
            SelectedPermissions = new List<String>();
            RoleList = new List<RoleData>();
        }

        //used for search filter
        public List<String> SelectedRoleFilter { get; set;}
        public List<SelectListItem> RoleFilterControl { get; set; }

        public List<String> SelectedFunctionsFilter { get; set; }
        public List<SelectListItem> FunctionsFilterControl { get; set; }



        //mensajes en vista
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }



    public class PermissionData
    {
        public String Key { get; set; }
        public String Text { get; set; }

    }

    public class RoleData
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String PermisosLbl { get; set; }
        public IList<String> ListaPermisos { get; set; }

    }
}
