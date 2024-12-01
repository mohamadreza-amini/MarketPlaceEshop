using DataTransferObject;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IServiceBase<Entity, DTO, KeyTypeId> where Entity : class where DTO : BaseDTO<KeyTypeId> where KeyTypeId : struct
    {
        DTO TranslateToDTO(Entity entity);
        Entity TranslateToEntity(DTO model);
        public TDestination Translate<TSource, TDestination>(TSource source);

        ////////////////////////////

        //برای کراد سرویس بیس بنویسم مثلا اونجاهاشو که میشه رو بنویس مثلا ساخت انتیتی بگیر هبسازه اپدیت انتیتی بگیره اپدیت کنه اونجا که یوزرمنیجر هست اوراید میکنم برای اون



    }
}


