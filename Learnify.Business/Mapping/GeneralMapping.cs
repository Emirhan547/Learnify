using AutoMapper;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.DTO.DTOs.StudentDto;
using Learnify.Entity.Concrete;

namespace Learnify.Business.MappingProfiles
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // CATEGORY
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            // COURSE
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
            CreateMap<Course, ResultCourseDto>()
     .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
     .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.FullName : string.Empty))
     // 🔹 Ders sayısını otomatik hesapla
     .ForMember(dest => dest.LessonCount,
                opt => opt.MapFrom(src => src.Lessons != null ? src.Lessons.Count : 0))
     .ReverseMap();


            // LESSON
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();
            CreateMap<Lesson, ResultLessonDto>()
                .ForMember(dest => dest.CourseTitle,
                           opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty))
                .ReverseMap();

            // INSTRUCTOR
            CreateMap<AppUser, CreateInstructorDto>().ReverseMap();
            CreateMap<AppUser, UpdateInstructorDto>().ReverseMap();
            CreateMap<AppUser, ResultInstructorDto>()
    .ForMember(dest => dest.CourseCount,
               opt => opt.MapFrom(src => src.Courses != null ? src.Courses.Count : 0))
    .ReverseMap();

            CreateMap<AppUser, ResultStudentDto>()
    .ForMember(dest => dest.EnrollmentCount,
               opt => opt.MapFrom(src => src.Enrollments != null ? src.Enrollments.Count : 0))
    .ReverseMap();


        }
    }
}
