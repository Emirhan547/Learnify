namespace Learnify.DTO.DTOs.CourseDto
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }          // 🔹 zorunlu alan
        public int? InstructorId { get; set; }       // 🔹 opsiyonel
    }
}
