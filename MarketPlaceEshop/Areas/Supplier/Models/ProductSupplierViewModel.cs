using DataTransferObject.DTOClasses.Product.Commands;
using DataTransferObject.DTOClasses.Product.Results;
using Service.ServiceClasses;

namespace MarketPlaceEshop.Areas.Supplier.Models;

public class ProductSupplierViewModel
{
    public PaginatedList<ProductPanelResult> Products { get; set; }
    public ProductSupplierCommand ProductSupplier {  get; set; }
    public PaginatedList<ProductSupplierFullResult> ProductSuppliers {  get; set; }
    public string? SerachText { get; set; }
}
