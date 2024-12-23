using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.ViewComponents;

public class ProfileViewComponent : ViewComponent
{
    private readonly ISupplierService _supplierService;

    public ProfileViewComponent(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellation)
    {
        var supplier = await _supplierService.GetSupplier(cancellation);
        return View(supplier);
    }
}
