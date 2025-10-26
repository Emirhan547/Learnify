using Learnify.DataAccess.Configurations;
using Learnify.DataAccess.Extensions;
using Learnify.Entity.Abstract;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Context
{
    public class LearnifyContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public LearnifyContext(DbContextOptions<LearnifyContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔹 Tüm configuration sınıflarını otomatik uygula
            builder.ApplyConfigurationsFromAssembly(typeof(LearnifyContext).Assembly);

            // 🔹 BaseEntity alanlarını otomatik uygula (CreatedDate, IsActive)
            builder.ApplyBaseEntityConfigurations();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                entity.UpdatedDate = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                    entity.CreatedDate = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
