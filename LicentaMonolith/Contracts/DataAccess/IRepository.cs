using System.Linq;

namespace Contracts.DataAccess
{
    public interface IRepository<T> : IRepository where T:class,IMaintainableEntity 
    {
        T Get(object entityId);
        IQueryable<T> All();
        void Add(T entity);
        void Update(T entity);
        void Delete(object entityId);
    }

    public interface IRepository
    {
    }
}