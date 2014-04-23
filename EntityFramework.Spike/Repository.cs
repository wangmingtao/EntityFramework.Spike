using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    public class Repository<T> where T : class, IEntity, new()
    {
        private DbContext _context;
        private IDbSet<T> _dbSet; 

        public Repository()
        {
            _context = new DataBaseContext();
            _dbSet = _context.Set<T>();
        }

        public IList<T> Query()
        {
            var query = _dbSet as IQueryable<T>;
            if (query != null)
            {
                query = query.AsNoTracking();

                return query.ToList();
            }

            return new List<T>();
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
