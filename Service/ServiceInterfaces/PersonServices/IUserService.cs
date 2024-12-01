using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Service.ServiceInterfaces.PersonServices;

public interface IUserService:IServiceBase<User,UserResult,Guid>
{
    Task<User?> GetRequesterUserAsync(HttpContext httpContext);
    string? RequesterId();
    bool IsAdmin();
    bool IsRequesterUser(Guid userId);
    Task<bool> CreateAsync(UserCommand userDTO);
    Task<bool> SignInAsync(LoginCommand loginDTO,string role);
    Task<UserResult?> GetUserbyEmailAsync(string email);
    Task LogOutAsync();

    /*  Task<IdentityResult> Create(UserDTO userDTO);
      Task<UserDTO> GetUser(Guid id);
      Task<List<UserDTO>> GetAllUser();
      Task<SignInResult> LoginToSystem(string username, string password);*/

}