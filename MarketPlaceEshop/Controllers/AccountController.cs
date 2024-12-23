using DataTransferObject.DTOClasses.Address.Commands;
using DataTransferObject.DTOClasses.Address.Results;
using DataTransferObject.DTOClasses.Person.Commands;
using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Exceptions;
using Service.ServiceInterfaces.AddressServices;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using Shared;
using System.Security.Claims;

namespace MarketPlaceEshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICustomerService _customerService;
        private readonly IAdressService _adressService;
        private readonly ICityService _cityService;
        private readonly IProvinceService _provinceService;
        private readonly IOrderService _orderService;
        public AccountController(IUserService userService, ICustomerService customerService, IAdressService adressService, IProvinceService provinceService, ICityService cityService, IOrderService orderService)
        {
            _userService = userService;
            _customerService = customerService;
            _adressService = adressService;
            _provinceService = provinceService;
            _cityService = cityService;
            _orderService = orderService;
        }

        [Authorize(Roles = "Customer")]
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
        public async Task<IActionResult> Register(UserCommand userDto, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                TempData["ToastRegister"] = "اطلاعات ورودی نامعتبر";
                return RedirectToAction("Register", "Account");
            }
            userDto.DateOfBirth = userDto.DateOfBirth.ToSolar();
            try
            {
                await _customerService.CreateAsync(userDto, cancellation);
            }
            catch (RegisterException ex)
            {
                TempData["ToastRegister"] = ex.Message;
                return RedirectToAction("Register", "Account");
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {           
            ViewData["returnurl"] = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand LoginDto, CancellationToken cancellation, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {
                    result = await _customerService.SignInAsync(LoginDto);
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

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOutAsync();
            return LocalRedirect("/");
        }


        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Address(CancellationToken cancellation, int? provinceId)
        {
            Guid.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString(), out Guid user);

            var address = new AddressViewModel
            {
                Addresses = await _adressService.GetAllByCustomerIdAsync(user, cancellation),
                Provinces = await _provinceService.GetAll(cancellation),
                Cities = provinceId.HasValue && provinceId.Value != 0 ? await _cityService.GetCityByProvinceIdAsync(provinceId.Value, cancellation) : new List<CityResult>(),
            };
            return View(address);
        }


        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateAddress(AddressCommand addressDto, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                TempData["ToastError"] = "اطلاعات ورودی نامعتبر";
            }
            else
            {
                await _adressService.CreateAsync(addressDto, cancellation);
            }
            return RedirectToAction("Address", "Account");
        }


        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Orders(CancellationToken cancellation, int pageIndex = 1)
        {
            var orders = await _orderService.GetAllOrders(cancellation, pageIndex, 5);
            return View(orders);
        }
    }
}
