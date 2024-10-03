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

public class SupplierConfig:BaseConfig<Supplier,Guid>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {
 
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.BankAccountNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CompanyName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CompanyRegistrationNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();

       builder.HasOne(x=>x.User).WithOne().HasForeignKey<Supplier>(x=>x.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();

        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.NeedConfirmation=true;
        base.Configure(builder);
    }
}
