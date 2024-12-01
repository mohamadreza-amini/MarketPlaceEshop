using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
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

public class BrandService : ServiceBase<Brand, BrandCommand, int>, IBrandService
{
    private readonly IBaseRepository<Brand, int> _brandRepository;
    private readonly IUserService _userService;

    public BrandService(IBaseRepository<Brand, int> brandRepository, IUserService userService)
    {
        _brandRepository = brandRepository;
        _userService = userService;
    }

    public async Task CreateAsync(BrandCommand brandDto, CancellationToken cancellation)
    {
        if (!_userService.IsAdmin())
            throw new AccessDeniedException();
        var brand = TranslateToEntity(brandDto);
        brand.Validate();
        await _brandRepository.CreateAsync(brand, cancellation);
    }

    public async Task<List<BrandResult>> GetAllAsync(CancellationToken cancellation)
    {
        var brands = await _brandRepository.GetAll().ToListAsync(cancellation);
        return Translate<List<Brand>, List<BrandResult>>(brands);
    }


}
