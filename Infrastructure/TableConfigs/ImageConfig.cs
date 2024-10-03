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

public class ImageConfig : BaseConfig<Image, Guid>
{
    public override void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(x => x.Path).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Priority).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
        
        builder.HasOne(x=>x.Product).WithMany(x=>x.Images).HasForeignKey(x=>x.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        base.RequireTraceable = true;
        base.UseForTracable = true;
        base.NeedConfirmation = true;
        base.Configure(builder);
    }
}
