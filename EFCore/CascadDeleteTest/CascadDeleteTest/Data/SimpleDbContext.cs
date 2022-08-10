﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simple.Repository;

public class SimpleDbContext : DbContext
{
    public SimpleDbContext(DbContextOptions<SimpleDbContext> options)
        : base(options)
    {
        Initialize();
    }

    /// <summary>
    /// 数据筛选器管理
    /// </summary>
    public virtual DataFilter DataFilter { get; set; } = new DataFilter();

    protected virtual void Initialize()
    {
        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // 获取本程序集内所有继承 EntityBase 的实体
        List<Type> entityTypes = Assembly.GetExecutingAssembly().GetTypes()
                                        .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(EntityBase)))
                                        .ToList();

        // 注册 Entity
        foreach (var entityType in entityTypes)
        {
            builder.Entity(entityType);
        }

        // 因为 ConfigureEntity 是一个泛型方法，需要先将其获取为 MethodInfo
        MethodInfo configureEntity = typeof(SimpleDbContext).GetMethod(
            nameof(ConfigureEntity),
            BindingFlags.Instance | BindingFlags.NonPublic
        ) ?? throw new NullReferenceException(nameof(configureEntity));

        // 遍历注册的实体进行配置
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            EntityTypeBuilder entityTypeBuilder = builder.Entity(entityType.ClrType);

            // Guid 主键默认不自动设置
            if(typeof(EntityBase<Guid>).IsAssignableFrom(entityType.ClrType))
            {
                PropertyBuilder idPropertyBuilder = entityTypeBuilder.Property(nameof(EntityBase<Guid>.Id));
                if (!idPropertyBuilder.Metadata.PropertyInfo!.IsDefined(typeof(DatabaseGeneratedAttribute), true))
                {
                    entityTypeBuilder.Property(nameof(EntityBase<Guid>.Id)).ValueGeneratedNever();
                }
            }

            // 表名注释
            string entitySummary = entityType.ClrType.GetSummary();
            if (!string.IsNullOrEmpty(entitySummary))
            {
                entityTypeBuilder.HasComment(entitySummary);
            }

            // 遍历实体属性
            PropertyInfo[] propertyInfos = entityType.ClrType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                
                // 属性注释
                string propertySummary = propertyInfo.GetSummary();
                if (!string.IsNullOrEmpty(propertySummary) && 
                    !typeof(EntityBase).IsAssignableFrom(propertyInfo.PropertyType) && 
                    !typeof(IEnumerable<EntityBase>).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    entityTypeBuilder.Property(propertyName).HasComment(propertySummary);
                }

                // Guid 处理为 char(36), 这样不论是 SqlServer 还是 MySql 都可以按字符串来处理 Guid，解决数据库排序方式不一致的问题
                if (propertyInfo.PropertyType.FullName == "System.Guid")
                {
                    entityTypeBuilder.Property(propertyName).HasColumnType("char(36)");
                }
            }

            // 设置级联删除
            foreach(var foreignKeys in entityType.GetForeignKeys())
            {
                Console.WriteLine($"级联删除：{foreignKeys}");
                foreignKeys.DeleteBehavior = DeleteBehavior.Cascade;
            }

            // 通过 Invoke 调用 ConfigureEntity，对实体进行通用配置（基本属性、筛选器等配置）
            configureEntity
                .MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { builder, entityType });

            // 创建实体实例，调用实例的 ConfigureEntity 对实体进行专有配置
            var entityConfigure = Activator.CreateInstance(entityType.ClrType) as EntityBase;
            if (entityConfigure != null)
            {
                entityConfigure.ConfigureEntity(builder);
            }
        }
    }

    #region SaveChanges 方法重写

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        try
        {
            HandleBeforeSave();

            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return 0;
        }
        finally
        {

        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        try
        {
            HandleBeforeSave();

            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return 0;
        }
        finally
        {

        }
    }

    #endregion

    protected virtual void HandleBeforeSave()
    {
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if (entry.State.IsIn(EntityState.Modified, EntityState.Deleted))
            {
                // 并发控制字段赋新值（ConcurrencyCheck）
                if (entry.Entity is IConcurrency concurrency)
                {
                    Entry(concurrency).Property(c => c.RowVersion).OriginalValue = concurrency.RowVersion;
                    concurrency.RowVersion = DateTimeOffset.UtcNow.Ticks;
                }
            }
        }
    }

    #region ChangeTracker 跟踪实体状态变化，集中统一的业务逻辑

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
        // 主键检查及设置
        if (entry.Entity is EntityBase<Guid> entityWithGuidId)
        {
            if (entityWithGuidId.Id == default)
            {
                Guid id = GuidHelper.Next();
                //entry.Property(nameof(EntityBase<Guid>.Id)).OriginalValue = id;
                entry.Property(nameof(EntityBase<Guid>.Id)).CurrentValue = id;
                //entityWithGuidId.Id = id;
            }
        }

        // 并发控制字段检查（ConcurrencyCheck）
        if (entry.Entity is IConcurrency concurrency)
        {
            if (concurrency.RowVersion == default)
            {
                concurrency.RowVersion = DateTimeOffset.UtcNow.Ticks;
            }
        }

        // 其他字段自动赋值
        if (entry.Entity is ICreatedTime createdTime)
        {
            createdTime.CreatedTime = DateTime.Now;
        }
    }

    protected virtual void EntityStateModified(EntityEntry entry)
    {
        // 字段自动赋值
        if (entry.Entity is IUpdatedTime updatedTime)
        {
            updatedTime.UpdatedTime = DateTime.Now;
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
        if (DataFilter.IgnoreSoftDelete)
        {
            return;
        }

        //// 从数据库重载实体，这样做有个问题，每删一个实体访问一次数据库，效率得不到保证
        //entry.Reload();

        // 设置软删字段
        softDelete.IsDeleted = true;

        // 先将状态置为修改（会触发修改的事件）
        entry.State = EntityState.Modified;

        // 遍历实体的属性，除了需要更新的属性外，其他全将状态设置为未修改。这样生成的 SQL 语句，只会更新相应的的字段
        IEnumerable<PropertyEntry> properties = entry.Properties;
        foreach (var property in properties)
        {
            string name = property.Metadata.Name;

            if (name == nameof(ISoftDelete.IsDeleted)) continue;
            if (entry.Entity is IUpdatedTime && name == nameof(IUpdatedTime.UpdatedTime)) continue;
            if (entry.Entity is IUpdatedUser && name == nameof(IUpdatedUser.UpdatedUserId)) continue;

            property.IsModified = false;
        }
    }

    #endregion

    #region 实体配置

    /// <summary>
    /// 自动配置实体
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="builder"></param>
    /// <param name="mutableEntityType"></param>
    protected virtual void ConfigureEntity<TEntity>(ModelBuilder builder, IMutableEntityType mutableEntityType)
        where TEntity : class
    {
        // 如果不是一个独立的实体，则返回
        if (mutableEntityType.IsOwned())
        {
            return;
        }

        // 配置全局数据筛选器
        ConfigureGlobalFilters<TEntity>(builder);
    }

    /// <summary>
    /// 配置全局筛选器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="builder"></param>
    protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder builder)
        where TEntity : class
    {
        // 生成筛选器需要的表达式树
        Expression<Func<TEntity, bool>>? expression = CreateFilterExpression<TEntity>();

        // 如果没有生成表达式树，则直接返回
        if (expression == null) return;

        // 设置实体的筛选器（注：筛选器只会生效最后一个）
        builder.Entity<TEntity>().HasQueryFilter(expression);
    }

    /// <summary>
    /// 生成全局筛选表达式
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    protected virtual Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>()
        where TEntity : class
    {
        Expression<Func<TEntity, bool>>? expression = null;

        // 如果实现了软删接口
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            expression = e => DataFilter.IgnoreSoftDelete || !EF.Property<bool>(e, nameof(ISoftDelete.IsDeleted));
        }

        // 多租户
        if (typeof(ITenant).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> tenantExpression = e => EF.Property<Guid>(e, nameof(ITenant.TenantId)) == Guid.Empty;
            expression = expression == null ? tenantExpression : CombineExpressions(expression, tenantExpression);
        }

        return expression;
    }

    // 来源：Abp 源码 AbpDbContext.cs
    protected Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expression1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expression2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

    // 来源：Abp 源码 AbpDbContext.cs
    class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression? node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }

            return base.Visit(node)!;
        }
    }

    #endregion
}
