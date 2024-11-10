using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.CategoryConfigs;

public class CategoryConfig : BaseConfig<Category, int>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.CategoryName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Level).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
        //برای ریشه nullable
        builder.HasOne(x => x.ParentCategory).WithMany(x => x.ChildCategories).HasForeignKey(x => x.ParentCategoryId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
       
        base.Configure(builder);
    }

}
