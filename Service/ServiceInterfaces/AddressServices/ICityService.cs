using DataTransferObject.DTOClasses.Address.Results;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface ICityService:IServiceBase<City,CityResult,int>
{
 
    Task<List<CityResult>> GetAllAsync(CancellationToken cancellation);
    Task<List<CityResult>> GetCityByProvinceIdAsync(int provinceId,CancellationToken cancellation);
}
