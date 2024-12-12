using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Admin;

[Area("Admin")]
public class ReportController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
