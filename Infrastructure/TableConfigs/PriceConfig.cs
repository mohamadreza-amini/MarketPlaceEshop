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

public class PriceConfig:BaseConfig<Price,Guid>
{
    public override void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.Property(x=>x.PriceValue).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12,2).IsRequired();
        builder.Property(x=>x.StartTime).IsRequired();
        builder.Property(x=>x.ExpiredTime).IsRequired(false);

        builder.HasOne(x=>x.productSupplier).WithMany(x=>x.Prices).HasForeignKey(x=>x.productSupplier).IsRequired().OnDelete(DeleteBehavior.NoAction);

        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.NeedConfirmation=true;
        base.Configure(builder);
    }
}
