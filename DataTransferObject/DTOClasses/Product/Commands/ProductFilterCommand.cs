using DataTransferObject.DTOClasses.Category.Results;
using DataTransferObject.DTOClasses.Product.Results;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.DTOClasses.Product.Commands;

public class ProductFilterCommand
{
    public string SearchText{ get; set; }
    public SortProduct SortProduct { get; set; }
    public List<int> SelectedBrandIds {  get; set; }=new ();
    public int? SelectedCategoryId { get; set; }
    public bool OnlyAvailable { get; set; }
    public List<CategoryResult> CategoryList { get; set; }
    public List<BrandResult> BrandList { get; set; }
    public int? PageIndex {  get; set; }
}
