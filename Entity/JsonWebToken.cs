using System.IdentityModel.Tokens.Jwt;

namespace app.Models
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public long Expiration { get; set; }
    }
}