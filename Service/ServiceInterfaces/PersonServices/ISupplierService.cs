using DataTransferObject.DTOClasses.Person.Commands;
using DataTransferObject.DTOClasses.Person.Results;
using Model.Entities.Person;
using Service.ServiceClasses;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.PersonServices;

public interface ISupplierService : IServiceBase<Supplier, UserResult, Guid>
{
    Task<bool> CreateAsync(SupplierCommand supplierDTO, CancellationToken cancellation);
    Task<PaginatedList<SupplierResult>> GetAllSuppliersbyStatusAsync(ConfirmationStatus status, CancellationToken cancellation, int pageIndex = 1, int pageSize = 20);
    Task<bool> ChangeSupplierStatusAsync(Guid supplierId, ConfirmationStatus confirmation, CancellationToken cancellation);
    Task<bool> SignInAsync(LoginCommand loginDto);
    Task<int> NumberOfSupplier(ConfirmationStatus confirmation, CancellationToken cancellation);
    Task<SupplierResult> GetSupplier(CancellationToken cancellation);
}
