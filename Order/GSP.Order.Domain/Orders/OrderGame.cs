namespace GSP.Order.Domain.Orders
{
    public class OrderGame
    {
        public OrderGame(int gameId, int orderId)
        {
            GameId = gameId;
            OrderId = orderId;
        }

        public int OrderGameId { get; set; }

        public int OrderId { get; set; }

        public int GameId { get; set; }

        public OrderBase Order { get; set; }
    }
}