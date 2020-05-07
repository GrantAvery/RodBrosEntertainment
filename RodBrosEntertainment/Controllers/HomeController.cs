using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RodBrosEntertainment.Models;

namespace RodBrosEntertainment.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(StoreContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products;

            try
            {
                products = _context.Products
                    .OrderBy(p => p.ProductId)
                    .Where(p => p.Active == Enums.ActiveStatus.Active)
                    .Take(3)
                    .ToList();

                return View(products);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
