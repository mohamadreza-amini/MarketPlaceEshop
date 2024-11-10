using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.ProductConfigs;

public class PriceConfig : BaseConfig<Price, Guid>
{
    public override void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.Property(x => x.PriceValue).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12, 2).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.ExpiredTime).IsRequired(false);

        builder.HasOne(x => x.ProductSupplier).WithMany(x => x.Prices).HasForeignKey(x => x.ProductSupplierId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        RequireTraceable = true;
        UseForTracable = true;
        base.Configure(builder);
    }
}
