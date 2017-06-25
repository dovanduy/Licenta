using System.Linq;

namespace Licenta.EntityFramework.UnitOfWork.Interfaces
{
    public interface IRepository<T> : IRepository where T : class, IMaintainableEntity
    {
        T Get(object entityId);
        IQueryable<T> AllEntities();
        void Add(T entity);
        void Update(T entity);
        void Delete(object entityId);
        void DeletePermamently(object entityId);
    }

    public interface IRepository
    {
    }
}