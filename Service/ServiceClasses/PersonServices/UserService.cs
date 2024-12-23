using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using System.Security.Claims;

namespace Service.ServiceClasses.PersonServices;
public class UserService : ServiceBase<User, UserResult, Guid>, IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ClaimsPrincipal _claimsPrincipal;
    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, ClaimsPrincipal claimsPrincipal)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _claimsPrincipal = claimsPrincipal;
    }
    public async Task<User?> GetRequesterUserAsync() => await _userManager.GetUserAsync(_claimsPrincipal);
    public async Task<UserResult> GetRequesterUserResult() => TranslateToDTO(await GetRequesterUserAsync() ?? new User());
    public string? RequesterId() => _claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public bool IsInRole(string role) => _claimsPrincipal.IsInRole(role);
    public bool IsAdmin() => IsInRole("Admin");

    public async Task<string?> GetRole()
    {
        var claim = _claimsPrincipal;
        if (claim == null) return null;
        var user = await _userManager.GetUserAsync(claim);
        if (user == null) return null;
        return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
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

    public async Task<bool> CreateAsync(UserCommand userDTO, Guid creatorId)
    {
        var user = Translate<UserCommand, User>(userDTO);
        user.Validate();
        if (await _userManager.FindByNameAsync(userDTO.Email) != null)
            throw new RegisterException("حسابی با مشخصات کاربر موجود است");
        user.CreatorUserId = user.UpdaterUserId = creatorId;
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

    public async Task<User?> GetUserbyIdAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
            return user;

        return null;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> AddToRoleAsync(User user, string roleName)
    {
        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
            throw new RegisterException(result.Errors.FirstOrDefault()?.Description ?? "عملیات ثبت نام ناموفق");

        return result.Succeeded;
    }

    public async Task UpdateUser(UserCommand userDto, Guid userId, CancellationToken cancellationToken)
    {
        if (!IsAdmin() && IsRequesterUser(userId))
            throw new AccessDeniedException();

        var newUser = Translate<UserCommand, User>(userDto);

        newUser.Validate();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new BadRequestException("کاربر نامعتبر");

        user.Email = newUser.Email;
        user.UserName = newUser.Email;
        user.UpdaterUserId = Guid.Parse(RequesterId()!);
        user.DateOfBirth = newUser.DateOfBirth;
        user.FirstName = newUser.FirstName;
        user.LastName = newUser.LastName;
        user.PhoneNumber = newUser.PhoneNumber;
        user.MobileNumber = newUser.MobileNumber;

        await _userManager.UpdateAsync(user);
    }


    public async Task DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        if (!IsAdmin() && IsRequesterUser(userId))
            throw new AccessDeniedException();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new BadRequestException("کاربر نامعتبر");
        user.IsDeleted = true;
        await _userManager.UpdateAsync(user);
    }
}
