using System.Collections.Generic;
using GSP.BLL.Dto.Rate;

namespace GSP.BLL.Dto.Game
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

        public List<RateDto> Rates { get; set; }

        public float AverageRate { get; set; }

        public GameDto()
        {
            Rates = new List<RateDto>();
        }
    }
}