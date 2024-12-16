using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
