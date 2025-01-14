﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.CategoryConfigs;

public class CategoryFeatureConfig : BaseConfig<CategoryFeature, int>
{
    public override void Configure(EntityTypeBuilder<CategoryFeature> builder)
    {
        builder.Property(x => x.FeatureName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Priority).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.Filterable).IsRequired().HasDefaultValue(false);

        builder.HasOne(x => x.Category).WithMany(x => x.CategoryFeatures).HasForeignKey(x => x.CategoryId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);

    }
}
