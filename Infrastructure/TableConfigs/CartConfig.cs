using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class CartConfig:BaseConfig<Cart,Guid>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.Property(x => x.StartFrom).IsRequired();
        builder.Property(X=>X.ExpiredFrom).IsRequired(false);
        builder.Property(x=>x.StatusCart).IsRequired();

        builder.HasOne(x=>x.Customer).WithMany(x=>x.Carts).HasForeignKey(x=>x.CustomerId).IsRequired().OnDelete(DeleteBehavior.NoAction);

        
        base.Configure(builder);
    }
}
