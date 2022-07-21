using EFCoreTest.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace EFCoreTest;

public class EDbContext : DbContext
{
    public bool IgnoreDeleteFilter { get; set; } = false;

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //// 基本注册
        //builder.Entity<TestDelete>().HasQueryFilter(e => !e.IsDeleted);

        Assembly assembly = typeof(EDbContext).Assembly;
        //Assembly assembly = Assembly.GetExecutingAssembly();
        List<Type> entityTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(EntityBase)) && !t.IsAbstract)
            .ToList();

        MethodInfo? configureFilters = typeof(EDbContext).GetMethod(
            nameof(ConfigureFilters),
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        if (configureFilters == null) throw new ArgumentNullException(nameof(configureFilters));

        foreach(Type entityType in entityTypes)
        {
            // 注册实体
            builder.Entity(entityType);

            // 注册筛选器
            configureFilters
                .MakeGenericMethod(entityType)
                .Invoke(this, new object[] { builder, entityType });
        }
    }

    protected virtual void ConfigureFilters<TEntity>(ModelBuilder builder, Type entityType)
            where TEntity : class
    {
        Expression<Func<TEntity, bool>>? expression = null;

        if (typeof(ISoftDelete).IsAssignableFrom(entityType))
        {
            expression = e => IgnoreDeleteFilter || !EF.Property<bool>(e, "IsDeleted");
        }

        if (typeof(ITenant).IsAssignableFrom(entityType))
        {
            Expression<Func<TEntity, bool>> tenantExpression = e => EF.Property<int>(e, "TenantId") == 1;
            expression = expression == null ? tenantExpression : CombineExpressions(expression, tenantExpression);
        }

        if (expression == null) return;

        builder.Entity<TEntity>().HasQueryFilter(expression);
    }

    protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expression1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expression2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
    }

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
}
