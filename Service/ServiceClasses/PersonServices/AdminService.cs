﻿using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.PersonServices;

public class AdminService : ServiceBase<Admin, UserResult, Guid>, IAdminService
{

    private readonly IBaseRepository<Admin, Guid> _adminRepository;
    private readonly IUserService _userService;
    public AdminService(IBaseRepository<Admin, Guid> baseRepository, IUserService userService)
    {
        _adminRepository = baseRepository;
        _userService = userService;
    }

    public async Task<bool> CreateAsync(UserCommand userDTO, CancellationToken cancellationToken)
    {
        var requesterId = _userService.IsAdmin() ? Guid.Parse(_userService.RequesterId()) : throw new AccessDeniedException();

        if (!await _userService.CreateAsync(userDTO))
            return false;

        await _adminRepository.CreateAsync(new Admin
        {
            Id = Guid.NewGuid(),
            CreatorUserId = requesterId,
            UpdaterUserId = requesterId

        }, cancellationToken);
        return (await _adminRepository.CommitAsync(cancellationToken)) == 1;

    }

    public async Task<bool> SignInAsync(LoginCommand loginDto)
    {
        return await _userService.SignInAsync(loginDto,"Admin");
    }

    public async Task LogOutAsync()
    {
        await _userService.LogOutAsync();
    }



}
