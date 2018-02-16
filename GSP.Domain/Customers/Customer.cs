using System;
using System.Collections.Generic;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace GSP.Domain.Customers
{
    [JsonObject(IsReference = true)]
    public class Customer : IdentityUser
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }
        
        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirthsday { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
