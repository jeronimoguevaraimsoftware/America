using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly AppDbContext _db;
        public IExampleRepository exampleRepository { get; private set; }

        public ContainerRepository(AppDbContext db)
        {
            _db = db;
            exampleRepository = new ExampleRepository(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
