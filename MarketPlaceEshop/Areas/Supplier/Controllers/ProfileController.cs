﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]
[Authorize(Roles = "Supplier")]

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
