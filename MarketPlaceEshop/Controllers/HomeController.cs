using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.ServiceInterfaces.PersonServices;
using System.Diagnostics;

namespace MarketPlaceEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger, IUserService _userService)
        {
            userService = _userService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
