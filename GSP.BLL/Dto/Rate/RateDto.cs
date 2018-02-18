namespace GSP.BLL.Dto.Rate
{
    public class RateDto
    {
        public int RateId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int GameId { get; set; }

        public string GameName { get; set; }

        public string Comment { get; set; }

        public string Rating { get; set; }

        public string CreatedOn { get; set; }
    }
}