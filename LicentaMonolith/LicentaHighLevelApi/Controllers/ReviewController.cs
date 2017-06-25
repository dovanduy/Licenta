using System.Web.Http;
using ApiContracts.Dtos;
using BusinessLogic.Services.Interfaces;

namespace LicentaMonolithHighLevelApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Review")]
    public class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IHttpActionResult Post(ReviewDto review)
        {
            _reviewService.AddNewReview(review);
            return Ok();
        }

        public IHttpActionResult Put(ReviewDto review)
        {
            _reviewService.UpdateReview(review);
            return Ok();
        }

        public IHttpActionResult Delete(int reviewId)
        {
            _reviewService.DeleteReview(reviewId);
            return Ok();
        }
    }
}
