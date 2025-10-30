using Learnify.Entity.Abstract;

namespace Learnify.Entity.Concrete
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsPublished { get; set; }

        // ✅ Yeni alanlar
        public string ImageUrl { get; set; }
        public double Rating { get; set; } = 0;
        public int StudentCount { get; set; } = 0;
        public string Duration { get; set; } 

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int InstructorId { get; set; }
        public AppUser Instructor { get; set; } = null!;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
