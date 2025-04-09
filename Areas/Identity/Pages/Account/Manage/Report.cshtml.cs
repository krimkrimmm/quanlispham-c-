using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Areas.Identity.Data;
using TMDT.Data;
using TMDT.Models; // Assuming Donhangs model namespace

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class ReportModel : PageModel
    {
        private readonly UserManager<LOGINUser> _userManager;
        private readonly TMDTDbContext _context;

        public ReportModel(UserManager<LOGINUser> userManager, TMDTDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public InputModel Input { get; set; }

        public Donhang Donhang { get; set; }
        public IList<Donhang> Donhangs { get; set; }
        public Products product { get; set; }
        public IList<Products> products { get; set; }

        public class InputModel
        {
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(LOGINUser user)
        {
            var userManager = _userManager;
            Email = await userManager.GetEmailAsync(user);
            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);

            Donhangs = await _context.Donhang.Include(c => c.ProductOrders).ToListAsync();
            products = await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            // Handle changing email logic here
            // Example: var result = await _userManager.SetEmailAsync(user, Input.NewEmail);
            // Ensure to handle success/failure and update IsEmailConfirmed accordingly

            return RedirectToPage();
        }
    }
}
