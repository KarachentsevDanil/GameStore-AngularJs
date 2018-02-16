using System;
using GSP.Domain.Games;

namespace GSP.BLL.Dto.Rate
{
    public class CreateRateDto
    {
        public int RateId { get; set; }

        public int CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public Rating Rating { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}