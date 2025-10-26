using Learnify.Entity.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyBaseEntityConfigurations(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType).Property<DateTime>("CreatedDate")
                           .HasDefaultValueSql("GETDATE()");

                    builder.Entity(entityType.ClrType).Property<bool>("IsActive")
                           .HasDefaultValue(true);
                }
            }
        }
    }
}
