using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Address;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface IAdressService:IServiceBase<Address,AddressDTO,Guid> 
{
   
    //ممکنه درخواسست دهنده ادمین باشه نه مشتری از ایدی فرستنده نشه فهمید 
    Task<int> Create (AddressDTO addressDTO,CancellationToken cancellation, Guid customerid=default);
    Task<AddressDTO> GetByAddressId(Guid addressid,CancellationToken cancellation);
    //بررسی اگه این درخواست یوزر بود ادرس برای خودش باشه
    Task<int> Update (AddressDTO addressDTO,CancellationToken cancellation);
    //بررسی اگه این درخواست یوزر بود ادرس برای خودش باشه
    Task<bool> Delete(Guid addressId, CancellationToken cancellation);
    //ایدی از اکسسور درخواست گرفته بشه اگه یوزر بود
    Task<List<AddressDTO>> GetAllByCustomerId(Guid customerid,CancellationToken cancellation);

}
