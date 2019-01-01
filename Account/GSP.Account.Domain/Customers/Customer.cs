using GSP.Account.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace GSP.Account.Domain.Customers
{
    public class Customer : IdentityUser
    {
        public string FullName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirthday { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }
    }
}