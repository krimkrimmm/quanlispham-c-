using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using TMDT.Areas.Identity.Data;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.CodeAnalysis;
using TMDT.Services;
using System.Collections;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ZaloPay.Helper; // HmacHelper, RSAHelper, HttpHelper, Utils (tải về ở mục DOWNLOADS)
using ZaloPay.Helper.Crypto;
using Newtonsoft.Json;

namespace TMDT.Controllers
{
    public class PaymentInformationsController : Controller
    {
        private readonly TMDTDbContext _context;
        private readonly UserManager<LOGINUser> _UserManager;
        private readonly IVoucherService _voucherService;
        private readonly ILocationService _locationService;

        public PaymentInformationsController(TMDTDbContext context, UserManager<LOGINUser> UserManager, IVoucherService voucherService, ILocationService locationService)
        {
            _context = context;
            _UserManager = UserManager;
            _voucherService = voucherService;
            _locationService = locationService;
        }

        //During process payment 
        /*var paymentResult = await ProcessPayment(price);
                if (paymentResult.IsSuccess)
                {
                    // Only remove items if payment is successful
                    var itemsToRemove = await _context.Cart.Where(x => cartItemIds.Contains(x.Id)).ToListAsync();
                    _context.Cart.RemoveRange(itemsToRemove);
                    await _context.SaveChangesAsync();

                    return Json(new { redirectUrl = Url.Action("Index", "PaymentResults"), message = paymentResult.Message });
                }
                else
                {
                    // Handle failed payment
                    return Json(new { message = "Payment failed: " + paymentResult.Message });
                }*/
        // POST: PaymentInformations/Index

        [HttpPost]
        public IActionResult ValidateVoucher(int voucherId)
        {
            var result = _voucherService.ValidateVoucher(voucherId);
            if (result.IsValid)
            {
                return Json(new { success = true, discount = result.Discount });
            }
            return Json(new { success = false, message = "Voucher không hợp lệ hoặc đã được sử dụng." });
        }

        // Action method to receive data and store in instance fields
        [HttpPost]
        public IActionResult CreateDonhang(float price, List<int> cartItemIds)
        {
            try
            {
                if (price == 0 || cartItemIds == null || cartItemIds.Count == 0)
                {
                    // Handle missing data scenario
                    var message = new
                    {
                        message = "Hay chon san pham can thanh toan"
                    };
                    return Json( message);
                }
                Console.WriteLine(price);
                Console.WriteLine($"Item Id type: {price.GetType().Name} : {price}");

                Console.WriteLine("loi cho nay\n" + price.ToString());

                // Example voucher and payment method (replace with actual logic to fetch these values)
                var vouchers = _voucherService.GetAvailableVouchers();

                string paymentMethod = "Credit Card";
                // Fetch cart items from database based on cartItemIds
                Console.WriteLine("loi cho nay hai\n");
                // Generate QR code and order number (example logic)
                string qrCode = GenerateQRCode("Order123456789");
                string orderNumber = "Order123456789"; // Example order number
                Console.WriteLine("loi cho nay ba\n");
                var viewModel = new PaymentInformationViewModel()
                {
                    CartItems = GetCartItemsByIds(cartItemIds),
                    TotalPrice = price,
                    Vouchers = vouchers,
                    PaymentMethod = paymentMethod,
                    QRCode = qrCode,
                    OrderNumber = orderNumber
                };
                
                Console.WriteLine("loi cho nay ba cham 5\n");
                _context.PaymentInformationViewModel.Add(viewModel);
                _context.SaveChanges();
                Console.WriteLine("loi cho nay ba cham 6\n");
                var Model =  _context.PaymentInformationViewModel
                                    .OrderByDescending(c => c.Id)
                                    .FirstOrDefault();

                Console.WriteLine("loi cho nay bon\n");
                var responseData = new
                {
                    redirectUrl = Url.Action("Index", "PaymentInformations", new { id = Model.Id })
                };
                Console.WriteLine("loi cho nay nam\n");
                return Json(responseData);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    message = "An error occurred while creating the order.",
                    error = ex.Message
                };
                return Json(errorResponse);
            }
        }

