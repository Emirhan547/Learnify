using Learnify.Entity.Abstract;
using System;

namespace Learnify.Entity.Concrete
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;
    }
}
