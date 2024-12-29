using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Model.Entities.Addresses;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData;

public class UserSeedData : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        var user = new User
        {
            Email = "mohamadreza@gmail.com",
            FirstName = "محمدرضا",
            LastName = "امینی",
            MobileNumber = "09111111111",
            PhoneNumber = "88888888",
            UserName = "mohamadreza@gmail.com",
            Id = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreatorUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            UpdaterUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            DateOfBirth = new DateTime(1999, 1, 1),
            NationalCode = "1234567890",
            NormalizedUserName = "mohamadreza@gmail.com".ToUpper(),
            NormalizedEmail = "mohamadreza@gmail.com".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = "UYUOUWWKOLK3SIPRMHLRRAOSETBXTJVH",
            CreateDatetime = DateTime.Now,
            UpdateDatetime = DateTime.Now,
        };

        user.PasswordHash = hasher.HashPassword(user, "123456");

        builder.HasData(user);

    }
}



public class RoleSeedData : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role
        {
            Id = Guid.Parse("6FE25E76-E02F-42D2-AC41-08DD25DE10F8"),
            Name = "Customer",
            RoleDescription = "Customer products",
            CreatorUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            UpdaterUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreateDatetime = DateTime.Now,
            UpdateDatetime = DateTime.Now,
            NormalizedName = "Customer".ToUpper()
        }, new Role
        {
            Id = Guid.Parse("F9E1F0B3-A9C4-4EEB-AC40-08DD25DE10F8"),
            Name = "Supplier",
            RoleDescription = "Seller of products",
            CreatorUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            UpdaterUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreateDatetime = DateTime.Now,
            UpdateDatetime = DateTime.Now,
            NormalizedName= "Supplier".ToUpper()
        }, new Role
        {
            Id = Guid.Parse("FC961CE4-16C1-4566-AC3F-08DD25DE10F8"),
            Name = "Admin",
            RoleDescription = "Site Manager",
            CreatorUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            UpdaterUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreateDatetime = DateTime.Now,
            UpdateDatetime = DateTime.Now,
            NormalizedName = "Admin".ToUpper()
        });
    }
}



public class UserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(new IdentityUserRole<Guid>
        {
            UserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            RoleId = Guid.Parse("FC961CE4-16C1-4566-AC3F-08DD25DE10F8")
        });
    }
}


public class AdminSeedData : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasData(new Admin
        {
            Id = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreatorUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            UpdaterUserId = Guid.Parse("D80D9352-0889-4B7A-AC71-2AE385EC05CA"),
            CreateDatetime = DateTime.Now,
            UpdateDatetime = DateTime.Now,
        });
    }
}