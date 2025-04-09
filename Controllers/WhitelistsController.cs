using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TMDT.Controllers
{
    public class WhitelistsController : Controller
    {
        private readonly TMDTDbContext _context;

        public WhitelistsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: Whilelists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Whilelist.ToListAsync());
        }

        // GET: Whilelists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whilelist = await _context.Whilelist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whilelist == null)
            {
                return NotFound();
            }

            return View(whilelist);
        }

        // GET: Whilelists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Whilelists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Name, Number,size,color,ImagePath,TimeOrder,TotalPrice")] Whitelist whilelist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(whilelist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(whilelist);
        }

        [HttpGet]
        public IActionResult Addtofavorist(int Id)
        {
            try
            {
                DateTime dateTime = DateTime.Now;

                var product = _context.Products.FirstOrDefault(q => q.Id == Id);
                var cart = _context.Whilelist.ToList();
                if (product != null)
                {

                    var existingCartItem = cart.FirstOrDefault(c => c.ProductId == Id);
                    if (existingCartItem != null)
                    {
                        return Json(new { success = false, message = "Sản phẩm đã co trong Favorist" });
                    }

                    var carts = new Whitelist()
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Number = product.Number,
                        TotalPrice = product.Price,
                        TimeOrder = dateTime,
                        ImagePath = product.ImagePath,
                    };

                    _context.Whilelist.Add(carts);

                    _context.SaveChanges();

                    return Json(new { success = true, message = "Sản phẩm đã được thêm vào Favorist" });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Đã xảy ra lỗi khi thêm sản phẩm vào giỏ hàng."); // Trả về mã lỗi 500 và thông báo lỗi
            }
        }

        // GET: Whilelists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whilelist = await _context.Whilelist.FindAsync(id);
            if (whilelist == null)
            {
                return NotFound();
            }
            return View(whilelist);
        }

        // POST: Whilelists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Name,size,color, Number, ImagePath,TimeOrder,TotalPrice")] Whitelist whilelist)
        {
            if (id != whilelist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(whilelist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhilelistExists(whilelist.Id))
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
            return View(whilelist);
        }
        public IActionResult DeleteFavourist(int id)
        {
            var whilelist = _context.Whilelist.Find(id);
            if (whilelist != null)
            {
                _context.Whilelist.Remove(whilelist);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Whilelists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whilelist = await _context.Whilelist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whilelist == null)
            {
                return NotFound();
            }

            return View(whilelist);
        }

        // POST: Whilelists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var whilelist = await _context.Whilelist.FindAsync(id);
            if (whilelist != null)
            {
                _context.Whilelist.Remove(whilelist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WhilelistExists(int id)
        {
            return _context.Whilelist.Any(e => e.Id == id);
        }
    }
}
