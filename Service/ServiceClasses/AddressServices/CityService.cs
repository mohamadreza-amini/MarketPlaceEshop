using DataTransferObject.DTOClasses.Address.Results;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Addresses;
using Service.ServiceInterfaces.AddressServices;


namespace Service.ServiceClasses.AddressServices;

public class CityService : ServiceBase<City, CityResult, int>, ICityService
{
    private readonly ICachedData _cachedData;
    private readonly IBaseRepository<City, int> _cityRepository;
    public CityService(ICachedData cachedData, IBaseRepository<City, int> baseRepository)
    {
        _cachedData = cachedData;
        _cityRepository = baseRepository;
    }

    public async Task<List<CityResult>> GetAllAsync(CancellationToken cancellation)
    {
        var cities = _cachedData.Get<List<City>>("cities");
        if (cities == null)
        {
            cities = await _cityRepository.GetAll().ToListAsync(cancellation);
            _cachedData.Set("cities", cities);
        }
        return Translate<List<City>, List<CityResult>>(cities);
    }

    public async Task<List<CityResult>> GetCityByProvinceIdAsync(int provinceId, CancellationToken cancellation)
    {
       return await _cityRepository.GetAll(x => x.ProvinceId == provinceId).ProjectToType<CityResult>().ToListAsync(cancellation);
    }
}
