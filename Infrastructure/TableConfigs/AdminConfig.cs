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

public class AdminConfig : BaseConfig<Admin, Guid>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {

        builder.Property(x => x.FirstName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.Property(x => x.LastName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Password).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MobileNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();

        builder.HasIndex(x => x.MobileNumber).IsUnique();

        base.UseForTracable = true;
        base.RequireTraceable = true;
       // base.NeedConfirmation = true;
        
        base.Configure(builder);
    }
}
