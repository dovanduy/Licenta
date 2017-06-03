using System.Threading.Tasks;
using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Services.Interfaces
{
    public interface IReviewService
    {
        Task Add(Review review);
        Task Delete(Review review);
        Task Delete(int reviewId);
        Task Update(Review review);
    }
}