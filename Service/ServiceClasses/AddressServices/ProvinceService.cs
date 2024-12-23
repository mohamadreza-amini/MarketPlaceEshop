using DataTransferObject.DTOClasses.Address.Results;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Addresses;
using Service.ServiceInterfaces.AddressServices;


namespace Service.ServiceClasses.AddressServices;

public class ProvinceService : ServiceBase<Province, ProvinceResult, int>, IProvinceService
{
    private readonly ICachedData _cachedData;
    private readonly IBaseRepository<Province, int> _provinceRepository;
    public ProvinceService(ICachedData cachedData, IBaseRepository<Province, int> baseRepository)
    {
        _cachedData = cachedData;
        _provinceRepository = baseRepository;
    }
    public async Task<List<ProvinceResult>> GetAll(CancellationToken cancellation)
    {
        var provinces = _cachedData.Get<List<Province>>("Provinces");
        if (provinces == null)
        {
            provinces = await _provinceRepository.GetAll().ToListAsync(cancellation);
            _cachedData.Set("Provinces", provinces);
        }
        return Translate<List<Province>, List<ProvinceResult>>(provinces);
    }
}
