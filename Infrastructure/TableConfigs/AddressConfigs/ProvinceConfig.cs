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

public class ProvinceConfig : BaseConfig<Province, int>
{
    public override void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(x => x.ProvinceName).HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(50).IsRequired();

        base.Configure(builder);
    }
}
