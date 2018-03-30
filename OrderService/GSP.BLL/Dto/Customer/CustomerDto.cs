﻿using GSP.Domain.Customers;

namespace GSP.BLL.Dto.Customer
{
    public class CustomerDto
    {
        public string CustomerId { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }
    }
}
