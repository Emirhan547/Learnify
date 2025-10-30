

namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        // ✅ View'da "Kayıt Tarihi" input'u için DateTime olmalı
        public DateTime EnrolledDate { get; set; } = DateTime.Now;

        // ✅ "Aktif mi?" checkbox'ı için gerekli property
        public bool IsActive { get; set; } = true;
    }
}
