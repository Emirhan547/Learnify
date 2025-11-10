using Learnify.Business.Utilities.Results;
using Learnify.DTO.DTOs.LessonDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface ILessonService : IGenericService<CreateLessonDto, UpdateLessonDto, ResultLessonDto>
    {
        Task<IDataResult<List<ResultLessonDto>>> GetLessonsByCourseIdAsync(int courseId);
        Task<IDataResult<UpdateLessonDto>> GetForUpdateAsync(int id);
    }
}
