using DataTransferObject.DTOClasses.Person.Commands;
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

public class UserService : ServiceBase<User, UserCommand, Guid>, IUserService
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
        var user = TranslateToEntity(userDTO);
        user.Validate();
        return (await _userManager.CreateAsync(user, userDTO.Password)).Succeeded; 
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
