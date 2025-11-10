using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _unitOfWork.Courses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kurs başarıyla oluşturuldu.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null)
                return new ErrorResult("Kurs bulunamadı.");

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kurs silindi.");
        }

        public async Task<IDataResult<List<ResultCourseDto>>> GetAllAsync()
        {
            var values = await _unitOfWork.Courses.GetAllAsync();
            var mapped = _mapper.Map<List<ResultCourseDto>>(values);
            return new SuccessDataResult<List<ResultCourseDto>>(mapped);
        }

        public async Task<IDataResult<ResultCourseDto>> GetByIdAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null)
                return new ErrorDataResult<ResultCourseDto>("Kurs bulunamadı.");

            return new SuccessDataResult<ResultCourseDto>(_mapper.Map<ResultCourseDto>(course));
        }

        public async Task<IResult> UpdateAsync(UpdateCourseDto dto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(dto.Id);
            if (course == null)
                return new ErrorResult("Kurs bulunamadı.");

            _mapper.Map(dto, course);
            _unitOfWork.Courses.Update(course);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kurs güncellendi.");
        }

        public async Task<IDataResult<List<ResultCourseDto>>> GetCoursesWithInstructorAsync()
        {
            var list = await _unitOfWork.Courses.GetCoursesWithInstructorAsync();
            return new SuccessDataResult<List<ResultCourseDto>>(_mapper.Map<List<ResultCourseDto>>(list));
        }

        public async Task<IDataResult<ResultCourseDto>> GetCourseDetailsAsync(int courseId)
        {
            var entity = await _unitOfWork.Courses.GetCourseDetailsAsync(courseId);
            if (entity == null)
                return new ErrorDataResult<ResultCourseDto>("Kurs bulunamadı.");

            return new SuccessDataResult<ResultCourseDto>(_mapper.Map<ResultCourseDto>(entity));
        }

        public async Task<IDataResult<List<ResultCourseDto>>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            var list = await _unitOfWork.Courses.GetCoursesByCategoryIdAsync(categoryId);
            return new SuccessDataResult<List<ResultCourseDto>>(_mapper.Map<List<ResultCourseDto>>(list));
        }

        public async Task<IDataResult<UpdateCourseDto>> GetForUpdateAsync(int id)
        {
            var entity = await _unitOfWork.Courses.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<UpdateCourseDto>("Kurs bulunamadı.");

            return new SuccessDataResult<UpdateCourseDto>(_mapper.Map<UpdateCourseDto>(entity));
        }
    }
}
