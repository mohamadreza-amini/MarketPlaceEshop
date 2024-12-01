using DataTransferObject.DTOClasses.Person.Commands;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.PersonServices;

public class CustomerService : ServiceBase<Customer, UserCommand, Guid>, ICustomerService
{
    private readonly IBaseRepository<Customer, Guid> _customerRepository;
    private readonly IUserService _userService;
    public CustomerService(IBaseRepository<Customer, Guid> baseRepository, IUserService userService)
    {
        _customerRepository = baseRepository;
        _userService = userService;
    }

    public async Task<bool> CreateAsync(UserCommand userDTO, CancellationToken cancellationToken)
    {
        var customerId = Guid.NewGuid();
        userDTO.Id = customerId;
        var requesterId = _userService.IsAdmin() ? Guid.Parse(_userService.RequesterId()) : customerId;

        if (!await _userService.CreateAsync(userDTO))
            return false;

        await _customerRepository.CreateAsync(new Customer
        {
            Id = customerId,
            RegisterDate = DateTime.Now,
            CreatorUserId = requesterId,
            UpdaterUserId = requesterId
        }, cancellationToken);
        return (await _customerRepository.CommitAsync(cancellationToken)) == 1;

    }

    public async Task<bool> SignInAsync(LoginCommand loginDto)
    {    
        return await _userService.SignInAsync(loginDto,"Customer");
    }

    public async Task LogOutAsync()
    {
        await _userService.LogOutAsync();
    }
}
