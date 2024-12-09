using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.ServiceClasses.PersonServices;

public class SupplierService : ServiceBase<Supplier, UserResult, Guid>, ISupplierService
{
    private readonly IUserService _userService;
    private readonly IBaseRepository<Supplier, Guid> _supplierRepository;


    public SupplierService(IUserService userService, IBaseRepository<Supplier, Guid> supplierRepository)
    {
        _userService = userService;
        _supplierRepository = supplierRepository;
    }

    public async Task<bool> CreateAsync(SupplierCommand supplierDTO, CancellationToken cancellation)
    {
        var supplierId = Guid.NewGuid();
        supplierDTO.UserDto.Id = supplierId;
        var requesterId = _userService.IsAdmin() ? Guid.Parse(_userService.RequesterId()) : supplierId;

        if (!await _userService.CreateAsync(supplierDTO.UserDto))
            return false;

        var supplier = Translate<SupplierCommand, Supplier>(supplierDTO);
        supplier.Id = supplierId;
        supplier.StartDate = DateTime.Now;
        supplier.CreatorUserId = requesterId;
        supplier.UpdaterUserId = requesterId;
        supplier.IsConfirmed = 1;

        await _supplierRepository.CreateAsync(supplier, cancellation);

        return (await _supplierRepository.CommitAsync(cancellation)) == 1;

    }

    public async Task<PaginatedList<SupplierResult>> GetAllSuppliersbyStatusAsync(ConfirmationStatus status, int pageIndex, int pageSize, CancellationToken cancellation)
    {
        var suppliers = await _supplierRepository.GetAllDataAsync(
            x => Translate<Supplier, SupplierResult>(x),
            cancellation,
            x => x.IsConfirmed == (byte)status,
            x => x.Include(x => x.User)
        );
        return await PaginatedList<SupplierResult>.CreateAsync(suppliers, pageIndex, pageSize, cancellation);
    }


    public async Task<bool> ChangeSupplierStatusAsync(Guid supplierId, ConfirmationStatus confirmation, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var supplier = await _supplierRepository.GetByIdAsync(supplierId, cancellation);

        if (supplier == null)
            return false;

        supplier.AdminConfirmedId = Guid.Parse(_userService.RequesterId());

        switch (confirmation)
        {
            case ConfirmationStatus.Confirmed:
                supplier.IsConfirmed = 2;
                supplier.ConfirmedDate = DateTime.Now;
                break;

            case ConfirmationStatus.Rejected:
                supplier.IsConfirmed = 3;
                break;

            case ConfirmationStatus.Unchecked:
                supplier.IsConfirmed = 1;
                break;
        }
        return (await _supplierRepository.CommitAsync(cancellation)) == 1;

    }

    public async Task<bool> SignInAsync(LoginCommand loginDto)
    {
        var supplierId = (await _userService.GetUserbyEmailAsync(loginDto.Email))?.Id;

        var supplier = await _supplierRepository.GetAsync(x => x.Id == supplierId, CancellationToken.None);

        switch (supplier?.IsConfirmed ?? default)
        {
            case 1:
                throw new SignInException("حساب شما در دست بررسی می باشد");
            case 2:
                break;
            case 3:
                throw new SignInException("حساب شما تایید نشد");

            default:
                throw new AccessDeniedException();
        }

        return await _userService.SignInAsync(loginDto, "Supplier");
    }

  
}
