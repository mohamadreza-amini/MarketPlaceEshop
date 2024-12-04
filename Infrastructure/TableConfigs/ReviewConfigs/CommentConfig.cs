using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Review;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.ReviewConfigs;

public class CommentConfig : BaseConfig<Comment, Guid>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(x => x.CommentText).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.DateOfRegistration).IsRequired();
        builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Customer).WithMany(x => x.Comments).HasForeignKey(x => x.CustomerId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        NeedConfirmation = true;
        base.Configure(builder);
    }
}
