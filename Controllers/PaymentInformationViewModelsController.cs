using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class PaymentInformationViewModelsController : Controller
    {

        private readonly TMDTDbContext _context;

        public PaymentInformationViewModelsController(TMDTDbContext context)
        {
            _context = context;
        }

        // GET: PaymentInformationViewModels
        public async Task<IActionResult> Index()
        {
            var viewModel = await _context.PaymentInformationViewModel
                                    .OrderByDescending(c => c.Id)
                                    .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound(); // Handle scenario where no data is found
            }

            return View( viewModel); // Pass a single instance to the view
        }

        // GET: PaymentInformationViewModels/Details/5
        public IActionResult Details()
        {
            var viewModel =  _context.PaymentInformationViewModel
                                   .OrderByDescending(c => c.Id)
                                   .FirstOrDefault();

            if (viewModel == null)
            {
                return NotFound(); // Handle scenario where no data is found
            }

            return View(viewModel); // Pass a single instance to the view
        }

        // GET: PaymentInformationViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentInformationViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalPrice,Voucher,PaymentMethod,QRCode,OrderNumber")] PaymentInformationViewModel paymentInformationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentInformationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentInformationViewModel);
        }

        // GET: PaymentInformationViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInformationViewModel = await _context.PaymentInformationViewModel.FindAsync(id);
            if (paymentInformationViewModel == null)
            {
                return NotFound();
            }
            return View(paymentInformationViewModel);
        }

        

        // POST: PaymentInformationViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalPrice,Voucher,PaymentMethod,QRCode,OrderNumber")] PaymentInformationViewModel paymentInformationViewModel)
        {
            if (id != paymentInformationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInformationViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInformationViewModelExists(paymentInformationViewModel.Id))
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
            return View(paymentInformationViewModel);
        }

        // GET: PaymentInformationViewModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInformationViewModel = await _context.PaymentInformationViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInformationViewModel == null)
            {
                return NotFound();
            }

            return View(paymentInformationViewModel);
        }

        // POST: PaymentInformationViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentInformationViewModel = await _context.PaymentInformationViewModel.FindAsync(id);
            if (paymentInformationViewModel != null)
            {
                _context.PaymentInformationViewModel.Remove(paymentInformationViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult DeleteDonhang()
        {
            try
            {
                    var orderToDelete = _context.PaymentInformationViewModel.FirstOrDefault(q => q.Id == 1);
        
                    if (orderToDelete == null)
                    {
                        return NotFound(); // Trả về NotFound nếu không tìm thấy đơn hàng với Id tương ứng
                    }

                    _context.PaymentInformationViewModel.Remove(orderToDelete);
                    _context.SaveChanges();
                
                return Json(new { message = "success" }); // Redirect to a relevant page after clearing
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine(ex.Message);
                return BadRequest("Error clearing data."); // Return a bad request response or handle as needed
            }
        }

        private bool PaymentInformationViewModelExists(int id)
        {
            return _context.PaymentInformationViewModel.Any(e => e.Id == id);
        }
    }
}
