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

public class SupplierConfig:BaseConfig<Supplier,Guid>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.Property(x => x.Name).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Password).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.BankAccountNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ChairmanName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CompanyRegistrationNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.HasIndex(x=>x.PhoneNumber).IsUnique();

        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.NeedConfirmation=true;
        base.Configure(builder);
    }
}
