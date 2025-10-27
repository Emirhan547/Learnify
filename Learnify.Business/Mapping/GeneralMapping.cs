using AutoMapper;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.Entity.Concrete;

namespace Learnify.Business.MappingProfiles
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, ResultCourseDto>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
             .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
             .ReverseMap();

            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();



            CreateMap<Category, ResultCategoryDto>()
    .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses.Count))
    .ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();


            CreateMap<Enrollment, ResultEnrollmentDto>()
    .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
    .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
    .ReverseMap();

            CreateMap<Enrollment, CreateEnrollmentDto>().ReverseMap();
            CreateMap<Enrollment, UpdateEnrollmentDto>().ReverseMap();

            CreateMap<Lesson, ResultLessonDto>()
    .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
    .ReverseMap();

            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();

            CreateMap<AppUser, ResultInstructorDto>()
    .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses.Count))
    .ReverseMap();

            CreateMap<AppUser, CreateInstructorDto>().ReverseMap();
            CreateMap<AppUser, UpdateInstructorDto>().ReverseMap();


        }
    }
}