using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TMDTDbContext _context;

        public HomeController(ILogger<HomeController> logger, TMDTDbContext context)
        {
            _logger = logger;
            _context = context;
        }
		public ActionResult Index(int page = 1, int pageSize = 10)
		{
			// Lấy danh sách sản phẩm từ cơ sở dữ liệu
			var products = _context.Products.ToList();

			// Phân trang dữ liệu
			var productsPerPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

			return View(productsPerPage);
		}

		public async Task<IActionResult> GetAll()
        {

            var product = await _context.Products.ToListAsync();
            return View(product);
        }
        public IActionResult Search()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Search(string key)
        {
            var products = _context.Products.Where(x => EF.Functions.Like(x.Name, "%" + key + "%")).ToList();
            ViewBag.SearchKey = key;
            // Trả về số lượng sản phẩm tìm thấy cùng với danh sách sản phẩm
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> SearchByCategory(int id)
        {
            var products = await _context.Products.Where(x => x.CategoryId == id).ToListAsync();
            var categories = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.CategoryName = categories.Name.ToString();
            ViewBag.CategoryId = id;
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
