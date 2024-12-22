using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ChangeInterceptors;

public class ChangeInterceptor:SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        PrepareEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PrepareEntity(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void PrepareEntity(DbContext dbContext)
    {
        foreach (var entity in dbContext.ChangeTracker.Entries())
        {
            if (entity.State == EntityState.Modified || entity.State == EntityState.Deleted)
            {
                try
                {
                    entity.Property("UpdateDatetime").CurrentValue = DateTime.Now;
                }
                catch (Exception ex) { }
               
            }
            if (entity.State == EntityState.Added)
            {
                try
                {
                    entity.Property("UpdateDatetime").CurrentValue = DateTime.Now;
                    entity.Property("CreateDatetime").CurrentValue = DateTime.Now;
                }
                catch (Exception ex) { }
                
            }
        }
    }

}
