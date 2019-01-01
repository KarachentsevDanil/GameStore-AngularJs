using GSP.Account.Domain.Enums;
using System;

namespace GSP.Account.BLL.DTOs.Customer
{
    public class CustomerRegistrationDto
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime? DateOfBirthday { get; set; }

        public string Country { get; set; }

        public string Language { get; set; }
    }
}