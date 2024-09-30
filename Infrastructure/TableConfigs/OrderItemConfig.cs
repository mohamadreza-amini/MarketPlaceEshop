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

public class OrderItemConfig:BaseConfig<OrderItem,Guid>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x=>x.Cost).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.UnitCost).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.Discount).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.UnitDiscount).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x=>x.Quantity).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x=>x.Sent).HasDefaultValue(false).IsRequired();
        builder.Property(x=>x.DateOfPosting).IsRequired(false);
        builder.Property(x => x.IsCanceled).HasDefaultValue(false).IsRequired();


        builder.HasOne(x=>x.ProductSupplier).WithMany(x=>x.OrderItems).HasForeignKey(x=>x.ProductSupplierId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.Order).WithMany(x=>x.OrderItems).HasForeignKey(x=>x.OrderId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        //تایید اردر در اردر هست در ایتم نیاوردم
        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);
    }
}
