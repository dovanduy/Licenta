namespace Licenta.EntityFramework.UnitOfWork.Interfaces
{
    public interface IRepositoryCacheProvider
    {
        IRepository<T> Get<T>(IDbContext context) where T : class, IMaintainableEntity;
    }
}