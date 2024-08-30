using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entityEntry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Entity.CreatedAt = DateTime.UtcNow;
                entityEntry.Entity.CreatedBy = "mehmet";
            }

            if (entityEntry.State != EntityState.Added && entityEntry.State != EntityState.Modified) continue;
            entityEntry.Entity.LastModified = DateTime.UtcNow;
            entityEntry.Entity.LastModifiedBy = "mehmet";
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) => entry.References.Any(r =>
        r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && (r.TargetEntry.State == EntityState.Added ||
                                                                      r.TargetEntry.State == EntityState.Modified));
}