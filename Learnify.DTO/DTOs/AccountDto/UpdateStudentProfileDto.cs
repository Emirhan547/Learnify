using Microsoft.AspNetCore.Http;

namespace Learnify.DTO.DTOs.AccountDto
{
    public class UpdateStudentProfileDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IFormFile? ProfileImage { get; set; }
        public string? ExistingImage { get; set; }
    }
}
