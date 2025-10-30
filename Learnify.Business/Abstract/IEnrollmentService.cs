using Learnify.DTO.DTOs.EnrollmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IEnrollmentService : IGenericService<CreateEnrollmentDto, UpdateEnrollmentDto, ResultEnrollmentDto>
    {
        Task<List<ResultEnrollmentDto>> GetAllWithCourseAndStudentAsync();
        Task<ResultEnrollmentDto?> GetByIdWithCourseAndStudentAsync(int id);
        Task<bool> EnrollStudentAsync(int courseId, int studentId);
        Task<bool> IsStudentEnrolledAsync(int courseId, int studentId);


    }
}
