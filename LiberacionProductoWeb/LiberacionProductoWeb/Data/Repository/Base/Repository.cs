using LiberacionProductoWeb.Data.Specifications.Base;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity, T entityOld = null, string DistribuitionBatch = null)
        {
            var current = entity; //await _dbContext.Set<T>().FindAsync(entity.Id);
            var old = entityOld;

            if (old != null)
            {
                var auditTrail = entity.AuditTrailComparison(current, entityOld, DistribuitionBatch);
                await _dbContext.Set<ReportAuditTrail>().AddRangeAsync(auditTrail);
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> AddAsync(IEnumerable<T> entity)
        {
            //foreach (var item in entity)
            //{
            //    _dbContext.Set<T>().Add(item);
            //}
            await _dbContext.AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.FirstOrDefault();
        }
        public async Task<T> UpdateAsync(IEnumerable<T> entity, IEnumerable<T> entityOld = null, string DistribuitionBatch = null)
        {
            foreach (var item in entity)
            {
                var current = item;
                var old = new Object();
                if (entityOld != null)
                {
                    foreach (var itemx in entityOld)
                    {
                        old = itemx;
                        if (itemx.Id == item.Id)
                        {
                            var auditTrail = item.AuditTrailComparison(current, (Entity)old, DistribuitionBatch);
                            _dbContext.Set<ReportAuditTrail>().AddRange(auditTrail);
                            _dbContext.Entry(item).State = EntityState.Modified;
                        }
                    }
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }
            await _dbContext.SaveChangesAsync();
            return entity.FirstOrDefault();
        }

        public async Task<T> DeleteAsync(IEnumerable<T> entity)
        {
            foreach (var item in entity)
            {
                _dbContext.Set<T>().Remove(item);
            }
            await _dbContext.SaveChangesAsync();
            return entity.FirstOrDefault();
        }

    }
}
