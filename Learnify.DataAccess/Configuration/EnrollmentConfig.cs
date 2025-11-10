using Learnify.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learnify.DataAccess.Configurations
{
    public class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasOne(x => x.Student)
                   .WithMany(u => u.Enrollments)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(x => x.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Status)
    .HasConversion<string>(); // enum'u DB'de string tutar — çok okunaklı

        }
    }
}
