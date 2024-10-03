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

public class ProductConfig:BaseConfig<Product,Guid>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x=>x.Titel).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Name).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Description).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x=>x.View).IsRequired().HasDefaultValue(0);
        builder.Property(x=>x.IsDisable).IsRequired().HasDefaultValue(false);

        builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Brand).WithMany(x => x.products).HasForeignKey(x => x.BrandId).IsRequired().OnDelete(DeleteBehavior.NoAction);



        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.NeedConfirmation=true;
        base.Configure(builder);
    }
}
