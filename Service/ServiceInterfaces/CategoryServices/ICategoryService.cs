using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.CategoryServices;

public interface ICategoryService : IServiceBase<Category, CategoryResult, int>
{
    Task CreateAsync(CategoryCommand CategoryDto, CancellationToken cancellation);
    Task<Dictionary<int, List<CategoryResult>>> GetAllDictionryAsync(CancellationToken cancellation);
    Task<List<CategoryResult>> GetAllListAsync(CancellationToken cancellation);
    Task<CategoryResult?> GetCategoryAsync(int categoryId, CancellationToken cancellation);

}
