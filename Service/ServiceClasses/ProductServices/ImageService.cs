using DataTransferObject.DTOClasses.Category.Commands;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Model.Entities.Categories;
using Model.Entities.Products;
using Model.Exceptions;
using Service.ServiceInterfaces.PersonServices;
using Service.ServiceInterfaces.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses.ProductServices;

public class ImageService : ServiceBase<Image, ImageResult, Guid>, IImageService
{
    private readonly IBaseRepository<Image,Guid> _imageRepository;
    private readonly IUserService _userService;

    public ImageService(IUserService userService, IBaseRepository<Image, Guid> imageRepository)
    {
        _userService = userService;
        _imageRepository = imageRepository;
    }

    public List<Image> PrepareToAdd(List<ImageCommand> imagesDto, Guid productId)
    {
        if (!_userService.IsAdmin() && !_userService.IsInRole("Supplier"))
            throw new AccessDeniedException();

        var images = Translate<List<ImageCommand>, List<Image>>(imagesDto);
        images.ForEach(x => x.ProductId = productId);
        images.ForEach(x => x.Validate());

        return images;
        //کامیت نشده برای استفاده در پروداکت
    }
}
