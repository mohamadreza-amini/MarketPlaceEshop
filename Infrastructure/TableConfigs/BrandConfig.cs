using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class BrandConfig:BaseConfig<Brand,int>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(x=>x.BrandName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();


        base.GeneratedValueForKey=true;
        base.NeedConfirmation=true;
        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);
    }

}
