using GSP.Domain.Params;
using GSP.Game.Domain.Enums;
using GSP.Game.Domain.Games;

namespace GSP.Game.Domain.Params
{
    public class GamesFilterParams : FilterParams<GameBase>
    {
        public int[] CategoriesIds { get; set; }

        public string Term { get; set; }

        public int? StartPrice { get; set; }

        public int? EndPrice { get; set; }
        
        public string CustomerId { get; set; }

        public GameSortMode SortMode { get; set; }
    }
}