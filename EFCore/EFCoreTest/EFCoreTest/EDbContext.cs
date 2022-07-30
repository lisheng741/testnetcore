using EFCoreTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using System.Reflection;

namespace EFCoreTest
{
    public class EDbContext : DbContext
    {
        public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        public override EntityEntry Add(object entity)
        {
            return base.Add(entity);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> entityTypes = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(EntityBase)) && !t.IsAbstract)
                .ToList();

            foreach (Type entityType in entityTypes)
            {
                EntityTypeBuilder entityTypeBuilder = builder.Entity(entityType);

                // 设置 Guid 类型
                PropertyInfo[] propertyInfos = entityType.GetProperties();
                foreach(PropertyInfo propertyInfo in propertyInfos)
                {
                    string propertyName = propertyInfo.Name;
                    if(propertyInfo.PropertyType.FullName == "System.Guid")
                    {
                        entityTypeBuilder.Property(propertyName).HasColumnType("char(36)");
                    }
                }

                //if(typeof(ISoftDelete).IsAssignableFrom(entityType))
                //{
                //    Expression<Func<ISoftDelete, bool>> expression = e => !e.IsDeleted;
                //    entityTypeBuilder.HasQueryFilter(expression);
                //}

                if (typeof(ISoftDelete).IsAssignableFrom(entityType))
                {
                    // EntityFramework Core 2.0全局过滤（HasQueryFilter）:https://www.cnblogs.com/CreateMyself/p/8491058.html

                    var parameter = Expression.Parameter(entityType);
                    var body = Expression.Equal(
                        Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant("IsDeleted"))
                        , Expression.Constant(false)
                    );

                    entityTypeBuilder.HasQueryFilter(Expression.Lambda(body, parameter));
                }

                var entityConfigure = Activator.CreateInstance(entityType) as EntityBase;

                if (entityConfigure != null)
                {
                    entityConfigure.ConfigureEntity(builder);
                }
            }
        }
    }
}
