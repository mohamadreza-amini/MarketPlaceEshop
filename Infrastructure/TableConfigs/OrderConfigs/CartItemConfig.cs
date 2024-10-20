using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.OrderConfigs;

public class CartItemConfig : BaseConfig<CartItem, Guid>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne(x => x.Customer).WithMany(x => x.CartItem).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.Cascade); 
        builder.HasOne(x => x.ProductSupplier).WithMany(x => x.CartItems).HasForeignKey(x => x.ProductSupplierId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
