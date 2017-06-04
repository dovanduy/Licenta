using System;

namespace Contracts.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IMaintainableEntity;
        void SaveChanges();
    }
}