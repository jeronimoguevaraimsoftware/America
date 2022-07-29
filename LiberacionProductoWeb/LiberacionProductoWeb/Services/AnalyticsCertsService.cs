using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Data.Repository.Base.External;
using Microsoft.Extensions.Configuration;
using LiberacionProductoWeb.Models.External;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;

namespace LiberacionProductoWeb.Services
{
    public class AnalyticsCertsService : IAnalyticsCertsService
    {

        private AppDbExternalContext _context;
        private readonly IPlantasRepository _plantasRepository;
        private readonly ITanquesRepository _tanquesRepository;
        private readonly IConfiguration _config;
        private readonly IProductoRepository _productoRepository;
        private readonly IStringLocalizer<Resource> _resource;
        public bool WSMexeFuncionalidad;
        public AnalyticsCertsService(AppDbExternalContext context, IPlantasRepository plantasRepository,
        ITanquesRepository tanquesRepository, IProductoRepository productoRepository, IConfiguration config, IStringLocalizer<Resource> resource)
        {
            //TODO add external database context
            _context = context;
            _plantasRepository = plantasRepository;
            _tanquesRepository = tanquesRepository;
            _productoRepository = productoRepository;
            _config = config;
            WSMexeFuncionalidad = bool.Parse(_config["FlagWSMexe:ServiceApiKey"]);
            _resource = resource;
        }

        Task<Dictionary<int, string>> IAnalyticsCertsService.GetPlants()
        {
            Dictionary<int, string> plants = new Dictionary<int, string>();
            switch (WSMexeFuncionalidad)
            {
                case true:
                    var plantas = _plantasRepository.GetAll();
                    foreach (var item in plantas)
                    {
                        plants.Add(item.Id_Planta, item.Descripcion);
                    }
                    break;
                default:
                    for (int i = 0; i < 5; i++)
                    {
                        plants.Add(i, "Planta " + i);
                    }
                    break;
            }
            return Task.FromResult(plants);
        }

        Task<Dictionary<string, string>> IAnalyticsCertsService.GetProducts(int plantId)
        {
            Dictionary<string, string> products = new Dictionary<string, string>();
            switch (WSMexeFuncionalidad)
            {
                case true:
                    var productos = _productoRepository.GetAll();
                    var tanques = _tanquesRepository.GetAll().Where(t => t.Id_Planta == plantId);
                    foreach (var item in productos)
                    {
                        if (tanques.Where(t => t.Product_Id == item.Product_Id).FirstOrDefault() != null)
                            products.Add(item.Product_Id, item.Product_Name);
                    }
                    break;
                default:
                    List<VwProductos> productosD = new List<VwProductos>();
                    productosD.Add(new VwProductos { Id_Producto = 1, Product_Id = "OX", Product_Name = _resource.GetString("Oxygen"), Product_Name_Esp = "OXÍGENO" });
                    productosD.Add(new VwProductos { Id_Producto = 2, Product_Id = "NI", Product_Name = _resource.GetString("Nitrogen"), Product_Name_Esp = "NITRÓGENO" });
                    productosD.Add(new VwProductos { Id_Producto = 3, Product_Id = "AR", Product_Name = _resource.GetString("Argon"), Product_Name_Esp = "ARGÓN" });
                    productosD.Add(new VwProductos { Id_Producto = 6, Product_Id = "HY", Product_Name = _resource.GetString("Argon"), Product_Name_Esp = "HIDRÓGENO" });
                    productosD.Add(new VwProductos { Id_Producto = 9, Product_Id = "CD", Product_Name = _resource.GetString("CO2"), Product_Name_Esp = "BIÓXIDO DE CARBONO" });

                    List<VwTanques> tanquesD = new List<VwTanques>();
                    tanquesD.Add(new VwTanques { Id_Tanque = 1, Descripcion = "LOX-01", Id_Planta = 1, Product_Id = "OX" });
                    tanquesD.Add(new VwTanques { Id_Tanque = 1, Descripcion = "LOX-01", Id_Planta = 2, Product_Id = "OX" });
                    tanquesD.Add(new VwTanques { Id_Tanque = 1, Descripcion = "LOX-02", Id_Planta = 32, Product_Id = "OX" });

                    tanquesD.Add(new VwTanques { Id_Tanque = 1, Descripcion = "LNI-03", Id_Planta = 53, Product_Id = "NI" });
                    tanquesD.Add(new VwTanques { Id_Tanque = 2, Descripcion = "LNI-01", Id_Planta = 1, Product_Id = "NI" });
                    tanquesD.Add(new VwTanques { Id_Tanque = 2, Descripcion = "LNI-04", Id_Planta = 3, Product_Id = "NI" });

                    foreach (var item in productosD)
                    {
                        if (tanquesD.Where(t => t.Product_Id == item.Product_Id).FirstOrDefault() != null)
                            products.Add(item.Product_Id, item.Product_Name);
                    }


                    break;
            }

            return Task.FromResult(products);
        }

        Task<List<VwTanques>>IAnalyticsCertsService.GetTanks(int plantId)
        {
            List<VwTanques> tanks = new List<VwTanques>();
            switch (WSMexeFuncionalidad)
            {
                case true:
                    //FEATURE GROUP BY TANQUES
                    var tanques = from tanq in _tanquesRepository.GetAll()
                                  group tanq.Descripcion
                                  by tanq.Descripcion
                                  into tbl
                                  select new VwTanques
                                  {
                                      Descripcion = tbl.Key

                                  };
                    foreach (var item in tanques)
                    {
                        tanks.Add(new VwTanques { Id_Tanque = item.Id_Tanque, Descripcion = item.Descripcion });
  
                    }
                    break;
                default:
                    tanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 1, Descripcion = "LOX-01" });
                    tanks.Add(new VwTanques { Product_Id = "OX", Id_Tanque = 1, Id_Planta = 32, Descripcion = "LOX-02" });
                    tanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 2, Id_Planta = 1, Descripcion = "LNI-01" });
                    tanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 32, Descripcion = "LNI-04" });
                    tanks.Add(new VwTanques { Product_Id = "NI", Id_Tanque = 3, Id_Planta = 2, Descripcion = "LNI-02" });
                    break;
            }

            return Task.FromResult(tanks);
        }
    }
}

