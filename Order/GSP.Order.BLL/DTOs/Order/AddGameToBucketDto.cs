namespace GSP.Order.BLL.DTOs.Order
{
    public class AddGameToBucketDto
    {
        public int OrderGameId { get; set; }

        public int OrderId { get; set; }

        public int GameId { get; set; }

        public string CustomerId { get; set; }
    }
}