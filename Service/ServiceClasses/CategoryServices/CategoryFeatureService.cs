using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.CategoryServices;

public class CategoryFeatureService : ServiceBase<CategoryFeature, CategoryFeatureResult, int>, ICategoryFeatureService
{

    private readonly IBaseRepository<CategoryFeature, int> _categoryFeatureRepository;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    public CategoryFeatureService(IBaseRepository<CategoryFeature, int> categoryFeatureRepository, ICategoryService categoryService, IUserService userService)
    {
        _categoryFeatureRepository = categoryFeatureRepository;
        _categoryService = categoryService;
        _userService = userService;
    }

    public async Task Create(CategoryFeatureCommand categoryDto, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();

        var categoryFeature = Translate<CategoryFeatureCommand, CategoryFeature>(categoryDto);
        categoryFeature.validate();

        if (await (_categoryService.GetCategoryAsync(categoryFeature.CategoryId, cancellation)) == null)
            throw new BadRequestException();

        await _categoryFeatureRepository.CreateAsync(categoryFeature, cancellation);
        await _categoryFeatureRepository.CommitAsync(cancellation);
    }


    public async Task<List<CategoryFeatureResult>> GetAllByCategoryId(int categoryId, CancellationToken cancellation)
    {
        var subCategories = await _categoryService.GetAllParentCategoryIds(categoryId, cancellation);
        var CategoryFeatureResultList = new List<CategoryFeatureResult>();

        foreach (var category in subCategories)
        {
            CategoryFeatureResultList.AddRange(await _categoryFeatureRepository
                .GetAll(x => x.CategoryId == category).ProjectToType<CategoryFeatureResult>().ToListAsync(cancellation));
        }

        return CategoryFeatureResultList;
    }

}
