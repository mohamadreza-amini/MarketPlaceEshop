using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Infrastructure.Contracts.Repository;
using Mapster;
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

public class BrandService : ServiceBase<Brand, BrandResult, int>, IBrandService
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
        var brand = Translate<BrandCommand, Brand>(brandDto);
        brand.Validate();
        await _brandRepository.CreateAsync(brand, cancellation);
    }

    public async Task<List<BrandResult>> GetAllAsync(CancellationToken cancellation)
    {
        return await _brandRepository.GetAll().ProjectToType<BrandResult>().ToListAsync(cancellation);
    }

    public async Task<BrandResult?> GetByIdAsync(int brandId, CancellationToken cancellation)
    {
        var brand = await _brandRepository.GetByIdAsync(brandId, cancellation);
        if (brand == null)
            return null;
        return TranslateToDTO(brand);
    }
}
