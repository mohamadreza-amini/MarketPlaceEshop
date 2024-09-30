using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{

  /*  public class BaseRepository<T, KeyTypeId> : IEntityTypeConfiguration<T> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
    {
        protected bool GeneratedValueForKry { get; set; } = true;
        protected bool UseForTraceable { get; set; } = false;
        protected bool RequireTraceable { get; set; } = false;

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {


            builder.HasKey(x => x.Id);
            if (GeneratedValueForKry)
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            }

            if (UseForTraceable)
            {
                builder.Ignore(x => x.CreateDatetime);
                builder.Ignore(x => x.UpdateDatetime);
                builder.Ignore(x => x.AdminCreated);
                builder.Ignore(x => x.AdminCreatedId);
                builder.Ignore(x => x.AdminUpdated);
                builder.Ignore(x => x.AdminUpdatedId);
            

            }
            else
            {
                builder.Property(x => x.CreateDatetime).IsRequired(RequireTraceable);
                builder.Property(x => x.UpdateDatetime).IsRequired(RequireTraceable);
                builder.HasOne(x => x.AdminCreated).WithMany().HasForeignKey(x => x.AdminCreatedId).IsRequired(RequireTraceable).OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(x => x.AdminUpdated).WithMany().HasForeignKey(x => x.AdminUpdatedId).IsRequired(RequireTraceable).OnDelete(DeleteBehavior.NoAction);



            }

        }
    }*/
}
