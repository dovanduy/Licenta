using System;
using System.Collections.Generic;
using System.Linq;
using ApiContracts.Dtos;
using BusinessLogic.Mappers;
using BusinessLogic.Services.Interfaces;
using Contracts.DataAccess;
using DataAccess;

namespace BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Review> _repository;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Review>();
        }

        public void AddNewReview(ReviewDto review)
        {
            if (!_repository.AllEntities().Any(x => x.UserId == review.UserId && x.ProductId == review.ProductId))
            {
                if (review.Rating < 0)
                    review.Rating = 0;
                if (review.Rating > 10)
                    review.Rating = 10;
                _repository.Add(ReviewMapper.Map(review));
                _unitOfWork.SaveChanges();
            }
            else
            {
                throw new Exception("User already reviewed the product!");
            }
        }

        public IList<ReviewDto> GetReviewsForProduct(int productId)
        {
            if (_repository.AllEntities().Any(x => x.ProductId == productId))
            {
                return _repository.AllEntities().Where(x => x.ProductId == productId).Select(ReviewMapper.Map).ToList();
            }
            return new List<ReviewDto>();
        }

        public void UpdateReview(ReviewDto review)
        {
            if (_repository.AllEntities().Any(x => x.Id == review.ReviewId))
            {
                if (review.Rating < 0)
                    review.Rating = 0;
                if (review.Rating > 10)
                    review.Rating = 10;
                _repository.Update(ReviewMapper.Map(review));
                _unitOfWork.SaveChanges();
            }
            else
            {
                throw new Exception($"There is no review with the id {review.ReviewId}");
            }
        }

        public void DeleteReview(int reviewId)
        {
            _repository.Delete(reviewId);
            _unitOfWork.SaveChanges();
        }
    }
}
