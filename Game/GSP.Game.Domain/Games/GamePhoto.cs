namespace GSP.Game.Domain.Games
{
    public class GamePhoto
    {
        public int GamePhotoId { get; set; }

        public int GameId { get; set; }

        public byte[] Content { get; set; }

        public GameBase Game { get; set; }
    }
}