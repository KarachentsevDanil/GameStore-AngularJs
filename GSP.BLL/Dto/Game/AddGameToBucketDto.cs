namespace GSP.BLL.Dto.Game
{
    public class AddGameToBucketDto
    {
        public int OrderGameId { get; set; }

        public int OrderId { get; set; }

        public int GameId { get; set; }

        public int CustomerId { get; set; }
    }
}