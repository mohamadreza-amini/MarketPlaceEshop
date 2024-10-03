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
        builder.HasOne(x=>x.User).WithOne().HasForeignKey<Admin>(x=>x.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        
        base.UseForTracable = true;
        base.RequireTraceable = true;
      
        base.Configure(builder);
    }
}
