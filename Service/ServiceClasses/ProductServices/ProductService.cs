﻿using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReportingServices;
using Service.ServiceInterfaces.ReviewServices;
using Shared.Enums;
using System.Linq.Expressions;

namespace Service.ServiceClasses.ProductServices;

public class ProductService : ServiceBase<Product, ProductResult, Guid>, IProductService
{
    private readonly IBaseRepository<Product, Guid> _productRepository;
    private readonly IUserService _userService;
    private readonly IImageService _imageService;
    private readonly IProductFeatureValueService _productFeatureValueService;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly IScoreService _scoreService;
    private readonly IProductFeatureValueService _productFeatureService;
    private readonly ICommentService _commentService;
    private readonly IProductSupplierService _productSupplierService;
    private readonly IHangfireServices _hangfireServices;


    public ProductService(IBaseRepository<Product, Guid> productRepository,
        IUserService userService,
        IImageService imageService,
        IProductFeatureValueService productFeatureValueService,
        IBrandService brandService, ICategoryService categoryService,
        IScoreService scoreService,
        IProductFeatureValueService productFeatureService,
        ICommentService commentService,
        IProductSupplierService productSupplierService, IHangfireServices hangfireServices)
    {
        _productRepository = productRepository;
        _userService = userService;
        _imageService = imageService;
        _productFeatureValueService = productFeatureValueService;
        _brandService = brandService;
        _categoryService = categoryService;
        _scoreService = scoreService;
        _productFeatureService = productFeatureService;
        _commentService = commentService;
        _productSupplierService = productSupplierService;
        _hangfireServices = hangfireServices;
    }

    public async Task<bool> CreateAsync(ProductCommand productDto, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin() && !_userService.IsInRole("Supplier"))
            throw new AccessDeniedException();

        var product = Translate<ProductCommand, Product>(productDto);
        var productId = Guid.NewGuid();
        product.Id = productId;
        if (await _brandService.GetByIdAsync(product.BrandId, cancellation) == null)
            throw new BadRequestException("[create product faild] - invalid brand");

        if (await _categoryService.GetCategoryAsync(product.CategoryId, cancellation) == null)
            throw new BadRequestException("[create product faild] - invalid category");

        product.Images = _imageService.PrepareToAdd(productDto.Images, productId);
        product.ProductFeatureValues = _productFeatureValueService.PrepareToAdd(productDto.ProductFeatureValues, productId);

        product.StartDate = DateTime.Now;
        product.CreatorUserId = Guid.Parse(_userService.RequesterId() ?? throw new BadRequestException("UserId not found"));
        product.UpdaterUserId = product.CreatorUserId;
        product.IsConfirmed = 1;

