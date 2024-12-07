using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using DataTransferObject.DTOClasses.Review.Results;
using Infrastructure.Contracts.Repository;
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        if (await _brandService.GetByIdAsync(product.BrandId, cancellation) == null)
            throw new BadRequestException("[create product faild] - invalid brand");

        if (await _categoryService.GetCategoryAsync(product.CategoryId, cancellation) == null)
            throw new BadRequestException("[create product faild] - invalid category");

        product.Images = _imageService.PrepareToAdd(productDto.Images, productId);
        product.ProductFeatureValues = _productFeatureValueService.PrepareToAdd(productDto.ProductFeatureValues, productId);

        product.StartDate = DateTime.Now;
        product.CreatorUserId = Guid.Parse(_userService.RequesterId() ?? throw new BadRequestException("UserId not found"));
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
            x => TranslateToDTO(x), cancellation, x => x.Id == productId, x => x.Include(x => x.Category).Include(x => x.Brand));
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




    //public async Task<ProductResult> GetById(Guid productId, CancellationToken cancellation)
    //{
    //    var query = await _productRepository.GetAllDataAsync(
    //        x => new ProductResult
    //        {
    //            Comments = Translate<List<Comment>, List<CommentResult>>(x.Comments.ToList()),
    //            AverageScore = x.Scores.Average(x => x.StarRating),
    //            Images = Translate<List<Image>, List<ImageResult>>(x.Images.ToList()),
    //            ProductFeatureValues = Translate<List<ProductFeatureValue>, List<ProductFeatureValueResult>>(x.ProductFeatureValues.ToList()),

    //        } ,
    //        cancellation,
    //        x => x.Id == productId,
    //        x => x.Include(x => x.Category).Include(x => x.Brand).Include(x=>x.Images).Include(x=>x.ProductFeatureValues).ThenInclude(x=>x.CategoryFeature).Include(x=>x.Comments).Include(x=>x.Scores));
    //    var product = await query?.FirstOrDefaultAsync(cancellation);

    //    if (product == null)
    //        throw new BadRequestException("محصول یافت نشد");
    //     اینو همینجوری نوشتم به عنوان راهی دیگه برای نوشتن بالایی  کامل ننوشتم و همون بالایی اصلیه و کامله
    //حس میکنم این روش بهتره بعدا وقت بود بررسی کن یا تغیر بده
    //    return product;
    //}
}
