using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class CartItemConfig:BaseConfig<CartItem,Guid>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Quantity).IsRequired();

        builder.Property(x=>x.IsCanceled).HasDefaultValue(false).IsRequired();

        builder.HasOne(x=>x.Cart).WithMany(x=>x.CartItems).HasForeignKey(x=>x.CartId).IsRequired();
        builder.HasOne(x=>x.ProductSupplier).WithMany(x=>x.CartItems).HasForeignKey(x=>x.ProductSupplierId).IsRequired();


  
        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);

    }
}
