namespace ApiContracts.Dtos
{
    public class ReviewDto
    {
        public int? ReviewId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; } 
        public int Rating { get; set; } 
        public string Text { get; set; } 
        public string UserNickname { get; set; }
    }
}
