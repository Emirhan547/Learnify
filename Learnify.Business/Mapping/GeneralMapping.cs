using AutoMapper;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.MappingProfiles
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // ✅ Category Mappings
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>()
                .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses != null ? src.Courses.Count : 0));

            // ✅ Course Mappings
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
            CreateMap<Course, ResultCourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.UserName : string.Empty));

            // ✅ Lesson Mappings
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();
            CreateMap<Lesson, ResultLessonDto>()
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty));

            // ✅ Enrollment Mappings
            CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();
            CreateMap<Enrollment, UpdateEnrollmentDto>().ReverseMap();
            CreateMap<Enrollment, ResultEnrollmentDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student != null ? src.Student.UserName : string.Empty))
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty));
        }
    }
}
