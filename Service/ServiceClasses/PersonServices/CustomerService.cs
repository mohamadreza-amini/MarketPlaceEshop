﻿using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Infrastructure.Contracts.Repository;
using Mapster;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;

namespace Service.ServiceClasses.PersonServices;

public class CustomerService : ServiceBase<Customer, UserResult, Guid>, ICustomerService
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

        if (!await _userService.CreateAsync(userDTO, requesterId))
            return false;
        var user = await _userService.GetUserbyIdAsync(customerId);
        if (user == null)
            return false;
        await _customerRepository.CreateAsync(new Customer
        {
            Id = customerId,
            RegisterDate = DateTime.Now,
            CreatorUserId = requesterId,
            UpdaterUserId = requesterId
        }, cancellationToken);
        var addedToRole = await _userService.AddToRoleAsync(user, "Customer");

        return addedToRole && (await _customerRepository.CommitAsync(cancellationToken)) == 1;

    }

    public async Task<bool> SignInAsync(LoginCommand loginDto)
    {
        return await _userService.SignInAsync(loginDto, "Customer");
    }


    public async Task<int> NumberOfCustomers(CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();
        return await _customerRepository.CountAsync(x => true, cancellation);
    }

    public async Task<PaginatedList<UserResult>> GetAll(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();
        var users = await _customerRepository.GetAllDataAsync(x => x, cancellation);

        if (users == null)
            return new PaginatedList<UserResult>(new List<UserResult>(), 0, 1, pageSize);

        return await PaginatedList<UserResult>.CreateAsync(users.ProjectToType<UserResult>(), pageIndex, pageSize, cancellation);
    }

}
