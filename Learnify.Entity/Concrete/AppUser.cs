using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }

        // ✅ Instructor için eklenen özellik
        public string? Profession { get; set; }

        // Navigation Properties
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
