using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface ICartItemRepository:IBaseRepository<CartItem,Guid>
{
    Task<int> UpdateQuantities(Guid productSupplierId, int quantity,CancellationToken cancellation);
    Task<int> RemoveWithoutCapacity(CancellationToken cancellation);
}
