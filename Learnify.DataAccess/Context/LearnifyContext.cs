using Learnify.DataAccess.Configurations;
using Learnify.DataAccess.Extensions;
using Learnify.Entity.Abstract;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learnify.DataAccess.Context
{
    public class LearnifyContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public LearnifyContext(DbContextOptions<LearnifyContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<LessonProgress> LessonProgresses { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(LearnifyContext).Assembly);
            builder.ApplyBaseEntityConfigurations();

            // 💬 Message ilişkileri
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
            builder.Entity<LessonProgress>()
       .HasOne(lp => lp.Enrollment)
       .WithMany(e => e.LessonProgresses)
       .HasForeignKey(lp => lp.EnrollmentId)
       .OnDelete(DeleteBehavior.NoAction);

            // (İsteğe bağlı) Global filtreler
            // builder.Entity<Course>().HasQueryFilter(c => c.IsActive);
            // builder.Entity<Category>().HasQueryFilter(c => c.IsActive);
            // builder.Entity<Lesson>().HasQueryFilter(l => l.IsActive);
            // builder.Entity<Enrollment>().HasQueryFilter(e => e.IsActive);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
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
