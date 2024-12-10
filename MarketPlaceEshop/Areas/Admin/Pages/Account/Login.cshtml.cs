using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.Areas.Admin.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAdminService _adminService;

        public LoginModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public LoginCommand LoginDto { get; set; }
        public string ReturnUrl { get; set; }


        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPost(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/Admin");
            var result = false;
            if (ModelState.IsValid)
            {
                try
                {
                    result = await _adminService.SignInAsync(LoginDto);
                    ViewData["Toast"] = "مشخصات وارد شده صحیح نمی باشد";
                }
                catch (SignInException ex)
                {
                    ViewData["Toast"] = ex.Message;
                }
                if (result)
                {
                    return LocalRedirect(returnUrl);
                }
              
            }

            return Page();
        }
    }
}
