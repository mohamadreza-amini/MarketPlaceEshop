using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IBrandService:IServiceBase<Brand,BrandCommand,int>
{
    Task CreateAsync(BrandCommand brandDto, CancellationToken cancellation);
    Task<List<BrandResult>> GetAllAsync(CancellationToken cancellation);
}
