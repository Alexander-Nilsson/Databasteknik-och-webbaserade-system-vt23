﻿using Microsoft.AspNetCore.Mvc;

namespace Laboration_3___Databasdriven_webbapplikation.Controllers
{
    public class HelloWorldController : Controller
    {

        // GET /HelloWorld/


        public IActionResult Index()
        {
            return View();
        }

        // GET /HelloWorld/Welcome/

        public IActionResult Welcome(string name, int numTimes = 1) {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

    }
}
