using System.Collections.Generic;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.IndentityModels;

namespace LiberacionProductoWeb.Services
{
    public interface ISecurityService
    {
        Task<IList<SectionData>> GetAllPermissionsAsync();
    }
}
