namespace Learnify.DTO.DTOs.InstructorDto
{
    public class UpdateInstructorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Profession { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
