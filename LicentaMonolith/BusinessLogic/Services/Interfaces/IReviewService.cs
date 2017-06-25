using System.Collections.Generic;
using ApiContracts.Dtos;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReviewService
    {
        void AddNewReview(ReviewDto review);
        void DeleteReview(int reviewId);
        IList<ReviewDto> GetReviewsForProduct(int productId);
        void UpdateReview(ReviewDto review);
    }
}