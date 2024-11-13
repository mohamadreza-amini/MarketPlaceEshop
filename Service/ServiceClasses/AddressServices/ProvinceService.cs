using DataTransferObject.DTOClasses.Address;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Mapster;
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

public class ProvinceService : ServiceBase<Province, ProvinceDTO, int>, IProvinceService
{
    private readonly ICachedData _cachedData;
    private readonly IBaseRepository<Province, int> _provinceRepository;
    public ProvinceService(ICachedData cachedData, IBaseRepository<Province, int> baseRepository)
    {
        _cachedData = cachedData;
        _provinceRepository = baseRepository;
    }
    public async Task<List<ProvinceDTO>> GetAll(CancellationToken cancellation)
    {
        var provinces = _cachedData.Get<List<Province>>("Provinces");
        if (provinces == null)
        {
            provinces = await _provinceRepository.GetAll().ToListAsync(cancellation);
            _cachedData.Set("Provinces", provinces);
        }
        return Translate<List<Province>, List<ProvinceDTO>>(provinces);
    }
}
