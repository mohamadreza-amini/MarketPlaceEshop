﻿using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using System.Linq.Expressions;

namespace Service.ServiceClasses.ProductServices;

public class ProductSupplierService : ServiceBase<ProductSupplier, ProductSupplierResult, Guid>, IProductSupplierService
{
    private readonly IProductSupplierRepository _productSupplierRepository;
    private readonly IUserService _userService;
    private readonly IPriceService _priceService;
    private readonly IBaseRepository<Product, Guid> _productRepository;
    private readonly ILogger<ProductSupplierService> logger;
    public ProductSupplierService(IProductSupplierRepository productSupplierRepository, IUserService userService, IPriceService priceService, IBaseRepository<Product, Guid> productRepository, ILogger<ProductSupplierService> _logger)
    {
        _productSupplierRepository = productSupplierRepository;
        _userService = userService;
        _priceService = priceService;
        _productRepository = productRepository;
        logger = _logger;
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

        await VerifyProductStatus(productSupplier, productSupplierDto, cancellationToken);
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
        productSupplier.UpdaterUserId = productSupplier.CreatorUserId;
        productSupplier.Validate();

        await _productSupplierRepository.CreateAsync(productSupplier, cancellationToken);
        await _productSupplierRepository.CommitAsync(cancellationToken);
        logger.LogInformation($"Add supplier [{productSupplierDto.SupplierId}] to the product [{productSupplierDto.ProductId}] with inventory [{productSupplierDto.Ventory}] " +
           $"with price [{productSupplierDto.PriceValue}] and discount [{productSupplierDto.Discount}] by [{productSupplier.CreatorUserId}]");
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

        await VerifyProductStatus(productSupplier, productSupplierDto, cancellationToken);
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
        logger.LogInformation($" supplier [{productSupplierDto.SupplierId}] changes for product [{productSupplierDto.ProductId}] with inventory [{productSupplierDto.Ventory}] " +
           $"with price [{productSupplierDto.PriceValue}] and discount [{productSupplierDto.Discount}] by [{productSupplier.UpdaterUserId}]");
    }


    private async Task VerifyProductStatus(ProductSupplier productSupplier, ProductSupplierCommand productSupplierDto, CancellationToken cancellationToken)
    {
        if (await IsDisableProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product is Disable");
        if (!await IsConfirmedProduct(productSupplier.ProductId, cancellationToken))
            throw new BadRequestException("product isn't confirmed");

        if (productSupplierDto.PriceValue <= productSupplierDto.Discount)
            throw new BadRequestException("تخفیف از قیمت بزرگتر وارد شده");
    }

    public async Task<Guid?> GetSuppierIdById(Guid ProductSupplierId, CancellationToken cancellation)
    {
        var productSupplier = await _productSupplierRepository.GetByIdAsync(ProductSupplierId, cancellation);
        Guid? supplierId = null;
        if (productSupplier != null)
            supplierId = productSupplier.SupplierId;
        return supplierId;
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

    public async Task<List<ProductSupplierResult>> GetAllSupplierByProductId(Guid productId, CancellationToken cancellation)
    {
        var query = await _productSupplierRepository.GetAllDataAsync(
            x => new ProductSupplierResult
            {
                Id = x.Id,
                SupplierId = x.SupplierId,
                CompanyName = x.Supplier.CompanyName,
                ProductId = x.ProductId,
                Ventory = x.Ventory,
                Discount = x.Discount,
                Price = x.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault()
            }
        , cancellation, x => x.ProductId == productId && x.Ventory > 0, x => x.Include(x => x.Supplier).Include(x => x.Prices));
        if (query == null)
            return new List<ProductSupplierResult>();
        return await query.OrderBy(x => x.Price).ToListAsync(cancellation);
    }


    //لیست محصول-تامین کننده برای دیدن ادمین و تامین کننده نه بقیه
    public async Task<PaginatedList<ProductSupplierFullResult>> GetAllProductSupplierByPerson(CancellationToken cancellation, int pageIndex = 1, int pageSize = 20)
    {
        Expression<Func<ProductSupplier, bool>> predicate;

        if (_userService.IsAdmin())
        {
            predicate = x => true;
        }
        else if (_userService.IsInRole("Supplier") && Guid.TryParse(_userService.RequesterId(), out Guid requesterId))
        {
            predicate = x => x.SupplierId == requesterId;
        }
        else
        {
            throw new AccessDeniedException();
        }
        var query = await _productSupplierRepository.GetAllDataAsync(
           x => new ProductSupplierFullResult
           {
               Id = x.Id,
               SupplierId = x.SupplierId,
               CompanyName = x.Supplier.CompanyName,
               ProductId = x.ProductId,
               Ventory = x.Ventory,
               Discount = x.Discount,
               Price = x.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault(),
               StartDate = x.Product.StartDate,
               BrandName = x.Product.Brand.BrandName,
               CategoryId = x.Product.CategoryId,
               CategoryName = x.Product.Category.CategoryName,
               Titel = x.Product.Titel,
               ProductName = x.Product.Name,
               AverageScore = x.Product.Scores != null && x.Product.Scores.Any() ? x.Product.Scores.Select(s => s.StarRating).Average() : 0
           }
           , cancellation, predicate);

        if (query == null)
            return await PaginatedList<ProductSupplierFullResult>.CreateAsync(query, 1, pageSize, cancellation);

        return await PaginatedList<ProductSupplierFullResult>.CreateAsync(query.OrderBy(x=>x.ProductId), pageIndex, pageSize, cancellation);
    }


    // مجموع موجودی حال اگه ادمین بود تمام تامین کنندگان اگه تامین کننده بود خودش
    public async Task<decimal> GetTotalInventory(CancellationToken cancellation)
    {
        if (_userService.IsAdmin())
        {
            return await _productSupplierRepository.GetTotalInventory(cancellation);
        }
        else if (_userService.IsInRole("Supplier") && Guid.TryParse(_userService.RequesterId(), out Guid supplierId))
        {
            return await _productSupplierRepository.GetTotalInventoryBySupplierId(supplierId, cancellation);
        }
        throw new AccessDeniedException();
    }

    private async Task<bool> IsConfirmedProduct(Guid productId, CancellationToken cancellation)
    {
        var product = (await _productRepository.GetByIdAsync(productId, cancellation)) ?? throw new BadRequestException("product not found");
        return product.IsConfirmed == 2;
    }

    private async Task<bool> IsDisableProduct(Guid productId, CancellationToken cancellation)
    {
        var product = (await _productRepository.GetByIdAsync(productId, cancellation)) ?? throw new BadRequestException("product not found");
        return product.IsDisable;
    }

}
