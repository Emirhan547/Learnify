namespace Learnify.DTO.DTOs.LessonDto
{
    public class ResultLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        // 🔹 Kurs ilişkisi
        public int CourseId { get; set; }
        public string? CourseTitle { get; set; }
    }
}
