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

public class ProductFeatureValueConfig : BaseConfig<ProductFeatureValue, int>
{
   

    public override void Configure(EntityTypeBuilder<ProductFeatureValue> builder)
    {

        builder.Property(x=>x.FeatureValue).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(500).IsRequired();

        builder.HasOne(x=>x.CategoryFeature).WithMany(x=>x.ProductFeatureValues).HasForeignKey(x=>x.CategoryFeatureId).OnDelete(DeleteBehavior.NoAction).IsRequired();
        builder.HasOne(x=>x.Product).WithMany(x=>x.ProductFeatureValues).HasForeignKey(x=>x.ProductId).OnDelete(DeleteBehavior.NoAction).IsRequired();

        builder.HasIndex(x => new {x.CategoryFeatureId,x.ProductId}).IsUnique();

        base.GeneratedValueForKey = true;
        base.RequireTraceable = true;
        base.UseForTracable = true;
        base.NeedConfirmation = true;
        base.Configure(builder);
    }
}
