using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodBrosEntertainment.Models;
using RodBrosEntertainment.ViewModels;

namespace RodBrosEntertainment.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly StoreContext _context;

        public AdminAccountController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<User> users;
            List<AccountListViewModel> aList = new List<AccountListViewModel>();

            try
            {
                users = _context.Users
                    .OrderBy(p => p.UserId)
                    .ToList();

                foreach(User u in users)
                {
                    AccountListViewModel acc = u.CopyTo<AccountListViewModel>();

                    aList.Add(acc);
                }

                return View(aList);
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
                User user = GetUserById(id);

                AccountEditViewModel uEdit = user.CopyTo<AccountEditViewModel>();

                return View(uEdit);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }

        [HttpPost]
        public IActionResult Edit(AccountEditViewModel uEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(uEdit);
            }

            User user = uEdit.CopyTo<User>();

            //get password because it threw an error because it didn't have the password
            //User upass = GetUserById(uEdit.UserId);
            //upass = uEdit.CopyTo<User>();

            //edit record
            _context.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            AccountCreateViewModel uCreate = new AccountCreateViewModel();
            return View(uCreate);
        }

        [HttpPost]
        public IActionResult Create(AccountCreateViewModel uCreate)
        {
            if(!ModelState.IsValid)
            {
                return View(uCreate);
            }
            
            User user = uCreate.CopyTo<User>();

            _context.Add(new User { Email = user.Email, Password = user.Password, Name = user.Name, UserType = user.UserType, Street1 = user.Street1, Street2 = user.Street2, City = user.City, State = user.State, Country = user.Country, Active = Enums.ActiveStatus.Active});
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                User user = GetUserById(id);

                AccountDeleteViewModel uDel = user.CopyTo<AccountDeleteViewModel>();

                return View(uDel);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { Exception = ex });
            }
        }


        [HttpPost]
        public IActionResult Delete(AccountDeleteViewModel uDel)
        {
            User user = GetUserById(uDel.UserId);

            //change active state
            if (user.Active == Enums.ActiveStatus.Active)
            {
                user.Active = Enums.ActiveStatus.Inactive;
            }
            else
            {
                user.Active = Enums.ActiveStatus.Active;
            }

            //update and save
            _context.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            User user = GetUserById(id);

            AccountEditViewModel eDet = user.CopyTo<AccountEditViewModel>();

            return View(eDet);
        }

        [HttpPost]
        public IActionResult Details(AccountEditViewModel eDet)
        {
            if (!ModelState.IsValid)
            {
                return View(eDet);
            }

            return RedirectToAction("Index");
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