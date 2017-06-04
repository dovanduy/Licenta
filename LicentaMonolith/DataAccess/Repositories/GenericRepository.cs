using System;
using System.Data.Entity;
using System.Linq;
using Contracts.DataAccess;
using Contracts.Exceptions;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T: class,IMaintainableEntity
    {
        internal MonolithDbContext _context;
        internal IDbSet<T> _dbSet;

        public GenericRepository(MonolithDbContext unitOfWork)
        {
            _context = unitOfWork;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public IQueryable<T> All()
        {
            return _dbSet.Where(x => !x.DateDeleted.HasValue).AsQueryable();
        }

        public void Delete(object entityId)
        {
            _dbSet.Find(entityId).DateDeleted = DateTime.Now;
        }

        public T Get(object entityId)
        {
            return _dbSet.Find(entityId);
        }

        public void Update(T entity)
        {
            var e = _dbSet.Find(entity.Id);
            if (e.RowVersion > entity.RowVersion)
                throw new AppConcurencyException("Concurency exception");
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
