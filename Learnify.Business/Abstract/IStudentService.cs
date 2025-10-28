using Learnify.DTO.DTOs.StudentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IStudentService
    {
        Task<List<ResultStudentDto>> GetAllAsync();
        Task<ResultStudentDto?> GetByIdAsync(int id);
        Task<List<StudentCourseDto>> GetStudentCoursesAsync(int studentId); // ✅ eklendi
        Task DeleteAsync(int id);
    }
}
