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

public class BrandConfig : BaseConfig<Brand, int>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(x => x.BrandName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();


        GeneratedValueForKey = true;
        NeedConfirmation = true;
        RequireTraceable = true;
        UseForTracable = true;
        base.Configure(builder);
    }

}
