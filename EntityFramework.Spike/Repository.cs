using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
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

        public IList<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string inculudeProperties = "")
        {
            var query = _dbSet as IQueryable<T>;

            if (!string.IsNullOrEmpty(inculudeProperties))
            {
                var properties = inculudeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);

                query = properties.Aggregate(query, (current, propertyName) => current.Include(propertyName));
            }

            try
            {
                query = query.AsNoTracking();
            }
            catch (ModelValidationException ex)
            {
                throw ex;
            }

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

        public T GetById(params object[] key)
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

        public virtual void Delete(T entityToDelete)
        {
            Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }

        public T Update(T updatedEntity, params object[] key)
        {
            T original = _dbSet.Find(key);

            if (original == null)
                return null;

            _context.Entry(original).CurrentValues.SetValues(updatedEntity);

            return original;
        }

        public void Attach(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }


    }
}
