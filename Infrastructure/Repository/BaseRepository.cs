using Infrastructure.Contracts.Repository;
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

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true)
    {
        if (predicate == null)
        {
            return isNoTracking ? _entitySet.AsNoTracking() : _entitySet.AsQueryable();
        }
        return isNoTracking ? _entitySet.Where(predicate).AsNoTracking() : _entitySet.Where(predicate);
    }

    public async Task<IQueryable<TResult>?> GetAllData<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true)
    {
        IQueryable<T> query = _entitySet;
        query = isNoTracking == true ? query.AsNoTracking() : query.AsQueryable();
        query = predicate != null ? query.Where(predicate) : query;
        query = include != null ? include(query) : query;
        return query != null && await query.AnyAsync() ? query.Select(selector) : default;
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
    {
        return await _entitySet.FirstOrDefaultAsync(predicate, cancellation);
    }

    public async Task<T?> GetByIdAsync(KeyTypeId id, CancellationToken cancellation)
    {
        return await GetAsync(x => x.Id.Equals(id), cancellation);
    }

    public async Task<int> CreateAsync(T data, CancellationToken cancellation)
    {
        await _entitySet.AddAsync(data, cancellation);
        return await CommitAsync(cancellation);
    }

    public async Task<int> UpdateAsync(T data, CancellationToken cancellation)
    {
        _entitySet.Update(data);
        return await CommitAsync(cancellation); 
    }

    public async Task<bool> HardDeleteAsync(KeyTypeId id, CancellationToken cancellation)
    {
        var data = await GetAsync(x => x.Id.Equals(id), cancellation);
        if (data != null)
        {
            _entitySet.Remove(data);
            await CommitAsync(cancellation);
            return true;
        }
        return false;
    }

    public async Task<bool> SoftDeleteAsync(KeyTypeId id, CancellationToken cancellation)
    {
        var data = await GetAsync(x => x.Id.Equals(id), cancellation);
        if (data != null)
        {
            data.IsDeleted = true;
            await CommitAsync(cancellation);
            return true;
        }
        return false;
    }
    public async Task<bool> IsExist(T data, CancellationToken cancellation)
    {
        return await _entitySet.AnyAsync(x => x.Id.Equals(data.Id),cancellation);
    }
    public async Task<int> CommitAsync(CancellationToken cancellation) => await _appDbContext.SaveChangesAsync(cancellation);

}





