using AutoMapper;
using LiberacionProductoWeb.Models.ConfigViewModels;
using LiberacionProductoWeb.Models.IndentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, PerfilesMapper>();
        }
    }
}
