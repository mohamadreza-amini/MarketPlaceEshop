using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Infrastructure.Contracts.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Orders;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Service.ServiceClasses.PersonServices;

public class UserService : ServiceBase<User, UserResult, Guid>, IUserService
{
    private readonly UserManager<User> _userManager;

    private readonly IHttpContextAccessor _contextAccessor;

    private readonly SignInManager<User> _signInManager;
    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _contextAccessor = httpContextAccessor;
    }

    public async Task<User?> GetRequesterUserAsync(HttpContext httpContext)
    {
        if (_contextAccessor.HttpContext == null)
            return null;
        return await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
    }

    public string? RequesterId()
    {
        return _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
    public bool IsAdmin()
    {
        if (_contextAccessor.HttpContext != null)
            return _contextAccessor.HttpContext.User.IsInRole("Admin");

        return false;
    }

    public bool IsRequesterUser(Guid userId)
    {
        var requsterId = RequesterId();
        try
        {
            if (requsterId != null)
                return userId == Guid.Parse(requsterId);
        }
        catch (Exception)
        {
            throw new AccessDeniedException();
        }
        return false;
    }

    public async Task<bool> CreateAsync(UserCommand userDTO)
    {
        var user = Translate<UserCommand,User>(userDTO);
        user.Validate();
        if (_userManager.FindByNameAsync(userDTO.Email) != null)
            throw new RegisterException("حسابی با مشخصات کاربر موجود است");

        var registerResult = await _userManager.CreateAsync(user, userDTO.Password);

        if (!registerResult.Succeeded)
            throw new RegisterException(registerResult.Errors.FirstOrDefault()?.Description ?? "عملیات ثبت نام ناموفق");

        return registerResult.Succeeded;
    }

    public async Task<bool> SignInAsync(LoginCommand loginDTO, string role)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.Email);

        if (user == null)
            throw new SignInException("کاربری با مشخصات وارد شده تعریف نشده است");

        var isInRole = await _userManager.IsInRoleAsync(user, role);

        if (!isInRole)
            return false;

        var signInResult = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, loginDTO.RememberMe, false);

        return signInResult.Succeeded;
    }

    public async Task<UserResult?> GetUserbyEmailAsync(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        if (user != null)
            return Translate<User, UserResult>(user);

        return null;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }











    //public async Task<List<UserDTO>> GetAllUser()
    //{
    //    var users = await _userManager.Users.ToListAsync();
    //    return users.Any() ? users.Select(TranslateToDTO).ToList() : new List<UserDTO>();
    //}

    //public async Task<UserDTO> GetUser(Guid id)
    //{
    //    var user = await _userManager.FindByIdAsync(id.ToString());
    //    return TranslateToDTO(user);
    //}

    //public async Task<SignInResult> LoginToSystem(string username, string password) {

    //    return await _signInManager.PasswordSignInAsync(username, password,isPersistent:true,lockoutOnFailure:false);

    //}


}
