using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class LessonManager : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LessonManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(CreateLessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            await _unitOfWork.Lessons.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Ders başarıyla eklendi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (lesson == null)
                return new ErrorResult("Ders bulunamadı.");

            _unitOfWork.Lessons.Delete(lesson);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Ders silindi.");
        }

        public async Task<IDataResult<List<ResultLessonDto>>> GetAllAsync()
        {
            var values = await _unitOfWork.Lessons.GetAllAsync();
            var mapped = _mapper.Map<List<ResultLessonDto>>(values);

            return new SuccessDataResult<List<ResultLessonDto>>(mapped);
        }

        public async Task<IDataResult<ResultLessonDto>> GetByIdAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (lesson == null)
                return new ErrorDataResult<ResultLessonDto>("Ders bulunamadı.");

            return new SuccessDataResult<ResultLessonDto>(_mapper.Map<ResultLessonDto>(lesson));
        }

        public async Task<IResult> UpdateAsync(UpdateLessonDto dto)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(dto.Id);
            if (lesson == null)
                return new ErrorResult("Ders bulunamadı.");

            _mapper.Map(dto, lesson);
            _unitOfWork.Lessons.Update(lesson);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult("Ders güncellendi.");
        }

        public async Task<IDataResult<List<ResultLessonDto>>> GetLessonsByCourseIdAsync(int courseId)
        {
            var values = await _unitOfWork.Lessons.GetLessonsByCourseIdAsync(courseId);
            var mapped = _mapper.Map<List<ResultLessonDto>>(values);

            return new SuccessDataResult<List<ResultLessonDto>>(mapped);
        }

        public async Task<IDataResult<UpdateLessonDto>> GetForUpdateAsync(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (lesson == null)
                return new ErrorDataResult<UpdateLessonDto>("Ders bulunamadı.");

            return new SuccessDataResult<UpdateLessonDto>(_mapper.Map<UpdateLessonDto>(lesson));
        }
    }
}
