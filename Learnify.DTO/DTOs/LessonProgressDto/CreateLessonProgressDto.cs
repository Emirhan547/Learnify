using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.LessonProgressDto
{
    public class CreateLessonProgressDto
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public bool IsCompleted { get; set; } = true;
        public object CourseId { get; set; }
    }
}
