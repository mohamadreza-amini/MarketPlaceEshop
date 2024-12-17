using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.ViewComponents;

public class ProfileTabViewComponent : ViewComponent
{
    private readonly IUserService userService;

    public ProfileTabViewComponent(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
       var user = await userService.GetRequesterUserResult();

        return View(user); 
    }
}
