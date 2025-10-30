using AutoMapper;
using Learnify.Business.Abstract;
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

        public async Task AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _unitOfWork.Courses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null) return;

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultCourseDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Courses.GetAllAsync();
            return _mapper.Map<List<ResultCourseDto>>(values);
        }

        public async Task<ResultCourseDto?> GetByIdAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            return _mapper.Map<ResultCourseDto?>(course);
        }

        public async Task UpdateAsync(UpdateCourseDto dto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(dto.Id);
            if (course == null) return;

            _mapper.Map(dto, course);
            _unitOfWork.Courses.Update(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultCourseDto>> GetCoursesWithInstructorAsync()
        {
            var values = await _unitOfWork.Courses.GetCoursesWithInstructorAsync();
            return _mapper.Map<List<ResultCourseDto>>(values);
        }

        public async Task<ResultCourseDto?> GetCourseDetailsAsync(int courseId)
        {
            var course = await _unitOfWork.Courses.GetCourseDetailsAsync(courseId);
            return _mapper.Map<ResultCourseDto?>(course);
        }

        public async Task<List<ResultCourseDto>> GetCoursesByCategoryIdAsync(int categoryId)
        {
            var values = await _unitOfWork.Courses.GetCoursesByCategoryIdAsync(categoryId);
            return _mapper.Map<List<ResultCourseDto>>(values);
        }

        public async Task<UpdateCourseDto?> GetForUpdateAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            return _mapper.Map<UpdateCourseDto?>(course);
        }

    }
}
