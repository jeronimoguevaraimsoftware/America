using LiberacionProductoWeb.Models.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class DetalleXLoteTanqueRepository : ExternalRepository<VwDetalleXLoteTanque>, IDetalleXLoteTanqueRepository
    {
        private readonly AppDbExternalContext _appDbContext;
        public DetalleXLoteTanqueRepository(AppDbExternalContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
