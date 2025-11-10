using Learnify.DTO.DTOs.CourseDto;
using Learnify.Entity.Enums;
using System;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class ResultEnrollmentDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = "";
        public string CourseTitle { get; set; } = "";
        public string InstructorName { get; set; } = "";

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrolledDate { get; set; }
        public EnrollmentStatus Status { get; set; }

        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }

        // ❗ Sadece özel durumlarda doldurulacak
        public ResultCourseDto? Course { get; set; }
        public bool IsActive { get; set; }
    }
}
