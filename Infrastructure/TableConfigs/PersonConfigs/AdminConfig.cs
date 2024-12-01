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

public class AdminConfig : BaseConfig<Admin, Guid>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasOne(x => x.User).WithOne().HasForeignKey<Admin>(x => x.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        UseForTracable = true;
        RequireTraceable = true;
        base.Configure(builder);
    }
}
