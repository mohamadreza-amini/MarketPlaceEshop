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
    private readonly IQueryable<T> _availableEntities;

    public BaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _entitySet = appDbContext.Set<T>();
        _availableEntities = _entitySet.Where(x => !x.IsDeleted);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true)
    {
        if (predicate == null)
        {
            return isNoTracking ? _availableEntities.AsNoTracking() : _availableEntities.AsQueryable();
        }
        return isNoTracking ? _availableEntities.Where(predicate).AsNoTracking() : _availableEntities.Where(predicate);
    }

    public IQueryable<TResult> GetAll<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true)
    {
        IQueryable<T> query = _availableEntities;
        query = isNoTracking == true ? query.AsNoTracking() : query.AsQueryable();
        query = predicate != null ? query.Where(predicate) : query;
        query = include != null ? include(query) : query;
        return query != null && query.Any() ? query.Select(selector) : default;
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _availableEntities.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> GetByIdAsync(KeyTypeId id)
    {
        return await GetAsync(x => x.Id.Equals(id));
    }

    public async Task<T> CreateAsync(T data)
    {
        await _entitySet.AddAsync(data);
        await CommitAsync();
        return data;
    }

    public async Task<T> UpdateAsync(T data)
    {
        _entitySet.Update(data);
        await CommitAsync();
        return data;
    }

    public async Task<bool> HardDeleteAsync(KeyTypeId id)
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

    public async Task<bool> SoftDeleteAsync(KeyTypeId id)
    {
        var data = await GetAsync(x => x.Id.Equals(id));
        if (data != null)
        {
            data.IsDeleted = true;
            await CommitAsync();
            return true;
        }
        return false;
    }

    public async Task<int> CommitAsync() => await _appDbContext.SaveChangesAsync();

}





