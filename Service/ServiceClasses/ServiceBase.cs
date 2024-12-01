using DataTransferObject;
using Mapster;
using Microsoft.AspNetCore.Http;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceClasses;

public abstract class ServiceBase<Entity, DTO, KeyTypeId> : IServiceBase<Entity, DTO, KeyTypeId> where Entity : class where DTO : BaseDTO<KeyTypeId> where KeyTypeId : struct
{

    public DTO TranslateToDTO(Entity entity)
    {
        return entity.Adapt<DTO>();
    }
    public Entity TranslateToEntity(DTO model)
    {
        return model.Adapt<Entity>();
    }
    public TDestination Translate<TSource, TDestination>(TSource source)
    {
        return source.Adapt<TDestination>();
    }

}
