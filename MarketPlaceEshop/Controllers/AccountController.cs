using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
