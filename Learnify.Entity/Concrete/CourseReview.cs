using Learnify.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class CourseReview : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int StudentId { get; set; }
        public AppUser Student { get; set; } = null!;

        public int Rating { get; set; } // 1–5
        public string Comment { get; set; } = string.Empty;

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}