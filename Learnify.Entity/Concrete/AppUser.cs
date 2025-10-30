// Entity/Concrete/AppUser.cs
using Microsoft.AspNetCore.Identity;

namespace Learnify.Entity.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? ProfileImage { get; set; }
        public string? Profession { get; set; }

        // Audit (IdentityUser<int> BaseEntity alamadığı için buraya ekliyoruz)
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigations
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

    }
}
