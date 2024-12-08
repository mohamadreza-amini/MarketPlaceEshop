using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface ICartItemRepository:IBaseRepository<CartItem,Guid>
{
    Task<int> UpdateQuantitiesById(Guid productSupplierId, int quantity, CancellationToken cancellation);
    Task<int> UpdateAllQuantities(CancellationToken cancellation);
    Task<int> RemoveWithoutCapacity(CancellationToken cancellation);
    Task<int> DeleteAllByCustomerId(Guid customerId, CancellationToken cancellation);
    Task<decimal> GetCartTotalPriceByCustomerId(Guid customerId, CancellationToken cancellation);
    Task<decimal> GetCartTotalDiscountByCustomerId(Guid customerId, CancellationToken cancellation);
}
