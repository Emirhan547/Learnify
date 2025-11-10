// Entity/Concrete/Enrollment.cs
using Learnify.Entity.Abstract;
using Learnify.Entity.Enums;

namespace Learnify.Entity.Concrete
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Enrolled;

        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;
        public List<LessonProgress> LessonProgresses { get; set; } = new();
    }
}