using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using Service.ServiceClasses;
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
    Task UpdateSupplierProduct(ProductSupplierCommand productSupplierDto, CancellationToken cancellationToken);
    Task<Guid?> GetSuppierIdById(Guid ProductSupplierId, CancellationToken cancellation);
    Task<int> GetInventory(Guid productSupplierId, CancellationToken cancellation);
    Task<bool> HasInventory(Guid productSupplierId, int quantity, CancellationToken cancellation);
    Task<bool> IsActiveProductSupplier(Guid productSupplierId, CancellationToken cancellation);
    Task<bool> ProductSupplierExists(Guid productSupplierId, CancellationToken cancellation);
    Task<List<ProductSupplierResult>> GetAllSupplierByProductId(Guid productId, CancellationToken cancellation);
    Task<PaginatedList<ProductSupplierFullResult>> GetAllProductSupplierByPerson(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20);
    Task<decimal> GetTotalInventory(CancellationToken cancellation);

}

