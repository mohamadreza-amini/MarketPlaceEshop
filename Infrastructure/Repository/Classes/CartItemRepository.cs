using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Orders;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Classes;

public class CartItemRepository : BaseRepository<CartItem, Guid>, ICartItemRepository
{
    public CartItemRepository(AppDbContext appDbContext) : base(appDbContext) { }

    public async Task<int> UpdateQuantitiesById(Guid productSupplierId, int quantity, CancellationToken cancellation)
    {
        var affectedItems = await _entitySet
            .Where(x => x.ProductSupplierId == productSupplierId && x.Quantity > quantity)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Quantity, quantity), cancellation);
        await RemoveWithoutCapacity(cancellation);
        return affectedItems;
    }

    public async Task<int> UpdateAllQuantities(CancellationToken cancellation)
    {
        var affectedItems = await _entitySet
            .Where(x => x.ProductSupplier.Ventory < x.Quantity)
            .ExecuteUpdateAsync(x => x.SetProperty(cartitem => cartitem.Quantity, cartitem => cartitem.ProductSupplier.Ventory), cancellation);
        await RemoveWithoutCapacity(cancellation);
        return affectedItems;
    }

    public async Task<int> RemoveWithoutCapacity(CancellationToken cancellation)
    {
        return await _entitySet
            .Where(x => x.Quantity == 0)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, true), cancellation);
    }


    public async Task<int> DeleteAllByCustomerId(Guid customerId, CancellationToken cancellation)
    {
        return await _entitySet
           .Where(x => x.CustomerId == customerId)
           .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, true), cancellation);
    }


    public async Task<decimal> GetCartTotalPriceByCustomerId(Guid customerId, CancellationToken cancellation)
    {
        return await _entitySet
            .Where(x => x.CustomerId == customerId)
            .SumAsync(x => x.ProductSupplier.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault());
    }

    public async Task<decimal> GetCartTotalDiscountByCustomerId(Guid customerId, CancellationToken cancellation)
    {
        return await _entitySet.Where(x => x.CustomerId == customerId).SumAsync(x => x.ProductSupplier.Discount);
    }


    public async Task<decimal> GetTotalValueOfCarts(CancellationToken cancellation)
    {
        return await _entitySet.SumAsync(x => x.Quantity * x.ProductSupplier.Prices.Where(x => x.ExpiredTime == null).Select(x => x.PriceValue).FirstOrDefault(), cancellation);
    }


}
