using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GSP.Common.Web.Authentication
{
    public class AuthenticationOptions
    {
        private const string TokenKey = "Game_Store_Key!";
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenKey));
        }
    }
}
