using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public interface IExternalRepository<T> where T : class
    {
        T Get(int id);

        T GetXDecimal(UInt64 id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        T ObtenerTodo(
                Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T GetFirsOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        IQueryable<T> GetAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

    }
}
