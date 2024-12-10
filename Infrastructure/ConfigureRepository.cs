using Infrastructure.Cache;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Infrastructure.Repository;
using Infrastructure.Repository.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class ConfigureRepository
{
    public static void RegisterRepositories(this IServiceCollection service)
    {
        service.AddScoped(typeof(IBaseRepository<,>),typeof(BaseRepository<,>));

        service.AddScoped<ICachedData, CachedData>();
        service.AddScoped<ICartItemRepository, CartItemRepository>();
        service.AddScoped<IOrderItemRepository, OrderItemRepository>();
        service.AddScoped<IProductSupplierRepository, ProductSupplierRepository>();
        service.AddScoped<IScoreRepository, ScoreRepository>();
        service.AddScoped<IViewLogRepository, ViewLogRepository>();
 
    }
}
