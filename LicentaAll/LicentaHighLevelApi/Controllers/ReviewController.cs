using System.Threading.Tasks;
using System.Web.Http;
using LicentaHighLevelApi.Model.DTOs;
using LicentaHighLevelApi.Services.Interfaces;

namespace LicentaHighLevelApi.Controllers
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

        public async Task<IHttpActionResult> Post(ReviewDTO review)
        {
            await _reviewService.Add(ReviewDTO.MapToBusContract(review));
            return Ok();
        }

        public async Task<IHttpActionResult> Put(ReviewDTO review)
        {
            await _reviewService.Update(ReviewDTO.MapToBusContract(review));
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int reviewId)
        {
            await _reviewService.Delete(reviewId);
            return Ok();
        }
    }
}
