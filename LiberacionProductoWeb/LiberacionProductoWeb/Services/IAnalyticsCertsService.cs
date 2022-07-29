using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using LiberacionProductoWeb.Models.External;

namespace LiberacionProductoWeb.Services
{
    public interface IAnalyticsCertsService
    {
        Task<Dictionary<int, string>> GetPlants();
        Task<Dictionary<string, string>> GetProducts(int plantId);
        Task<List<VwTanques>> GetTanks(int plantId);
    }
}
