using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class FeedBackController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
