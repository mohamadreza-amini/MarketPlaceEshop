using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]


public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
