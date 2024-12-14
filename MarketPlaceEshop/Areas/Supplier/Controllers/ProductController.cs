using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Areas.Supplier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Addresses;
using Model.Entities.Categories;
using Model.Entities.Products;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;

namespace MarketPlaceEshop.Areas.Supplier.Controllers;
[Area("Supplier")]


public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;
    private readonly ICategoryFeatureService _categoryFeatureService;

    public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ICategoryFeatureService categoryFeatureService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _categoryFeatureService = categoryFeatureService;
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
        product.ProductDto.Images=new List<ImageCommand>();
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

}

