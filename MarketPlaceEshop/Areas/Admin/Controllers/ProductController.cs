using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Product.Commands;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    public ProductController(IBrandService brandService, ICategoryService categoryService)
    {
        _brandService = brandService;
        _categoryService = categoryService;
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
        var categories =await _categoryService.GetAllListSortAsync(cancellation);
        return View(categories);
    }

    public async Task<IActionResult> CreateCategory(CategoryCommand categoryDto,CancellationToken cancellation)
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


}
