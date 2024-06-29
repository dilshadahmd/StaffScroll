using Microsoft.AspNetCore.Identity;

namespace StaffScroll.Sevices
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAsync(UserManager<IdentityUser>? userManager,
            RoleManager<IdentityRole>? roleManager)
        {
            if (userManager == null || roleManager == null)
            {
                Console.WriteLine("usermanager or role manager is null");
                return;
            }

            //check if we have the admin role or not
            var exists = await roleManager.RoleExistsAsync("admin");
            if (!exists)
            {
                Console.WriteLine("Admin role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            //check if we have the client role or not
            exists = await roleManager.RoleExistsAsync("client");
            if (!exists)
            {
                Console.WriteLine("Client role is not defined and will be created");
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            //check if we have atleast one admin user or not
            var adminUsers = await userManager.GetUsersInRoleAsync("admin");
            if (adminUsers.Any())
            {
                Console.WriteLine("Admin user alreadu exists => exit");
                return;
            }

            //create the admin user 
            var user = new IdentityUser()
            {
                UserName = "admin@beststore.com",
                Email = "admin@beststore.com",
            };

            string defaultPassword = "Admin123*";

            var result = await userManager.CreateAsync(user,defaultPassword);
            if (result.Succeeded) {
                //set the user role 
                await userManager.AddToRoleAsync(user,"admin");
                Console.WriteLine("Admin user created successfully");
                Console.WriteLine("Email; " + user.Email + " Password: " + defaultPassword);
            }
            else
            {
                Console.WriteLine("Unable to create admin user: " + result.Errors.First().Description);
            }
        }
    }
}