        [HttpPost]
        public IActionResult GetDistrictbyId(int cityId)
        {
            var districts = _locationService.GetDistrictsByCity(cityId);
            if (districts != null)
            {
                // Chỉ lấy các thuộc tính cần thiết của huyện/thị xã (ví dụ: Name)
                var districtList = districts.Select(d => new { Id = d.Id, Name = d.Name }).ToList();
                return Json(new { success = true, districts = districtList });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        // Action method to retrieve data from instance fields
        public IActionResult Index(int? id)
        {
            var cities = _locationService.GetCities();
            ViewBag.Cities = cities;
            if (id == null)
            {
                return BadRequest();
            }

            var viewModel = _context.PaymentInformationViewModel
                .Include(vm => vm.CartItems).Include(c => c.Vouchers) // Đảm bảo load dữ liệu CartItems
                .FirstOrDefault(m => m.Id == id);

            if (viewModel == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy dữ liệu
            }

            return View(viewModel); // Truyền instance của PaymentInformationViewModel cho view
        }

        // Hàm lấy thông tin chi tiết sản phẩm từ cartItemIds (cần điều chỉnh cho phù hợp)
        private List<Cart> GetCartItemsByIds(List<int> cartItemIds)
        {
            var Items = new List<Cart>();

            foreach (var itemId in cartItemIds)
            {
                Console.WriteLine($"Item Id type: {itemId.GetType().Name} : {itemId}");
                // Chuyển đổi itemId từ string sang int
                    // Tìm sản phẩm trong cơ sở dữ liệu dựa trên itemIdInt (kiểu int)
                    var CartItem = _context.Cart.FirstOrDefault(c => c.Id == itemId);

                    if (CartItem != null)
                    {
                        Items.Add(CartItem);
                    }
            }
            return Items;
        }

        // Hàm tính tổng giá tiền của các sản phẩm trong giỏ hàng (cần điều chỉnh cho phù hợp)


        // Hàm tạo mã QR (cần điều chỉnh cho phù hợp)
        private string GenerateQRCode(string orderNumber)
        {
            // Thực hiện logic để tạo mã QR dựa trên orderNumber
            // Ví dụ:
            return "QRCode123";
        }


        // GET: PaymentInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var paymentInformation = await _context.PaymentInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInformation == null)
            {
                return NotFound();
            }

            return View(paymentInformation);
        }

        // GET: PaymentInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Address,Email,CreditCardNumber,ExpiryDate,CVV,BillingAddress")] PaymentInformation paymentInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentInformation);
        }

