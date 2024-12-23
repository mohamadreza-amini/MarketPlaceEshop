using DataTransferObject.DTOClasses.Category.Results;

namespace MarketPlaceEshop.Areas.Admin.Models;

public class CategoryFeatureViewModel
{
    public List< CategoryFeatureResult> CategoryFeatures { get; set; }
    public List<CategoryResult> CategoryResults { get; set; }
    public CategoryResult Category { get; set; }
}
