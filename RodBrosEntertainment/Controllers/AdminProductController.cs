using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodBrosEntertainment.Models;
using RodBrosEntertainment.ViewModels;

namespace RodBrosEntertainment.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly StoreContext _context;

        public AdminProductController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> prod;
            List<ProductListViewModel> lprod = new List<ProductListViewModel>();

            try
            {
                prod = _context.Products
                    .OrderBy(p => p.ProductId)
                    .ToList();

                foreach (Product p in prod)
                {
                    ProductListViewModel pList = p.CopyTo<ProductListViewModel>();

                    lprod.Add(pList);
                }

                return View(lprod);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Product prod = GetProductById(id);

                ProductEditViewModel pEdit = prod.CopyTo<ProductEditViewModel>();

                return View(pEdit);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel pEdit)
        {
            if(!ModelState.IsValid)
            {
                return View(pEdit);
            }

            Product prod = pEdit.CopyTo<Product>();

            //audit records
            prod.ChangeUserId = 1;
            prod.ChangeDateTime = DateTime.Now;

            //edit record
            _context.Update(prod);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductCreateViewModel pCreate = new ProductCreateViewModel();
            return View(pCreate);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel pCreate)
        {
            Product prod = pCreate.CopyTo<Product>();

            //add audit data to prod
            prod.AddUserId = 1;
            prod.AddDateTime = DateTime.Now;

            //add record to database
            //currently defaulting prod type to online
            _context.Add(new Product { Name = prod.Name, Description = prod.Description, ImageUrl = prod.ImageUrl, Price = prod.Price, ProductType = Enums.ProductType.Online, Active = Enums.ActiveStatus.Active, AddUserId = prod.AddUserId, AddDateTime = prod.AddDateTime });
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Product prod = GetProductById(id);

                ProductEditViewModel pDel = prod.CopyTo<ProductEditViewModel>();

                return View(pDel);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }


        [HttpPost]
        public IActionResult Delete(ProductEditViewModel pDel)
        {
            Product prod = GetProductById(pDel.ProductId);

            //change active state
            if (prod.Active == Enums.ActiveStatus.Active)
            {
                prod.Active = Enums.ActiveStatus.Inactive;
            }
            else
            {
                prod.Active = Enums.ActiveStatus.Active;
            }

            //audit records
            prod.ChangeUserId = 1;
            prod.ChangeDateTime = DateTime.Now;

            //update and save
            _context.Update(prod);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Product prod = GetProductById(id);

            return View(prod);
        }

        [HttpPost]
        public IActionResult Details(Product prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }

            return RedirectToAction("Index");
        }

        public Product GetProductById(int prodId)
        {
            Product prod;

            prod = _context.Products
                    .Where(p => p.ProductId == prodId)
                    .FirstOrDefault();

            return prod;
        }

    }
}