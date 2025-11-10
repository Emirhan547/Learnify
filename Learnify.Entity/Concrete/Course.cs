using Learnify.Entity.Abstract;

namespace Learnify.Entity.Concrete
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public bool IsPublished { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public double Rating { get; set; } = 0;
        public int StudentCount { get; set; } = 0;
        public string Duration { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int InstructorId { get; set; }
        public AppUser Instructor { get; set; } = null!;

        public List<Lesson> Lessons { get; set; } = new();
        public List<Enrollment> Enrollments { get; set; } = new();
        public List<CourseReview> Reviews { get; set; } = new();
    }
}