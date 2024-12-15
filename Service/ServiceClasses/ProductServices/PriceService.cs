using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Infrastructure.Repository.Classes;
using Microsoft.EntityFrameworkCore;
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

public class PriceService : ServiceBase<Price, PriceResult, Guid>, IPriceService
{
    private readonly IBaseRepository<Price, Guid> _priceRepository;
    private readonly IUserService _userService;
    private readonly IProductSupplierRepository _productSupplierRepository;
    public PriceService(IUserService userService, IBaseRepository<Price, Guid> priceRepository, IProductSupplierRepository productSupplierRepository)
    {
        _userService = userService;
        _priceRepository = priceRepository;     
        _productSupplierRepository = productSupplierRepository;
    }

    public async Task<bool> AddPriceAsync(PriceCommand priceDto, CancellationToken cancellation)
    {
        var expireLastPrice = await ExpireLastPrice(priceDto.ProductSupplierId, cancellation);
        if (!expireLastPrice)
            throw new BadRequestException();
        var price = Translate<PriceCommand, Price>(priceDto);
        price.StartTime = DateTime.Now;
        price.CreatorUserId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        price.UpdaterUserId = price.CreatorUserId;
        await _priceRepository.CreateAsync(price, cancellation);
        return true;
        //کامیت نمیکنه در اضافه شدن تامین کننده به محصول به صورت یونیت اف ورک یکجا اضافه میشه
    }

    public async Task<bool> ExpireLastPrice(Guid productSupplierId, CancellationToken cancellation)
    {
        var productSupplier = await _productSupplierRepository.GetByIdAsync(productSupplierId, cancellation);
        if (productSupplier == null)
            return true;
        //در حالت بالا هنوز تامین کننده برای اون محصول ساخته نشده و قیمت قبلی نداره

        if (!_userService.IsAdmin() && !_userService.IsRequesterUser(productSupplier.SupplierId))
            throw new AccessDeniedException();

        var lastPrice = await _priceRepository.GetAsync(x => x.ProductSupplierId == productSupplierId && x.ExpiredTime == null, cancellation);
        if (lastPrice == null)
            return true;
        // در این حالت تامین کننده ساخته شده اما یا قیمت نداره یا قیمت باز نداره
        lastPrice.ExpiredTime = DateTime.Now;
        lastPrice.UpdaterUserId = Guid.Parse(_userService.RequesterId() ?? Guid.Empty.ToString());
        return await _priceRepository.CommitAsync(cancellation) > 0;
    }
}
