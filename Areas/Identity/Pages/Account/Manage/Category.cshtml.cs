using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class CategoriesModel : PageModel
    {
        private readonly TMDTDbContext _context;

        public CategoriesModel(TMDTDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Categories Input { get; set; }

        public IList<Categories> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }

            _context.Add(Input);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Category"); // Adjust redirect to the correct Index page or Refresh
        }

        //edit
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (Input.Id <= 0)
            {
                return BadRequest("Invalid category data");
            }

            var categoryToUpdate = await _context.Categories.FindAsync(Input.Id);
            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate.Name = Input.Name;  // Update the name from the Input model

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Category"); // Redirect or return success
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoryExists(categoryToUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, ex.Message); // Server error
                }
            }
            catch (Exception ex) // Generic exception handling if needed
            {
                return StatusCode(500, ex.Message); // Server error
            }
        }

        //delete
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                TempData["Error"] = "Category not found.";
                return RedirectToPage("./Category");
            }

            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception details
                TempData["Error"] = $"Error deleting category: {ex.Message}";
            }

            return RedirectToPage("./Category");
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
