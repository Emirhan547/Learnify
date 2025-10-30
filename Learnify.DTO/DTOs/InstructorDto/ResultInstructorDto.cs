namespace Learnify.DTO.DTOs.InstructorDto
{
    public class ResultInstructorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public int CourseCount { get; set; }
        public bool IsActive { get; set; }
    }
}
