using DataTransferObject.DTOClasses.Category.Results;
using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using System.Diagnostics;
using System.Text;

namespace MarketPlaceEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly ICategoryService _categoryService;
        public HomeController(ILogger<HomeController> logger, IUserService _userService, ICategoryService categoryService)
        {
            userService = _userService;
            _logger = logger;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellation)
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