        // GET: PaymentInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInformation = await _context.PaymentInformation.FindAsync(id);
            if (paymentInformation == null)
            {
                return NotFound();
            }
            return View(paymentInformation);
        }

        // POST: PaymentInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Address,Email,CreditCardNumber,ExpiryDate,CVV,BillingAddress")] PaymentInformation paymentInformation)
        {
            if (id != paymentInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentInformationExists(paymentInformation.Id))
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
            return View(paymentInformation);
        }

        // GET: PaymentInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentInformation = await _context.PaymentInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentInformation == null)
            {
                return NotFound();
            }

            return View(paymentInformation);
        }

        // POST: PaymentInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentInformation = await _context.PaymentInformation.FindAsync(id);
            if (paymentInformation != null)
            {
                _context.PaymentInformation.Remove(paymentInformation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentInformationExists(int id)
        {
            return _context.PaymentInformation.Any(e => e.Id == id);
        }

        // Xu ly thanh toan [HttpPost]
        [HttpPost]
        public IActionResult ProcessPaymentCOD(string pttt, string email, string phone, string name, string city, string district, string address, string notes, float totalPrice, string QRcode, List<int> productIds)
        {
            try
            {
                if (totalPrice <= 0)
                {
                    return BadRequest(new { message = "Total price must be greater than zero." });
                }
                Console.WriteLine("co chay den day");


                var paymentResult = new PaymentResult()
                {
                    IsSuccess = true,
                    Message = notes,
                    Diachi = $"{address}, {district}, {city}",
                    Sdt = phone,
                    Tennguoinhan = $"{name}",
                    ProductOrders = GetProductsbyproductid(productIds),
                    price = totalPrice,
                    TransactionId = pttt,
                    Trangthaidonhang = "Chua giao",
                    QRcode = GenerateQRCode(QRcode),
                    Createtime = DateTime.Now,
                };

                _context.PaymentResult.Add(paymentResult);
                _context.SaveChanges();
                var donhangcangiao = new Donhang()
                {
                    IsSuccess = true,
                    Message = notes,
                    Diachi = $"{address}, {district}, {city}",
                    Sdt = phone,
                    Tennguoinhan = $"{name}",
                    ProductOrders = GetProductsbyproductid(productIds),
                    price = totalPrice,
                    TransactionId = pttt,
                    Trangthaidonhang = "chua giao",
                    QRcode = GenerateQRCode(QRcode),
                    Createtime = DateTime.Now,
                };

                _context.Donhang.Add(donhangcangiao);
                _context.SaveChanges();

                return Json(new { success = true, message = "Payment processed successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        private List<ProductOrders> GetProductsbyproductid(List<int> productids)
        {
            var products = new List<ProductOrders>();
            foreach (var itemId in productids)
            {
                Console.WriteLine($"Item Id type: {itemId.GetType().Name} : {itemId}");
                // Chuyển đổi itemId từ string sang int
                // Tìm sản phẩm trong cơ sở dữ liệu dựa trên itemIdInt (kiểu int)
                var productItem = _context.Products.FirstOrDefault(c => c.Id == itemId);
                var cartitem = _context.Cart.FirstOrDefault(c => c.ProductId == itemId);
                var product = new ProductOrders() 
                { 
                    Number = cartitem.Number,
                    Color = productItem.Color,
                    decrease  = productItem.decrease,
                    Name = productItem.Name,
                    Price = cartitem.TotalPrice,
                    Size = cartitem.Size,
                    Description = productItem.Description,
                   CategoryId = productItem.CategoryId, 
                    ImagePath = productItem.ImagePath,
                };
                products.Add(product);
            }
            return products;
        }

        static string appid = "553";
        static string key1 = "9phuAOYhan4urywHTh0ndEXiV3pKHr5Q";
        static string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";
        //static string createOrderUrl = "https://zalopay.com.vn/v001/tpe/createorder";

        public async Task<IActionResult> ProcessPaymentThroughGatewayZalopay(string pttt,string email, string phone, string name, string city, string district, string address, string notes, float totalPrice, string QRcode, List<int> productIds)
        {
            try
            {
                var transid = Guid.NewGuid().ToString();
                var embeddata = new { merchantinfo = "embeddata123" };
                var items = new[]{
                    new { itemid = "knb", itemname = name, itemprice = totalPrice, itemquantity = 1 }
                };
                var param = new Dictionary<string, string>();

                param.Add("appid", appid);
                param.Add("appuser", name);
                param.Add("apptime", Utils.GetTimeStamp().ToString());
                param.Add("amount", totalPrice.ToString()); // Use the actual total price
                param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transid); // Transaction code with format yyMMdd_xxxx
                param.Add("embeddata", JsonConvert.SerializeObject(embeddata));
                param.Add("item", JsonConvert.SerializeObject(items));
                param.Add("description", "ZaloPay demo");
                param.Add("bankcode", "zalopayapp");

                var data = appid + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                           + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

                var result = await HttpHelper.PostFormAsync(createOrderUrl, param);
                Console.WriteLine("tai day");
                foreach (var entry in result)
                {
                    Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
                }

                // Assuming result contains a status or message indicating success or failure
                if (result.ContainsKey("returncode") && result["returncode"].ToString() == "1")
                {

                    var paymentResult = new PaymentResult()
                    {
                        IsSuccess = true,
                        Message = notes,
                        Diachi = $"{address}, {district}, {city}",
                        Sdt = phone,
                        Tennguoinhan = $"{name}",
                        ProductOrders = GetProductsbyproductid(productIds),
                        price = totalPrice,
                        TransactionId = pttt,
                        Trangthaidonhang = "Chua giao",
                        QRcode = GenerateQRCode(QRcode),
                        Createtime = DateTime.Now,
                    };

                    _context.PaymentResult.Add(paymentResult);
                    _context.SaveChanges();
                    var donhangcangiao = new Donhang()
                    {
                        IsSuccess = true,
                        Message = notes,
                        Diachi = $"{address}, {district}, {city}",
                        Sdt = phone,
                        Tennguoinhan = $"{name}",
                        ProductOrders = GetProductsbyproductid(productIds),
                        price = totalPrice,
                        TransactionId = pttt,
                        Trangthaidonhang = "chua giao",
                        QRcode = GenerateQRCode(QRcode),
                        Createtime = DateTime.Now,
                    };

                    _context.Donhang.Add(donhangcangiao);
                    _context.SaveChanges();
                    if (result.ContainsKey("orderurl"))
                    {
                        return Json(result["orderurl"]);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Order URL not found in response." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Payment failed through ZaloPay." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> ProcessPaymentThroughGatewayMomo(string pttt, string email, string city, string district, string address, string notes, float totalPrice, string QRcode, List<int> productIds)
        {
            try
            {
                if (totalPrice <= 0)
                {
                    return BadRequest(new { message = "Total price must be greater than zero." });
                }

                var user = await _UserManager.GetUserAsync(User);
                if (user == null)
                {
                    return Unauthorized(new { message = "User not authenticated." });
                }

                var paymentResult = new PaymentResult()
                {
                    IsSuccess = true,
                    Message = notes,
                    Diachi = $"{address}, {district}, {city}",
                    Sdt = user.PhoneNumber,
                    Tennguoinhan = $"{user.UserName}",
                    ProductOrders = GetProductsbyproductid(productIds),
                    price = totalPrice,
                    TransactionId = pttt,
                    Trangthaidonhang = "Chua giao",
                    QRcode = GenerateQRCode(QRcode),
                    Createtime = DateTime.Now,
                };

                _context.PaymentResult.Add(paymentResult);
                await _context.SaveChangesAsync();
                var donhangcangiao = new Donhang()
                {
                    IsSuccess = true,
                    Message = notes,
                    Diachi = $"{address}, {district}, {city}",
                    Sdt = user.PhoneNumber,
                    Tennguoinhan = $"{user.UserName}",
                    ProductOrders = GetProductsbyproductid(productIds),
                    price = totalPrice,
                    TransactionId = pttt,
                    Trangthaidonhang = "chua giao",
                    QRcode = GenerateQRCode(QRcode),
                    Createtime = DateTime.Now,
                };

                _context.Donhang.Add(donhangcangiao);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Payment processed successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private static void SendConfirmationEmail(string email)
        {
            if (email == null)
            {
                Console.WriteLine("Địa chỉ email không được null.");
                return;
            }

            // Email của người gửi
            string fromAddress = "giangthanhoang2003@gmail.com";
            // Mật khẩu email của người gửi
            string password = "giangvanhung";

            // Tạo đối tượng MailMessage

			// Cấu hình SmtpClient
			var client = new SmtpClient("smtp.gmail.com")
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress, password),
				EnableSsl = true,
				Port = 587 // Adjust if necessary
			};
			var mail = new MailMessage
			{
				From = new MailAddress(fromAddress),
				Subject = "Xác nhận Payment",
				Body = "Xin chào, bạn đã thanh toán giỏ hàng thành công.",
			    IsBodyHtml = true
			};
			mail.To.Add(email);
			// Gửi email
			try
            {
                client.SendMailAsync(mail);
                Console.WriteLine("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi xảy ra: " + ex.Message);
            }
        }


        private static void HandlePaymentError(string error)
        {
            // Xử lý lỗi thanh toán
            // ...

        }

    }
}
