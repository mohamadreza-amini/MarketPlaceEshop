using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using DataTransferObject.DTOClasses.Review.Commands;
using MarketPlaceEshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Entities.Review;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReviewServices;
using System.Data;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MarketPlaceEshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly ICommentService _commentService;
        private readonly IScoreService _scoreService;
        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ICommentService commentService, IScoreService scoreService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _commentService = commentService;
            _scoreService = scoreService;
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

        public async Task<IActionResult> CategorySearch(CancellationToken cancellation, int? selectedCategoryId)
        {
            var filter = new ProductFilterCommand { SelectedCategoryId = selectedCategoryId };
            var productsfilter = new ProductsFilterViewModel
            {
                Products = await _productService.GetAllbyFilterCommand(filter, cancellation, 1, 12),
            };
            productsfilter.Filter = filter;
            productsfilter.Filter.CategoryList = await _categoryService.GetAllListSortAsync(cancellation);
            productsfilter.Filter.BrandList = await _brandService.GetAllAsync(cancellation);
            return View("Search",productsfilter);
        }






        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellation)
        {
            var product = await _productService.GetById(productId, cancellation);
            return View(product);
        }




        public async Task<IActionResult> AddComments(CommentCommand comment, CancellationToken cancellation)
        {
            try
            {
                if (!User.IsInRole("Customer"))
                    throw new BadRequestException("لطفا وارد حساب خود شوید");
                await _commentService.AddCommentAsync(comment, cancellation);
                TempData["ToastSuccess"] = "نظر با موفقیت ثبت شد و در انتظار تایید می باشد";
            }
            catch (BaseException ex)
            {
                TempData["ToastError"] = ex.Message.Replace("Bad request!", "");
            }
            var product = await _productService.GetById(comment.ProductId, cancellation);
            return View("GetProduct", product);
        }


        public async Task<IActionResult> AddScore(ScoreCommand scoreDto, CancellationToken cancellation)
        {
            try
            {
                if (!User.IsInRole("Customer"))
                    throw new BadRequestException("لطفا وارد حساب خود شوید");
                scoreDto.CustomerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? throw new AccessDeniedException());
                await _scoreService.AddScoreAsync(scoreDto, cancellation);
                TempData["ToastSuccess"] = "امتیاز با موفقیت ثبت شد ";
            }
            catch (BaseException ex)
            {
                TempData["ToastError"] = ex.Message.Replace("Bad request!", "");
            }
            var product = await _productService.GetById(scoreDto.ProductId, cancellation);
            return View("GetProduct", product);
        }
    }
}



