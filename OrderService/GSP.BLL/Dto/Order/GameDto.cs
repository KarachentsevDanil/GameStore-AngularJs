namespace GSP.Orders.BLL.Dto.Order
{
    public class GameDto
    {
        public int GameId { get; set; }

        public string Name { get; set; }
        
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Photo { get; set; }

        public string PhotoContent { get; set; }
    }
}