using System;
using System.Threading.Tasks;
using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public class ExampleRepository : Repository<EntityExample>, IExampleRepository
    {
        private readonly AppDbContext _appDbContext;
        public ExampleRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        //public override async Task<EntityExample> GetByIdAsync(int id)
        //{
        //    return await _dbContext.Set<EntityExample>().FindAsync(id);
        //}

        //public  Task UpdateAsync(EntityExample entity)
        //{
        //    var ObjBD = await _dbContext.entityExamples.FindAsync(entity.Id);
        //    ObjBD.Description = entity.Description;
        //    _appDbContext.SaveChanges();
        //    return Task.CompletedTask;
        //}

    }
}
