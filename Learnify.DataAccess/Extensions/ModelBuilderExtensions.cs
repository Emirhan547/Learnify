using Learnify.Entity.Abstract;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Learnify.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyBaseEntityConfigurations(this ModelBuilder builder)
        {
            var entities = builder.Model.GetEntityTypes()
                .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType));

            foreach (var entity in entities)
            {
                // Default values
                builder.Entity(entity.ClrType)
                    .Property(nameof(BaseEntity.IsActive))
                    .HasDefaultValue(true);

                builder.Entity(entity.ClrType)
                    .Property(nameof(BaseEntity.CreatedDate))
                    .HasDefaultValueSql("GETUTCDATE()");
            }
        }

        public static void ApplySoftDeleteFilter(this ModelBuilder builder)
        {
            var entities = builder.Model.GetEntityTypes()
                .Where(t => typeof(BaseEntity).IsAssignableFrom(t.ClrType));

            foreach (var entity in entities)
            {
                builder.Entity(entity.ClrType)
                    .HasQueryFilter(CreateIsDeletedRestriction(entity.ClrType));
            }
        }

        private static LambdaExpression CreateIsDeletedRestriction(Type entityType)
        {
            var param = Expression.Parameter(entityType, "e");
            var prop = Expression.Property(param, nameof(BaseEntity.IsDeleted));
            var condition = Expression.Equal(prop, Expression.Constant(false));
            return Expression.Lambda(condition, param);
        }
    }
}
