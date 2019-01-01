using System.Collections.Generic;

namespace GSP.Game.Domain.Games
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<GameBase> Games { get; set; }

        public Category()
        {
            Games = new List<GameBase>();
        }

        public Category(string name)
        {
            Name = name;
            Games = new List<GameBase>();
        }
    }
}