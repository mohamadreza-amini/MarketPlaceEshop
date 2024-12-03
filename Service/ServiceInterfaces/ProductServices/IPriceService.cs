using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IPriceService:IServiceBase<Price,PriceResult,Guid>
{
    Task<bool> AddPriceAsync(PriceCommand priceDto,CancellationToken cancellation);
    Task<bool> ExpireLastPrice(Guid productSupplierId,CancellationToken cancellation);
}
