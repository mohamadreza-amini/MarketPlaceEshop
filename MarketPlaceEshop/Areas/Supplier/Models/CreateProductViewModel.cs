using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;

namespace MarketPlaceEshop.Areas.Supplier.Models;

public class CreateProductViewModel
{
    public List<BrandResult> Brands { get; set; }
    public List<CategoryResult> Categories { get; set; }
}
