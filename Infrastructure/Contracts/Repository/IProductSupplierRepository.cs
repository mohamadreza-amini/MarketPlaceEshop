using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface IProductSupplierRepository : IBaseRepository<ProductSupplier, Guid>
{
    Task<int> GetInventory(Guid productSupplierId, CancellationToken cancellation);
}
