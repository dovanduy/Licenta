using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
using Licenta.EntityFramework.UnitOfWork.Interfaces;
using Licenta.Review.Services.Interfaces;
using Microsoft.Practices.ObjectBuilder2;

namespace Licenta.Review.Services
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<EntityFramework.Review> _repository;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<EntityFramework.Review>();
        }

        public async Task<EntityFramework.Review> AddNewReview(Messaging.Model.Review reviewToAdd)
        {
            bool userAlreadyReaviewed =
                _repository.AllEntities().Any(x => x.UserId == reviewToAdd.UserId &&
                                                   x.ProductId == reviewToAdd.ProductId &&
                                                   !x.DateDeleted.HasValue);

            if (userAlreadyReaviewed)
                throw new EntityCommandExecutionException($"User {reviewToAdd.UserId} already reviewed product {reviewToAdd.ProductId}.");


            var addedReview = new EntityFramework.Review
            {
                ProductId = reviewToAdd.ProductId,
                Rating = reviewToAdd.Rating,
                UserId = reviewToAdd.UserId,
                Text = reviewToAdd.Text,
                UserNickname = reviewToAdd.UserNickname,
                UserBoughtProduct = reviewToAdd.UserBoughtProduct
            };

            _repository.Add(addedReview);
            await _unitOfWork.SaveChangesAsync();
            return addedReview;
        }

        public async Task<int> DeleteReview(int reviewId)
        {
            if (!_repository.AllEntities().Any(x => x.Id == reviewId))
                throw new EntityCommandExecutionException($"No product with id {reviewId}");
            _repository.Delete(reviewId);
            await _unitOfWork.SaveChangesAsync();
            return _repository.Get(reviewId).ProductId;
        }

        public async Task<EntityFramework.Review> UpdateReview(Messaging.Model.Review reviewToEdit)
        {
            if (!_repository.AllEntities().Any(x => x.Id == reviewToEdit.ReviewId && !x.DateDeleted.HasValue))
                throw new EntityCommandExecutionException($"No review with id {reviewToEdit.ReviewId}");

            var editedReview = _repository.AllEntities().First(x => x.Id == reviewToEdit.ReviewId);

            _repository.Update(editedReview);

            editedReview.Rating = reviewToEdit.Rating;
            editedReview.Text = reviewToEdit.Text;
            editedReview.UserNickname = reviewToEdit.UserNickname;

            await _unitOfWork.SaveChangesAsync();

            return editedReview;
        }

        public async Task<List<int>> HandleProductDeleted(int productId)
        {
            if (_repository.AllEntities().All(x => x.ProductId == productId && !x.DateDeleted.HasValue))
                return new List<int>();

            var reviewsToChange = _repository.AllEntities()
                .Where(x => x.ProductId == productId && !x.DateDeleted.HasValue)
                .ToList();

            foreach (EntityFramework.Review review in reviewsToChange)
            {
                _repository.Delete(review);
                review.ProductDeleted = true;
            }

            await _unitOfWork.SaveChangesAsync();
            return reviewsToChange.Select(x => x.Id).ToList();
        }

        public double GetProductRating(int productId)
        {
            IEnumerable<int> allProductRatings = _repository.AllEntities()
                .Where(x => !x.DateDeleted.HasValue)
                .Where(x => x.ProductId == productId)
                .Select(x => x.Rating)
                .ToList()
                .Select(Convert.ToInt32);

            int ratingsCount = allProductRatings.Count();
            if (ratingsCount == 0)
                return 0;

            double summedRatings = 0;
            allProductRatings.ForEach(x => summedRatings += x);
            return summedRatings / ratingsCount;
        }
    }
}
