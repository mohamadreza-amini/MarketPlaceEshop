using DataTransferObject.DTOClasses.Address;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface ICityService:IServiceBase<City,CityDTO,int>
{
 
    Task<List<CityDTO>> GetAll(CancellationToken cancellation);
    Task<List<CityDTO>> GetCityByProvinceId(int provinceId,CancellationToken cancellation);
}
