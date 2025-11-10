using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnify.DataAccess.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.HasIndex(x => x.Title);

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(10,2)");

            builder.Property(x => x.Duration)
                   .HasMaxLength(50);

            builder.Property(x => x.ImageUrl)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.HasOne(x => x.Category)
                   .WithMany(c => c.Courses)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor)
                   .WithMany(i => i.Courses)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
