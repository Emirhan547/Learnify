using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.LessonProgressDto;
using Learnify.Entity.Concrete;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class LessonProgressManager : ILessonProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonProgressManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddOrUpdateAsync(CreateLessonProgressDto dto)
        {
            var existing = await _unitOfWork.LessonProgresses
                .GetByLessonAndStudentAsync(dto.LessonId, dto.StudentId);

            if (existing != null)
            {
                existing.IsCompleted = dto.IsCompleted;
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResult("İlerleme güncellendi.");
            }

            var entity = _mapper.Map<LessonProgress>(dto);
            await _unitOfWork.LessonProgresses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("İlerleme kaydedildi.");
        }

        public async Task<IDataResult<bool>> IsLessonCompletedAsync(int lessonId, int studentId)
        {
            var data = await _unitOfWork.LessonProgresses
                .GetByLessonAndStudentAsync(lessonId, studentId);

            return new SuccessDataResult<bool>(data != null && data.IsCompleted);
        }

        public async Task<IDataResult<int>> GetCompletedCountAsync(int courseId, int studentId)
        {
            var count = await _unitOfWork.LessonProgresses
                .GetCompletedCountByCourseAndStudentAsync(courseId, studentId);

            return new SuccessDataResult<int>(count);
        }
    }
}
