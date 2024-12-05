using Microsoft.EntityFrameworkCore.Query;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts.Repository;

public interface IBaseRepository<T, KeyTypeId> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, bool isNoTracking = true);
    Task<IQueryable<TResult>?> GetAllDataAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken cancellationToken, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = true);
    Task<T?> GetByIdAsync(KeyTypeId id, CancellationToken cancellation);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
    Task<T> CreateAsync(T data, CancellationToken cancellation);
    T Update(T data);
    Task<bool> HardDeleteAsync(KeyTypeId id, CancellationToken cancellation);
    Task<bool> SoftDeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
    Task<bool> IsExistAsync(T data, CancellationToken cancellation);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation);
    Task<int> CommitAsync(CancellationToken cancellation);
}


