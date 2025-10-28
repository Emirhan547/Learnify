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
        private readonly ICourseDal _courseDal;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourseManager(ICourseDal courseDal, IUnitOfWork uow, IMapper mapper)
        {
            _courseDal = courseDal;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ResultCourseDto>> GetAllAsync()
        {
            var entities = await _courseDal.GetAllAsync(null, c => c.Category, c => c.Instructor);
            return _mapper.Map<List<ResultCourseDto>>(entities);
        }

        public async Task<ResultCourseDto?> GetByIdAsync(int id)
        {
            var entity = await _courseDal.GetByIdAsync(id, c => c.Category, c => c.Instructor);
            return entity == null ? null : _mapper.Map<ResultCourseDto>(entity);
        }

        public async Task AddAsync(CreateCourseDto dto)
        {
            var entity = _mapper.Map<Course>(dto);
            await _courseDal.AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(UpdateCourseDto dto)
        {
            var existing = await _courseDal.GetByIdAsync(dto.Id);
            if (existing == null) return;

            _mapper.Map(dto, existing);
            _courseDal.Update(existing);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _courseDal.GetByIdAsync(id);
            if (entity == null) return;

            _courseDal.Delete(entity);
            await _uow.CommitAsync();
        }
    }
}
