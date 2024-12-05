using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ProductServices;

public class ProductSupplierService : ServiceBase<ProductSupplier, ProductSupplierResult, Guid>, IProductSupplierService
{
    private readonly IProductSupplierRepository _productSupplierRepository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IPriceService _priceService;

    public ProductSupplierService(IProductSupplierRepository productSupplierRepository, IUserService userService, IProductService productService, IPriceService priceService)
    {
        _productSupplierRepository = productSupplierRepository;
        _userService = userService;
        _productService = productService;
        _priceService = priceService;
    }


    public async Task AddSupplierToProduct(ProductSupplierCommand productSupplierDto, CancellationToken cancellationToken)
    {
        if (!_userService.IsAdmin() && (!_userService.IsRequesterUser(productSupplierDto.SupplierId) || !_userService.IsInRole("Supplier")))
            throw new AccessDeniedException();

        var productSupplier = await _productSupplierRepository.GetAsync(x => x.ProductId == productSupplierDto.ProductId && x.SupplierId == productSupplierDto.SupplierId, cancellationToken);

        if (productSupplier != null)
            throw new BadRequestException("تامین کننده هم اکنون هم فروشنده محصول میباشد");

        productSupplier = Translate<ProductSupplierCommand, ProductSupplier>(productSupplierDto);
        productSupplier.Id = Guid.NewGuid();

        if (await _productService.IsDisableProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product is Disable");
        if (!await _productService.IsConfirmedProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product isn't confirmed");

        if (productSupplierDto.PriceValue >= productSupplierDto.Discount)
            throw new BadRequestException("تخفیف از قیمت بزرگتر وارد شده");

        if (productSupplier.Ventory > 0)
        {
            var priceresult = await _priceService.AddPriceAsync(new PriceCommand
            {
                PriceValue = productSupplierDto.PriceValue,
                ProductSupplierId = productSupplier.Id
            }, cancellationToken);
        }
        else
        {
            productSupplier.Discount = 0;
        }
        productSupplier.CreatorUserId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        productSupplier.Validate();

        await _productSupplierRepository.CreateAsync(productSupplier, cancellationToken);
        await _productSupplierRepository.CommitAsync(cancellationToken);
    }



    public async Task UpdateSupplierProduct(ProductSupplierCommand productSupplierDto, CancellationToken cancellationToken)
    {
        if (!_userService.IsAdmin() && (!_userService.IsRequesterUser(productSupplierDto.SupplierId) || !_userService.IsInRole("Supplier")))
            throw new AccessDeniedException();

        var productSupplier = await _productSupplierRepository.GetAsync(x => x.ProductId == productSupplierDto.ProductId && x.SupplierId == productSupplierDto.SupplierId, cancellationToken);

        if (productSupplier == null)
            throw new BadRequestException("تامین کننده فروشنده محصول نمیباشد");

        productSupplier.Ventory = productSupplierDto.Ventory;
        productSupplier.IsDisable = productSupplierDto.IsDisable;
        productSupplier.Discount = productSupplierDto.Discount;

        if (await _productService.IsDisableProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product is Disable");
        if (!await _productService.IsConfirmedProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product isn't confirmed");

        if (productSupplierDto.PriceValue >= productSupplierDto.Discount)
            throw new BadRequestException("تخفیف از قیمت بزرگتر وارد شده");

        if (productSupplier.Ventory > 0)
        {
            var priceresult = await _priceService.AddPriceAsync(new PriceCommand
            {
                PriceValue = productSupplierDto.PriceValue,
                ProductSupplierId = productSupplier.Id
            }, cancellationToken);
        }
        else
        {
            await _priceService.ExpireLastPrice(productSupplier.Id, cancellationToken);
            productSupplier.Discount = 0;
        }

        productSupplier.UpdaterUserId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        productSupplier.Validate();

        await _productSupplierRepository.CommitAsync(cancellationToken);
    }

    public async Task<ProductSupplierResult?> GetSuppierProductById(Guid ProductSupplierId, CancellationToken cancellation)
    {
        var productSupplier = await _productSupplierRepository.GetByIdAsync(ProductSupplierId, cancellation);
        ProductSupplierResult? productSupplierResult = null;
        if (productSupplier != null)
            productSupplierResult = TranslateToDTO(productSupplier);
        return productSupplierResult;
    }

    public async Task<int> GetInventory(Guid productSupplierId, CancellationToken cancellation)
    {
        return await _productSupplierRepository.GetInventory(productSupplierId, cancellation);
    }

    public async Task<bool> HasInventory(Guid productSupplierId, int quantity, CancellationToken cancellation)
    {
        return await GetInventory(productSupplierId, cancellation) >= quantity;
    }


    public async Task<bool> IsActiveProductSupplier(Guid productSupplierId, CancellationToken cancellation)
    {
        var product = (await _productSupplierRepository.GetByIdAsync(productSupplierId, cancellation)) ?? throw new BadRequestException("product-supplier not found");
        return product.IsDisable;
    }

    public async Task<bool> ProductSupplierExists(Guid productSupplierId, CancellationToken cancellation)
    {
        return await _productSupplierRepository.GetByIdAsync(productSupplierId, cancellation) != null;
    }
}
