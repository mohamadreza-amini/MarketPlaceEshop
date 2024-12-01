using DataTransferObject.DTOClasses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Model.Entities.Person;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.PersonServices
{
    /* public class RoleService :ServiceBase<Role,RoleDTO,Guid>//,IRoleService
     {

         private readonly RoleManager<Role> roleManager;

         public RoleService(RoleManager<Role> roleManager)
         {
             this.roleManager = roleManager;
         }

         public async Task<RoleDTO> Create(RoleDTO userDTO)
         {
             var role = TranslateToEntity(userDTO);
             await roleManager.CreateAsync(role);
             return  userDTO;
         }

         public async Task<RoleDTO> GetRole(Guid id)
         {
             var data=  await roleManager.FindByIdAsync(id.ToString());
             return TranslateToDTO(data);
         }

         public RoleDTO TranslateToDTO(Role entity)
         {
             return entity.Adapt<RoleDTO>();
         }

         public Role TranslateToEntity(RoleDTO model)
         {
             return model.Adapt<Role>();
         }
     }*/
}
