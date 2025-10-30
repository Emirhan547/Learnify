using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.LessonProgressDto
{
    public class UpdateLessonProgressDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}
