namespace Learnify.DTO.DTOs.LessonDto
{
    public class ResultLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string VideoUrl { get; set; } = string.Empty;
        public int Duration { get; set; } // dakika
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty; // ✅
        public bool IsActive { get; set; }
    }
}
