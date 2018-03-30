using System;
using Microsoft.AspNetCore.Identity;

namespace GSP.Accounts.Domain.Customers
{
    public class Customer : IdentityUser
    {
        public string FullName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirthsday { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }
    }
}
