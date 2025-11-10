

using Learnify.Entity.Enums;

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class UpdateEnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public EnrollmentStatus Status { get; set; }  // ✅ enum
        public DateTime EnrolledDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
