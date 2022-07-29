using LiberacionProductoWeb.Models.CheckListViewModels;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.Dummys;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.Principal;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.RAPModels;

namespace LiberacionProductoWeb.Services
{
    public interface IRapService
    {

        Task<List<TanqueModel>> GetRapTanques(string plantId, string productId, string tankId);
        Task<List<RapComplemento>> GetRapCompTanques(string plantId, string productId, string tankId);
        Task<List<RapComplemento>> GetRapCompPipas(string plantId, string productId, string tankId);
        Task<List<SelectListItem>> GetPresentaciones(string productId);
        Task<List<SelectListItem>> GetPurezas(string productId);
        Task<List<SelectListItem>> GetRegistrosSan(string productId);
        Task<List<SelectListItem>> GetFormFarm(string productId);
        Task<List<PipaModel>> GetRapPipas(string plantId, string productId, string tankId);
        List<AnalisisPipa> GetAnalisis(List<List<Analisis>> rap, string type, string productId);
        Task<List<SelectListItem>> DisposicionXplanta(string plantId, List<SelectListItem> disp);


    }
}
