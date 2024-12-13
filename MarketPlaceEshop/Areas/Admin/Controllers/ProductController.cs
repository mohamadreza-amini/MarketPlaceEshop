using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.Categories;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    private readonly ICategoryFeatureService _categoryFeatureService;

    public ProductController(IBrandService brandService, ICategoryService categoryService, ICategoryFeatureService categoryFeatureService)
    {
        _brandService = brandService;
        _categoryService = categoryService;
        _categoryFeatureService = categoryFeatureService;
    }

    public IActionResult Index()
    {
        return View();
    }

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







    public async Task<IActionResult> CategoryFeature(CancellationToken cancellation, int? categoryId)
    {
        var atr =await PrepareCategoryFeature(cancellation, categoryId);
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

}
