using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class CustomerBehaviorModel : PageModel
    {
        private readonly TMDTDbContext _context;

        public CustomerBehaviorModel(TMDTDbContext context)
        {
            _context = context;
        }

        public IList<CustomerBehaviors> CustomerBehaviors { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Replace with actual logic to populate CustomerBehaviors
            CustomerBehaviors = await _context.Donhang
                .GroupBy(d => d.Tennguoinhan)
                .Select(g => new CustomerBehaviors
                {
                    CustomerName = g.First().Tennguoinhan, // Replace with actual customer name
                    TotalPurchases = g.Count(),
                    TotalSpent = g.Sum(d => d.price),
                    LastPurchaseDate = g.Max(d => d.Createtime),
                    FavoriteCategories = new List<string>() // Replace with actual logic
                })
                .ToListAsync();

            return Page();
        }
    }
}
