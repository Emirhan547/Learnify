namespace Learnify.DTO.DTOs.LessonDto
{
    public class UpdateLessonDto
    {
        public int LessonID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

        // ✅ CourseID eklendi (Update sırasında kurs değişikliği için)
        public int CourseID { get; set; }
    }
}