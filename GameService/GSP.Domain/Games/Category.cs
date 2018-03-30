using System.Collections.Generic;

namespace GSP.Games.Domain.Games
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Category()
        {
            Games = new List<Game>();
        }

        public Category(string name)
        {
            Name = name;
            Games = new List<Game>();
        }
    }
}
