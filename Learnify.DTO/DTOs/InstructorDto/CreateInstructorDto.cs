namespace Learnify.DTO.DTOs.InstructorDto
{
    public class CreateInstructorDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Profession { get; set; }
        public string? ProfileImage { get; set; }
        public string Password { get; set; } = string.Empty; // 🔸 Identity için eklendi

    }
}
