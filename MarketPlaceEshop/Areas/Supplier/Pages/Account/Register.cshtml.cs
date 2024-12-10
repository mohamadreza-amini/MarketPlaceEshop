using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.ServiceInterfaces.PersonServices;
using Shared;

namespace MarketPlaceEshop.Areas.Supplier.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ISupplierService _supplierService;

        public RegisterModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        [BindProperty]
        public SupplierCommand SupplierDto { get; set; }

        public void OnGet()
        {

        }

        public async Task OnPost(CancellationToken cancellation)
        {
            SupplierDto.UserDto.DateOfBirth= SupplierDto.UserDto.DateOfBirth.ToGregorian();
           await _supplierService.CreateAsync(SupplierDto,cancellation);
        }
    }
}
