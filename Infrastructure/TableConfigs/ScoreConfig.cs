using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class ScoreConfig:BaseConfig<Score,Guid>
{
    public override void Configure(EntityTypeBuilder<Score> builder)
    {
        builder.Property(x=>x.StarRating).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired().HasDefaultValue(0);

        builder.HasOne(x=>x.ProductSupplier).WithMany(x=>x.Scores).HasForeignKey(x=>x.ProductSupplierId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Customer).WithMany(x => x.Scores).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => new { x.CustomerId, x.ProductSupplierId }).IsUnique();

        // builder.HasCheckConstraint("CK_YourTable_YourByteColumn", "[StarRating] >= 0 AND [StarRating] <= 5");


        base.Configure(builder);

    }
}
