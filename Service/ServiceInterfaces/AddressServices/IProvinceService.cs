using DataTransferObject.DTOClasses.Address.Results;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface IProvinceService:IServiceBase<Province,ProvinceResult,int>
{
    
    Task<List<ProvinceResult>> GetAll(CancellationToken cancellation);
}
