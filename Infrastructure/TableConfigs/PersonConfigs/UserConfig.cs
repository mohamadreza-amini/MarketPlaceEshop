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

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FirstName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.MobileNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.PhoneNumber).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(30).IsRequired();
        builder.Property(x => x.Email).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(100).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.NationalCode).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(10).IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.Property(x => x.CreateDatetime).IsRequired();
        builder.Property(x => x.UpdateDatetime).IsRequired();

        builder.HasIndex(x => x.MobileNumber).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.UserName).IsUnique();

        builder.HasOne(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.UpdaterUser).WithMany().HasForeignKey(x => x.UpdaterUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);

    }

}
