using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.PersonServices;
using Shared.Enums;

namespace MarketPlaceEshop.Controllers
{
    public class TestController : Controller
    {
        private readonly ISupplierService _supplierService;

        public TestController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task Confirm(Guid supplierId, CancellationToken cancellation)
        {
            await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Confirmed, cancellation);
        }

        public async Task Reject(Guid supplierId,CancellationToken cancellation)
        {
            await _supplierService.ChangeSupplierStatusAsync(supplierId, ConfirmationStatus.Rejected, cancellation);
        }
    }
}
