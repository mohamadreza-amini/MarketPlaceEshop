using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Cache;

public interface ICachedData
{
    T? Get<T>(string key);
    T Set<T>(string key, T data, MemoryCacheEntryOptions? options = null);
}
