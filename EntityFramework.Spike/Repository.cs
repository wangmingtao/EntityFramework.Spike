using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    public class Repository<T> where T : class, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly IDbSet<T> _dbSet; 

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IList<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = _dbSet as IQueryable<T>;

            query = query.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetById(object key)
        {
            return _dbSet.Find(key);
        }

        public void Add(T newEntity)
        {
            _dbSet.Add(newEntity);
        }

        public void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public T Update(object key, T updatedEntity)
        {
            T original = _dbSet.Find(key);

            if (original == null)
                return null;

            _context.Entry(original).CurrentValues.SetValues(updatedEntity);

            return original;
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

    }
}
