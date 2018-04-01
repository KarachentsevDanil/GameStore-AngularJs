using System;

namespace GSP.Accounts.BLL.Dto.Customer
{
    public class CustomerRegistrationDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirthsday { get; set; }
        public string Password { get; set; }
    }
}
