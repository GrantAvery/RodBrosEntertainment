using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RodBrosEntertainment.Controllers
{
    public class ConnectController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // TODO how to get content? stored in DB? probably hardcoded for now

            return View();
        }

    }
}