using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.LessonDto
{
    public class ResultLessonDto
    {
        public int LessonID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
    }
}
