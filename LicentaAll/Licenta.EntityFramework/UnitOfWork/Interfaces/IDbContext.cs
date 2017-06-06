using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Licenta.EntityFramework.UnitOfWork.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}