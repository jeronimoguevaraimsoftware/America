using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Data.Specifications.Base;

namespace LiberacionProductoWeb.Data.Repository.Base
{
    public interface IRepository<T> where T : Entity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, T entityOld = null, string DistribuitionBatch = null);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> AddAsync(IEnumerable<T> entity);
        Task<T> UpdateAsync(IEnumerable<T> entity, IEnumerable<T> entityOld = null, string DistribuitionBatch = null);
        Task<T> DeleteAsync(IEnumerable<T> entity);
     
    }
}
