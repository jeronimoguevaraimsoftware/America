using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using LiberacionProductoWeb.Models.IndentityModels;

namespace LiberacionProductoWeb.Services
{
    public class ExtraUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ExtraUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {

            //adds extra paramters to user identity model
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("NombreUsuario", user.NombreUsuario ?? ""));
            identity.AddClaim(new Claim("EmailUsuario", user.EmailUsuario ?? ""));
            identity.AddClaim(new Claim("MexeUsuario", user.MexeUsuario ?? ""));
            identity.AddClaim(new Claim("PlantaUsuario", user.PlantaUsuario ?? ""));
            identity.AddClaim(new Claim("ImagenUsuario", user.ImagenBase64Usuario ?? ""));
            identity.AddClaim(new Claim("LogActiveDirectory", user.LogActiveDirectory.ToString() , ClaimValueTypes.Boolean));
            identity.AddClaim(new Claim("FechaUltimoIntento", user.FechaUltimoIntento.ToString(), ClaimValueTypes.DateTime));
            identity.AddClaim(new Claim("FechaUltimaSesion", user.FechaUltimaSesion.ToString(), ClaimValueTypes.DateTime));
            return identity;
        }
    }
}
