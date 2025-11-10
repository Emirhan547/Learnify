namespace Learnify.DTO.DTOs.LessonDto
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public int Duration { get; set; } // dakika
        public int CourseId { get; set; }
    }
}
