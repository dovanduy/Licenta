using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace BusinessLogic
{
    public class DependencyInjectionExtention : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IReviewService, ReviewService>();
            Container.RegisterType<IProductService, ProductService>();
            Container.RegisterType<ICategoryService, CategoryService>();
            Container.RegisterType<IInventoryService,InventoryService>();

            Container.AddNewExtension<DataAccess.DependencyInjectionExtention>();
        }
    }
}
