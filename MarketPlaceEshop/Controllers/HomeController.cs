using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Shared.Enums;
using System.Diagnostics;

namespace MarketPlaceEshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService userService;
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    public HomeController(ILogger<HomeController> logger, IUserService _userService, ICategoryService categoryService, IProductService productService)
    {
        userService = _userService;
        _logger = logger;
        _categoryService = categoryService;
        _productService = productService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellation)
    {
        var products = await _productService.GetAllbyFilterCommand(new ProductFilterCommand() { SortProduct = SortProduct.MostVisited }, cancellation, 1, 6);
        return View(products);
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
