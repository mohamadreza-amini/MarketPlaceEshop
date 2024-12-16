using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Service.ServiceClasses;

namespace MarketPlaceEshop.Models;

public class ProductsFilterViewModel
{
    public ProductFilterCommand Filter { get; set; }
    public PaginatedList<ProductminiResult> Products { get; set; }
}
