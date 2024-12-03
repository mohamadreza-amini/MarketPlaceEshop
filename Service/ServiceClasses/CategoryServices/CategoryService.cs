using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Categories;
using Model.Exceptions;
using Service.ServiceInterfaces;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.ServiceClasses.CategoryServices;

public class CategoryService : ServiceBase<Category, CategoryResult, int>, ICategoryService
{
    private readonly IBaseRepository<Category, int> _categoryRepository;
    private readonly IUserService _userService;
    private ICachedData _cachedData;
    public CategoryService(IBaseRepository<Category, int> categoryRepository, IUserService userService, ICachedData cachedData)
    {
        _categoryRepository = categoryRepository;
        _userService = userService;
        _cachedData = cachedData;
    }

    public async Task CreateAsync(CategoryCommand CategoryDto, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var category = Translate<CategoryCommand, Category>(CategoryDto);

        if (category.ParentCategoryId == 0)
        {
            category.Level = 1;
        }
        else
        {
            var parent = (await _categoryRepository.GetAsync(x => x.Id == category.ParentCategoryId, cancellation)) ?? throw new BadRequestException();
            category.Level = parent.Level + 1;
        }
        category.validate();
        await _categoryRepository.CreateAsync(category, cancellation);
        await _categoryRepository.CommitAsync(cancellation);
    }

    public async Task<Dictionary<int, List<CategoryResult>>> GetAllDictionryAsync(CancellationToken cancellation)
    {
        var categories = _cachedData.Get<Dictionary<int, List<CategoryResult>>>("AllCategoriesDictionary");
        if (categories == null)
        {
            var categorylist = await _categoryRepository.GetAll().ToListAsync(cancellation);
            var categoryresultlist = Translate<List<Category>, List<CategoryResult>>(categorylist);

            categories = new Dictionary<int, List<CategoryResult>>();

            var list = new List<CategoryResult>();
            foreach (var categoryResult in categoryresultlist)
            {
                if (categories[categoryResult.Level] == null)
                    categories[categoryResult.Level] = new List<CategoryResult>();

                categories[categoryResult.Level].Add(categoryResult);
            }
            _cachedData.Set("AllCategoriesDictionary", categories);
        }
        return categories;
    }


    public async Task<List<CategoryResult>> GetAllListAsync(CancellationToken cancellation)
    {
        var categories = _cachedData.Get<List<CategoryResult>>("AllCategoriesList");
        if (categories == null)
        {
            var categorylist = await _categoryRepository.GetAll().ToListAsync(cancellation);
            categories = Translate<List<Category>, List<CategoryResult>>(categorylist);
            _cachedData.Set("AllCategoriesList", categories);
        }
        return categories;

    }

    public async Task<CategoryResult?> GetCategoryAsync(int categoryId, CancellationToken cancellation)
    {
        var categories = await GetAllListAsync(cancellation);
        return categories.FirstOrDefault(x => x.Id == categoryId);
    }
}
