using Newtonsoft.Json;
using System.Collections.Generic;

namespace GSP.Domain.Games
{
    [JsonObject(IsReference = true)]
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
