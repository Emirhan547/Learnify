namespace Learnify.DTO.DTOs.StudentDto
{
    public class ResultStudentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // 🔹 Yeni alan — öğrencinin aldığı kurs sayısı
        public int EnrollmentCount { get; set; }
    }
}
