using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Person;
using Model.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;

public class ViewLogConfig:BaseConfig<ViewLog,int>
{
    public override void Configure(EntityTypeBuilder<ViewLog> builder)
    {

        builder.Property(x=>x.IP).HasColumnType(SqlDbType.VarChar.ToString()).IsRequired();

        builder.Property(x => x.Url).HasColumnType(SqlDbType.VarChar.ToString()).IsRequired();

        builder.Property(x => x.DateTime).IsRequired();

        builder.HasOne(x=>x.User).WithMany().HasForeignKey(x=>x.userId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

        base.Configure(builder);
    }


}
