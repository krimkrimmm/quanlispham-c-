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
    public class PaymentResultsController : Controller
    {
        private readonly TMDTDbContext _context;

        public PaymentResultsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: PaymentResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentResult.ToListAsync());
        }

        // GET: PaymentResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paymentResult = await _context.PaymentResult
                .Include(x => x.ProductOrders).FirstOrDefaultAsync(m => m.Id == id);
            if (paymentResult == null)
            {
                return NotFound();
            }

            return View(paymentResult);
        }

        // GET: PaymentResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsSuccess,Message,Tennguoinhan,Sdt,Diachi,Createtime, price,TransactionId,Trangthaidonhang,Error")] PaymentResult paymentResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentResult);
        }

        // GET: PaymentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentResult = await _context.PaymentResult.FindAsync(id);
            if (paymentResult == null)
            {
                return NotFound();
            }
            return View(paymentResult);
        }

        // POST: PaymentResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsSuccess,Message,Tennguoinhan,Sdt,Diachi,Createtime,price,TransactionId,Trangthaidonhang,Error")] PaymentResult paymentResult)
        {
            if (id != paymentResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentResultExists(paymentResult.Id))
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
            return View(paymentResult);
        }

        // GET: PaymentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentResult = await _context.PaymentResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentResult == null)
            {
                return NotFound();
            }

            return View(paymentResult);
        }

        // POST: PaymentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentResult = await _context.PaymentResult.FindAsync(id);
            var donhang = await _context.Donhang.FindAsync(id);
            if (paymentResult != null)
            {
                _context.PaymentResult.Remove(paymentResult); 
                await _context.SaveChangesAsync();

            }
            if (donhang != null)
            {
                _context.Donhang.Remove(donhang);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentResultExists(int id)
        {
            return _context.PaymentResult.Any(e => e.Id == id);
        }
    }
}
