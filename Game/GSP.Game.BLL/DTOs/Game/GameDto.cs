using System.Collections.Generic;

namespace GSP.Game.BLL.DTOs.Game
{
    public class GameDto
    {
        public int GameId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Photo { get; set; }

        public string PhotoContent { get; set; }

        public string Icon { get; set; }

        public string IconContent { get; set; }

        public string File { get; set; }

        public string FileContent { get; set; }

        public string FileExtinction { get; set; }
        
        public List<GamePhotoDto> Photos { get; set; }

        public float AverageRate { get; set; }

        public bool IsGameBought { get; set; }
    }
}