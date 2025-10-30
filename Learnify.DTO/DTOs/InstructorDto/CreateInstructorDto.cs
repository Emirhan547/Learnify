namespace Learnify.DTO.DTOs.InstructorDto
{
    public class CreateInstructorDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Profession { get; set; } = null!;

        // ✅ View'daki checkbox ile eşleşecek property
        public bool IsActive { get; set; } = true;
    }
}
