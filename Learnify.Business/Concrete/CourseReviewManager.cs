using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.Business.Utilities.Results;
using Learnify.DataAccess.Abstract;
using Learnify.DTO.DTOs.CourseReviewDto;
using Learnify.Entity.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CourseReviewManager : ICourseReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseReviewManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // 🔹 CREATE
        public async Task<IResult> AddAsync(CreateCourseReviewDto dto)
        {
            var entity = _mapper.Map<CourseReview>(dto);
            entity.ReviewDate = DateTime.UtcNow;
            await _unitOfWork.CourseReviews.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Yorum eklendi.");
        }

        // 🔹 UPDATE
        public async Task<IResult> UpdateAsync(UpdateCourseReviewDto dto)
        {
            var entity = await _unitOfWork.CourseReviews.GetByIdAsync(dto.Id);
            if (entity == null)
                return new ErrorResult("Yorum bulunamadı.");

            _mapper.Map(dto, entity);
            _unitOfWork.CourseReviews.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Yorum güncellendi.");
        }

        // 🔹 DELETE
        public async Task<IResult> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.CourseReviews.GetByIdAsync(id);
            if (entity == null)
                return new ErrorResult("Yorum bulunamadı.");

            _unitOfWork.CourseReviews.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Yorum silindi.");
        }

        // 🔹 GET ALL
        public async Task<IDataResult<List<ResultCourseReviewDto>>> GetAllAsync()
        {
            var list = await _unitOfWork.CourseReviews.GetAllAsync();
            return new SuccessDataResult<List<ResultCourseReviewDto>>(
                _mapper.Map<List<ResultCourseReviewDto>>(list)
            );
        }

        // 🔹 GET BY ID
        public async Task<IDataResult<ResultCourseReviewDto>> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.CourseReviews.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<ResultCourseReviewDto>("Yorum bulunamadı.");

            return new SuccessDataResult<ResultCourseReviewDto>(
                _mapper.Map<ResultCourseReviewDto>(entity)
            );
        }

        // 🔹 GET FOR UPDATE
        public async Task<IDataResult<UpdateCourseReviewDto>> GetForUpdateAsync(int id)
        {
            var entity = await _unitOfWork.CourseReviews.GetByIdAsync(id);
            if (entity == null)
                return new ErrorDataResult<UpdateCourseReviewDto>("Yorum bulunamadı.");

            return new SuccessDataResult<UpdateCourseReviewDto>(
                _mapper.Map<UpdateCourseReviewDto>(entity)
            );
        }

        // 🔹 GET BY COURSE
        public async Task<IDataResult<List<ResultCourseReviewDto>>> GetByCourseIdAsync(int courseId)
        {
            var reviews = await _unitOfWork.CourseReviews.GetCourseReviewsAsync(courseId);
            return new SuccessDataResult<List<ResultCourseReviewDto>>(
                _mapper.Map<List<ResultCourseReviewDto>>(reviews)
            );
        }
    }
}
