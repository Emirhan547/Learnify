namespace Learnify.DTO.DTOs.LessonDto
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int CourseId { get; set; }
    }
}
