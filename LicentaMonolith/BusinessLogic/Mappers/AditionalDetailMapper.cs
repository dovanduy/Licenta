using ApiContracts.Dtos;
using DataAccess;

namespace BusinessLogic.Mappers
{
    public static class AditionalDetailMapper
    {
        public static AditionalDetail Map(AditionalDetailDto dto)
        {
            return new AditionalDetail
            {
                Id = dto.AditionalDetailId,
                RowVersion = dto.RowVersion,
                Text = dto.Text,
                Name = dto.Name
            };
        }

        public static AditionalDetail Map(AditionalDetailDto dto, int productId)
        {
            return new AditionalDetail
            {
                Id = dto.AditionalDetailId,
                ProductId = productId,
                RowVersion = dto.RowVersion,
                Text = dto.Text,
                Name = dto.Name
            };
        }

        public static AditionalDetailDto Map(AditionalDetail aditionalDetail)
        {
            return new AditionalDetailDto
            {
                AditionalDetailId = aditionalDetail.Id,
                RowVersion = aditionalDetail.RowVersion,
                Text = aditionalDetail.Text,
                Name = aditionalDetail.Name
            };
        }
    }
}
