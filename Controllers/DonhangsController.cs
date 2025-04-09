using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class DonhangsController : Controller
    {
        private readonly TMDTDbContext _context;

        public DonhangsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: Donhangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donhang.ToListAsync());
        } 
        
        public async Task<IActionResult> Dagiaohang(int? Id)
        {
            var trangthai = "Da giao";

            var Donhang = await _context.Donhang
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == Id);
            var paymentresult = await _context.PaymentResult
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == Id);
            if (Donhang == null || paymentresult == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if(Donhang.Trangthaidonhang == "Da giao" && paymentresult.Trangthaidonhang == "Da giao")
            {
                return RedirectToAction(nameof(Index));
            }
            else {
                Donhang.Trangthaidonhang = trangthai;
                paymentresult.Trangthaidonhang = trangthai;
                _context.Donhang.Update(Donhang);
                _context.PaymentResult.Update(paymentresult);
               _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Chuagiaohang(int? Id)
        {
            var trangthai = "Chua giao";

            var Donhang = await _context.Donhang
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == Id);
            var paymentresult = await _context.PaymentResult
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == Id);
            if (Donhang == null || paymentresult == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (Donhang.Trangthaidonhang == "Chua giao" && paymentresult.Trangthaidonhang == "Chua giao")
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Donhang.Trangthaidonhang = trangthai;
                paymentresult.Trangthaidonhang = trangthai;
                _context.Donhang.Update(Donhang);
                _context.PaymentResult.Update(paymentresult);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PrintDonhang()
        {
            return View(await _context.Donhang.ToListAsync());
        }

        // GET: Donhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Donhang = await _context.Donhang
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == id);
            if (Donhang == null)
            {
                return NotFound();
            }

            return View(Donhang);
        }

        [HttpPost]
        public IActionResult GeneratePdfForSelectedOrders(List<int> selecteddonhangIds)
        {
            if (selecteddonhangIds == null || selecteddonhangIds.Count == 0)
            {
                return BadRequest("No orders selected.");
            }

            var responseList = new List<object>();

            foreach (var id in selecteddonhangIds)
            {
                var response = new
                {
                    redirectUrl = Url.Action("PrintDetails", "Donhangs", new { Id = id })
                };
                responseList.Add(response);
            }

            return Json(responseList);
        }

        // Action method to display order details in a new tab/window
        public IActionResult PrintDetails(int? Id)
        {
            // Retrieve selected order IDs from TempData
            var selectedDonhangs =  _context.Donhang.Include(c => c.ProductOrders).FirstOrDefault(c => c.Id == Id);
            return View(selectedDonhangs);
        }

        // GET: Donhangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsSuccess,Message,Tennguoinhan,Sdt,Diachi,Createtime,price,TransactionId,Trangthaidonhang,Error")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donhang);
        }

        // GET: Donhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhang.FindAsync(id);
            if (donhang == null)
            {
                return NotFound();
            }
            return View(donhang);
        }

        // POST: Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsSuccess,Message,Tennguoinhan,Sdt,Diachi,Createtime,price,TransactionId,Trangthaidonhang,Error")] Donhang donhang)
        {
            if (id != donhang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonhangExists(donhang.Id))
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
            return View(donhang);
        }

        // GET: Donhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donhang = await _context.Donhang
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donhang == null)
            {
                return NotFound();
            }

            return View(donhang);
        }

        // POST: Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donhang = await _context.Donhang.FindAsync(id);
            if (donhang != null)
            {
                _context.Donhang.Remove(donhang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonhangExists(int id)
        {
            return _context.Donhang.Any(e => e.Id == id);
        }
    }
}
