using System;
using System.Collections;
using System.Data.Entity;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    public class UnitOfWork : IDisposable
    {
        private bool _disposed;

        private readonly Hashtable _repositories = new Hashtable();
        private readonly DbContext _context = new DataBaseContext();

        public Repository<T> GetRepository<T>() where T : class, IEntity, new()
        {
            if (!_repositories.Contains(typeof(T)))
            {
                _repositories[typeof(T)] = new Repository<T>(_context);
            }

            return _repositories[typeof(T)] as Repository<T>;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            ExecuteDispose();
            GC.SuppressFinalize(this);
        }

        private void ExecuteDispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            ExecuteDispose();
        }
    }
}
