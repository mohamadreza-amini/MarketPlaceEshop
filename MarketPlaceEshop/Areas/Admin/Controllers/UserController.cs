using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.PersonServices;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Admin;
[Area("Admin")]
public class UserController : Controller
{
    private readonly ISupplierService _supplierService;

    public UserController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
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


    public async Task ConfirmSupplier( Guid supplierId, CancellationToken cancellation)
    {
        await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Confirmed, cancellation);
    }

    public async Task RejectSupplier( Guid supplierId, CancellationToken cancellation)
    {
        await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Rejected, cancellation);
    }
}
