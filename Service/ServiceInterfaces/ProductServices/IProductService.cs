using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using Service.ServiceClasses;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IProductService : IServiceBase<Product, ProductResult, Guid>
{
    Task<bool> CreateAsync(ProductCommand productDto, CancellationToken cancellation);
    Task ChangeProductStatus(Guid productId, ConfirmationStatus confirmationStatus, CancellationToken cancellation);
    Task<bool> IsConfirmedProduct(Guid productId, CancellationToken cancellation);
    Task<bool> IsDisableProduct(Guid productId, CancellationToken cancellation);
    Task<bool> ProductExists(Guid productId, CancellationToken cancellation);
    Task<ProductResult> GetById(Guid productId, CancellationToken cancellation);
    Task<PaginatedList<ProductminiResult>> GetAllbyFilterCommand(ProductFilterCommand filterDto, CancellationToken cancellation, int pageIndex = 1, int pageSize = 20);

    Task<PaginatedList<ProductPanelResult>> GetProductPanelsAsync(CancellationToken cancellationToken, ConfirmationStatus? confirmation = null, int pageIndex = 1, int pageSize = 20);
}
