using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Areas.Supplier.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Addresses;
using Model.Entities.Categories;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;
using Shared.Enums;
using System.Security.Claims;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]
[Authorize(Roles = "Supplier")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IProductSupplierService _productSupplierService;
    public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ICategoryFeatureService categoryFeatureService, IProductSupplierService productSupplierService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _categoryFeatureService = categoryFeatureService;
        _productSupplierService = productSupplierService;
    }

    [HttpGet]
    public async Task<IActionResult> CreateProductCategory(CancellationToken cancellation)
    {
        var createProductModel = new CreateProductViewModel()
        {
            Categories = await _categoryService.GetAllListSortAsync(cancellation),
            Brands = await _brandService.GetAllAsync(cancellation)
        };

        return View(createProductModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(int categoryId, int brandId, CancellationToken cancellation)
    {
        if (categoryId == 0 || brandId == 0)
        {
            TempData["ToastError"] = "اطلاعات برند و دسته بندی وارد نشده است";
            return LocalRedirect("/supplier/product/CreateProductCategory");
        }
        var product = new ProductGenerateViewModel
        {
            ProductDto = new ProductCommand()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                CategoryName = (await _categoryService.GetCategoryAsync(categoryId, cancellation))!.CategoryName,
                BrandName = (await _brandService.GetByIdAsync(brandId, cancellation))!.BrandName,
                ProductFeatureValues = await _categoryFeatureService.GetFeatureCommand(categoryId, cancellation)
            }
        };
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductGenerateViewModel product, CancellationToken cancellation)
    {
        if (!ModelState.IsValid)
        {
            TempData["ToastError"] = "اطلاعات وارد شده صحیح نیست";
            return RedirectToAction("CreateProduct", "Product", new { area = "Supplier", categoryId = product.ProductDto.CategoryId, brandId = product.ProductDto.BrandId });
        }
        var root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");
        var uploadedFiles = new List<string>();
        if (!Directory.Exists(root))
            Directory.CreateDirectory(root);

        await SaveImages(product, root, uploadedFiles, cancellation);

        var result = await _productService.CreateAsync(product.ProductDto, cancellation);
        if (!result)
        {
            DeleteImages(uploadedFiles);
            TempData["ToastError"] = "عملیات ناموفق";
            return RedirectToAction("CreateProduct", "Product", new { area = "Supplier", categoryId = product.ProductDto.CategoryId, brandId = product.ProductDto.BrandId });
        }
        TempData["ToastSuccess"] = "درخواست ثبت محصول با موفقیت انجام شد";
        return RedirectToAction("index", "home", new { area = "Supplier" });
    }

    private async Task SaveImages(ProductGenerateViewModel product, string root, List<string> uploadedFiles, CancellationToken cancellation)
    {
        product.ProductDto.Images = new List<ImageCommand>();
        foreach (var image in product.ProductImages)
        {
            var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var address = Path.Combine(root, newFileName);
            product.ProductDto.Images.Add(new ImageCommand
            {
                Path = address,
                FileSize = (int)image.Length / 1000,
                MimeType = image.ContentType
            });
            uploadedFiles.Add(address);

            using (var stream = new FileStream(address, FileMode.Create))
            {
                await image.CopyToAsync(stream, cancellation);
            }
        }
    }
    private void DeleteImages(List<string> uploadedFiles)
    {
        foreach (var file in uploadedFiles)
        {
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);
        }
    }


    //اضافه شدن تامین کننده به محصول
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellation, string? serachText = null, int pageIndex = 1,int pageSize = 10)
    {
        var products = new ProductSupplierViewModel
        {
            Products = await _productService.GetAllProductPanelsAsync(cancellation, serachText, ConfirmationStatus.Confirmed, pageIndex, pageSize),
            SerachText=serachText
        };
        return View(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductSupplier(ProductSupplierCommand ProductSupplier, CancellationToken cancellation)
    {
        if (!ModelState.IsValid || !Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid requester))
        {
            TempData["ToastError"] = "اطلاعات ورودی نامعتبر";
            return RedirectToAction("GetAllProducts", "Product", new { area = "Supplier" });
        }
           
        ProductSupplier.SupplierId = requester;
        try
        {
            await _productSupplierService.AddSupplierToProduct(ProductSupplier, cancellation);
        }
        catch (BadRequestException ex)
        {
            TempData["ToastError"] = ex.Message.Replace("Bad request!","");
            return RedirectToAction("GetAllProducts", "Product", new { area = "Supplier" });
        }
        return RedirectToAction("GetAllProductSuppliers", "Product", new {area="Supplier"});
    }


    //دیدن و تغیر محصولات تامین کننده
    public async Task<IActionResult> GetAllProductSuppliers(CancellationToken cancellation, string? serachText = null, int pageIndex = 1, int pageSize = 10)
    {
        var productSupplier = new ProductSupplierViewModel
        {
           ProductSuppliers= await _productSupplierService.GetAllProductSupplierByPerson(cancellation, pageIndex, pageSize)
        };
        return View(productSupplier);
    }


    [HttpPost]
    public async Task<IActionResult> EditProductSupplier(ProductSupplierCommand ProductSupplier, CancellationToken cancellation)
    {
        if (!ModelState.IsValid || !Guid.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid requester))
        {
            TempData["ToastError"] = "اطلاعات ورودی نامعتبر";
            return RedirectToAction("GetAllProductSuppliers", "Product", new { area = "Supplier" });
        }

        ProductSupplier.SupplierId = requester;
        try
        {
            await _productSupplierService.UpdateSupplierProduct(ProductSupplier, cancellation);
        }
        catch (BadRequestException ex)
        {
            TempData["ToastError"] = ex.Message.Replace("Bad request!", "");
        }
        return RedirectToAction("GetAllProductSuppliers", "Product", new { area = "Supplier" });
    }

}

