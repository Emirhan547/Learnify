namespace Learnify.DTO.DTOs.CourseDto
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }

        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        // ✅ Yeni alanlar
        public string ImageUrl { get; set; } 
        public string Duration { get; set; } 
    }
}
