using DataTransferObject.DTOClasses.Product.Commands;
using System.ComponentModel.DataAnnotations;

namespace MarketPlaceEshop.Areas.Supplier.Models;

public class ProductGenerateViewModel
{
    public ProductCommand ProductDto { get; set; }
    [Required(ErrorMessage ="بارگزاری عکس الزامی است")]
    public List<IFormFile> ProductImages { get; set; }=new List<IFormFile>();
}
