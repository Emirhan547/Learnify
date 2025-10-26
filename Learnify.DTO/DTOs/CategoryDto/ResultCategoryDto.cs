﻿namespace Learnify.DTO.DTOs.CategoryDto
{
    public class ResultCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CourseCount { get; set; }
    }
}
