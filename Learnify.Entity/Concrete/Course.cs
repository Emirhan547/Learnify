using Learnify.Entity.Abstract;
using System.Collections.Generic;

namespace Learnify.Entity.Concrete
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsPublished { get; set; } = false;

        // Relations
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int? InstructorId { get; set; }
        public AppUser? Instructor { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
