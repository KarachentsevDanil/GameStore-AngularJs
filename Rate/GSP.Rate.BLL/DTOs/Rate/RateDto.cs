using System;

namespace GSP.Rate.BLL.DTOs.Rate
{
    public class RateDto
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public int GameId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public string CreatedOn { get; set; }

        public DateTime CreatedOnDate { get; set; }
    }
}