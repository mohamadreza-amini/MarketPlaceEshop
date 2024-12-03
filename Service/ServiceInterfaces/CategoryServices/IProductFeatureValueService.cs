using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Category.Results;
using Model.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.CategoryServices;

public interface IProductFeatureValueService:IServiceBase<ProductFeatureValue,ProductFeatureValueResult,int>
{
    List<ProductFeatureValue> PrepareToAdd(List<ProductFeatureValueCommand> productFeaturesDto,Guid ProductId);
}
