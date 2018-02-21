using System;

namespace GSP.BLL.Dto.Rate
{
    public class CreateRateDto
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}