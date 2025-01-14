﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.OrderConfigs;

public class OrderItemConfig : BaseConfig<OrderItem, Guid>
{
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(x => x.UnitCost).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.UnitDiscount).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.Quantity).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.Sent).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.DateOfPosting).IsRequired(false);
        builder.Property(x => x.IsCanceled).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.ProductSupplier).WithMany(x => x.OrderItems).HasForeignKey(x => x.ProductSupplierId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Order).WithMany(x => x.OrderItems).HasForeignKey(x => x.OrderId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        RequireTraceable = true;
        UseForTracable = true;
        base.Configure(builder);
    }
}
