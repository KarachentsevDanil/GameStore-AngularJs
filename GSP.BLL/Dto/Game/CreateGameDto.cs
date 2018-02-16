namespace GSP.BLL.Dto.Game
{
    public class CreateGameDto
    {
        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public byte[] Photo { get; set; }
    }
}