using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Test.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T FindById(int id);

        T Add(T entity);

        T Update(T entity);

        T Remove(T entity);
    }

    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }

        public T Update(T entity)
        {
            return _context.Set<T>().Update(entity).Entity;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}