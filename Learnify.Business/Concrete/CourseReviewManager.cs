using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DataAccess.Abstract;
using Learnify.DataAccess.EntityFramework;
using Learnify.DTO.DTOs.CourseReviewDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class CourseReviewManager:ICourseReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseReviewManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddReviewAsync(int studentId, CreateCourseReviewDto dto)
        {
            var review = _mapper.Map<CourseReview>(dto);
            review.StudentId = studentId;
            review.ReviewDate = DateTime.UtcNow;

            await _unitOfWork.CourseReviews.AddAsync(review);
        }

        public async Task<List<ResultCourseReviewDto>> GetCourseReviewsAsync(int courseId)
        {
            var reviews = await _unitOfWork.CourseReviews.GetCourseReviewsAsync(courseId);
            return _mapper.Map<List<ResultCourseReviewDto>>(reviews);
        }
    }
}
