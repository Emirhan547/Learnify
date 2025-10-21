using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public AppUser? Student { get; set; }

        public int CourseID { get; set; }
        public Course? Course { get; set; }

        public DateTime EnrolledDate { get; set; } = DateTime.Now;
    }
}
