using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.ReviewConfigs;

public class ScoreConfig : BaseConfig<Score, Guid>
{
    public override void Configure(EntityTypeBuilder<Score> builder)
    {
        builder.Property(x => x.StarRating).HasColumnType(SqlDbType.Int.ToString()).IsRequired().HasDefaultValue(0);

        builder.HasOne(x => x.Product).WithMany(x => x.Scores).HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer).WithMany(x => x.Scores).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.HasIndex(x => new { x.CustomerId, x.ProductId }).IsUnique();

        base.Configure(builder);
    }
}
