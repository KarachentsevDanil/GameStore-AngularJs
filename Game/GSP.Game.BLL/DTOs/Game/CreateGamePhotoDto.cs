namespace GSP.Game.BLL.DTOs.Game
{
    public class CreateGamePhotoDto
    {
        public int GameId { get; set; }

        public byte[] PhotoData { get; set; }

        public string Photo { get; set; }
    }
}