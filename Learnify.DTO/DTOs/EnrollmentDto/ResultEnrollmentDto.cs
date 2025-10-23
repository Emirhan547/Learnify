using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class ResultEnrollmentDto
    {
        public int EnrollmentID { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime EnrolledDate { get; set; }
    }
}
