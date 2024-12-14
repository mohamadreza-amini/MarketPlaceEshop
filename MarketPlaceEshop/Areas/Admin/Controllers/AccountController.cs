using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Service.ServiceClasses.PersonServices;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IUserService _userService;
    public AccountController(IAdminService adminService, IUserService userService)
    {
        _adminService = adminService;
        _userService = userService;
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



    public async Task<IActionResult> LogOut()
    {
        await _userService.LogOutAsync();
        return LocalRedirect("/");
    }
}
