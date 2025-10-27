namespace Learnify.DTO.DTOs.CourseDto
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int? InstructorId { get; set; }
    }
}
