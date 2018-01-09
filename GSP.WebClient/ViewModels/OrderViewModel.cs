using System;
using System.Collections.Generic;
using GSP.Domain.Orders;

namespace GSP.WebClient.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public string Customer { get; set; }

        public DateTime SaleDate { get; set; }

        public List<GameViewModel> Games { get; set; }
    }
}
