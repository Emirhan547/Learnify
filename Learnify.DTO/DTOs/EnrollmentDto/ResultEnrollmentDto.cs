using Learnify.DTO.DTOs.CourseDto;
using System;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class ResultEnrollmentDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
        public int StudentId { get; set; }   // 👈 eklendi
        public int CourseId { get; set; }
        // ✅ Tarih ve durum alanları eklendi
        public DateTime EnrolledDate { get; set; }
        public bool IsActive { get; set; }
        public ResultCourseDto Course { get; set; } = null!;

    }
}
