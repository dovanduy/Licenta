using Licenta.ProductView.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Licenta.ProductView.API.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        public IList<EntityFramework.ProductView> Get()
        {
            using(ProductViewDbContext unitOfWork = new ProductViewDbContext())
            {
                return unitOfWork.ProductViews.ToList();
            }
        }
    }
}
