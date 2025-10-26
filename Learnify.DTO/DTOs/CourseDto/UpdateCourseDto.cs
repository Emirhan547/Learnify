using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.CourseDto
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }                 // ✅ CourseID yerine Id
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public int? InstructorId { get; set; }
    }
}
