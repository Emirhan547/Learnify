namespace Learnify.DTO.DTOs.LessonDto
{
    public class UpdateLessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public int Duration { get; set; }
        public int CourseId { get; set; }
        public bool IsActive { get; set; }
    }
}
