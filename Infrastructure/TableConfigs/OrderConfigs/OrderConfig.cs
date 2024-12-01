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

public class OrderConfig : BaseConfig<Order, Guid>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.ShippedDate).IsRequired();
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Address).WithMany(x => x.Orders).HasForeignKey(x => x.AddressId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Customer).WithMany(x => x.Orders).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        RequireTraceable = true;
        UseForTracable = true;
        NeedConfirmation = true;
        base.Configure(builder);
    }
}
