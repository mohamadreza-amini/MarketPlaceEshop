using DataTransferObject.DTOClasses.Product.Commands;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.ProductServices;

namespace MarketPlaceEshop.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IBrandService _brandService;

    public ProductController(IBrandService brandService)
    {
        _brandService = brandService;
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
}
