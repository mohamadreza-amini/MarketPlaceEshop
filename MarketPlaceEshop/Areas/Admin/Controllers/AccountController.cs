using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly IAdminService _adminService;

    public AccountController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["returnurl"] = returnUrl;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand LoginDto, CancellationToken cancellation, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/Admin");
        var result = false;
        if (ModelState.IsValid)
        {
            try
            {
                result = await _adminService.SignInAsync(LoginDto);
                ViewData["ToastLogin"] = "مشخصات وارد شده صحیح نمی باشد";
            }
            catch (SignInException ex)
            {
                ViewData["ToastLogin"] = ex.Message;
            }
            if (result)
            {
                return LocalRedirect(returnUrl);
            }

        }

        return View();
    }
}
