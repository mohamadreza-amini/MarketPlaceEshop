using Infrastructure.Contracts.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData;

public class IdentitySeedData
{
    public async static Task AddIdentityData(IServiceProvider serviceProvider)
    {
        var userManger = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        var adminRepository = serviceProvider.GetRequiredService<IBaseRepository<Admin, Guid>>();

        if ((await userManger.FindByEmailAsync("mohamadreza@gmail.com")) == null)
        {
            await userManger.CreateAsync(new User
            {
                Email = "mohamadreza@gmail.com",
                FirstName = "محمدرضا",
                LastName = "امینی",
                MobileNumber = "09111111111",
                PhoneNumber = "88888888",
                UserName = "mohamadreza@gmail.com",
                Id = Guid.Parse("CCBAC04B-F656-4671-851A-88512924B9CC"),
                CreatorUserId = Guid.Parse("CCBAC04B-F656-4671-851A-88512924B9CC"),
                UpdaterUserId = Guid.Parse("CCBAC04B-F656-4671-851A-88512924B9CC"),
                DateOfBirth = new DateTime(1999, 1, 1),
                NationalCode = "1234567890"
            }
            , "Aa@123456");
        }
        
        var user = await userManger.FindByEmailAsync("mohamadreza@gmail.com");

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = "Admin",
                RoleDescription = "Site Manager",
                CreatorUserId = user.Id,
                UpdaterUserId = user.Id,
            });
            await roleManager.CreateAsync(new Role
            {
                Name = "Supplier",
                RoleDescription = "Seller of products",
                CreatorUserId = user.Id,
                UpdaterUserId = user.Id,
            });
            await roleManager.CreateAsync(new Role
            {
                Name = "Customer",
                RoleDescription = "Customer products",
                CreatorUserId = user.Id,
                UpdaterUserId = user.Id
            });

            await userManger.AddToRoleAsync(user, "Admin");
        }

        var admin = new Admin
        {
            Id = user.Id,
            CreatorUserId = user.Id,
            UpdaterUserId = user.Id
        };

        if (!await adminRepository.IsExist(admin,CancellationToken.None))
        {
            await adminRepository.CreateAsync(admin, CancellationToken.None);
            await adminRepository.CommitAsync(CancellationToken.None);
        }
    }
}
