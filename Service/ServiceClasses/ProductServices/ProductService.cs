using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using DataTransferObject.DTOClasses.Review.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Categories;
using Model.Entities.Products;
using Model.Entities.Review;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReviewServices;
using Shared.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

    public ProductService(IBaseRepository<Product, Guid> productRepository,
        IUserService userService,
        IImageService imageService,
        IProductFeatureValueService productFeatureValueService,
        IBrandService brandService, ICategoryService categoryService,
        IScoreService scoreService,
        IProductFeatureValueService productFeatureService,
        ICommentService commentService,
        IProductSupplierService productSupplierService)
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
            x => TranslateToDTO(x), cancellation, x => x.Id == productId && x.IsConfirmed == 2, x => x.Include(x => x.Category).Include(x => x.Brand));
        var product = await query?.FirstOrDefaultAsync(cancellation);

        if (product == null)
            throw new BadRequestException("محصول یافت نشد");
        //میشد برد توی ریپازیتوری یا کلا پروداکت رو کامل اورد به جای گرفتن از سرویس های مختلف
        //بخشی از کار دیگه که میشد کرد رو پایین نوشتم کامنته
        product.AverageScore = await _scoreService.GetProductAverageRating(productId, cancellation);
        product.ProductFeatureValues = await _productFeatureValueService.GetAllByProductId(productId, cancellation);
        product.Comments = await _commentService.GetAllCommentByProductId(productId, cancellation);
        product.Images = await _imageService.GetAllByProductId(productId, cancellation);
        product.productSuppliers = await _productSupplierService.GetAllSupplierByProductId(productId, cancellation);
        product.ScoreCount = await _scoreService.NumberOfScore(productId, cancellation);
        product.CommentCount = product.Comments.Count();
        return product;
    }




    /*    public async Task<ProductResult> GetByIdss(Guid productId, CancellationToken cancellation)
        {
            var query = await _productRepository.GetAllDataAsync(
                x => new ProductResult
                {
                    Comments = Translate<List<Comment>, List<CommentResult>>(x.Comments.ToList()),
                    AverageScore = x.Scores.Average(x => x.StarRating),
                    Images = Translate<List<Image>, List<ImageResult>>(x.Images.ToList()),
                    ProductFeatureValues = Translate<List<ProductFeatureValue>, List<ProductFeatureValueResult>>(x.ProductFeatureValues.ToList()),
                    productSuppliers = Translate<List<ProductSupplier>, List<ProductSupplierResult>>(x.productSuppliers.ToList()),

                },
                cancellation,
                x => x.Id == productId && x.IsConfirmed == 2,
                x => x.Include(x => x.Category).Include(x => x.Brand).Include(x => x.Images).Include(x => x.ProductFeatureValues).ThenInclude(x => x.CategoryFeature).Include(x => x.Comments).Include(x => x.Scores));
            var product = await query?.FirstOrDefaultAsync(cancellation);


            if (product == null)
                throw new BadRequestException("محصول یافت نشد");

            product.CommentCount = product.Comments?.Count() ?? 0;

            product.ScoreCount = await _scoreService.NumberOfScore(productId, cancellation);

            //  اینو همینجوری نوشتم به عنوان راهی دیگه برای نوشتن بالایی  کامل ننوشتم و همون بالایی اصلیه و کامله
            //حس میکنم این روش بهتره بعدا وقت بود بررسی کن یا تغیر بده
            return product;
        }*/




    public async Task<PaginatedList<ProductminiResult>> GetAllbyFilterCommand(ProductFilterCommand filterDto, CancellationToken cancellation, int pageIndex = 1, int pageSize = 20)
    {
        var query = await _productRepository.GetAllDataAsync(x => x, cancellation, x => x.IsConfirmed == 2, x => x.Include(x => x.Images).Include(x => x.productSuppliers).ThenInclude(x => x.Prices));

        if (query == null)
            return new PaginatedList<ProductminiResult>(new List<ProductminiResult>(), 0, 1, pageSize);
        //اینجوری مشکل داره نمیتونه لیست برند و کتگوری پر بشه کلا روش خوبی نیست
        query = await FilterProducts(query, filterDto, cancellation);

        query = SortProducts(query, filterDto.SortProduct);

        var products = query.Select((x => new ProductminiResult
        {
            Id = x.Id,
            Name = x.Name,
            Titel = x.Titel,
            ImagePath = x.Images.Select(x => x.Path).FirstOrDefault()!,
            Price = x.productSuppliers.SelectMany(x => x.Prices).Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).Min()
        }));

        return await PaginatedList<ProductminiResult>.CreateAsync(products, pageIndex, pageSize, cancellation);
        //مقادیر فیلتر ها باید در اندپوینت همراه خروجی این سرویس در یه مدل ویو ترکیب بشن و برگردانده بشن و وظیفه سرویس نیست فیلتر رو پر کنه برگردانه برای ویو
        //در اون مدل ویو باید فیلترها  پر بشن مقادیر لیست برند و کتگوری از سرویس گرفته بشن و همراه خروجی این لیست محصولات صقحه بندی شده برگشت داده بشن
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
            case SortProduct.Oldest:
                query = query.OrderBy(x => x.CreateDatetime);
                break;
            case SortProduct.Cheapest:
                query = query.OrderBy(x => x.productSuppliers.SelectMany(x => x.Prices)
                .Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).DefaultIfEmpty(decimal.MaxValue).Min());
                break;
            case SortProduct.MostExpensive:
                query = query.OrderByDescending(x => x.productSuppliers.SelectMany(x => x.Prices)
                .Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).DefaultIfEmpty(decimal.MinValue).Min());
                break;
            default: break;
        }
        return query;
    }




    public async Task<PaginatedList<ProductPanelResult>> GetProductPanelsAsync(CancellationToken cancellationToken, ConfirmationStatus? confirmation = null, int pageIndex = 1, int pageSize = 20)
    {
        Expression<Func<Product, bool>> predicate = x => true;
        if (confirmation != null)
        {
            predicate = x => x.IsConfirmed == (byte)confirmation.Value;
        }
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

        return await PaginatedList<ProductPanelResult>.CreateAsync(query,pageIndex,pageSize,cancellationToken);

    }

}











