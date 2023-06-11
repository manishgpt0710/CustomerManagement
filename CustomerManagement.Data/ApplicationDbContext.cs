using System;
using System.Reflection.Emit;
using System.Security.Principal;
using CustomerManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CustomerManagement.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {

    }

    public virtual DbSet<Customer> Customer { get; set; }
    public virtual DbSet<ContactInfo> ContactInfo { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.AddTimestampsForAllModels();
        builder.Entity<ContactInfo>().HasOne<Customer>().WithMany(cu => cu.ContactInfos).HasForeignKey(co => co.CustomerId);
        builder.Entity<Order>().HasOne<Customer>().WithMany(cu => cu.Orders).HasForeignKey(o => o.CustomerId);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess = true)
    {
        this.UpdateEntityTimestamps();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.UpdateEntityTimestamps();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
