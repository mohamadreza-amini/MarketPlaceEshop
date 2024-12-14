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

public class ImageConfig : BaseConfig<Image, Guid>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Path).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Priority).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
