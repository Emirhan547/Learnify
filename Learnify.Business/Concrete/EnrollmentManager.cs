using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IEnrollmentDal _enrollmentDal;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EnrollmentManager(IEnrollmentDal enrollmentDal, IUnitOfWork uow, IMapper mapper)
        {
            _enrollmentDal = enrollmentDal;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ResultEnrollmentDto>> GetAllAsync()
        {
            var entities = await _enrollmentDal.GetAllWithCourseAndStudentAsync();
            return _mapper.Map<List<ResultEnrollmentDto>>(entities);
        }

        public async Task<ResultEnrollmentDto?> GetByIdAsync(int id)
        {
            var entity = await _enrollmentDal.GetByIdWithCourseAndStudentAsync(id);
            return entity == null ? null : _mapper.Map<ResultEnrollmentDto>(entity);
        }

        public async Task AddAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _enrollmentDal.AddAsync(entity);
            await _uow.CommitAsync();
        }

        public async Task UpdateAsync(UpdateEnrollmentDto dto)
        {
            var entity = await _enrollmentDal.GetByIdAsync(dto.Id);
            if (entity == null) return;

            _mapper.Map(dto, entity);
            _enrollmentDal.Update(entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _enrollmentDal.GetByIdAsync(id);
            if (entity == null) return;

            _enrollmentDal.Delete(entity);
            await _uow.CommitAsync();
        }

        public async Task DeleteByStudentAndCourseAsync(int studentId, int courseId)
        {
            var enrollment = await _enrollmentDal
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment != null)
            {
                _enrollmentDal.Delete(enrollment);
                await _uow.CommitAsync();
            }
        }

    }
}
