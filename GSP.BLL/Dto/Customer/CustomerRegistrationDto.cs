﻿using System;
using GSP.Domain.Customers;

namespace GSP.BLL.Dto.Customer
{
    public class CustomerRegistrationDto
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirthsday { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }
    }
}