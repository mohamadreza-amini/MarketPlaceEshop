using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.CategoryServices;

public interface ICategoryFeatureService : IServiceBase<CategoryFeature, CategoryFeatureResult, int>
{
    Task Create(CategoryFeatureCommand categoryDto, CancellationToken cancellation);
    Task<List<CategoryFeatureResult>> GetAllByCategoryId(int categoryId,CancellationToken cancellation);
    Task<List<ProductFeatureValueCommand>> GetFeatureCommand(int categoryId, CancellationToken cancellation);
}
