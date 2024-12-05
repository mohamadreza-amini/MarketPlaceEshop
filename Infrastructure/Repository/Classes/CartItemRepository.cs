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

    public async Task<int> UpdateQuantities(Guid productSupplierId, int quantity, CancellationToken cancellation)
    {
        var affectedItems = await _entitySet
            .Where(x => x.ProductSupplierId == productSupplierId && x.Quantity > quantity)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.Quantity, quantity), cancellation);
        await RemoveWithoutCapacity(cancellation);
        return affectedItems;
    }
    public async Task<int> RemoveWithoutCapacity(CancellationToken cancellation)
    {
        return await _entitySet
            .Where(x => x.Quantity == 0)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, true), cancellation);
    }
}
