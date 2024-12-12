using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]


public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
