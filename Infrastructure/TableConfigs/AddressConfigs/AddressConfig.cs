﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.AddressConfigs;

public class AddressConfig : BaseConfig<Address, Guid>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Neighborhood).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.AddressDetail).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200).IsRequired();
        builder.Property(x => x.HouseNumber).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.UnitNumber).HasColumnType(SqlDbType.Int.ToString()).IsRequired();
        builder.Property(x => x.PostalCode).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.City).WithMany(x => x.Addresses).HasForeignKey(x => x.CityId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
