using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public class FormulaCatalogRepository : Repository<FormulaCatalog>, IFormulaCatalogRepository
    {
        private readonly AppDbContext _appDbContext;
        public FormulaCatalogRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}

