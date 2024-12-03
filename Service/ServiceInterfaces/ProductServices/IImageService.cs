using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Model.Entities.Categories;
using Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces.ProductServices;

public interface IImageService:IServiceBase<Image,ImageResult,Guid>
{
    List<Image> PrepareToAdd(List<ImageCommand> imagesDto,Guid productId);

}
