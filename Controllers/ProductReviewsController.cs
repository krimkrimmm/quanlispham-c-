using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class ProductReviewsController : Controller
    {
        private readonly TMDTDbContext _context;

        public ProductReviewsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: ProductReviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductReviews.ToListAsync());
        }

        // GET: ProductReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }
        [HttpPost]
        public JsonResult AddReviews(int productId, string userId, float rating, string comment)
        {
            try
            {
                var productReview = new ProductReviews()
                {
                    UserId = userId,
                    ProductId = productId,
                    Rating = rating,
                    Comment = comment,
                    CreatedAt = DateTime.UtcNow
                };
                _context.ProductReviews.Add(productReview);
                _context.SaveChanges();
                Console.WriteLine("da luu");
                return Json(new { message = "Success", data = productReview });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error", errors = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetReviews(int productId)
        {
            var reviews = _context.ProductReviews
                .Where(r => r.ProductId == productId)
                .Select(r => new { r.Id, r.ProductId, r.UserId, r.Rating, r.Comment, r.CreatedAt})
                .ToList();


            return Json(new { message = "Success", data = reviews});
        }

        // GET: ProductReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,UserId,Rating,Comment,CreatedAt")] ProductReviews productReviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productReviews);
        }

        // GET: ProductReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews.FindAsync(id);
            if (productReviews == null)
            {
                return NotFound();
            }
            return View(productReviews);
        }

        // POST: ProductReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,UserId,Rating,Comment,CreatedAt")] ProductReviews productReviews)
        {
            if (id != productReviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductReviewsExists(productReviews.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productReviews);
        }

        // GET: ProductReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productReviews = await _context.ProductReviews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productReviews == null)
            {
                return NotFound();
            }

            return View(productReviews);
        }

        // POST: ProductReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productReviews = await _context.ProductReviews.FindAsync(id);
            if (productReviews != null)
            {
                _context.ProductReviews.Remove(productReviews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public JsonResult DeleteReviews(int commentId)
        {
            var productReviews = _context.ProductReviews.Find(commentId);
            if (productReviews != null)
            {
                _context.ProductReviews.Remove(productReviews);
            }

            _context.SaveChanges();
            return Json(new { success = true});
        }

        private bool ProductReviewsExists(int id)
        {
            return _context.ProductReviews.Any(e => e.Id == id);
        }
    }
}
