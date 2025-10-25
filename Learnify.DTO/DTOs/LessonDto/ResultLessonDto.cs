namespace Learnify.DTO.DTOs.LessonDto
{
    public class ResultLessonDto
    {
        public int LessonID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        // ✅ CourseID eklendi (Update işlemi için gerekli)
        public int CourseID { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
    }
}