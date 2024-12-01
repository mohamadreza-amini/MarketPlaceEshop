using DataTransferObject.DTOClasses.Person.Commands;
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

public class AdminService : ServiceBase<Admin, UserCommand, Guid>, IAdminService
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
        var adminId = Guid.NewGuid();
        userDTO.Id = adminId;
        var requesterId = _userService.IsAdmin() ? Guid.Parse(_userService.RequesterId()) : throw new AccessDeniedException();

        if (!await _userService.CreateAsync(userDTO))
            return false;

        await _adminRepository.CreateAsync(new Admin
        {
            Id = adminId,
            CreatorUserId = requesterId,
            UpdaterUserId = requesterId

        }, cancellationToken);
        return (await _adminRepository.CommitAsync(cancellationToken)) == 1;

    }






}
