using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GSP.SPA.Authentication
{
    public class AuthenticationOptions
    {
        private const string TokenKey = "mysupersecret_secretkey123!";
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenKey));
        }
    }
}
