using Learnify.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class LessonProgress : BaseEntity
    {
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }
        public int CourseId { get; set; }
    }
}
