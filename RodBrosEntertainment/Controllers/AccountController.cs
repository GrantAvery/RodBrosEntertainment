using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodBrosEntertainment.Models;

namespace RodBrosEntertainment.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;

        public AccountController(StoreContext context)
        {
            _context = context;
        }

        //[Authorize] // TODO when login is set up
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // TODO how to get current user? Need to build out login system
                User user = GetUserById(1);

                return View(user);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        public User GetUserById(int userId)
        {
            User user;

            user = _context.Users
                    .Where(p => p.UserId == userId)
                    .FirstOrDefault();

            return user;
        }
    }
}