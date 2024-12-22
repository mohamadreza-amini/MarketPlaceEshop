using DataTransferObject.DTOClasses.Category.Results;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceClasses.PersonServices;
using Service.ServiceInterfaces.CategoryServices;
using Service.ServiceInterfaces.PersonServices;
using System.Text;

namespace MarketPlaceEshop.ViewComponents;

public class MenuTabViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;
    public MenuTabViewComponent(ICategoryService categoryService)
    {      
        _categoryService = categoryService;
    }
    public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellation)
    {
        var categories = await _categoryService.GetAllListAsync(cancellation); // داده‌ها را دریافت کنید
        var categoryDictionary = categories.GroupBy(c => c.Level).ToDictionary(g => g.Key, g => g.ToList());

        var menuHtml = GenerateCustomMenuHtml(categoryDictionary);

        ViewBag.MenuHtml = menuHtml;

        return View();

    }


    private static string GenerateCustomMenuHtml(Dictionary<int, List<CategoryResult>> categoryDict, int? parentId = null, int currentLevel = 1)
    {
        if (!categoryDict.ContainsKey(currentLevel))
            return string.Empty;

        var categories = parentId == null
            ? categoryDict[currentLevel].Where(c => c.ParentCategoryId == null).ToList()
            : categoryDict[currentLevel].Where(c => c.ParentCategoryId == parentId).ToList();

        if (!categories.Any())
            return string.Empty;

        var htmlBuilder = new StringBuilder();

        // شروع <ul> برای سطح فعلی
        if (currentLevel == 1)
        {
            htmlBuilder.Append("<ul>");
        }
        else if (currentLevel == 2)
        {
            htmlBuilder.Append("<ul class=\"row\">");
            // htmlBuilder.Append("<li ><a href=\"#\">همه لول یک</a></li>");
        }
        else
        {
            htmlBuilder.Append("<ul>");
        }

        foreach (var category in categories)
        {
            // کلاس‌ها بر اساس سطح تنظیم می‌شوند
            string listItemClass = currentLevel == 2 ? "col-3" : string.Empty;

            htmlBuilder.AppendFormat("<li class=\"{0}\">", listItemClass);


            var url = $"/product/categorysearch?selectedCategoryId={category.Id}";
            htmlBuilder.AppendFormat("<a href=\"{0}\">{1}</a>", url, category.CategoryName);



            // بازگشتی برای زیردسته‌ها
            var subMenu = GenerateCustomMenuHtml(categoryDict, category.Id, currentLevel + 1);
            if (!string.IsNullOrEmpty(subMenu))
            {
                htmlBuilder.Append(subMenu);
            }

            htmlBuilder.Append("</li>");
        }

        htmlBuilder.Append("</ul>");

        return htmlBuilder.ToString();
    }


}