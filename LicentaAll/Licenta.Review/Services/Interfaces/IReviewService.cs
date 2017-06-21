using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Review.Services.Interfaces
{
    public interface IReviewService
    {
        Task<EntityFramework.Review> AddNewReview(Messaging.Model.Review reviewToAdd);
        Task<int> DeleteReview(int categoryId);
        Task<EntityFramework.Review> UpdateReview(Messaging.Model.Review categoryToEdit);
        Task<List<int>> HandleProductDeleted(int productId);
        double GetProductRating(int productId);
    }
}