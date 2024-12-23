using DataTransferObject.DTOClasses.Category.Results;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces.CategoryServices;
using System.Text;

namespace MarketPlaceEshop.ViewComponents;

public class MenuListViewComponent:ViewComponent
{
    private readonly ICategoryService _categoryService;
    public MenuListViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellation)
    {
        var categories = await _categoryService.GetAllListAsync(cancellation); 
        var categoryDictionary = categories.GroupBy(c => c.Level).ToDictionary(g => g.Key, g => g.ToList());

        var menuHtml = GenerateCategoryMenuHtml(categoryDictionary);

        ViewBag.MenuHtml = menuHtml;

        return View();
    }

    public static string GenerateCategoryMenuHtml(Dictionary<int, List<CategoryResult>> categoryDict, int? parentId = null, int currentLevel = 1)
    {
        if (!categoryDict.ContainsKey(currentLevel))
            return string.Empty;

        var categories = parentId == null
            ? categoryDict[currentLevel].Where(c => c.ParentCategoryId == null).ToList()
            : categoryDict[currentLevel].Where(c => c.ParentCategoryId == parentId).ToList();

        if (!categories.Any())
            return string.Empty;

        var htmlBuilder = new StringBuilder();

        if (currentLevel == 1)
        {
            htmlBuilder.Append("<ul class=\"category-list\">");
        }
        else
        {
            htmlBuilder.Append("<ul>");
        }

        foreach (var category in categories)
        {
            bool hasChildren = categoryDict.ContainsKey(currentLevel + 1) &&
                               categoryDict[currentLevel + 1].Any(c => c.ParentCategoryId == category.Id);

            string liClass = hasChildren ? " class=\"has-children\"" : string.Empty;
            htmlBuilder.AppendFormat("<li{0}>", liClass);

            var url = $"/product/categorysearch?selectedCategoryId={category.Id}";
            htmlBuilder.AppendFormat("<a href=\"{0}\">{1}</a>", url, category.CategoryName);

            if (hasChildren)
            {
                var subMenu = GenerateCategoryMenuHtml(categoryDict, category.Id, currentLevel + 1);
                if (!string.IsNullOrEmpty(subMenu))
                {
                    htmlBuilder.Append(subMenu);
                }
            }
            htmlBuilder.Append("</li>");
        }
        htmlBuilder.Append("</ul>");
        return htmlBuilder.ToString();
    }

}