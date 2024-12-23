using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Person;
using Model.Entities.Products;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Classes;

public class ProductSupplierRepository : BaseRepository<ProductSupplier, Guid>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<int> GetInventory(Guid productSupplierId, CancellationToken cancellation)
    {
        return await _entitySet.Where(x => x.Id == productSupplierId).Select(x => x.Ventory).FirstOrDefaultAsync(cancellation);
    }

    public async Task<decimal> GetTotalInventory(CancellationToken cancellation)
    {
        var data = await _entitySet.Where(x => x.Ventory > 0).Select(x => new
        {
            x.Ventory,
            PriceValue = x.Prices
           .Where(p => p.ExpiredTime == null)
           .Select(p => p.PriceValue)
           .FirstOrDefault() 
        }).ToListAsync(cancellation);

        return data.Sum(x => x.Ventory * x.PriceValue);
    }

    public async Task<decimal> GetTotalInventoryBySupplierId(Guid supplierId, CancellationToken cancellation)
    {
        var data = await _entitySet
            .Where(x => x.Ventory > 0 && x.SupplierId == supplierId)
            .Select(x => new
            {
                x.Ventory,
                PriceValue = x.Prices
                .Where(p => p.ExpiredTime == null)
                .Select(p => p.PriceValue)
                .FirstOrDefault()  
            }).ToListAsync(cancellation);

        return data.Sum(x => x.Ventory * x.PriceValue);
    }
}
