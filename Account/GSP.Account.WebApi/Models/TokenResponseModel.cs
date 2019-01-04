using GSP.Account.BLL.DTOs.Customer;
using Newtonsoft.Json;

namespace GSP.Account.WebApi.Models
{
    public class TokenResponseModel
    {
        public CustomerDto Customer { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}