using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs;


public class BaseConfig<T, KeyTypeId> : IEntityTypeConfiguration<T> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
{
    protected bool GeneratedValueForKey { get; set; } = false;
    protected bool UseForTracable { get; set; } = false;
    
    protected bool RequireTraceable { get; set; } = false;
    protected bool NeedConfirmation { get; set; } = false;

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {

        builder.HasKey(x => x.Id);

        if (GeneratedValueForKey)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }

        if (!UseForTracable)
        {
            builder.Ignore(x => x.CreatorId);
            builder.Ignore(x => x.UpdaterId);
            builder.Ignore(x => x.CreateDatetime);
            builder.Ignore(x => x.UpdateDatetime);
        }
        else
        {
            builder.Property(x => x.CreatorId).IsRequired(RequireTraceable);
            builder.Property(x => x.CreateDatetime).IsRequired(RequireTraceable);
            builder.Property(x => x.UpdaterId).IsRequired(RequireTraceable);
            builder.Property(x => x.UpdateDatetime).IsRequired(RequireTraceable);
        }


        if (!NeedConfirmation)
        {
            builder.Ignore(x => x.IsConfirmed);
            builder.Ignore(x => x.ConfirmedDate);
            builder.Ignore(x => x.AdminConfirmed);
            builder.Ignore(x => x.AdminConfirmedId);

        }
        else
        {

            builder.Property(x => x.IsConfirmed).HasDefaultValue(ConfirmationStatus.Unchecked).IsRequired();

            builder.HasOne(x => x.AdminConfirmed).WithMany().HasForeignKey(x => x.AdminConfirmedId).OnDelete(DeleteBehavior.NoAction);
        }

    }
}
