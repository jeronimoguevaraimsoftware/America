using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using Microsoft.AspNetCore.Identity;

namespace LiberacionProductoWeb.Models.IndentityModels
{
    public class ApplicationUser : IdentityUser, ICloneable
    {
        public ApplicationUser()
        {

        }
        public ApplicationUser(string Id, string MexeUsuario, string NombreUsuario, string EmailUsuario, bool Activo, string Rol,
                string PlantaUsuario, int AccessFailedCount, DateTimeOffset? LockoutEnd)
        {
            this.Id = Id;
            this.MexeUsuario = MexeUsuario;
            this.NombreUsuario = NombreUsuario;
            this.EmailUsuario = EmailUsuario;
            this.Activo = Activo;
            this.Rol = Rol;
            this.PlantaUsuario = PlantaUsuario;
            this.AccessFailedCount = AccessFailedCount;
            this.LockoutEnabled = LockoutEnabled;
        }

        [Required(ErrorMessage = "NombreUsuario")]
        [Column(TypeName = "varchar(250)")]
        [Display(Name = "NombreUsuario")]
        public String NombreUsuario { get; set; }
        [Column(TypeName = "varchar(70)")]
        public String EmailUsuario { get; set; }
        [Column(TypeName = "varchar(20)")]
        public String MexeUsuario { get; set; }
        [Column(TypeName = "varchar(300)")]
        public String PlantaUsuario { get; set; }
        [Required(ErrorMessage = "Activo")]
        [Display(Name = "Activo")]
        [DefaultValue("true")]
        public Boolean Activo { get; set; }
        [Column(TypeName = "varchar(200)")]
        public String ImagenBase64Usuario { get; set; }
        [Column(TypeName = "varchar(200)")]
        public String MetaDataUsuario { get; set; }
        [DefaultValue("DateTime.Now")]
        public DateTime FechaCreacion { get; set; }
        public Boolean LogActiveDirectory { get; set; }
        [Column(TypeName = "varchar(150)")]
        public String Rol { get; set; }
        public DateTime FechaUltimoIntento { get; set; }
        public DateTime FechaUltimaSesion { get; set; }
        //external key user
        public Int32 ExternalId { get; set; }
        public object Clone()
        {
            return new ApplicationUser(this.Id, this.MexeUsuario, this.NombreUsuario, this.EmailUsuario, this.Activo, this.Rol, 
                this.PlantaUsuario, this.AccessFailedCount, this.LockoutEnd);
        }
    }

}
