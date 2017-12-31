using GSP.Domain.Customers;
using Newtonsoft.Json;

namespace GSP.Domain.Games
{
    [JsonObject(IsReference = true)]
    public class Rate
    {
        public int RateId { get; set; }

        public int CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public Rating Rating { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Game Game { get; set; }
    }
}
