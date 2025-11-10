namespace Learnify.DTO.DTOs.EnrollmentDto
{
    public class CreateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledDate { get; set; } = DateTime.Now;

    }
}
