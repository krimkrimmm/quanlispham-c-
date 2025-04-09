using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class CartsController : Controller
    {
        private readonly TMDTDbContext _context;

        public CartsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cart.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddToCart(int Id, int quantity)
        {
            try
            {
                DateTime dateTime = DateTime.Now;

                var product = _context.Products.FirstOrDefault(q => q.Id == Id);
                var cart = _context.Cart.ToList();
                if (product != null)
                {
                    if (quantity <= 0)
                    {
                        // Nếu số lượng mua là 0 hoặc âm, không thực hiện thêm vào giỏ hàng
                        return BadRequest("Số lượng không hợp lệ");
                    }

                    var existingCartItem = cart.FirstOrDefault(c => c.ProductId == Id);
                    if (existingCartItem != null)
                    {
                        return Json(new { success = false, message = "Sản phẩm đã co trong giỏ hàng" });
                    }

                    var carts = new Cart()
                    {
                        ProductId = product.Id,
                        Number = quantity, // Sử dụng số lượng mua được truyền từ tham số
                        Name = product.Name,
                        TimeOrder = dateTime,
                        Color = product.Color,
                        Size = product.Size,
                        TotalPrice = product.Price * quantity, // Tính tổng giá sản phẩm dựa trên số lượng mua
                        ImagePath = product.ImagePath,
                    };

                    _context.Cart.Add(carts);

                    product.Number -= quantity; // Giảm số lượng sản phẩm còn lại trong kho

                    if (product.Number < 0)
                    {
                        product.Number = 0;
                    }

                    _context.Products.Update(product);
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng" });
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

        [HttpPost]
        public IActionResult DeletePaidItems([FromBody] List<int> cartItemIds)
        {
            try
            {
                foreach (var itemId in cartItemIds)
                {
                    var cartItem = _context.Cart.Find(itemId);
                    if (cartItem != null)
                    {
                        _context.Cart.Remove(cartItem);
                    }
                }
                _context.SaveChanges();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /*[HttpGet]
        public IActionResult CheckVoucher(int Id, string voucher)
        {
            var products = _context.Products.FirstOrDefault();
            var cart = _context.Cart.FirstOrDefault(q => q.Id == Id);

            if (products != null && cart != null && cart.DiscountApplied == false && products.VoucherCode == voucher)
            {
                cart.TotalPrice = cart.TotalPrice * (products.decrease / 100);
                cart.DiscountApplied = true;
                _context.Update(cart);
                _context.SaveChanges();
                return Json(new { isValid = true }); // Trả về JSON để xử lý kết quả phía client
            }
            else
            {
                return Json(new { isValid = false }); // Trả về JSON để xử lý kết quả phía client
            }

            
        }
        */


        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProductId, Name, ImagePath,size,color,Number,TotalPrice,DiscountApplied")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, int number)
        {
            var cart = await _context.Cart.FindAsync(id);
            var product = await _context.Products.FirstOrDefaultAsync(q => q.Id == cart.ProductId);
            if (cart != null)
            {
                cart.Number = number;
                cart.TotalPrice = number * product.Price;

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cart);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CartExists(cart.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }
            return View(cart);
        }

        /*[HttpPost] // Change to HttpPost
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id, int number)
        {
            var cart = await _context.Cart.FindAsync(Id);
            if (cart == null)
            {
                return NotFound();
            }

            cart.Number = number;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Json(new { isValid = false });
        }*/


        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletebyId(int Id)
        {
            try
            {
                var cartItem = await _context.Cart.ToListAsync();
                var cart = await _context.Cart.FindAsync(Id);
                if (cart != null)
                {
                    _context.Cart.Remove(cart);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                var cartItem = await _context.Cart.ToListAsync();
                if (cartItem != null)
                {
                    _context.Cart.RemoveRange(cartItem);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
