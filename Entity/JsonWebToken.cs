using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace app.Models
{
    public class JsonWebToken
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}