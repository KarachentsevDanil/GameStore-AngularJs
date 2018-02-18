using GSP.Domain.Games;

namespace GSP.Domain.Params
{
    public class GamesFilterParams : FilterParams<Game>
    {
        public int[] CategoriesIds { get; set; }

        public string Term { get; set; }

        public int? StartPrice { get; set; }

        public int? EndPrice { get; set; }

        public string Customer { get; set; }

        public string CustomerId { get; set; }

        public GamesOutputMode OutputMode { get; set; }
    }

    public enum GamesOutputMode
    {
        All = 0,
        TopSell = 1,
        TopRate = 2
    }
}
