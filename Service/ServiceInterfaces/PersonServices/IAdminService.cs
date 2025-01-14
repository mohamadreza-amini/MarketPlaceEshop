﻿using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Model.Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.PersonServices;

public interface IAdminService : IServiceBase<Admin, UserResult, Guid>
{
    Task<bool> CreateAsync(UserCommand userDTO, CancellationToken cancellationToken);
    Task<bool> SignInAsync(LoginCommand loginDto);

}
