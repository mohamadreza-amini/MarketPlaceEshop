using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
