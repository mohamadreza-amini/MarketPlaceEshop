using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class BaseRepository<T, KeyTypeId> : IBaseRepository<T, KeyTypeId> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
{
    private readonly AppDbContext _appDbContext;
    private readonly DbSet<T> _entitySet;

    public BaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _entitySet = appDbContext.Set<T>();
    }

    public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true)
    {
        if (predicate == null)
        {
            return await Task.Run(() =>isNoTracking? _entitySet.AsNoTracking() : _entitySet.AsQueryable());
        }

        return await Task.Run(() => isNoTracking ? _entitySet.Where(predicate).AsNoTracking():_entitySet.Where(predicate));
    }

    public async Task<IQueryable<TResult>> GetAll<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true)
    {
        IQueryable<T> query = _entitySet;
        query = isNoTracking == true ? query.AsNoTracking() : query.AsQueryable();
        query = predicate != null ? query.Where(predicate) : query;
        query = include != null ? include(query) : query;
        return query != null && query.Any() ? query.Select(selector) : default;

    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entitySet.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> GetByIdAsync(KeyTypeId id)
    {
        return await GetAsync(x => x.Id.Equals(id));
    }

    public async Task<T> CreateDataAsync(T data)
    {
        await _entitySet.AddAsync(data);
        return data;
    }

    public async Task<bool> DeleteDataAsync(KeyTypeId id)
    {
        var data = await GetAsync(x => x.Id.Equals(id));
        if (data != null)
        {
            _entitySet.Remove(data);
            await CommitAsync();
            return true;
        }
        return false;
    }

    public async Task<T> UpdateDataAsync(T data)
    {
        _entitySet.Update(data);
        await CommitAsync();
        return data;
    }

    public async Task<int> CommitAsync() => await _appDbContext.SaveChangesAsync();

}
