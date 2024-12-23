using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Infrastructure.Contracts.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities.Categories;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;

namespace Service.ServiceClasses.CategoryServices;

public class ProductFeatureValueService : ServiceBase<ProductFeatureValue, ProductFeatureValueResult, int>, IProductFeatureValueService
{
    private readonly IBaseRepository<ProductFeatureValue, int> _productFeatureRepository;
    private readonly IUserService _userService;

    public ProductFeatureValueService(IBaseRepository<ProductFeatureValue, int> productFeatureRepository, IUserService userService)
    {
        _productFeatureRepository = productFeatureRepository;
        _userService = userService;
    }

    public List<ProductFeatureValue> PrepareToAdd(List<ProductFeatureValueCommand> productFeaturesDto, Guid ProductId)
    {
        if (!_userService.IsAdmin() && !_userService.IsInRole("Supplier"))
            throw new AccessDeniedException();

        var productFeatures = Translate<List<ProductFeatureValueCommand>, List<ProductFeatureValue>>(productFeaturesDto);
        productFeatures.ForEach(x => x.ProductId = ProductId);
        productFeatures.ForEach(x => x.Validate());

        return productFeatures;
        //کامیت نشده برای استفاده در پروداکت
    }

    public async Task<List<ProductFeatureValueResult>> GetAllByProductId(Guid ProductId, CancellationToken cancellation)
    {
        var query =await _productFeatureRepository.GetAllDataAsync(
            x => new ProductFeatureValueResult
            {
                Id = x.Id,
                FeatureValue = x.FeatureValue,
                FeatureName = x.CategoryFeature.FeatureName
            },
            cancellation,
            x => x.ProductId == ProductId,
            x => x.Include(x => x.CategoryFeature));

        if (query == null)
            return new List<ProductFeatureValueResult>();
        return await query.ToListAsync(cancellation);
    }
}
