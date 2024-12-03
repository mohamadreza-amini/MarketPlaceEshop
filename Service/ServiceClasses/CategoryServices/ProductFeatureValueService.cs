using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Categories;
using Model.Exceptions;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        productFeatures.ForEach(x=>x.ProductId = ProductId);
        productFeatures.ForEach(x => x.Validate());

        return productFeatures;
        //کامیت نشده برای استفاده در پروداکت
    }
}
