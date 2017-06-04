using Contracts.ApiDtos;
using DataAccess;

namespace BusinessLogic.Mappers
{
    public static class ReviewMapper
    {
        public static Review Map(ReviewDto dto)
        {
            return new Review
            {
                Id = dto.ReviewId ?? 0,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Text = dto.Text,
                UserId = dto.UserId,
                UserNickname = dto.UserNickname
            };
        }

    }
}
