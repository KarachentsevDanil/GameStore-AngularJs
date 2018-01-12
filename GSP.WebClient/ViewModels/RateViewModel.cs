using GSP.Domain.Games;

namespace GSP.WebClient.ViewModels
{
    public class RateViewModel
    {
        public int RateId { get; set; }

        public string Customer { get; set; }

        public string Comment { get; set; }

        public Rating Rating { get; set; }

        public string DisplayRating { get; set; }

        public string CreatedOn { get; set; }

        public int GameId { get; set; }
    }
}