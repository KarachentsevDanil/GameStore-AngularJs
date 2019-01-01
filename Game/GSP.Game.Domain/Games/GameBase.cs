using System.Collections.Generic;

namespace GSP.Game.Domain.Games
{
    public class GameBase
    {
        public GameBase()
        {
            Photos = new List<GamePhoto>();
        }

        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public byte[] Photo { get; set; }

        public byte[] Icon { get; set; }

        public byte[] FileContent { get; set; }

        public string FileExtinction { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<GamePhoto> Photos { get; set; }

        public Category Category { get; set; }

        public float AverageRate { get; set; }

        public int OrdersCount { get; set; }
    }
}