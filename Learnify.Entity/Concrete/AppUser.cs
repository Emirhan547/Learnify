// Entity/Concrete/AppUser.cs
using Learnify.Entity.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Learnify.Entity.Concrete
{
    public class AppUser : IdentityUser<int>, IAuditable
    {
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Profession { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Course> Courses { get; set; } = new();
        public List<Enrollment> Enrollments { get; set; } = new();

        public List<Message> SentMessages { get; set; } = new();
        public List<Message> ReceivedMessages { get; set; } = new();
    }
}