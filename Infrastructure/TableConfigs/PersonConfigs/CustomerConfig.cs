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

public class CustomerConfig : BaseConfig<Customer, Guid>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.Property(x => x.RegisterDate).IsRequired();

        builder.HasOne(x => x.User).WithOne().HasForeignKey<Customer>(x => x.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);

        RequireTraceable = true;
        UseForTracable = true;
        base.Configure(builder);
    }
}
