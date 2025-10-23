using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CourseManager : ICourseService
    {

        private readonly ICourseDal _courseDal;
        private readonly IMapper _mapper;

        public CourseManager(ICourseDal courseDal, IMapper mapper)
        {
            _courseDal = courseDal;
            _mapper = mapper;
        }

        public async Task<List<ResultCourseDto>> GetAllAsync()
        {
            var values = await _courseDal.GetAllWithIncludeAsync(x => x.Category, x => x.Instructor);
            return _mapper.Map<List<ResultCourseDto>>(values);
        }

        public async Task<ResultCourseDto> GetByIdAsync(int id)
        {
            var value = await _courseDal.GetByIdWithIncludeAsync(id, x => x.Category, x => x.Instructor);
            return _mapper.Map<ResultCourseDto>(value);
        }

        public async Task AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _courseDal.AddAsync(entity);
        }

        public async Task UpdateAsync(UpdateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _courseDal.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _courseDal.DeleteAsync(id);
        }
    }
}
