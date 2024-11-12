using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData;

public class ProvinceSeedData : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasData(
                new Province { Id = 1, ProvinceName = "آذربایجان شرقی" },
                new Province { Id = 2, ProvinceName = "آذربایجان غربی" },
                new Province { Id = 3, ProvinceName = "اردبیل" },
                new Province { Id = 4, ProvinceName = "اصفهان" },
                new Province { Id = 5, ProvinceName = "البرز" },
                new Province { Id = 6, ProvinceName = "ایلام" },
                new Province { Id = 7, ProvinceName = "بوشهر" },
                new Province { Id = 8, ProvinceName = "تهران" },
                new Province { Id = 9, ProvinceName = "چهارمحال بختیاری" },
                new Province { Id = 10, ProvinceName = "خراسان جنوبی" },
                new Province { Id = 11, ProvinceName = "خراسان رضوی" },
                new Province { Id = 12, ProvinceName = "خراسان شمالی" },
                new Province { Id = 13, ProvinceName = "خوزستان" },
                new Province { Id = 14, ProvinceName = "زنجان" },
                new Province { Id = 15, ProvinceName = "سمنان" },
                new Province { Id = 16, ProvinceName = "سیستان و بلوچستان" },
                new Province { Id = 17, ProvinceName = "فارس" },
                new Province { Id = 18, ProvinceName = "قزوین" },
                new Province { Id = 19, ProvinceName = "قم" },
                new Province { Id = 20, ProvinceName = "کردستان" },
                new Province { Id = 21, ProvinceName = "کرمان" },
                new Province { Id = 22, ProvinceName = "کرمانشاه" },
                new Province { Id = 23, ProvinceName = "کهکیلویه و بویراحمد" },
                new Province { Id = 24, ProvinceName = "گلستان" },
                new Province { Id = 25, ProvinceName = "گیلان" },
                new Province { Id = 26, ProvinceName = "لرستان" },
                new Province { Id = 27, ProvinceName = "مازندران" },
                new Province { Id = 28, ProvinceName = "مرکزی" },
                new Province { Id = 29, ProvinceName = "هرمزگان" },
                new Province { Id = 30, ProvinceName = "همدان" },
                new Province { Id = 31, ProvinceName = "یزد" }
         );
    }
}
