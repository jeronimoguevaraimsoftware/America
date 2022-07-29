using System;
using AutoMapper;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Helpers
{
    public class RunProfile : Profile
    {
        public RunProfile()
        {
            CreateMap<GeneralCatalog, General>().ReverseMap();

        }
    }
}
