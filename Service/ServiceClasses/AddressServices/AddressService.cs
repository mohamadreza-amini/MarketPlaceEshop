using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Address;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Model.Entities.Addresses;
using Model.Exceptions;
using Service.ServiceInterfaces;
using Service.ServiceInterfaces.AddressServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.AddressService;

public class AddressService : ServiceBase<Address, AddressDTO, Guid>, IAdressService
{
    private readonly IBaseRepository<Address, Guid> _addressRepository;
    private readonly IUserService _userService;
    public AddressService(IUserService userService, IBaseRepository<Address, Guid> addressRepository)
    {
        _addressRepository = addressRepository;
        _userService = userService;
    }

    public async Task<int> Create(AddressDTO addressDTO, CancellationToken cancellation, Guid customerid = default)
    {
        var address = TranslateToEntity(addressDTO);
        if (!_userService.IsAdmin())
            address.CustomerId = _userService.RequesterId() == null ? throw new AccessDeniedException() : Guid.Parse(_userService.RequesterId());
        else
            address.CustomerId = customerid;

        address.validate();
        return await _addressRepository.CreateAsync(address, cancellation);
    }

    public async Task<bool> Delete(Guid addressId, CancellationToken cancellation)
    {
        var address = await _addressRepository.GetByIdAsync(addressId, cancellation);
        if (address == null)
            throw new AccessDeniedException();

        if (_userService.IsAdmin() || _userService.IsRequesterUser(address.CustomerId))
            return await _addressRepository.SoftDeleteAsync(addressId, cancellation);

        throw new AccessDeniedException();
    }

    public async Task<List<AddressDTO>> GetAllByCustomerId(Guid customerid, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin() && !_userService.IsRequesterUser(customerid))
            throw new AccessDeniedException();

        var query = await _addressRepository.GetAllData(x => x, x => x.CustomerId == customerid, x => x.Include(x => x.City).ThenInclude(x => x.Province));
        var addresses = await query?.ToListAsync(cancellation);
        if (addresses == null)
            return new List<AddressDTO>();

        return Translate<List<Address>, List<AddressDTO>>(addresses);
    }

    public async Task<AddressDTO> GetByAddressId(Guid addressid, CancellationToken cancellation)
    {
        var address = await _addressRepository.GetByIdAsync(addressid, cancellation);

        if (address != null && (_userService.IsAdmin() || _userService.IsRequesterUser(address.CustomerId)))
            return TranslateToDTO(address);

        throw new AccessDeniedException();

    }

    public async Task<int> Update(AddressDTO addressDTO, CancellationToken cancellation)
    {
        var address = TranslateToEntity(addressDTO);
        var preAddress = await _addressRepository.GetByIdAsync(address.Id, cancellation);
        if (preAddress == null || (!_userService.IsRequesterUser(preAddress.CustomerId) && !_userService.IsAdmin()))
            throw new AccessDeniedException();

        address.CustomerId = preAddress.CustomerId;
        address.validate();
        return await _addressRepository.UpdateAsync(address,cancellation);

    }
}
