using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
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
    public ProductService(IBaseRepository<Product, Guid> productRepository, IUserService userService, IImageService imageService, IProductFeatureValueService productFeatureValueService, IBrandService brandService, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _userService = userService;
        _imageService = imageService;
        _productFeatureValueService = productFeatureValueService;
        _brandService = brandService;
        _categoryService = categoryService;
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
}
