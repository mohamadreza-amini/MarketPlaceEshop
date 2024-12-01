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

public interface ISupplierService : IServiceBase<Supplier, UserCommand, Guid>
{
    Task<bool> CreateAsync(SupplierCommand supplierDTO, CancellationToken cancellation);
    Task<PaginatedList<SupplierResult>> GetAllSuppliersbyStatusAsync(ConfirmationStatus status, int pageIndex, int pageSize,CancellationToken cancellation);
    Task<bool> ChangeSupplierStatusAsync(Guid supplierId,ConfirmationStatus confirmation,CancellationToken cancellation);
}
