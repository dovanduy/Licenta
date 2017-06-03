namespace Licenta.Messaging.Model
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public byte Rating { get; set; }
        public string Text { get; set; }
        public bool UserBoughtProduct { get; set; } 
        public string UserNickname { get; set; }
        public bool ProductDeleted { get; set; }
    }
}
