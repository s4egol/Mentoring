using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Data
{
    public static class DatabaseInitializer
    {
        public static void Init(IServiceScope serviceScope)
        {
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

            AddRoles(roleManager)
                .GetAwaiter()
                .GetResult();

            AddAdminsAsync(userManager)
                .GetAwaiter()
                .GetResult();

            AddUsersAsync(userManager)
                .GetAwaiter()
                .GetResult();
        }

        private static async Task AddRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Constants.Roles.Buyer))
            {
                var identityRole = new IdentityRole(Constants.Roles.Buyer);

                await roleManager.CreateAsync(identityRole);
                await roleManager.AddClaimAsync(identityRole, new Claim(ClaimTypes.Role, Constants.Roles.Buyer));
            }

            if (!await roleManager.RoleExistsAsync(Constants.Roles.Manager))
            {
                var identityRole = new IdentityRole(Constants.Roles.Manager);

                await roleManager.CreateAsync(identityRole);
                await roleManager.AddClaimAsync(identityRole, new Claim(ClaimTypes.Role, Constants.Roles.Manager));
            }
        }

        private static async Task AddAdminsAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                Id = "1",
                FirstName = "Ivan",
                LastName = "Miatselski",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "S4egol",
            };

            await AddUserAsync(userManager, user, "a123a", Constants.Roles.Manager);
        }

        private static async Task AddUsersAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                Id = "2",
                FirstName = "User",
                LastName = "User",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                UserName = "User",
            };

            await AddUserAsync(userManager, user, "user", Constants.Roles.Buyer);
        }

        private static async Task AddUserAsync(UserManager<AppUser> userManager, AppUser user, string password, string role)
        {
            var existedUser = await userManager.FindByNameAsync(user.UserName);

            if (existedUser != null)
            {
                return;
            }

            var createdUser = await userManager.CreateAsync(user, password);

            if (createdUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
