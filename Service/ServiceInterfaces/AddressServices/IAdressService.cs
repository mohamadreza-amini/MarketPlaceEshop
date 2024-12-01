using DataTransferObject.DTOClasses;
using DataTransferObject.DTOClasses.Address.Commands;
using DataTransferObject.DTOClasses.Address.Results;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.AddressServices;

public interface IAdressService:IServiceBase<Address,AddressResult, Guid> 
{
   
    //ممکنه درخواسست دهنده ادمین باشه نه مشتری از ایدی فرستنده نشه فهمید 
    Task<int> CreateAsync (AddressCommand addressDTO,CancellationToken cancellation, Guid customerid=default);
    Task<AddressResult> GetByAddressIdAsync(Guid addressid,CancellationToken cancellation);
    //بررسی اگه این درخواست یوزر بود ادرس برای خودش باشه
    Task<int> UpdateAsync (AddressCommand addressDTO,CancellationToken cancellation);
    //بررسی اگه این درخواست یوزر بود ادرس برای خودش باشه
    Task<bool> DeleteAsync(Guid addressId, CancellationToken cancellation);
    //ایدی از اکسسور درخواست گرفته بشه اگه یوزر بود
    Task<List<AddressResult>> GetAllByCustomerIdAsync(Guid customerid,CancellationToken cancellation);

}
