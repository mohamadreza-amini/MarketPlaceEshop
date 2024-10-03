using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Categories;

public class CategoryFeature : BaseEntity<int>
{
    public string FeatureName { get; set; }
    public byte Priority { get; set; }

    /*      فقط مولفه هایی که این مقدار رو یک داشته باشن قابل اومدن در فیلترهای محصولات هستن و
     *     موقع مقدار دادن به اون ویژگی برای اون محصول که اینو یک داره یک مودال به وارد کننده
     *     مقدار نشون میده که قبلی ها این مقادیر رو وارد کردن پیشنهادمیکنه
     *     ..یا یکی رو کپی کنه یا توی همون فرمت ها بزنه و تکراری به رمت دیگه نزنه مثلا 8 پیکسل 8 و
    */
    public bool Filterable { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductFeatureValue>? ProductFeatureValues { get; set; }

}
