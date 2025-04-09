using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TMDT.Areas.Identity.Data;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class RoleIndexModel : PageModel
    {
        private readonly UserManager<LOGINUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleIndexModel(UserManager<LOGINUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<UserRoleViewModel> Users { get; set; }
        public List<string> Roles { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Role { get; set; }
        }

        public class UserRoleViewModel
        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            Users = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Users.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Role = roles.FirstOrDefault()
                });
            }

            Roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm th?y ng??i dùng.");
                return Page();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không th? xóa các vai trò hi?n t?i.");
                return Page();
            }

            var addResult = await _userManager.AddToRoleAsync(user, Input.Role);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không th? thêm vai trò m?i.");
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError(string.Empty, "Tên vai trò không ???c ?? tr?ng.");
                return Page();
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist)
            {
                ModelState.AddModelError(string.Empty, "Vai trò ?ã t?n t?i.");
                return Page();
            }

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không th? t?o vai trò.");
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm th?y ng??i dùng.");
                return Page();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không th? xóa ng??i dùng.");
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm th?y vai trò.");
                return Page();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Không th? xóa vai trò.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
