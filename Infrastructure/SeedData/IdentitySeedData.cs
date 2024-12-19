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
            var userId= Guid.NewGuid();
            await userManger.CreateAsync(new User
            {
                Email = "mohamadreza@gmail.com",
                FirstName = "محمدرضا",
                LastName = "امینی",
                MobileNumber = "09111111111",
                PhoneNumber = "88888888",
                UserName = "mohamadreza@gmail.com",
                Id = userId,
                CreatorUserId = userId,
                UpdaterUserId = userId,
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
        }

        if(! await userManger.IsInRoleAsync(user, "Admin"))
        {
            await userManger.AddToRoleAsync(user, "Admin");
        }

        if (!await roleManager.RoleExistsAsync("Supplier"))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = "Supplier",
                RoleDescription = "Seller of products",
                CreatorUserId = user.Id,
                UpdaterUserId = user.Id,
            });
        }

        if(!await roleManager.RoleExistsAsync("Customer"))
        {
            await roleManager.CreateAsync(new Role
            {
                Name = "Customer",
                RoleDescription = "Customer products",
                CreatorUserId = user.Id,
                UpdaterUserId = user.Id
            });
        }

        var admin = new Admin
        {
            Id = user.Id,
            CreatorUserId = user.Id,
            UpdaterUserId = user.Id
        };

        if (!await adminRepository.IsExistAsync(admin,CancellationToken.None))
        {
            await adminRepository.CreateAsync(admin, CancellationToken.None);
            await adminRepository.CommitAsync(CancellationToken.None);
        }
    }
}
