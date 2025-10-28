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

            // 🔹 Tüm entity configuration sınıflarını uygula
            builder.ApplyConfigurationsFromAssembly(typeof(LearnifyContext).Assembly);

            // 🔹 BaseEntity ya da IAuditable için ortak yapılandırmalar
            builder.ApplyBaseEntityConfigurations();
        }

        // 🔹 Audit otomasyonu: tüm IAuditable entity’lerde CreatedDate/UpdatedDate/IsActive otomatik set edilir
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditable &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                var entity = (IAuditable)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (entity.CreatedDate == default)
                        entity.CreatedDate = now;

                    entity.IsActive = true;
                }

                entity.UpdatedDate = now;
            }
        }
    }
}
