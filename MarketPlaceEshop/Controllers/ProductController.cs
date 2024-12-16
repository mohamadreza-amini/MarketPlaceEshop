using DataTransferObject.DTOClasses.Product.Commands;
using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;

namespace MarketPlaceEshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(CancellationToken cancellation, ProductFilterCommand filter, int pageIndex = 1)
        {
            pageIndex = filter.PageIndex.HasValue && filter.PageIndex != 0 ? filter.PageIndex.Value : pageIndex;

            var productsfilter = new ProductsFilterViewModel
            {
                Products = await _productService.GetAllbyFilterCommand(filter, cancellation, pageIndex, 12),
            };

            productsfilter.Filter = filter ?? new ProductFilterCommand();
            productsfilter.Filter.CategoryList = await _categoryService.GetAllListSortAsync(cancellation);
            productsfilter.Filter.BrandList = await _brandService.GetAllAsync(cancellation);
            return View(productsfilter);

        }




        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellation)
        {
            var product = await _productService.GetById(productId, cancellation);
            return View(product);
        }
    }
}
