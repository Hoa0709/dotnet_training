using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Route("Api")] 
    [ApiController]  
    public class AuthenticateController : ControllerBase
    {  
        private readonly IAccount _account;  
  
        public AuthenticateController(IAccount account)  
        {  
            _account = account;  
        }  
        
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {    
            var x = await _account.SignInAsync(model);
            return (x == null)?Unauthorized():Ok(x);
        }  
        
        [HttpPost("Register")]   
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {  
            return (await _account.SignUpAsync(model))?Ok(new Response { Status = "Success", Message = "User created successfully!" }):StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }   
        private async Task<string> GetUserAsync() => await _account.GetUserIdAsync((string)Request.Headers["Authorization"]);
        [Authorize] 
        [HttpPost("ChangePassWord")]   
        public async Task<IActionResult> ChangePassWord([FromBody] string CurrentPassword, string NewPassWord)  
        {  
            return (await _account.ChangePassWordAsync(await GetUserAsync(),CurrentPassword,NewPassWord))?Ok(new Response { Status = "Success", Message = "User created successfully!" }):StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        }
    }  


}