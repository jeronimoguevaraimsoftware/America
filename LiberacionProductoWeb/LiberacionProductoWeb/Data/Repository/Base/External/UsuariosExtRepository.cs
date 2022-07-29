using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class UsuariosExtRepository : ExternalRepository<VwUsuarios>, IUsuariosExtRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public UsuariosExtRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
