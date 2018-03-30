using System;

namespace GSP.Games.Domain.Games
{
    public class Rate
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Game Game { get; set; }
    }
}
