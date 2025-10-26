namespace Learnify.DTO.DTOs.InstructorDto
{
    public class ResultInstructorDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Profession { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public int CourseCount { get; set; }
    }
}
