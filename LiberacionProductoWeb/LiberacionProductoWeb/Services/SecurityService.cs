using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LiberacionProductoWeb.Models.IndentityModels;

namespace LiberacionProductoWeb.Services
{
    public class SecurityService : ISecurityService
    {
        public async Task<IList<SectionData>> GetAllPermissionsAsync()
        {
            var result = new List<SectionData>();
            var assembly = Assembly.GetEntryAssembly();
            var resourceStream = assembly.GetManifestResourceStream("LiberacionProductoWeb.Properties.permisos.json");

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                string permissionsString = await reader.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<SectionData>>(permissionsString);
            }

            return result;
        }
    }
}
