﻿using System;
using GSP.Domain.Customers;
using Newtonsoft.Json;

namespace GSP.Domain.Games
{
    public class Rate
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Game Game { get; set; }
    }
}
