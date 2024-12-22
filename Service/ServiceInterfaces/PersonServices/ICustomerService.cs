using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using Service.ServiceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.PersonServices;

public interface ICustomerService:IServiceBase<Customer,UserResult,Guid>
{
    Task<bool> CreateAsync(UserCommand userDTO,CancellationToken cancellationToken);
    Task<bool> SignInAsync(LoginCommand loginDto);
    Task<int> NumberOfCustomers(CancellationToken cancellation);
    Task<PaginatedList<UserResult>> GetAll(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10);
}
