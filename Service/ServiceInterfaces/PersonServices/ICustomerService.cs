using DataTransferObject.DTOClasses.Person.Commands;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.PersonServices;

public interface ICustomerService:IServiceBase<Customer,UserCommand,Guid>
{
    Task<bool> CreateAsync(UserCommand userDTO,CancellationToken cancellationToken);
    Task<bool> SignInAsync(LoginCommand loginDto);

}
