using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IExampleRepository : IRepository<EntityExample>
    {
         //Task UpdateAsync(EntityExample entity);
         Task<EntityExample> GetByIdAsync(int id);
    }
}
