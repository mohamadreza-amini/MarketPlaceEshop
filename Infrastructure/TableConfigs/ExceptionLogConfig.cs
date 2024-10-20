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

public class ExceptionLogConfig:BaseConfig<ExceptionLog,int>
{
    public override void Configure(EntityTypeBuilder<ExceptionLog> builder)
    {
        builder.Property(x => x.ExceptionType).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.Message).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.InnerException).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.Property(x => x.StackTrace).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.Property(x => x.DateTime).IsRequired();

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

        base.GeneratedValueForKey= true;
        base.Configure(builder);
    }
}
