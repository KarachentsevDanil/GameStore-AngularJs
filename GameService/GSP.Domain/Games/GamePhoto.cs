namespace GSP.Games.Domain.Games
{
    public class GamePhoto
    {
        public int GamePhotoId { get; set; }

        public int GameId { get; set; }

        public byte[] Content { get; set; }

        public virtual GSP.Games.Domain.Games.Game Game { get; set; }
    }
}
