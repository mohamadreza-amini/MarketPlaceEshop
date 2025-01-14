﻿using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Service.ServiceClasses.PersonServices;
using Service.ServiceInterfaces.PersonServices;
using Shared;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;

[Area("Supplier")]
public class AccountController : Controller
{
    private readonly ISupplierService _supplierService;
    private readonly IUserService _userService;
    public AccountController(ISupplierService supplierService, IUserService userService)
    {
        _supplierService = supplierService;
        _userService = userService;
    }

    [Authorize(Roles = "Supplier")]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(SupplierCommand SupplierDto,CancellationToken cancellation)
    {
        if (!ModelState.IsValid)
        {
            TempData["ToastRegister"] = "اطلاعات ورودی نامعتبر";
            return RedirectToAction("Register", "Account", new {area="Supplier"});
        }
        SupplierDto.UserDto.DateOfBirth = SupplierDto.UserDto.DateOfBirth.ToSolar();
        try
        {
            await _supplierService.CreateAsync(SupplierDto, cancellation);
        }
        catch (RegisterException ex)
        {
            TempData["ToastRegister"] = ex.Message;
            return RedirectToAction("Register", "Account", new { area = "Supplier" });
        }
        return View("SuccessfulRegistration");
    }


    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        if (User.IsInRole("Supplier"))
            return RedirectToAction("index", "Home", new { area = "Supplier" });
        ViewData["returnurl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand LoginDto, CancellationToken cancellation, string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/Supplier");
        var result = false;
        if (ModelState.IsValid)
        {
            try
            {
                result = await _supplierService.SignInAsync(LoginDto);
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


    [Authorize(Roles = "Supplier")]
    public async Task<IActionResult> LogOut()
    {
        await _userService.LogOutAsync();
        return LocalRedirect("/");
    }

}
