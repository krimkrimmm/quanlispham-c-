using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Areas.Identity.Pages.Account.Manage
{
    public class ReviewModel : PageModel
    {
        private readonly TMDTDbContext _context;

        public ReviewModel(TMDTDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductReviews productreview { get; set; }

        public IList<ProductReviews> productreviews { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            productreviews = await _context.ProductReviews.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            _context.Add(productreviews);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Review"); // Adjust redirect to the correct Index pageProductresh
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (productreview.Id <= 0)
            {
                return BadRequest("Invalid Review data");
            }

            var productreviewToUpdate = await _context.ProductReviews.FindAsync(productreview.Id);
            if (productreviewToUpdate == null)
            {
                return NotFound();
            }

            productreviewToUpdate.ProductId = productreview.ProductId;
            productreviewToUpdate.UserId = productreview.UserId;
            productreviewToUpdate.Rating = productreview.Rating;
            productreviewToUpdate.Comment = productreview.Comment;
            productreviewToUpdate.CreatedAt = productreview.CreatedAt;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Review"); // Redirect or return success
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductreviewsExists(productreviewToUpdate.Id))
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
            var productreview = await _context.ProductReviews.FindAsync(id);
            if (productreview == null)
            {
                TempData["Error"] = "Review not found.";
                return RedirectToPage("./Review");
            }

            try
            {
                _context.ProductReviews.Remove(productreview);
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


        private bool ProductreviewsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
