using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IProductSupplierService : IServiceBase<ProductSupplier, ProductSupplierResult, Guid>
{
    Task AddSupplierToProduct(ProductSupplierCommand productSupplierDto, CancellationToken cancellationToken);
    Task<ProductSupplierResult?> GetSuppierProductById(Guid ProductSupplierId, CancellationToken cancellation);
    Task<int> GetInventory(Guid productSupplierId, CancellationToken cancellation);
    Task<bool> HasInventory(Guid productSupplierId, int quantity, CancellationToken cancellation);
    Task<bool> IsActiveProductSupplier(Guid productSupplierId, CancellationToken cancellation);
    Task<bool> ProductSupplierExists(Guid productSupplierId, CancellationToken cancellation);
}

