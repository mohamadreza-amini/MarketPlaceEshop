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

public class CustomerConfig : BaseConfig<Customer, Guid>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.FirstName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.MobileNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.RegisterDate).IsRequired();
        builder.Property(x => x.DateOfRegistration).IsRequired();
        builder.Property(x => x.Password).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.NationalCode).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(20).IsRequired();


        builder.HasIndex(x=>x.MobileNumber).IsUnique();

        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);
    }
}
