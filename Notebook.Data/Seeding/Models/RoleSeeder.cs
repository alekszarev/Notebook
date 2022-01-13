using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Data.Models;

namespace Notebook.Data.Seeding.Models
{
    public class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(NotebookContext dbContext, IServiceProvider serviceProvider)
        {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                string ownerRole = "Owner";
                string clientRole = "Client";
                await SeedRoleAsync(roleManager, ownerRole);
                await SeedRoleAsync(roleManager, clientRole);
        }

        private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new Role(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

            }
        }
    }
}
