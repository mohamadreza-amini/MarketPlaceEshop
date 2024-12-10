using Microsoft.Extensions.DependencyInjection;
using Service.ServiceClasses.AddressServices;
using Service.ServiceClasses.CategoryServices;
using Service.ServiceClasses.OrderServices;
using Service.ServiceClasses.PersonServices;
using Service.ServiceClasses.ProductServices;
using Service.ServiceClasses.ReportingServices;
using Service.ServiceClasses.ReviewServices;
using Service.ServiceInterfaces.AddressServices;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.OrderServices;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using Service.ServiceInterfaces.ReportingServices;
using Service.ServiceInterfaces.ReviewServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses;

public static class ConfigureService
{
    public static void RegisterServices(this IServiceCollection service)
    {
        service.AddScoped<IAdressService, AddressService>();
        service.AddScoped<ICityService, CityService>();
        service.AddScoped<IProvinceService, ProvinceService>();

        service.AddScoped<ICategoryFeatureService, CategoryFeatureService>();
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IProductFeatureValueService, ProductFeatureValueService>();
        service.AddScoped<ICartItemService,CartItemService>();
        service.AddScoped<IOrderItemService, OrderItemService>();
        service.AddScoped<IOrderService, OrderService>();

        service.AddScoped<IAdminService, AdminService>();
        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<ISupplierService, SupplierService>();
        service.AddScoped<IUserService, UserService>();

        service.AddScoped<IBrandService, BrandService>();
        service.AddScoped<IImageService, ImageService>();
        service.AddScoped<IPriceService, PriceService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IProductSupplierService, ProductSupplierService>();

        service.AddScoped<IViewLogService, ViewLogService>();
        service.AddScoped<ICommentService, CommentService>();
        service.AddScoped<IScoreService, ScoreService>();

    }
}
