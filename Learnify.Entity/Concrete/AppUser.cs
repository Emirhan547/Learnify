using Learnify.Entity.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Learnify.Entity.Concrete
{
    public class AppUser : IdentityUser<int>, IAuditable
    {
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Profession { get; set; }

        // Navigation properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // Audit fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
