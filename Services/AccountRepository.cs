using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace app.Repository
{
    public interface IAccount
    {
        Task<JsonWebToken> SignIn(LoginModel model);
        Task<Boolean> SignUp(RegisterModel model);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
    }
    public class AccountRepository : IAccount
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AccountRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<Boolean> SignUp(RegisterModel model){
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                throw new Exception("User already exists!");  
            AppUser user = new AppUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return false;
  
            return true;
        }
        public async Task<JsonWebToken> SignIn(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new Exception("Invalid credentials.");
            }
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>{
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id),
                    };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:expiryMinutes"])),
                signingCredentials: signIn);

            return new JsonWebToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = Convert.ToInt64(token.ValidTo)
            };
        }

        public JsonWebToken RefreshAccessToken(string token)
        {
            throw new NotImplementedException();
        }

        public void RevokeRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        // private RefreshToken GetRefreshToken(string token)
        //     => _refreshTokens.SingleOrDefault(x => x.Token == token);
    }

}