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

        public GameSortMode SortMode { get; set; }
    }

    public enum GameSortMode
    {
        ByName = 0,
        ByCountOfSales = 1,
        ByRating = 2,
        ByPriceHighToLow = 3,
        ByPriceLowToHigh = 4
    }
}
