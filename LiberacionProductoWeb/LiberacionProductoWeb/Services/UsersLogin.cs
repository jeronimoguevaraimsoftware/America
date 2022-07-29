using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class UsersLogin : IUsersLogin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public UsersLogin(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            this._appDbContext = appDbContext;
        }

        public async Task<List<string>> FindByRolePlantIdAsync(string plantId, string role)
        {
            var users = await this._appDbContext.Users.Where(x => x.Rol.ToUpper().Contains(role)).ToListAsync();
            users = users.Where(x => !string.IsNullOrEmpty(x.PlantaUsuario) && x.PlantaUsuario.Split(",").Select(y => y.Trim()).Contains(plantId)).ToList();
            return users.Select(x => x.EmailUsuario).Distinct().ToList();
        }

        public async Task<ApplicationUser> GetUserInfo(string usr)
        {
            var user = await _userManager.FindByNameAsync(usr);
            if (user.SecurityStamp != null)
            {
                user.Rol = (await _userManager.GetRolesAsync(user)).FirstOrDefault().ToString();
            }
            return user;

        }

        public void UpdLoginUserMexe()
        {
            throw new NotImplementedException();
        }
    }
}
