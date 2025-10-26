using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnify.DataAccess.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(10,2)");

            builder.HasOne(x => x.Category)
                   .WithMany(c => c.Courses)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Instructor)
                   .WithMany(i => i.Courses)
                   .HasForeignKey(x => x.InstructorId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
