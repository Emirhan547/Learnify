using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.CourseDto
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CategoryId { get; set; }     // ✅ tutarlı isim
        public int? InstructorId { get; set; }  // ✅ opsiyonel
    }
}
