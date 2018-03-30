using System.Collections.Generic;

namespace GSP.Games.Domain.Games
{
    public class Game
    {
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
        
        public virtual ICollection<GamePhoto> Photos { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual Category Category { get; set; }
        
        public Game()
        {
            Rates = new List<Rate>();
            Photos = new List<GamePhoto>();
        }
    }
}
