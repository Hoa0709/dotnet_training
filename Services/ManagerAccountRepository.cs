using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using app.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public interface IManagerAccount
    {
        Task<List<UserRolesViewModel>> GetListUser();
    }
    public class ManagerAccountRepository : IManagerAccount
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public ManagerAccountRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await userManager.GetRolesAsync(user));
        }
        public async Task<List<UserRolesViewModel>> GetListUser()
        {
            try
            {
                var users = await userManager.Users.ToListAsync();
                var userRolesViewModel = new List<UserRolesViewModel>();
                foreach (var user in users)
                {
                    var thisViewModel = new UserRolesViewModel();
                    thisViewModel.UserId = user.Id;
                    thisViewModel.UserName = user.UserName;
                    thisViewModel.Email = user.Email;
                    thisViewModel.FullName = user.FullName;
                    thisViewModel.Birthday = user.Birthday;
                    thisViewModel.IdentityNumber = user.IdentityNumber;
                    thisViewModel.Roles = await GetUserRoles(user);
                    userRolesViewModel.Add(thisViewModel);
                }
                return userRolesViewModel;
            }
            catch
            {
                throw new Exception("Error when get list user!");
            }
        }
    }
}