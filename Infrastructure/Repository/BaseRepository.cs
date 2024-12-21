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
    protected readonly DbSet<T> _entitySet;
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

    public async Task<IQueryable<TResult>?> GetAllDataAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        CancellationToken cancellationToken,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool isNoTracking = true)
    {
        IQueryable<T> query = _entitySet;
        query = isNoTracking == true ? query.AsNoTracking() : query.AsQueryable();
        query = predicate != null ? query.Where(predicate) : query;
        query = include != null ? include(query) : query;
        return query != null && await query.AnyAsync(cancellationToken) ? query.Select(selector) : default;
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation)
        => await _entitySet.FirstOrDefaultAsync(predicate, cancellation);


    public async Task<T?> GetByIdAsync(KeyTypeId id, CancellationToken cancellation)
        => await GetAsync(x => x.Id.Equals(id), cancellation);


    public async Task<T> CreateAsync(T data, CancellationToken cancellation)
    {
        await _entitySet.AddAsync(data, cancellation);
        return data;
    }

    public T Update(T data)
    {
        _entitySet.Update(data);
        return data;
    }

    public async Task<bool> HardDeleteAsync(KeyTypeId id, CancellationToken cancellation)
    {
        var data = await GetAsync(x => x.Id.Equals(id), cancellation);
        if (data != null)
        {
            _entitySet.Remove(data);
            return true;
        }
        return false;
    }

    public async Task<bool> SoftDeleteAsync(Expression<Func<T,bool>> predicate, CancellationToken cancellation)
    {
        var data = await GetAsync(predicate, cancellation);
        if (data != null)
        {
            data.IsDeleted = true;
            return true;
        }
        return false;
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate,CancellationToken cancellation)
    {
       return await _entitySet.CountAsync(predicate,cancellation);
    }


    public async Task<bool> IsExistAsync(T data, CancellationToken cancellation) 
        => await _entitySet.AnyAsync(x => x.Id.Equals(data.Id), cancellation);

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation) 
        => await _entitySet.AnyAsync(predicate, cancellation);

    public async Task<int> CommitAsync(CancellationToken cancellation) 
        => await _appDbContext.SaveChangesAsync(cancellation);

}





