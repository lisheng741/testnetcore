using EFCoreTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Reflection;

namespace EFCoreTest;

public class EDbContext : DbContext
{
    public bool IgnoreDeleteFilter { get; set; } = false;

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) 
    {
        Initialize();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //// 基本注册
        //builder.Entity<TestDelete>().HasQueryFilter(e => !e.IsDeleted);

        Assembly assembly = typeof(EDbContext).Assembly;
        List<Type> entityTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(EntityBase)) && !t.IsAbstract)
            .ToList();

        // 注册 Entity
        foreach (var entityType in entityTypes)
        {
            builder.Entity(entityType);
        }
    }

    protected virtual void Initialize()
    {
        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
    {
        TrackedEntity(e.Entry);
    }

    private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
    {
        TrackedEntity(e.Entry);
    }

    private void TrackedEntity(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Added:

                break;
            case EntityState.Modified:

                break;
            case EntityState.Deleted:
                entry.Reload();
                (entry.Entity as ISoftDelete).IsDeleted = true;
                break;
        }
    }
}
