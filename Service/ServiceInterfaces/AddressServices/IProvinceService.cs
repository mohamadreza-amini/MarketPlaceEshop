using DataTransferObject.DTOClasses.Address;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface IProvinceService:IServiceBase<Province,ProvinceDTO,int>
{
    
    Task<List<ProvinceDTO>> GetAll(CancellationToken cancellation);
}
