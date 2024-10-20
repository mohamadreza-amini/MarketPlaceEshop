using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.PersonConfigs;

public class SupplierConfig : BaseConfig<Supplier, Guid>
{
    public override void Configure(EntityTypeBuilder<Supplier> builder)
    {

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.BankAccountNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CompanyName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.CompanyRegistrationNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.User).WithOne().HasForeignKey<Supplier>(x => x.Id).OnDelete(DeleteBehavior.NoAction).IsRequired();

        RequireTraceable = true;
        UseForTracable = true;
        NeedConfirmation = true;
        base.Configure(builder);
    }
}
