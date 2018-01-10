using System.Collections.Generic;

namespace GSP.WebClient.ViewModels
{
    public class GameViewModel
    {
        public int GameId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }
        
        public string PhotoData { get; set; }

        public Dictionary<int, string> Ratings { get; set; }
    }
}
