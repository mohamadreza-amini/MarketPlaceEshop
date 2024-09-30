using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class AddressConfig : BaseConfig<Address, Guid>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Province).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.Neighborhood).IsRequired();
        builder.Property(x => x.AddressDetail).IsRequired();
        builder.Property(x => x.HouseNumber).IsRequired();
        builder.Property(x => x.UnitNumber).IsRequired();
        builder.Property(x => x.PostalCode).IsRequired();

        builder.HasOne(x => x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerId).IsRequired();

        base.Configure(builder);
    }
}
