using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Services.Interfaces
{
    public interface IReviewService
    {
        void AddNewReview(ReviewDto review);
        void DeleteReview(int reviewId);
        IList<Review> Get(Func<IQueryable<Review>, IQueryable<Review>> query = null);
        void UpdateReview(ReviewDto review);
    }
}