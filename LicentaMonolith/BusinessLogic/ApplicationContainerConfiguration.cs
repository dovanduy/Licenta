using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace BusinessLogic
{
    public class ApplicationContainerConfiguration
    {
        public static void Register(IUnityContainer container)
        {
            DataAccess.ApplicationContainerConfiguration.Register(container);

            container.RegisterType<IReviewService, ReviewService>();
        }
    }
}
