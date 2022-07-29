using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.IndentityModels
{
    public class ApplicationRole : IdentityRole
    {
        [Column(TypeName = "varchar(250)")]
        public String CustomName { get; set; }
        public ApplicationRole()
        {

        }
        public ApplicationRole(string customName,string name)
        {
            this.CustomName = customName;
            this.Name = name;
        }

    }
}
