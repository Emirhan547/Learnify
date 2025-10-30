namespace Learnify.DTO.DTOs.LessonDto
{
    public class CreateLessonDto
    {
        public string Title { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public int Duration { get; set; } // dakika cinsinden
        public int CourseId { get; set; }
        public bool IsActive { get; set; } = true; // varsayılan aktif
    }
}
