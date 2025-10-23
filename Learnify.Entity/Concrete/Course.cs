using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        // 🔸 Instructor ilişkisini opsiyonel hale getirdik
        public int? InstructorID { get; set; }
        public AppUser? Instructor { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
