﻿using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Infrastructure.Contracts.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Person;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Shared.Enums;

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

        if (!await _userService.CreateAsync(supplierDTO.UserDto, requesterId))
            return false;
        var user = await _userService.GetUserbyIdAsync(supplierId);
        if (user == null)
            return false;
        var supplier = Translate<SupplierCommand, Supplier>(supplierDTO);
        supplier.Id = supplierId;
        supplier.StartDate = DateTime.Now;
        supplier.CreatorUserId = requesterId;
        supplier.UpdaterUserId = requesterId;
        supplier.IsConfirmed = 1;

        var addedToRole = await _userService.AddToRoleAsync(user, "Supplier");
        await _supplierRepository.CreateAsync(supplier, cancellation);

        return addedToRole && (await _supplierRepository.CommitAsync(cancellation)) == 1;
    }

    public async Task<PaginatedList<SupplierResult>> GetAllSuppliersbyStatusAsync(ConfirmationStatus status, CancellationToken cancellation, int pageIndex = 1, int pageSize = 20)
    {
        var suppliers = await _supplierRepository.GetAllDataAsync(
            x => x,
            cancellation,
            x => x.IsConfirmed == (byte)status,
            x => x.Include(x => x.User)
        );
        return await PaginatedList<SupplierResult>.CreateAsync(suppliers?.ProjectToType<SupplierResult>(), pageIndex, pageSize, cancellation);
    }

    public async Task<bool> ChangeSupplierStatusAsync(Guid supplierId, ConfirmationStatus confirmation, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var supplier = await _supplierRepository.GetByIdAsync(supplierId, cancellation);
        if (supplier == null)
            return false;

        supplier.AdminConfirmedId = Guid.Parse(_userService.RequesterId()!);
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
        if (supplier == null)
            throw new SignInException("کاربری با مشخصات وارد شده تعریف نشده است");

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

    public async Task<int> NumberOfSupplier(ConfirmationStatus confirmation, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();
        return await _supplierRepository.CountAsync(x => x.IsConfirmed == (byte)confirmation, cancellation);
    }


    public async Task<SupplierResult> GetSupplier(CancellationToken cancellation)
    {
        if (!_userService.IsInRole("Supplier") || !Guid.TryParse(_userService.RequesterId(), out Guid supplierId))
            throw new AccessDeniedException();

        var suppliers = await _supplierRepository.GetAllDataAsync(
            x => x,
            cancellation,
            x => x.Id==supplierId,
            x => x.Include(x => x.User)
        );
        if (suppliers == null || !suppliers.Any())
            throw new BadRequestException();

        var supplier = await suppliers.ProjectToType<SupplierResult>().FirstOrDefaultAsync(cancellation);
        return supplier! ;
    }
}
