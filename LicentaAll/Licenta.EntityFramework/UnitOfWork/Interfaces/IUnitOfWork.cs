using System;
using System.Threading.Tasks;

namespace Licenta.EntityFramework.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IMaintainableEntity;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}