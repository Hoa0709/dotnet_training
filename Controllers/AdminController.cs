using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using app.Models;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace app.Controllers
{
    [Route("Admin")] 
    [ApiController]  
    public class AdminController : ControllerBase
    {  
        
        private readonly UserManager<AppUser> userManager;  
        private readonly RoleManager<IdentityRole> roleManager;  
        private readonly IConfiguration _configuration;  
  
        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)  
        {  
            this.userManager = userManager;  
            this.roleManager = roleManager;  
            _configuration = configuration;  
        }  
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            var user = await userManager.FindByNameAsync(model.Username);  
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await userManager.GetRolesAsync(user);  
  
                var claims = new List<Claim>{
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("User", model.Username),
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
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);  
  
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  
        
        [HttpPost("Register")]   
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {  
            var userExists = await userManager.FindByNameAsync(model.Username);  
            if (userExists != null)  
                //return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });  
                throw new Exception("User already exists!");
            AppUser user = new AppUser()  
            {  
                Email = model.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.Username  
            };  
            var result = await userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });  
        }  
    }  


}