using System.Collections.Generic;
using GSP.Domain.Orders;
using Newtonsoft.Json;

namespace GSP.Domain.Games
{
    [JsonObject(IsReference = true)]
    public class Game
    {
        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public byte[] Photo { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderGame> Orders { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual Category Category { get; set; }
        
        public Game()
        {
            Orders = new List<OrderGame>();
            Rates = new List<Rate>();
        }
    }
}
