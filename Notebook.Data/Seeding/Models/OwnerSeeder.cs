using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Data.Models;

namespace Notebook.Data.Seeding.Models
{
    public class OwnerSeeder : ISeeder
    {
        public async Task SeedAsync(NotebookContext dbContext, IServiceProvider serviceProvider)
        {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var owner = new User
                {
                    Name = "Aleks Zarev",
                    UserName = "alekszarev@aol.com",
                    Email = "alekszarev@aol.com",
                    NormalizedUserName = "ALEKSZAREV@AOL.COM",
                    EmailConfirmed = true
                };
                PasswordHasher<User> pass = new PasswordHasher<User>();
                owner.PasswordHash = pass.HashPassword(owner, "1234567");

                var result = await userManager.CreateAsync(owner);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }


                var role = await userManager.AddToRoleAsync(owner, "Owner");
                if (!role.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, role.Errors.Select(e => e.Description)));
                }

        }
    }
}
