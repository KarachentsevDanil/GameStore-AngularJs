namespace GSP.Accounts.BLL.Dto.Customer
{
    public class CustomerLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
