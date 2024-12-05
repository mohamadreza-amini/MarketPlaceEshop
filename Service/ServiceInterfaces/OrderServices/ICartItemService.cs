using DataTransferObject.DTOClasses.Order.Commands;
using DataTransferObject.DTOClasses.Order.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.OrderServices;

public interface ICartItemService : IServiceBase<CartItem, CartItemResult, Guid>
{
    //بعد هر تغیر ظرفیت بررسی میکند اون محصول توی هر سبد خریدی بود و تعدادش بیشتر از موجودی بود به سقف موجودی میرسونه صفر باشه حذف میکنه
    Task RefreshCartQuantitiesAsync(Guid productSupplierId, int quantity, CancellationToken cancellation);
    //ایتم در سبد خرید موجود است
    Task<bool> IsItemInCartAsync(Guid customerId, Guid productSupplierId, CancellationToken cancellation);
    Task ChangeItemQuantityInCartAsync(CartItemCommand cartItemDto, CancellationToken cancellation);
    Task AddCartItemAsync(CartItemCommand cartItemDto, CancellationToken cancellation);

}
