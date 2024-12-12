using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
