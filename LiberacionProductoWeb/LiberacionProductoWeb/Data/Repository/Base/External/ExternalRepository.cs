using LiberacionProductoWeb.Data.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository.Base.External
{
    public class ExternalRepository<T> : IExternalRepository<T> where T : class
    {

        protected readonly AppDbExternalContext Context;
        internal DbSet<T> dbSet;

        //constructor
        public ExternalRepository(AppDbExternalContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            try
            {
                if (filter != null)
                    query = query.Where(filter);

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                if (orderBy != null)
                    return orderBy(query).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener datos" + ex.ToString());

            }

            return query.ToList();
        }

        public T GetFirsOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener datos" + ex.ToString());
            }


            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public T GetXDecimal(UInt64 id)
        {
            return dbSet.Find(id);
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public T ObtenerTodo(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            try
            {
                if (filter != null)
                    query = query.Where(filter);

                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                if (orderBy != null)
                    return orderBy(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("error al obtener datos" + ex.ToString());

            }

            return query.FirstOrDefault();
        }

        public IQueryable<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return this.Context.Set<T>()
                .Where(expression).AsNoTracking();
        }
    }
}
