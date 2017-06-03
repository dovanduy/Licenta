using Licenta.Messaging.Model;

namespace LicentaHighLevelApi.Model.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public byte Rating { get; set; }
        public string Text { get; set; }
        public bool UserBoughtProduct { get; set; }
        public string UserNickname { get; set; }

        public static Review MapToBusContract(ReviewDTO reviewDto)
        {
            return new Review
            {
                ProductId = reviewDto.ProductId,
                Rating = reviewDto.Rating,
                ReviewId = reviewDto.ReviewId,
                Text = reviewDto.Text,
                UserId = reviewDto.UserId,
                UserBoughtProduct = reviewDto.UserBoughtProduct,
                UserNickname = reviewDto.UserNickname
            };
        }
    }
}