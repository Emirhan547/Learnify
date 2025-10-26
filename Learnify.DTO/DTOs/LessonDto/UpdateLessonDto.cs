namespace Learnify.DTO.DTOs.LessonDto
{
    public class UpdateLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Order { get; set; }
        public int CourseId { get; set; }
    }
}
