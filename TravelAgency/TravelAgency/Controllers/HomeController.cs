using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.DAL;
using TravelAgency.Domain.ViewModels;
using TravelAgency.Interface;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       // private readonly DbContextOptions<DataContext> _dataContext;
        private readonly IAccountService _account;
        
        public HomeController(ILogger<HomeController> logger/*,DbContextOptions<DataContext> db*/,IAccountService account)
        {
           // _dataContext = db;
            _account = account;
            _logger = logger;
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginRegistrView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _account.Login(new Interface.Models.RegAndLog.LoginUser()
                    {
                        Email = model.Login.Email,
                        Password = model.Login.Password
                    });

                    if (response != null)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new System.Security.Claims.ClaimsPrincipal(response));
                        return Ok(model) /*Redirect("/Home/SiteInformation")*/;
                    }

                    // Если response null, можно добавить ошибку
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
                catch (Exception ex)
                {
                    // Ловим исключение и добавляем его в ModelState
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            // Получаем все ошибки из ModelState
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            return BadRequest(errors);
        }
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SiteInformation", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginRegistrView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Выполнение регистрации
                    var response = await _account.Register(new Interface.Models.RegAndLog.RegistrationUser()
                    {
                        Email = model.Register.Email,
                        Password = model.Register.Password,
                        PasswordConfirm = model.Register.PasswordConfirm,
                        Login = model.Register.Login
                    });

                    // Если регистрация прошла успешно
                    if (response != null)
                    {
                        return Ok(model)/*Redirect("/Home/SiteInformation")*/;  // Возвращаем успешный ответ
                    }

                    // В случае ошибки (например, если ошибка в response)
                    ModelState.AddModelError("","Unknown registration error occurred.");
                }
                catch (Exception ex)
                {
                    // Ловим исключение и добавляем его в ModelState
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            // Собираем все ошибки из ModelState и возвращаем их
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            return BadRequest(new { errors });
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
    }
}
