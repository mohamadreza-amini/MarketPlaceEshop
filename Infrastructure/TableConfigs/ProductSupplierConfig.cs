using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Suppliers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class ProductSupplierConfig:BaseConfig<ProductSupplier,Guid>
{
    public override void Configure(EntityTypeBuilder<ProductSupplier> builder)
    {
        builder.Property(x => x.Ventory).HasDefaultValue(0).IsRequired();
        builder.Property(x=>x.Discount).HasColumnType(SqlDbType.Decimal.ToString()).HasPrecision(12,2).HasDefaultValue(0).IsRequired();
        builder.Property(x=>x.IsDisable).HasDefaultValue(false).IsRequired();
        
        builder.HasOne(x=>x.Supplier).WithMany(x=>x.ProductSuppliers).HasForeignKey(x=>x.SupplierId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Product).WithMany(x => x.productSuppliers).HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);


        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);
    }
}
