using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.Repositories;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EnrollmentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _unitOfWork.Enrollments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null) return;

            _unitOfWork.Enrollments.Delete(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultEnrollmentDto>> GetAllAsync()
        {
            var values = await _unitOfWork.Enrollments.GetAllAsync();
            return _mapper.Map<List<ResultEnrollmentDto>>(values);
        }

        public async Task<ResultEnrollmentDto?> GetByIdAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            return _mapper.Map<ResultEnrollmentDto?>(enrollment);
        }

        public async Task UpdateAsync(UpdateEnrollmentDto dto)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(dto.Id);
            if (enrollment == null) return;

            _mapper.Map(dto, enrollment);
            _unitOfWork.Enrollments.Update(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ResultEnrollmentDto>> GetAllWithCourseAndStudentAsync()
        {
            var enrollments = await _unitOfWork.Enrollments.GetAllWithCourseAndStudentAsync();

            var mapped = _mapper.Map<List<ResultEnrollmentDto>>(enrollments);

            foreach (var item in mapped)
            {
                item.TotalLessons = enrollments
                    .First(x => x.Id == item.Id)
                    .Course.Lessons.Count;

                item.CompletedLessons = enrollments
                    .First(x => x.Id == item.Id)
                    .LessonProgresses.Count(lp => lp.IsCompleted);
            }

            return mapped;
        }


        public async Task<ResultEnrollmentDto?> GetByIdWithCourseAndStudentAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdWithCourseAndStudentAsync(id);
            return _mapper.Map<ResultEnrollmentDto?>(enrollment);
        }

        public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
        {
            // Kullanıcı zaten kayıtlı mı?
            var exists = await _unitOfWork.Enrollments.GetAllAsync(e =>
                e.CourseId == courseId && e.StudentId == studentId);

            if (exists != null && exists.Any())
                return false; // zaten kayıtlı

            var entity = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                EnrolledDate = DateTime.UtcNow,
                Status = "Active" // ✅ Status ekledik
            };

            await _unitOfWork.Enrollments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> IsStudentEnrolledAsync(int courseId, int studentId)
        {
            var exists = await _unitOfWork.Enrollments.GetWhere(e =>
                e.CourseId == courseId && e.StudentId == studentId);

            return exists.Any();
        }

    }
}
