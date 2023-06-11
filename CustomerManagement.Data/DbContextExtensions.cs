using System;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Data;

public static class DbContextExtensions
{
    public const string Created = "Created";
    public const string Updated = "Updated";

    public static void AddTimestampsForAllModels(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (!entityType.GetProperties().Any(p => p.Name == Created))
            {
                entityType.AddProperty(Created, typeof(DateTime));
            }
            if (!entityType.GetProperties().Any(p => p.Name == Updated))
            {
                entityType.AddProperty(Updated, typeof(DateTime?));
            }
        }
    }

    public static void UpdateEntityTimestamps(this DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Property(Created).CurrentValue.Equals(default(DateTime)))
                {
                    entry.Property(Created).CurrentValue = DateTime.UtcNow;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(Updated).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}

