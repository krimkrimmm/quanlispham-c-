using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class DoanhthuModel : PageModel
    {
        private readonly TMDTDbContext _context;

        public DoanhthuModel(TMDTDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donhang Donhang { get; set; }

        public IList<Donhang> Donhangs { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Donhangs = await _context.Donhang.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            _context.Add(Donhangs);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Review"); // Adjust redirect to the correct Index pageProductresh
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (Donhang.Id <= 0)
            {
                return BadRequest("Invalid Review data");
            }

            var DonhangToUpdate = await _context.Donhang.FindAsync(Donhang.Id);
            if (DonhangToUpdate == null)
            {
                return NotFound();
            }

            DonhangToUpdate.Id = Donhang.Id;
            DonhangToUpdate.price = Donhang.price;
            DonhangToUpdate.Message = Donhang.Message;
            DonhangToUpdate.Diachi = Donhang.Diachi;
            DonhangToUpdate.Createtime = Donhang.Createtime;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Donhang"); // Redirect or return success
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DonhangsExists(DonhangToUpdate.Id))
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
            var Donhang = await _context.Donhang.FindAsync(id);
            if (Donhang == null)
            {
                TempData["Error"] = "Review not found.";
                return RedirectToPage("./Review");
            }

            try
            {
                _context.Donhang.Remove(Donhang);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Review deleted successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception details
                TempData["Error"] = $"Error deleting Review: {ex.Message}";
            }

            return RedirectToPage("./Review");
        }


        private bool DonhangsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
