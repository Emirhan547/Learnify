

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class UpdateEnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // ✅ Tarih formatlaması için DateTime
        public DateTime EnrolledDate { get; set; }

        // ✅ Checkbox için property
        public bool IsActive { get; set; }
    }
}
