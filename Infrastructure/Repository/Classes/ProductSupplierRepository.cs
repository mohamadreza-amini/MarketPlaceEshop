using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
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
        return await _entitySet
            .Where(x => x.Ventory > 0)
            .SumAsync(x => x.Ventory * x.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault(), cancellation);

    }

    public async Task<decimal> GetTotalInventoryBySupplierId(Guid supplierId, CancellationToken cancellation)
    {
        return await _entitySet
            .Where(x => x.Ventory > 0 && x.SupplierId == supplierId)
            .SumAsync(x => x.Ventory * x.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault(), cancellation);
    }
}
