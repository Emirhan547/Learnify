using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.LessonProgressDto
{
    public class ResultLessonProgressDto
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }

        // View için ekstra bilgiler
        public string LessonTitle { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
    }
}
