using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Service.ServiceInterfaces.PersonServices;

public interface IUserService : IServiceBase<User, UserResult, Guid>
{
    Task<User?> GetRequesterUserAsync();
    Task<UserResult> GetRequesterUserResult();
    string? RequesterId();
    bool IsInRole(string role);
    Task<string?> GetRole();
    bool IsAdmin();
    bool IsRequesterUser(Guid userId);
    Task<bool> CreateAsync(UserCommand userDTO, Guid creatorId);
    Task<bool> SignInAsync(LoginCommand loginDTO, string role);
    Task<UserResult?> GetUserbyEmailAsync(string email);
    Task LogOutAsync();
    Task<User?> GetUserbyIdAsync(Guid userId);

    Task<bool> AddToRoleAsync(User user, string roleName);

    /*  Task<IdentityResult> Create(UserDTO userDTO);
      Task<UserDTO> GetUser(Guid id);
      Task<List<UserDTO>> GetAllUser();
      Task<SignInResult> LoginToSystem(string username, string password);*/

}