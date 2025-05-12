using DiplomMetod.Data.Entites;
using Microsoft.AspNetCore.Identity;

namespace DiplomMetod.Data.Identity
{
    public class IdentityInitializer
    {

        private const string adminRoleName = "Administrator";
        private const string userRoleName = "User";

        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@mail.ru";
            var adminPassword = "1dEaq54@hG";

            if (roleManager.FindByNameAsync(adminRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(adminRoleName)).Wait();
            }
            if (roleManager.FindByNameAsync(userRoleName).Result == null)
            {
                roleManager.CreateAsync(new IdentityRole(userRoleName)).Wait();
            }
            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = userManager.CreateAsync(admin, adminPassword).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, adminRoleName).Wait();
                }
            }
        }
    }
}
