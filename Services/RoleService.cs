using Microsoft.AspNetCore.Identity;
using TMDT.Data;

namespace TMDT.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly LOGINDbContext _context;

        public RoleService(RoleManager<IdentityRole> roleManager, LOGINDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task CreateRoles(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public async Task AddRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public async Task RemoveRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }
    }
}
