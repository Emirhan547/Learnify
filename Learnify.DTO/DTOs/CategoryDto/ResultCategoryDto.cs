namespace Learnify.DTO.DTOs.CategoryDto
{
    public class ResultCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CourseCount { get; set; }
        public bool IsActive { get; set; }
    }
}
