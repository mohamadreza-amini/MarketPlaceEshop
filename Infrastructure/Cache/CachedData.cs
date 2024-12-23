using Infrastructure.Contracts.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache;

public class CachedData : ICachedData
{
    private readonly IMemoryCache _memoryCache;
    public CachedData(IMemoryCache iMemoryCache)
    {
        _memoryCache = iMemoryCache;
    }

    public T Set<T>(string key, T data, MemoryCacheEntryOptions? options = null)
    {
        if (options == null)
        {
            options = new MemoryCacheEntryOptions()
            {
                 AbsoluteExpiration = DateTimeOffset.Now.AddHours(1),
                 SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        return _memoryCache.Set(key, data, options);
    }

    public T? Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}