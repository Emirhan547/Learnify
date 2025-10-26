namespace Learnify.DTO.DTOs.CourseDto
{
  
        public class ResultCourseDto
        {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public int? InstructorId { get; set; }      // ✅ opsiyonel hale getirildi
        public string? InstructorName { get; set; }
    }

    }

