
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface IUsersLogin
    {
        public void UpdLoginUserMexe();
        public Task<ApplicationUser> GetUserInfo(string usr);

        Task<List<string>> FindByRolePlantIdAsync(string plantId, string role);
    }
}
