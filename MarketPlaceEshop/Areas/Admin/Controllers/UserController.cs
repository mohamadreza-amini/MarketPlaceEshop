using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.AddressServices;
using Service.ServiceInterfaces.PersonServices;
using Shared;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Admin;
[Area("Admin")]
public class UserController : Controller
{
    private readonly ISupplierService _supplierService;
    private readonly IAdminService _adminService;
    private readonly ICustomerService _customerService;
    public UserController(ISupplierService supplierService, IAdminService adminService, ICustomerService customerService)
    {
        _supplierService = supplierService;
        _adminService = adminService;
        _customerService = customerService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> MembershipRequest(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10)
    {
        var SuppliersDto = await _supplierService.GetAllSuppliersbyStatusAsync(ConfirmationStatus.Unchecked, cancellation, pageIndex, pageSize);
        return View(SuppliersDto);
    }


    public async Task ConfirmSupplier(Guid supplierId, CancellationToken cancellation)
    {
        await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Confirmed, cancellation);
    }

    public async Task RejectSupplier(Guid supplierId, CancellationToken cancellation)
    {
        await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Rejected, cancellation);
    }




    [HttpGet]
    public IActionResult RegisterAdmin()
    {      
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RegisterAdmin(UserCommand userDto, CancellationToken cancellation)
    {
        if (!ModelState.IsValid) 
            return RedirectToAction("RegisterAdmin", "User", new {area="Admin"});
        userDto.DateOfBirth = userDto.DateOfBirth.ToSolar();

        try
        {
            var result = await _adminService.CreateAsync(userDto, cancellation);
            TempData["ToastSuccess"] = "ادمین جدید با موفقیت ثبت شد";
        }
        catch (Exception ex) {
            TempData["ToastError"] = "خطایی رخ داده";
        }
        return View();
    }



    public async Task<IActionResult> Suppliers(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10)
    {
        var suppliers = await _supplierService.GetAllSuppliersbyStatusAsync(ConfirmationStatus.Confirmed, cancellation, pageIndex, pageSize);
        return View(suppliers);
    }


    public async Task<IActionResult> Customers(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10)
    {
        var customers = await _customerService.GetAll(cancellation, pageIndex, pageSize);
        return View(customers);
    }
}
