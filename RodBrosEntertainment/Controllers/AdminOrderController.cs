using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodBrosEntertainment.Models;
using RodBrosEntertainment.ViewModels;

namespace RodBrosEntertainment.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly StoreContext _context;

        public AdminOrderController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Order> order;
            List<OrderListViewModel> oList = new List<OrderListViewModel>();

            try
            {
                order = _context.Orders
                    .OrderBy(o => o.OrderId)
                    .ToList();

                foreach (Order o in order)
                {
                    OrderListViewModel ord = o.CopyTo<OrderListViewModel>();

                    //get user information
                    User user = GetUserById(o.UserId);
                    ord.Name = user.Name;
                    ord.Email = user.Email; 

                    oList.Add(ord);
                }

                return View(oList);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        public IActionResult StatusCart(int id)
        {
            try
            {
                Order order = GetOrderById(id);

                return View(order);
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult StatusCart(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.StatusId = Enums.OrderStatus.Cart;

            //Update audit records
            order.ChangeUserId = 1;
            order.ChangeDateTime = DateTime.Now;

            _context.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult StatusCreated(int id)
        {
            try
            {
                Order order = GetOrderById(id);

                return View(order);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult StatusCreated(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.StatusId = Enums.OrderStatus.Created;

            //Update audit records
            order.ChangeUserId = 1;
            order.ChangeDateTime = DateTime.Now;

            _context.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult StatusDelivered(int id)
        {
            try
            {
                Order order = GetOrderById(id);

                return View(order);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult StatusDelivered(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.StatusId = Enums.OrderStatus.Delivered;

            //Update audit records
            order.ChangeUserId = 1;
            order.ChangeDateTime = DateTime.Now;

            _context.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult StatusInTransit(int id)
        {
            try
            {
                Order order = GetOrderById(id);

                return View(order);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult StatusInTransit(Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.StatusId = Enums.OrderStatus.InTransit;

            //Update audit records
            order.ChangeUserId = 1;
            order.ChangeDateTime = DateTime.Now;

            _context.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            List<OrderProduct> oprod;
            List<OrderProdListViewModel> lprod = new List<OrderProdListViewModel>();

            try
            {
                oprod = _context.OrderProducts
                    .Where(p => p.OrderProductId == id)
                    .OrderBy(p => p.ProductId)
                    .ToList();

                foreach (OrderProduct p in oprod)
                {
                    OrderProdListViewModel pList = p.CopyTo<OrderProdListViewModel>();

                    //Get product information
                    Product product = GetProductById(p.ProductId);
                    pList.Name = product.Name;
                    pList.ProductType = product.ProductType;
                    pList.Price = product.Price;
                    pList.Active = product.Active;

                    //Get audit information from order
                    Order order = GetOrderById(p.OrderId);
                    pList.AddUserId = order.AddUserId;
                    pList.AddDateTime = order.AddDateTime;
                    pList.ChangeUserId = order.ChangeUserId;
                    pList.ChangeDateTime = order.ChangeDateTime;

                    lprod.Add(pList);
                }

                return View(lprod);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult Details(OrderProdListViewModel oprod)
        {
            if (!ModelState.IsValid)
            {
                return View(oprod);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ProdDetails(int id)
        {
            Product prod = GetProductById(id);

            return View(prod);
        }

        [HttpPost]
        public IActionResult ProdDetails(Product prod)
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

        public User GetUserById(int userId)
        {
            User user;

            user = _context.Users
                    .Where(p => p.UserId == userId)
                    .FirstOrDefault();

            return user;
        }

        public Order GetOrderById(int orderId)
        {
            Order order;

            order = _context.Orders
                    .Where(o => o.OrderId == orderId)
                    .FirstOrDefault();

            return order;
        }
    }
}