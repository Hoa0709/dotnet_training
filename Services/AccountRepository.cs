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
        Task<JsonWebToken> SignInAsync(LoginModel model);
        Task<Boolean> SignUpAsync(RegisterModel model);
        Task<Boolean> ChangePassWordAsync(string UserId, string CurrentPassword, string NewPassWord);
        Task<String> GetUserIdAsync(string tokens);
        Task<Boolean> CheckAccountAsync(string UserId);
        Task<JsonWebToken> RefreshAccessToken(string token);
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
        public async Task<Boolean> SignUpAsync(RegisterModel model){
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                throw new Exception("User already exists!");  
            var emailExists = await userManager.FindByEmailAsync(model.Email);  
            if (emailExists != null)  
                throw new Exception("Email already registed!");
            AppUser user = new AppUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                throw new Exception("User creation failed! Please check user details and try again.");
            return true;
        }
        public async Task<JsonWebToken> SignInAsync(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new Exception("Invalid credentials.");
            }
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>(){
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id)
                    };
            foreach(var role in userRoles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration["Jwt:expiryMinutes"])),
                signingCredentials: signIn);

            return new JsonWebToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = Convert.ToString(token.ValidTo)
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

        public async Task<string> GetUserIdAsync(string tokens)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(tokens.Replace("Bearer ", "")) as JwtSecurityToken;         
            var UserId = await Task.FromResult(token.Claims.First(claim => claim.Type == "UserId").Value);   
            return UserId;
        }

        public async Task<Boolean> CheckAccountAsync(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        Task<JsonWebToken> IAccount.RefreshAccessToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePassWordAsync(string UserId, string CurrentPassword, string NewPassWord)
        {
            var user = await userManager.FindByIdAsync(UserId);
            var result = await userManager.ChangePasswordAsync(user, CurrentPassword, NewPassWord);  
            if (!result.Succeeded)  
                throw new Exception("User creation failed! Please check user details and try again.");
            return true;
        }
        // private RefreshToken GetRefreshToken(string token)
        //     => _refreshTokens.SingleOrDefault(x => x.Token == token);
    }

}