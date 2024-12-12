﻿using DataTransferObject.DTOClasses.Person.Commands;
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

    public AccountController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

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
            return RedirectToAction("Register");
        }
        SupplierDto.UserDto.DateOfBirth = SupplierDto.UserDto.DateOfBirth.ToSolar();
        await _supplierService.CreateAsync(SupplierDto, cancellation);
        return View("SuccessfulRegistration");
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









}
