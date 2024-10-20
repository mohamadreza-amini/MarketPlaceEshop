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

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.RoleDescription).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(500).IsRequired();
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreateDatetime).IsRequired();
        builder.Property(x => x.UpdateDatetime).IsRequired();

        builder.HasOne(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.UpdaterUser).WithMany().HasForeignKey(x => x.UpdaterUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    }
}
