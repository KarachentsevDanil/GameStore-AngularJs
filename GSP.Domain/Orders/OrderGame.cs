using System;
using System.Collections.Generic;
using System.Text;
using GSP.Domain.Games;
using Newtonsoft.Json;

namespace GSP.Domain.Orders
{
    [JsonObject(IsReference = true)]
    public class OrderGame
    {
        public int OrderGameId { get; set; }

        public int OrderId { get; set; }

        public int GameId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Game Game { get; set; }
    }
}
