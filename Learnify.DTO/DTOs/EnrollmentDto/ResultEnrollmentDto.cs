using System;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class ResultEnrollmentDto
    {
        public int EnrollmentID { get; set; }

        // ✅ ID'ler eklendi (Update işlemi için gerekli)
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public int CourseID { get; set; }
        public string CourseTitle { get; set; } = string.Empty;

        public DateTime EnrolledDate { get; set; }
    }
}