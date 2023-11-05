using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using app.Repository;

namespace app.Controllers
{
    [Authorize(Roles = "superadmin,admin")]
    [Route("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IManagerAccount managerAccount;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IManagerAccount managerAccount)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.managerAccount = managerAccount;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await managerAccount.GetListUser());
        }
        [HttpGet("Roles")]
        public async Task<IActionResult> Roles()
        {
            return Ok(await roleManager.Roles.ToListAsync());
        }
        [HttpPost("Roles")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return Ok(new Response { Status = "Success", Message = "Role created" });
        }
        [AllowAnonymous]
        [HttpPost("CreateDefault")]
        public async Task<IActionResult> CreateDefault()
        {
            var x = await userManager.FindByNameAsync("superadmin");
            var r = await roleManager.RoleExistsAsync("superadmin") && await roleManager.RoleExistsAsync("admin") && await roleManager.RoleExistsAsync("manager") && await roleManager.RoleExistsAsync("staff");
            if (x == null && !r)
            {
                await ContextSeed.SeedRolesAsync(userManager, roleManager);
                await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
                return Ok(new Response { Status = "Success", Message = "OK" });
            }
            else return BadRequest(new Response { Status = "Fail", Message = "Existed" });
        }
        [HttpPost]
        public async Task<IActionResult> UserRole([FromBody] UserRolesModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest(new Response { Status = "Fail", Message = "Not Found User" });
            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                return BadRequest(new Response { Status = "Fail", Message = "Cannot remove user existing roles" });
            }
            result = await userManager.AddToRolesAsync(user, model.Roles);
            if (!result.Succeeded)
            {
                return BadRequest(new Response { Status = "Fail", Message = "Cannot add selected roles to user" });
            }
            return Ok(new Response { Status = "Success", Message = "Role added for user" });
        }
        [HttpPut("ResetPass/{id}")]
        public async Task<IActionResult> ResetPass(string id, [FromBody] LoginModel model)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null || user.UserName != model.Username)
            {
                return BadRequest(new Response { Status = "Fail", Message = "Not Found User" });
            }
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "ResetPass User OK"});
            }else return BadRequest(new Response { Status = "Fail", Message = "ResetPass User Fail" });
        }
    }
}