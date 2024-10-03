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

public class UserConfig:BaseConfig<User,Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {

        builder.Property(x => x.FirstName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.MobileNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Password).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.NationalCode).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(10).IsRequired();

        builder.HasIndex(x => x.MobileNumber).IsUnique();

        //باشه یا نه یک طرفه باشه
      /*  builder.HasOne(x => x.Supplier).WithOne(x => x.User).HasForeignKey<User>(x => x.SupplierId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        builder.HasOne(x => x.Customer).WithOne(x => x.User).HasForeignKey<User>(x => x.CustomerId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        builder.HasOne(x => x.Admin).WithOne(x => x.User).HasForeignKey<User>(x => x.AdminId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);*/


        base.Configure(builder);
    }
}
