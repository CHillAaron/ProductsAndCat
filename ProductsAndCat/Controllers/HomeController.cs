using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsAndCat.Context;
using ProductsAndCat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndCat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ProdAndCatDbContext _context;

        public HomeController(ILogger<HomeController> logger, ProdAndCatDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() 
        {
            ViewBag.allProducts = _context.Products.Include(e=>e.AllCategories).ToList();
            return View();
        }
        [HttpPost("Create/Product")]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Redirect($"/");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("ProductDetail/{ProductId}")]
        public IActionResult Productdetail(int productId)
        {
            ViewBag.show = _context.Products
                                   .Include(p=>p.AllCategories)
                                   .ThenInclude(nav=>nav.ListOfCategories)
                                   .FirstOrDefault(p=>p.ProductId == productId);


            ViewBag.UnassignedCats = _context.Categories.Where(c => !c.allProducts.Any(p=>p.ProductId == productId)).ToList();

            ViewBag.allCategories = _context.Categories;

            ViewBag.ProductId = productId;
            return View();
        }
        [HttpPost("add/TheCategory")]
        public IActionResult AddCategory(ProdAndCat fromForm)
        {
            int productId = fromForm.ProductId;
            //ProdAndCat addCat = new ProdAndCat();
            //addCat.CategoryId = categoryId;
            //addCat.ProdcutId = productId;
            _context.ProdsAndCats.Add(fromForm);
            //_context.ProdsAndCats.Add(addCat);
            _context.SaveChanges();
            
            return Redirect($"/ProductDetail/{productId}");
        }
        [HttpGet("Category")]
        public IActionResult Category()
        {
            ViewBag.allCategories = _context.Categories.ToList();
            return View();
        }
        [HttpPost("Create/Category")]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return Redirect($"/Category");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("CategoryDetail/{CategoryId}")]
        public IActionResult Categorydetail(int categoryId)
        {
            ViewBag.show = _context.Categories
                                   .Include(p => p.allProducts)
                                   .ThenInclude(nav => nav.ListOfProducts)
                                   .FirstOrDefault(p => p.CategoryId == categoryId);


            ViewBag.UnassignedProducts = _context.Products.Where(c => !c.AllCategories.Any(p => p.CategoryId == categoryId)).ToList();

            ViewBag.allCategories = _context.Categories;

            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpPost("add/TheProduct")]
        public IActionResult AddProduct(ProdAndCat fromForm)
        {
            int categoryId = fromForm.CategoryId;
            //ProdAndCat addCat = new ProdAndCat();
            //addCat.CategoryId = categoryId;
            //addCat.ProdcutId = productId;
            _context.ProdsAndCats.Add(fromForm);
            //_context.ProdsAndCats.Add(addCat);
            _context.SaveChanges();

            return Redirect($"/CategoryDetail/{categoryId}");
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
