namespace Learnify.DTO.DTOs.CourseDto
{
    public class ResultCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string InstructorName { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        // ✅ Home sayfası için ek alanlar
        public string ImageUrl { get; set; } = null!;
        public double Rating { get; set; }
        public int StudentCount { get; set; }
        public string Duration { get; set; } 
    }
}
