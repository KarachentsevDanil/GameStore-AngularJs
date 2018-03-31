namespace GSP.Games.BLL.Dto.Game
{
    public class CreateGamePhotoDto
    {
        public int GameId { get; set; }

        public byte[] PhotoData { get; set; }

        public string Photo { get; set; }
    }
}
