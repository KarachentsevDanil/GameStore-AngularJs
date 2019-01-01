using System;

namespace GSP.Domain.Games
{
    public class RateBase
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}