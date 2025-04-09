using Microsoft.AspNetCore.Identity;
using TMDT.Areas.Identity.Data;

namespace TMDT.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<LOGINUser> _userManager;

        public UserService(UserManager<LOGINUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserToRoleAsync(LOGINUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            // Xử lý kết quả
        }
    }
}
