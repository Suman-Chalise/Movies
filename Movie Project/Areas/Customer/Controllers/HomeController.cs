﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Movie_Project.Area.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
     
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
