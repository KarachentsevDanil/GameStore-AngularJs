namespace GSP.Orders.Domain.Orders
{
    public class OrderGame
    {
        public OrderGame()
        { }

        public OrderGame(int gameId, int orderId)
        {
            GameId = gameId;
            OrderId = orderId;
        }

        public int OrderGameId { get; set; }

        public int OrderId { get; set; }

        public int GameId { get; set; }

        public string GameName { get; set; }

        public string GameCategory { get; set; }

        public string GameDescription { get; set; }

        public float Price { get; set; }

        public byte[] GamePhoto { get; set; }

        public virtual Order Order { get; set; }
    }
}
