using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class ProductModel : PageModel
    {
        private readonly TMDTDbContext _context;

        public ProductModel(TMDTDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Products product { get; set; }

        public List<Products> products { get; set; }

        public IActionResult OnGet()
        {
            products = _context.Products.ToList();
            
            return Page();
        }

        public IActionResult OnGetCategoryDetails()
        {
            var categories = _context.Categories.Select(c => new {
                id = c.Id,
                name = c.Name
            }).ToList();

            return new JsonResult(new { message = "Success", data = categories });
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Product");
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            var productToUpdate = await _context.Products.FindAsync(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Products>(
                productToUpdate,
                "product",
                p => p.Name, p => p.Description, p => p.Price, p => p.Number, p => p.decrease, p => p.ImagePath, p => p.Color, p => p.Size, p => p.CategoryId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Product");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Product");
        }
    }
}
