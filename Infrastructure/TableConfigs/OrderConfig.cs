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

public class OrderConfig : BaseConfig<Order, Guid>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.TotalCost).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.TotalDiscount).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.IsCanceled).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.ShippedDate).IsRequired();

        builder.HasOne(x=>x.Address).WithMany(x=>x.Orders).HasForeignKey(x=>x.AddressId).IsRequired();
        builder.HasOne(x=>x.Customer).WithMany(x=>x.Orders).HasForeignKey(x=>x.CustomerId).IsRequired();

        base.RequireTraceable = true;
        base.UseForTracable = true;
        base.NeedConfirmation = true;
        base.Configure(builder);
    }
}
