using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Address.Commands;
using DataTransferObject.DTOClasses.Address.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Model.Entities.Addresses;
using Model.Exceptions;
using Service.ServiceInterfaces.AddressServices;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.AddressServices;

public class AddressService : ServiceBase<Address, AddressResult, Guid>, IAdressService
{
    private readonly IBaseRepository<Address, Guid> _addressRepository;
    private readonly IUserService _userService;
    public AddressService(IUserService userService, IBaseRepository<Address, Guid> addressRepository)
    {
        _addressRepository = addressRepository;
        _userService = userService;
    }

    public async Task<int> CreateAsync(AddressCommand addressDTO, CancellationToken cancellation, Guid customerid = default)
    {
        var address = Translate<AddressCommand, Address>(addressDTO);
        if (!_userService.IsAdmin())
            address.CustomerId = _userService.RequesterId() == null ? throw new AccessDeniedException() : Guid.Parse(_userService.RequesterId());
        else
            address.CustomerId = customerid;

        address.validate();
        await _addressRepository.CreateAsync(address, cancellation);
        return await _addressRepository.CommitAsync(cancellation);
    }

    public async Task<bool> DeleteAsync(Guid addressId, CancellationToken cancellation)
    {
        var address = await _addressRepository.GetByIdAsync(addressId, cancellation);
        if (address == null)
            throw new AccessDeniedException();

        if (_userService.IsAdmin() || _userService.IsRequesterUser(address.CustomerId))
        {
            await _addressRepository.SoftDeleteAsync(x => x.Id == addressId, cancellation);
            await _addressRepository.CommitAsync(cancellation);
        }
        throw new AccessDeniedException();
    }

    public async Task<List<AddressResult>> GetAllByCustomerIdAsync(Guid customerid, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin() && !_userService.IsRequesterUser(customerid))
            throw new AccessDeniedException();

        var query = await _addressRepository.GetAllDataAsync(x => x, cancellation, x => x.CustomerId == customerid, x => x.Include(x => x.City).ThenInclude(x => x.Province));

        if (query == null)
            return new List<AddressResult>(); 
        var addresses = await query?.ToListAsync(cancellation);     
        return Translate<List<Address>,List<AddressResult>>(addresses);
    }

    public async Task<AddressResult> GetByAddressIdAsync(Guid addressid, CancellationToken cancellation)
    {
        var address = await _addressRepository.GetByIdAsync(addressid, cancellation);

        if (address != null && (_userService.IsAdmin() || _userService.IsRequesterUser(address.CustomerId)))
            return TranslateToDTO(address);

        throw new AccessDeniedException();

    }

    public async Task<int> UpdateAsync(AddressCommand addressDTO, CancellationToken cancellation)
    {
        var address = Translate<AddressCommand, Address>(addressDTO);
        var preAddress = await _addressRepository.GetByIdAsync(address.Id, cancellation);
        if (preAddress == null || (!_userService.IsRequesterUser(preAddress.CustomerId) && !_userService.IsAdmin()))
            throw new AccessDeniedException();

        address.CustomerId = preAddress.CustomerId;
        address.validate();
        _addressRepository.Update(address);
        return await _addressRepository.CommitAsync(cancellation);

    }
}
