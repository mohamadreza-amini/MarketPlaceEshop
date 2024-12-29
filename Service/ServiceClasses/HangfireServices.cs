using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Entities.Products;
using Model.Entities.Reports;
using Service.ServiceInterfaces.ReportingServices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses;

public class HangfireServices : IHangfireServices
{
    private readonly IServiceProvider _serviceProvider;
    private List<ViewLog> _views { get; set; } = new();
    private ConcurrentDictionary<Guid, int> _productViews { get; set; } = new();

    public HangfireServices(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void AddViewLog(ViewLog log) => _views.Add(log);

    public async Task SaveViewLogs()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var viewLogRepository = scope.ServiceProvider.GetRequiredService<IViewLogRepository>();
            await viewLogRepository.SaveViewLogs(_views);
            _views = new();
        }
    }

    public void LogProductView(Guid ProductId)
    {
        if (_productViews.ContainsKey(ProductId))
        {
            _productViews[ProductId]++;
        }
        else
        {
            _productViews[ProductId] = 1;
        }
    }


    public async Task SaveProductViewLogs()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var productRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository<Product, Guid>>();
            var products = await productRepository.GetAll(x => _productViews.Keys.Contains(x.Id), false).ToListAsync(CancellationToken.None);

            foreach (var product in products)
                product.View += _productViews[product.Id];

            await productRepository.CommitAsync(CancellationToken.None);
            _productViews = new();
        }

    }
}


