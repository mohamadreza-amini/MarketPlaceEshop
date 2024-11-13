using DataTransferObject.DTOClasses.Address;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Addresses;
using Service.ServiceInterfaces.AddressServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.AddressService;

public class CityService : ServiceBase<City, CityDTO, int>, ICityService
{
    private readonly ICachedData _cachedData;
    private readonly IBaseRepository<City, int> _cityRepository;
    public CityService(ICachedData cachedData, IBaseRepository<City, int> baseRepository)
    {
        _cachedData = cachedData;
        _cityRepository = baseRepository;
    }

    public async Task<List<CityDTO>> GetAll(CancellationToken cancellation)
    {
        var cities = _cachedData.Get<List<City>>("cities");
        if (cities == null)
        {
            cities = await _cityRepository.GetAll().ToListAsync(cancellation);
            _cachedData.Set("cities", cities);
        }
        return Translate<List<City>, List<CityDTO>>(cities);
    }

    public async Task<List<CityDTO>> GetCityByProvinceId(int provinceId, CancellationToken cancellation)
    {
        var cities = await _cityRepository.GetAll(x => x.ProvinceId == provinceId).ToListAsync(cancellation);

        return Translate<List<City>, List<CityDTO>>(cities);
    }
}
