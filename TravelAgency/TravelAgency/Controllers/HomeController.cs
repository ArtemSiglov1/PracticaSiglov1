using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.ViewModels;

namespace TravelAgency.Controllers
{
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
        public IActionResult SiteInformation()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult _LoginRegistrationPartial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errors);
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(errors);
            }
            return Ok(model);
        }
    }
}
