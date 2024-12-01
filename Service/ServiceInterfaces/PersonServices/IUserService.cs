using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Person.Commands;
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

public interface IUserService//:IServiceBase<User,,>
{
    Task<User?> GetRequesterUserAsync(HttpContext httpContext);
    string? RequesterId();
    bool IsAdmin();
    bool IsRequesterUser(Guid userId);
    Task<bool> CreateAsync(UserCommand userDTO);


    /*  Task<IdentityResult> Create(UserDTO userDTO);
      Task<UserDTO> GetUser(Guid id);
      Task<List<UserDTO>> GetAllUser();
      Task<SignInResult> LoginToSystem(string username, string password);*/

}