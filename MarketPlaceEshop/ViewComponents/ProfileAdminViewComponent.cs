using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.PersonServices;

namespace MarketPlaceEshop.ViewComponents;

public class ProfileAdminViewComponent: ViewComponent
{
    private readonly IUserService userService;

    public ProfileAdminViewComponent(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await userService.GetRequesterUserResult();

        return View(user);
    }
}
