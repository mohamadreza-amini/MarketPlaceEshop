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

public class AddressConfig : BaseConfig<Address, Guid>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Province).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.City).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Neighborhood).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.AddressDetail).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200).IsRequired();
        builder.Property(x => x.HouseNumber).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.UnitNumber).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.PostalCode).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerId).IsRequired();

        base.UseForTracable = true;
        base.RequireTraceable = true;

        base.Configure(builder);
    }
}
