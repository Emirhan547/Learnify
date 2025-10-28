namespace Learnify.DTO.DTOs.StudentDto
{
    public class StudentCourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
