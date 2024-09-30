using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class AdminConfig:BaseConfig<Admin,Guid>
{
    public override void Configure(EntityTypeBuilder<Admin> builder)
    {

        builder.Property(x => x.FirstName).IsRequired();

        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.MobileNumber).IsRequired();


        base.Configure(builder);
    }
}
