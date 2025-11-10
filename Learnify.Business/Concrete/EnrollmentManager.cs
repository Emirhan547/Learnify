using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using Learnify.Entity.Enums;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IResult> AddAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<Enrollment>(dto);
            await _unitOfWork.Enrollments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Öğrenci derse kaydedildi.");
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (entity == null)
                return new ErrorResult("Kayıt bulunamadı.");

            _unitOfWork.Enrollments.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kayıt silindi.");
        }

        public async Task<IDataResult<List<ResultEnrollmentDto>>> GetAllAsync()
        {
            var data = await _unitOfWork.Enrollments.GetAllAsync();
            return new SuccessDataResult<List<ResultEnrollmentDto>>(
                _mapper.Map<List<ResultEnrollmentDto>>(data)
            );
        }

        public async Task<IDataResult<ResultEnrollmentDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<ResultEnrollmentDto>("Kayıt bulunamadı.");

            return new SuccessDataResult<ResultEnrollmentDto>(
                _mapper.Map<ResultEnrollmentDto>(entity)
            );
        }

        public async Task<IResult> UpdateAsync(UpdateEnrollmentDto dto)
        {
            var entity = await _unitOfWork.Enrollments.GetByIdAsync(dto.Id);
            if (entity == null)
                return new ErrorResult("Kayıt bulunamadı.");

            _mapper.Map(dto, entity);
            _unitOfWork.Enrollments.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kayıt güncellendi.");
        }

        public async Task<IDataResult<List<ResultEnrollmentDto>>> GetAllWithCourseAndStudentAsync()
        {
            var enrollments = await _unitOfWork.Enrollments.GetAllWithCourseAndStudentAsync();

            var dtos = _mapper.Map<List<ResultEnrollmentDto>>(enrollments);

            foreach (var x in dtos)
            {
                var entity = enrollments.First(e => e.Id == x.Id);
                x.TotalLessons = entity.Course.Lessons.Count;
                x.CompletedLessons = entity.LessonProgresses.Count(lp => lp.IsCompleted);
            }

            return new SuccessDataResult<List<ResultEnrollmentDto>>(dtos);
        }

        public async Task<IDataResult<ResultEnrollmentDto>> GetByIdWithCourseAndStudentAsync(int id)
        {
            var entity = await _unitOfWork.Enrollments.GetByIdWithCourseAndStudentAsync(id);
            if (entity == null)
                return new ErrorDataResult<ResultEnrollmentDto>("Kayıt bulunamadı.");

            return new SuccessDataResult<ResultEnrollmentDto>(
                _mapper.Map<ResultEnrollmentDto>(entity)
            );
        }

        public async Task<IResult> EnrollStudentAsync(int courseId, int studentId)
        {
            var exists = await _unitOfWork.Enrollments.GetWhere(
                e => e.CourseId == courseId && e.StudentId == studentId
            );

            if (exists.Any())
                return new ErrorResult("Öğrenci zaten kayıtlı.");

            var entity = new Enrollment
            {
                CourseId = courseId,
                StudentId = studentId,
                Status = EnrollmentStatus.Enrolled
            };



            await _unitOfWork.Enrollments.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Öğrenci derse kaydedildi.");
        }

        public async Task<IResult> IsStudentEnrolledAsync(int courseId, int studentId)
        {
            var exists = await _unitOfWork.Enrollments.GetWhere(
                e => e.CourseId == courseId && e.StudentId == studentId
            );

            return exists.Any()
                ? new SuccessResult("Öğrenci kayıtlı.")
                : new ErrorResult("Öğrenci kayıtlı değil.");
        }
    }
}
