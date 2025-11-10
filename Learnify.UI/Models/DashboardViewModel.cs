using Learnify.Entity.Concrete;

namespace Learnify.UI.Models
{
    public class DashboardViewModel
    {
        public int TotalCourses { get; set; }
        public int TotalCategories { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalStudents { get; set; }
        public int TotalEnrollments { get; set; }

        public List<Learnify.DTO.DTOs.CourseDto.ResultCourseDto>? LatestCourses { get; set; }
        public List<AppUser>? LatestStudents { get; set; }
    }
}
