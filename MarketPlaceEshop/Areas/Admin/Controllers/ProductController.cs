using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Areas.Admin.Models;
using MarketPlaceEshop.Areas.Supplier.Models;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Categories;
using Service.ServiceClasses.ProductServices;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;
using Shared.Enums;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;
    private readonly IProductService _productService;
    public ProductController(IBrandService brandService, ICategoryService categoryService, ICategoryFeatureService categoryFeatureService, IProductService productService)
    {
        _brandService = brandService;
        _categoryService = categoryService;
        _categoryFeatureService = categoryFeatureService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        return View();
    }


    //brand
    [HttpGet]
    public async Task<IActionResult> Brands(CancellationToken cancellation)
    {
        var brands = await _brandService.GetAllAsync(cancellation);
        return View(brands);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand(BrandCommand brandDto, CancellationToken cancellation)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Branderror"] = "فرمت برند وارد شده صحیح نیست";
        }
        else
        {
            await _brandService.CreateAsync(brandDto, cancellation);
        }
        var brands = await _brandService.GetAllAsync(cancellation);
        return View("Brands", brands);
    }





    //category
    public async Task<IActionResult> Category(CancellationToken cancellation)
    {
        var categories = await _categoryService.GetAllListSortAsync(cancellation);
        return View(categories);
    }

    public async Task<IActionResult> CreateCategory(CategoryCommand categoryDto, CancellationToken cancellation)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categoryerror"] = "فرمت دسته بندی وارد شده صحیح نیست";
        }
        else
        {
            await _categoryService.CreateAsync(categoryDto, cancellation);
        }
        var categories = await _categoryService.GetAllListSortAsync(cancellation);
        return View("Category", categories);
    }






    //category Feature
    public async Task<IActionResult> CategoryFeature(CancellationToken cancellation, int? categoryId)
    {
        var atr = await PrepareCategoryFeature(cancellation, categoryId);
        return View(atr);
    }

    public async Task<IActionResult> CreateCategoryFeature(CategoryFeatureCommand categoryFeatureDto, CancellationToken cancellation)
    {
        if (!ModelState.IsValid)
        {
            ViewData["categoryFeature"] = "فرمت ویژگی وارد شده صحیح نیست";
        }
        else
        {
            await _categoryFeatureService.Create(categoryFeatureDto, cancellation);
        }
        var atr = await PrepareCategoryFeature(cancellation, categoryFeatureDto.CategoryId);
        return View("CategoryFeature", atr);
    }


    private async Task<CategoryFeatureViewModel> PrepareCategoryFeature(CancellationToken cancellation, int? categoryId)
    {
        var atr = new CategoryFeatureViewModel()
        {
            CategoryFeatures = new List<CategoryFeatureResult>(),
        };
        atr.CategoryResults = await _categoryService.GetAllListSortAsync(cancellation);
        if (categoryId.HasValue)
        {
            atr.CategoryFeatures = await _categoryFeatureService.GetAllByCategoryId(categoryId.Value, cancellation);
            atr.Category = atr.CategoryResults.FirstOrDefault(x => x.Id == categoryId)!;
        }
        else
        {
            atr.Category = atr.CategoryResults.FirstOrDefault()!;
            atr.CategoryFeatures = await _categoryFeatureService.GetAllByCategoryId(atr.Category.Id, cancellation);
        }
        return atr;
    }







    //add product

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
            return LocalRedirect("/Admin/product/CreateProductCategory");
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
            return RedirectToAction("CreateProduct", "Product", new { area = "Admin", categoryId = product.ProductDto.CategoryId, brandId = product.ProductDto.BrandId });
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
            return RedirectToAction("CreateProduct", "Product", new { area = "Admin", categoryId = product.ProductDto.CategoryId, brandId = product.ProductDto.BrandId });
        }
        TempData["ToastSuccess"] = "درخواست ثبت محصول با موفقیت انجام شد";
        return RedirectToAction("index", "home", new { area = "Admin" });
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






    //confirm product
    public async Task<IActionResult> ProductRequest(CancellationToken cancellation, int pageIndex = 1, int pageSize = 10)
    {
        var productsDto = await _productService.GetAllProductPanelsAsync(cancellation, null, ConfirmationStatus.Unchecked, pageIndex, pageSize);
        return View(productsDto);
    }


    public async Task ConfirmProduct(Guid productId, CancellationToken cancellation)
    {
        await _productService.ChangeProductStatus(productId, ConfirmationStatus.Confirmed, cancellation);
    }

    public async Task RejectProduct(Guid productId, CancellationToken cancellation)
    {
        await _productService.ChangeProductStatus(productId, ConfirmationStatus.Rejected, cancellation);
    }






}
