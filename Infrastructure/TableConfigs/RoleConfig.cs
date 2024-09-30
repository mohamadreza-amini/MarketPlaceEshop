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

public class RoleConfig:BaseConfig<Role,Guid>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x=>x.RoleName).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.RoleDescription).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(500).IsRequired();

        base.RequireTraceable=true;
        base.UseForTracable=true;
        base.Configure(builder);
    }
}
