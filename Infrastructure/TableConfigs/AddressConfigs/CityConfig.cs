using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TableConfigs.AddressConfigs;

public class CityConfig : BaseConfig<City, int>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(x => x.CityName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();

        builder.HasOne(x => x.Province).WithMany(x => x.cities).HasForeignKey(x => x.ProvinceId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}
