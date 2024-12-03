using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IProductService:IServiceBase<Product,ProductResult,Guid>
{
    Task<bool> CreateAsync(ProductCommand productDto,CancellationToken cancellation); 
    Task ChangeProductStatus(Guid productId,ConfirmationStatus confirmationStatus,CancellationToken cancellation);

    
   
}
