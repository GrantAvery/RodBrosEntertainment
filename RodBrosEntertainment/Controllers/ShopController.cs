using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RodBrosEntertainment.Models;
using RodBrosEntertainment.ViewModels;

namespace RodBrosEntertainment.Controllers
{
    public class ShopController : Controller
    {
        private readonly StoreContext _context;

        public ShopController(StoreContext context)
        {
            _context = context;
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
                    .ToList();

                return View(products);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        [Route("Shop/Product/{productId}")]
        public IActionResult Product(int productId)
        {
            Product product;

            try
            {
                product = _context.Products
                    .Where(p => p.ProductId == productId)
                    .Where(p => p.Active == Enums.ActiveStatus.Active)
                    .FirstOrDefault();

                return View(product);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        //[Authorize] // TODO when login is set up
        [HttpGet]
        public IActionResult Cart()
        {
            // A cart is really just an order that hasn't been completed yet
            Order order;
            List<OrderProduct> orderProducts;
            List<Product> products = new List<Product>();
            OrderFullViewModel orderFullVM;

            try
            {
                order = _context.Orders
                    .Where(p => p.UserId == 1) // TODO: temporary until a login is setup so we know what user is using the site
                    .Where(p => p.StatusId == Enums.OrderStatus.Cart)
                    .OrderBy(p => p.OrderId)
                    .FirstOrDefault(); // We'll have to make sure when completing an order we create a new order with OrderStatus.Cart for that user and always only one

                orderProducts = _context.OrderProducts
                    .Where(p => p.OrderId == order.OrderId)
                    .ToList();

                foreach (OrderProduct orderProduct in orderProducts)
                {
                    orderProduct.Product = _context.Products
                        .Where(p => p.ProductId == orderProduct.ProductId)
                        .FirstOrDefault();
                }

                orderFullVM = order.CopyTo<OrderFullViewModel>();
                orderFullVM.OrderProducts = orderProducts;

                return View(orderFullVM);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        [Route("Shop/AddToCart/{productId}")]
        public IActionResult AddToCart(int productId)
        {
            Order order;
            List<OrderProduct> orderProducts;

            try
            {
                order = _context.Orders
                    .Where(p => p.UserId == 1) // TODO: temporary until a login is setup so we know what user is using the site
                    .Where(p => p.StatusId == Enums.OrderStatus.Cart)
                    .FirstOrDefault(); // We'll have to make sure when completing an order we create a new order with OrderStatus.Cart and always only one

                orderProducts = _context.OrderProducts
                    .Where(p => p.OrderId == order.OrderId)
                    .ToList();

                bool alreadyInOrder = false;

                foreach (OrderProduct orderProduct in orderProducts)
                {
                    // The product is already in the cart, so increase the quantity
                    if (orderProduct.ProductId == productId)
                    {
                        orderProduct.Quantity++;
                        _context.Update(orderProduct);
                        alreadyInOrder = true;
                    }
                }

                if (!alreadyInOrder)
                {
                    _context.Add(new OrderProduct { OrderId = order.OrderId, ProductId = productId, Quantity = 1 });
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Shop");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpGet]
        [Route("Shop/DeleteFromCart/{orderProductId}")]
        public IActionResult DeleteFromCart(int orderProductId)
        {
            OrderProduct orderProduct;

            try
            {
                orderProduct = _context.OrderProducts
                    .Where(p => p.OrderProductId == orderProductId)
                    .FirstOrDefault();

                _context.Remove(orderProduct);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }

            return RedirectToAction("Cart", "Shop");
        }

        [HttpGet]
        public IActionResult CheckOut(int id)
        {
            Order order;
            OrderCheckOutViewModel orderCheckOutVM;

            try
            {
                order = _context.Orders
                    .Where(p => p.OrderId == id)
                    .FirstOrDefault();

                orderCheckOutVM = order.CopyTo<OrderCheckOutViewModel>();

                return View(orderCheckOutVM);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult CheckOut(OrderCheckOutViewModel orderCheckOutVM)
        {
            if (!ModelState.IsValid)
            {
                return View(orderCheckOutVM);
            }

            Order order = orderCheckOutVM.CopyTo<Order>();

            order.OrderDate = DateTime.Now;
            order.StatusId = Enums.OrderStatus.Created;

            //audit records
            order.ChangeUserId = 1;
            order.ChangeDateTime = DateTime.Now;

            //edit record
            _context.Update(order);
            _context.SaveChanges();

            // Make a fresh order to sit in the Cart status
            _context.Add(new Order { UserId = 1, StatusId = Enums.OrderStatus.Cart, AddUserId = 2, AddDateTime = DateTime.Now });
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // TODO this is temporary to give us easy view into what orders exist
        //[Authorize] // TODO when login is set up
        [HttpGet]
        [Route("Shop/Orders")]
        public IActionResult Orders()
        {
            List<Order> orders;

            try
            {
                orders = _context.Orders
                    .OrderBy(p => p.OrderId)
                    .ToList();

                return View(orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        //[Authorize] // TODO when login is set up
        [HttpGet]
        [Route("Shop/OrdersByUser/{userId}")]
        public IActionResult OrdersByUser(int userId)
        {
            List<Order> orders;

            try
            {
                orders = _context.Orders
                    .OrderBy(o => o.OrderId)
                    .Where(o => o.UserId == userId)
                    .Where(o => o.StatusId != Enums.OrderStatus.Cart)
                    .ToList();

                return View(orders);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        //[Authorize] // TODO when login is set up
        [HttpGet]
        [Route("Shop/Order/{orderId}")]
        public IActionResult Order(int orderId)
        {
            Order order;
            List<OrderProduct> orderProducts;
            List<Product> products = new List<Product>();
            OrderFullViewModel orderFullVM;

            try
            {
                order = _context.Orders
                    .Where(p => p.OrderId == orderId)
                    .FirstOrDefault();

                orderProducts = _context.OrderProducts
                    .Where(p => p.OrderId == order.OrderId)
                    .ToList();

                foreach (OrderProduct orderProduct in orderProducts)
                {
                    orderProduct.Product = _context.Products
                        .Where(p => p.ProductId == orderProduct.ProductId)
                        .FirstOrDefault();
                }

                orderFullVM = order.CopyTo<OrderFullViewModel>();
                orderFullVM.OrderProducts = orderProducts;

                return View(orderFullVM);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }
    }
}

