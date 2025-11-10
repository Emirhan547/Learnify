using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.EnrollmentDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IEnrollmentService : IGenericService<CreateEnrollmentDto, UpdateEnrollmentDto, ResultEnrollmentDto>
    {
        Task<IDataResult<List<ResultEnrollmentDto>>> GetAllWithCourseAndStudentAsync();
        Task<IDataResult<ResultEnrollmentDto>> GetByIdWithCourseAndStudentAsync(int id);
        Task<IResult> EnrollStudentAsync(int courseId, int studentId);
        Task<IResult> IsStudentEnrolledAsync(int courseId, int studentId);
    }
}