        if (_userService.IsAdmin())
        {
            product.IsConfirmed = 2;
            product.ConfirmedDate = DateTime.Now;
            product.AdminConfirmedId = product.CreatorUserId;
        }
        try
        {
            await _productRepository.CreateAsync(product, cancellation);
            return (await _productRepository.CommitAsync(cancellation)) > 0;
        }
        catch (Exception ex)
        {
            throw new BadRequestException("[create product faild] " + ex.Message);
        }
    }

    public async Task ChangeProductStatus(Guid productId, ConfirmationStatus confirmationStatus, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var product = await _productRepository.GetByIdAsync(productId, cancellation);
        if (product == null)
            throw new BadRequestException("product not found");

        product.IsConfirmed = (byte)confirmationStatus;
        product.ConfirmedDate = DateTime.Now;
        product.AdminConfirmedId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        product.UpdaterUserId = product.AdminConfirmedId;

        await _productRepository.CommitAsync(cancellation);
    }

    public async Task<bool> IsConfirmedProduct(Guid productId, CancellationToken cancellation)
    {
        var product = (await _productRepository.GetByIdAsync(productId, cancellation)) ?? throw new BadRequestException("product not found");
        return product.IsConfirmed == 2;
    }

    public async Task<bool> IsDisableProduct(Guid productId, CancellationToken cancellation)
    {
        var product = (await _productRepository.GetByIdAsync(productId, cancellation)) ?? throw new BadRequestException("product not found");
        return product.IsDisable;
    }

    public async Task<bool> ProductExists(Guid productId, CancellationToken cancellation)
    {
        return await _productRepository.GetByIdAsync(productId, cancellation) != null;
    }

    public async Task<ProductResult> GetById(Guid productId, CancellationToken cancellation)
    {
        var query = await _productRepository.GetAllDataAsync(
            x => new ProductResult
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                BrandName = x.Brand.BrandName,
                Description = x.Description,
                Titel = x.Titel,
                CategoryName = x.Category.CategoryName,
                CategoryId = x.CategoryId,
            }, cancellation, x => x.Id == productId && x.IsConfirmed == 2, x => x.Include(x => x.Category).Include(x => x.Brand));

        if (query == null || query.Any() == false)
            throw new BadRequestException("محصول یافت نشد");

        var product = await query.FirstOrDefaultAsync(cancellation);
        _hangfireServices.LogProductView(productId);

        product!.AverageScore = await _scoreService.GetProductAverageRating(productId, cancellation);
        product.ProductFeatureValues = await _productFeatureValueService.GetAllByProductId(productId, cancellation);
        product.Comments = await _commentService.GetAllCommentByProductId(productId, cancellation);
        product.Images = await _imageService.GetAllByProductId(productId, cancellation);
        product.productSuppliers = await _productSupplierService.GetAllSupplierByProductId(productId, cancellation);
        product.ScoreCount = await _scoreService.NumberOfScore(productId, cancellation);
        product.CommentCount = product.Comments.Count();
        return product;
    }


    public async Task<PaginatedList<ProductminiResult>> GetAllbyFilterCommand(ProductFilterCommand filterDto, CancellationToken cancellation, int pageIndex = 1, int pageSize = 20)
    {
        var query = await _productRepository.GetAllDataAsync(x => x, cancellation, x => x.IsConfirmed == 2);

        if (query == null)
            return new PaginatedList<ProductminiResult>(new List<ProductminiResult>(), 0, 1, pageSize);

        query = await FilterProducts(query, filterDto, cancellation);
        query = SortProducts(query, filterDto.SortProduct);

        var products = query.Select((x => new ProductminiResult
        {
            Id = x.Id,
            Name = x.Name,
            Titel = x.Titel,
            ImagePath = x.Images.Select(x => x.Path).FirstOrDefault()!,
            Price = x.productSuppliers.Where(ps => ps.Prices != null && ps.Prices.Any()).SelectMany(ps => ps.Prices).Where(p => p.ExpiredTime == null).Select(p => (decimal?)p.PriceValue).Min() ?? 0,
            AverageScore = x.Scores != null && x.Scores.Any() ? x.Scores.Select(s => s.StarRating).Average() : 0,
            Discount = x.productSuppliers.Any(x => x.Discount > 0 && x.Ventory > 0)
        }));
        return await PaginatedList<ProductminiResult>.CreateAsync(products, pageIndex, pageSize, cancellation);
    }


    private async Task<IQueryable<Product>> FilterProducts(IQueryable<Product> query, ProductFilterCommand filterDto, CancellationToken cancellation)
    {
        if (!string.IsNullOrWhiteSpace(filterDto.SearchText))
            query = query.Where(x => x.Name.Contains(filterDto.SearchText) || x.Titel.Contains(filterDto.SearchText));

        if (filterDto.OnlyAvailable)
            query = query.Where(x => x.productSuppliers.Any(x => x.Ventory > 0));

        if (filterDto.SelectedBrandIds.Any())
            query = query.Where(x => filterDto.SelectedBrandIds.Contains(x.BrandId));

        if (filterDto.SelectedCategoryId.HasValue && filterDto.SelectedCategoryId.Value != 0)
        {
            var subCategories = await _categoryService.GetAllSubCategoryIdbyCategoryId(filterDto.SelectedCategoryId.Value, cancellation);
            query = query.Where(x => subCategories.Contains(x.CategoryId));
        }
        return query;
    }


    private IQueryable<Product> SortProducts(IQueryable<Product> query, SortProduct sortProduct)
    {
        switch (sortProduct)
        {
            case SortProduct.Newest:
                query = query.OrderByDescending(x => x.CreateDatetime);
                break;
            case SortProduct.MostVisited:
                query = query.OrderByDescending(x => x.View);
                break;
            case SortProduct.Cheapest:
                query = query.OrderBy(x => x.productSuppliers.Where(ps => ps.Prices != null && ps.Prices.Any()).SelectMany(x => x.Prices)
                .Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).Min());
                break;
            case SortProduct.MostExpensive:
                query = query.OrderByDescending(x => x.productSuppliers.SelectMany(x => x.Prices)
                .Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).Min());
                break;
            default: break;
        }
        return query;
    }


    public async Task<PaginatedList<ProductPanelResult>> GetAllProductPanelsAsync(CancellationToken cancellationToken, string? searchText = null, ConfirmationStatus? confirmation = null, int pageIndex = 1, int pageSize = 20)
    {
        Expression<Func<Product, bool>> predicate = x => true;
        if (confirmation != null)
            predicate = x => x.IsConfirmed == (byte)confirmation.Value;

        var query = await _productRepository.GetAllDataAsync(x => new ProductPanelResult
        {
            Description = x.Description,
            StartDate = x.StartDate,
            BrandName = x.Brand.BrandName,
            CategoryId = x.CategoryId,
            Titel = x.Titel,
            CategoryName = x.Category.CategoryName,
            Id = x.Id,
            Name = x.Name,
            Images = x.Images.Select(x => new ImageResult
            {
                Id = x.Id,
                Path = x.Path,
                ProductId = x.ProductId
            }).ToList(),
            ProductFeatureValues = x.ProductFeatureValues.Select(x => new ProductFeatureValueResult
            {
                Id = x.Id,
                FeatureName = x.CategoryFeature.FeatureName,
                FeatureValue = x.FeatureValue
            }).ToList()
        }, cancellationToken, predicate);

        if (!string.IsNullOrWhiteSpace(searchText))
            query = query?.Where(x => x.Name.Contains(searchText) || x.Titel.Contains(searchText));

        return await PaginatedList<ProductPanelResult>.CreateAsync(query, pageIndex, pageSize, cancellationToken);
    }

}











