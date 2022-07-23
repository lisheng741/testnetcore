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
        TrackedEntityEntry(e.Entry);
    }

    private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
    {
        TrackedEntityEntry(e.Entry);
    }

    private void TrackedEntityEntry(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                EntityStateAdded(entry);
                break;
            case EntityState.Modified:
                EntityStateModified(entry);
                break;
            case EntityState.Deleted:
                EntityStateDeleted(entry);
                break;
        }
    }

    protected virtual void EntityStateAdded(EntityEntry entry)
    {
        if (entry.Entity is ICreatedInfo createdEntity)
        {
            createdEntity.CreatedTime = DateTime.Now;
        }
    }

    protected virtual void EntityStateModified(EntityEntry entry)
    {
        if (entry.Entity is IUpdatedInfo updatedEntity)
        {
            updatedEntity.UpdatedTime = DateTime.Now;
        }
    }

    protected virtual void EntityStateDeleted(EntityEntry entry)
    {
        // 如果没有实现软删接口则直接返回，正常硬删
        if (!(entry.Entity is ISoftDelete softDelete))
        {
            return;
        }

        // 如果实现了软删，又想要硬删
        if (IgnoreDeleteFilter)
        {
            return;
        }

        //// 从数据库重载实体，这样做有个问题，每删一个实体访问一次数据库，效率得不到保证
        //entry.Reload();

        // 先将状态置为修改（会触发修改的事件）
        entry.State = EntityState.Modified;

        // 修改的事件结束以后，回来：遍历实体的属性，除了 IsDeleted 属性外，全将状态设置为未修改
        // 这样生成的 SQL 语句，只会更新一个 IsDeleted 的字段
        IEnumerable<PropertyEntry> properties = entry.Properties;

        foreach (var property in properties)
        {
            string name = property.Metadata.Name;
            if (name == nameof(ISoftDelete.IsDeleted)) continue;
            if ((entry.Entity is IUpdatedInfo) && (name == nameof(IUpdatedInfo.UpdatedTime) || name == nameof(IUpdatedInfo.UpdatedUserId))) continue;

            property.IsModified = false;
        }

        softDelete.IsDeleted = true;


        #region 方案1：abp的软删方案

        //// 从数据库重载实体，这样做有个问题，每删一个实体访问一次数据库，效率得不到保证
        //entry.Reload();
        //softDelete.IsDeleted = true;

        #endregion


        #region 方案2：

        //// 先将状态置为修改（会触发修改的事件）
        //entry.State = EntityState.Modified;

        //// 修改的事件结束以后，回来：遍历实体的属性，除了 IsDeleted 属性外，全将状态设置为未修改
        //// 这样生成的 SQL 语句，只会更新一个 IsDeleted 的字段
        //IEnumerable<PropertyEntry> properties = entry.Properties;
        //foreach (var property in properties)
        //{
        //    if (property.Metadata.Name == nameof(ISoftDelete.IsDeleted)) continue;
        //    property.IsModified = false;
        //}
        //softDelete.IsDeleted = true;

        #endregion


        #region 方案3：

        //// 状态修改
        //entry.State = EntityState.Unchanged;

        //// 设置单个状态修改
        //entry.Property(nameof(ISoftDelete.IsDeleted)).IsModified = true;
        //softDelete.IsDeleted = true;

        #endregion
    }
}
