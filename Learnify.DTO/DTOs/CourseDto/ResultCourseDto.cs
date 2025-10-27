namespace Learnify.DTO.DTOs.CourseDto
{
    public class ResultCourseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;  // 🔹 Category.Name

        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }               // 🔹 AppUser.FullName
    }
}
