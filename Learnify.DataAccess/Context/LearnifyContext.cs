// Learnify.DataAccess/Context/LearnifyContext.cs
using Learnify.DataAccess.Extensions;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Learnify.DataAccess.Context
{
    public class LearnifyContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public LearnifyContext(DbContextOptions<LearnifyContext> options) : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<LessonProgress> LessonProgresses => Set<LessonProgress>();
        public DbSet<CourseReview> CourseReviews => Set<CourseReview>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Prod’da LazyLoading kapalı kalsın; Include ile deterministik yükleme yapıyoruz.
            // optionsBuilder.UseLazyLoadingProxies(); // Gerekirse ileride açılabilir.
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fluent API config’leri otomatik yükle
            builder.ApplyConfigurationsFromAssembly(typeof(LearnifyContext).Assembly);

            // BaseEntity default’ları (IsActive, CreatedDate …)
            builder.ApplyBaseEntityConfigurations();

            // Tüm BaseEntity’lerde IsDeleted = false global filtresi
            builder.ApplySoftDeleteFilter();

            // Message ilişkileri
            builder.Entity<Message>()
                   .HasOne(m => m.Sender)
                   .WithMany(u => u.SentMessages)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                   .HasOne(m => m.Receiver)
                   .WithMany(u => u.ReceivedMessages)
                   .HasForeignKey(m => m.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict);

            // LessonProgress → Enrollment NoAction (çevrimsel cascade’i engeller)
            builder.Entity<LessonProgress>()
                   .HasOne(lp => lp.Enrollment)
                   .WithMany(e => e.LessonProgresses)
                   .HasForeignKey(lp => lp.EnrollmentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }

        public override int SaveChanges()
        {
            ApplyAuditingAndSoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditingAndSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Added/Modified için Created/Updated atamalarını yapar,
        /// Delete isteklerini SoftDelete’e çevirir.
        /// </summary>
        private void ApplyAuditingAndSoftDelete()
        {
            var utcNow = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is null) continue;

                // BaseEntity tabanlı audit
                if (entry.Entity is Learnify.Entity.Abstract.BaseEntity be)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            // CreatedDate default’ı varsa override etmiyoruz ama yine de garanti altına alalım
                            if (be.CreatedDate == default) be.CreatedDate = utcNow;
                            be.IsActive = be.IsActive == default ? true : be.IsActive;
                            be.UpdatedDate = utcNow;
                            break;

                        case EntityState.Modified:
                            be.UpdatedDate = utcNow;
                            // CreatedDate’in kazara güncellenmesini engelle
                            entry.Property(nameof(be.CreatedDate)).IsModified = false;
                            break;

                        case EntityState.Deleted:
                            // SoftDelete: fiziksel silme yerine işaretleme
                            be.IsDeleted = true;
                            be.UpdatedDate = utcNow;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }

                // AppUser (Identity) audit — IAuditable olarak ele alındı
                if (entry.Entity is AppUser user)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (user.CreatedDate == default) user.CreatedDate = utcNow;
                            user.IsActive = user.IsActive == default ? true : user.IsActive;
                            user.UpdatedDate = utcNow;
                            break;

                        case EntityState.Modified:
                            user.UpdatedDate = utcNow;
                            entry.Property(nameof(user.CreatedDate)).IsModified = false;
                            break;

                            // Identity tarafında soft delete genelde uygulanmaz; ihtiyaç olursa eklenebilir.
                    }
                }
            }
        }
    }
}
