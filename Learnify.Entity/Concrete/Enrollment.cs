// Entity/Concrete/Enrollment.cs
using Learnify.Entity.Abstract;

namespace Learnify.Entity.Concrete
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public ICollection<LessonProgress> LessonProgresses { get; set; } = new List<LessonProgress>();
    }
}
