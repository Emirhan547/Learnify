using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnify.DataAccess.Configurations
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.VideoUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(x => x.Order)
                   .HasDefaultValue(1);

            builder.HasOne(x => x.Course)
                   .WithMany(c => c.Lessons)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
