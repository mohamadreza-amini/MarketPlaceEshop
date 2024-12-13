using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Infrastructure.Contracts.Cache;
using Infrastructure.Contracts.Repository;
using Mapster;
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
            category.ParentCategoryId = null;
        }
        else
        {
            var parent = (await _categoryRepository.GetAsync(x => x.Id == category.ParentCategoryId, cancellation)) ?? throw new BadRequestException();
            category.Level = parent.Level + 1;
        }
        category.validate();
        await _categoryRepository.CreateAsync(category, cancellation);
        await _categoryRepository.CommitAsync(cancellation);
        _cachedData.Remove("AllCategoriesDictionary");
        _cachedData.Remove("AllCategoriesList");
    }

    public async Task<Dictionary<int, List<CategoryResult>>> GetAllDictionryAsync(CancellationToken cancellation)
    {
        var categories = _cachedData.Get<Dictionary<int, List<CategoryResult>>>("AllCategoriesDictionary");
        if (categories == null)
        {
            var categoryresultlist = await _categoryRepository.GetAll().ProjectToType<CategoryResult>().ToListAsync(cancellation);

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
            var query = await _categoryRepository
                .GetAllDataAsync(
                x => new CategoryResult
                {
                    Id = x.Id,
                    CategoryName = x.CategoryName,
                    Level = x.Level,
                    ParentCategoryId = x.ParentCategoryId,
                    ParentCategoryName = x.ParentCategory == null ? "root" : x.ParentCategory.CategoryName
                }, cancellation);
            if (query != null)
            {
                categories = await query.ToListAsync(cancellation);
            }
            else {
                categories = new List<CategoryResult>(); 
            }
            _cachedData.Set("AllCategoriesList", categories);
        }
        return categories;
    }

    public async Task<List<CategoryResult>> GetAllListSortAsync(CancellationToken cancellation)
    {
        var categories = await GetAllListAsync(cancellation);
        return categories.OrderBy(x => x.Level).ThenBy(x => x.ParentCategoryId).ToList();
    }

    public async Task<CategoryResult?> GetCategoryAsync(int categoryId, CancellationToken cancellation)
    {
        var categories = await GetAllListAsync(cancellation);
        return categories.FirstOrDefault(x => x.Id == categoryId);
    }

    //کتگوری رو میگیره پدراشو میده
    public async Task<List<int>> GetAllParentCategoryIds(int categoryId, CancellationToken cancellation)
    {
        var categories = await GetAllListAsync(cancellation);

        var category = categories.FirstOrDefault(x => x.Id == categoryId);
        if (category == null)
            //چون کش میشه ممکنه بلافاصله نیاد
            throw new BadRequestException("دسته بندی یافت نشد");

        var subCategory = new List<int>();
        subCategory.Add(categoryId);
        ParentCategoryId(subCategory, category.ParentCategoryId, categories);
        return subCategory;
    }

    private void ParentCategoryId(List<int> parentsCategory, int? parentId, List<CategoryResult> categories)
    {
        if (parentId == null)
            return;

        var parent = categories.FirstOrDefault(x => x.Id == parentId.Value);
        parentsCategory.Add(parent!.Id);
        ParentCategoryId(parentsCategory, parent.ParentCategoryId, categories);
    }


    //کنگوری رو میگیره فرزنداشو میده
    public async Task<List<int>> GetAllSubCategoryIdbyCategoryId(int categoryId, CancellationToken cancellation)
    {
        var categories = await GetAllDictionryAsync(cancellation);
        var parentLevel = categories.FirstOrDefault(x => x.Value.Any(y => y.Id == categoryId)).Key;
        if (parentLevel == default && !categories[parentLevel].Any(y => y.Id == categoryId))
            return new List<int>();
        var childCategories = new List<int> { categoryId };

        for (var level = parentLevel + 1; categories.TryGetValue(level, out var currentLevelCategories); level++)
        {
            foreach (var category in currentLevelCategories)
            {
                if (childCategories.Contains(category.ParentCategoryId.GetValueOrDefault()))
                    childCategories.Add(category.Id);
            }
        }
        return childCategories;
    }

}
