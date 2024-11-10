using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.CategoryConfigs;

public class ProductFeatureValueConfig : BaseConfig<ProductFeatureValue, int>
{


    public override void Configure(EntityTypeBuilder<ProductFeatureValue> builder)
    {

        builder.Property(x => x.FeatureValue).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(500).IsRequired();

        builder.HasOne(x => x.CategoryFeature).WithMany(x => x.ProductFeatureValues).HasForeignKey(x => x.CategoryFeatureId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(x => x.Product).WithMany(x => x.ProductFeatureValues).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade).IsRequired();

        builder.HasIndex(x => new { x.CategoryFeatureId, x.ProductId }).IsUnique();

        base.Configure(builder);
    }
}
