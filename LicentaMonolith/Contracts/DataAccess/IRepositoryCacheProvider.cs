namespace Contracts.DataAccess
{
    public interface IRepositoryCacheProvider
    {
        IRepository<T> Get<T>(IDbContext context) where T : class, IMaintainableEntity;
    }
}