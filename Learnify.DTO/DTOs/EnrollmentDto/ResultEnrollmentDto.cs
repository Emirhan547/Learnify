using System;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class ResultEnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;

        public DateTime EnrolledDate { get; set; }
    }
}
