namespace Learnify.DTO.DTOs.LessonDto
{
    public class ResultLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int Duration { get; set; } // dakika
        public bool IsActive { get; set; }
    }
}
