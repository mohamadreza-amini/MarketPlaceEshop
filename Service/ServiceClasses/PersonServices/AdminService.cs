using DataTransferObject.DTOClasses.Person.Commands;
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
        var adminId = Guid.NewGuid();
        userDTO.Id = adminId;

        if (!_userService.IsAdmin() || !Guid.TryParse(_userService.RequesterId(), out Guid requesterId))
            throw new AccessDeniedException();
        
        if (!await _userService.CreateAsync(userDTO,requesterId))
            return false;
        var user = await _userService.GetUserbyIdAsync(adminId);
        if(user == null)
            return false;
        await _adminRepository.CreateAsync(new Admin
        {
            Id = adminId,
            CreatorUserId = requesterId,
            UpdaterUserId = requesterId

        }, cancellationToken);

       var addedToRole = await _userService.AddToRoleAsync(user, "Admin");
        return addedToRole && (await _adminRepository.CommitAsync(cancellationToken)) == 1;

    }

    public async Task<bool> SignInAsync(LoginCommand loginDto)
    {
        return await _userService.SignInAsync(loginDto, "Admin");
    }





}
