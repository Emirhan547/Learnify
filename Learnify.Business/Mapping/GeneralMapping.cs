using AutoMapper;
using Learnify.DTO.DTOs.AccountDto;
using Learnify.DTO.DTOs.CategoryDto;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.DTO.DTOs.CourseReviewDto;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.DTO.DTOs.LessonDto;
using Learnify.DTO.DTOs.LessonProgressDto;
using Learnify.DTO.DTOs.MessageDto;
using Learnify.DTO.DTOs.NotificationDto;
using Learnify.Entity.Concrete;

namespace Learnify.Business.MappingProfiles
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // 📘 COURSE MAPPINGS
            // 📘 COURSE MAPPINGS
            CreateMap<Course, ResultCourseDto>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
             .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
             .ReverseMap();

            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();



            // 📗 CATEGORY MAPPINGS
            CreateMap<Category, ResultCategoryDto>()
                .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses != null ? src.Courses.Count : 0))
                .ReverseMap();

            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

            CreateMap<Enrollment, ResultEnrollmentDto>()
    .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
    .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
    .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
    .ReverseMap();


            CreateMap<CreateEnrollmentDto, Enrollment>();
            CreateMap<UpdateEnrollmentDto, Enrollment>().ReverseMap();

            // 📕 LESSON MAPPINGS
            CreateMap<Lesson, ResultLessonDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : string.Empty))
                .ReverseMap();

            CreateMap<CreateLessonDto, Lesson>().ReverseMap();
            CreateMap<UpdateLessonDto, Lesson>().ReverseMap();

            // 🧑‍🏫 INSTRUCTOR MAPPINGS (AppUser)
            CreateMap<AppUser, ResultInstructorDto>()
                .ForMember(dest => dest.CourseCount, opt => opt.MapFrom(src => src.Courses != null ? src.Courses.Count : 0))
                .ReverseMap();

            CreateMap<CreateInstructorDto, AppUser>().ReverseMap();
            CreateMap<UpdateInstructorDto, AppUser>().ReverseMap();

            // GeneralMapping.cs
            // GeneralMapping.cs
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // 🔹 UserName = Email
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ReverseMap();

            CreateMap<Message, ResultMessageDto>()
    .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.FullName))
    .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.FullName))
    .ReverseMap();

            CreateMap<CreateMessageDto, Message>().ReverseMap();
            CreateMap<UpdateMessageDto, Message>().ReverseMap();
            CreateMap<Notification, ResultNotificationDto>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
    .ReverseMap();

            CreateMap<CreateNotificationDto, Notification>().ReverseMap();
            CreateMap<UpdateNotificationDto, Notification>().ReverseMap();
            CreateMap<LessonProgress, ResultLessonProgressDto>()
    .ForMember(dest => dest.LessonTitle, opt => opt.MapFrom(src => src.Lesson.Title))
    .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Lesson.Course.Title));

            CreateMap<CreateLessonProgressDto, LessonProgress>();
            CreateMap<UpdateLessonProgressDto, LessonProgress>();

            CreateMap<CourseReview, ResultCourseReviewDto>()
    .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName));

            CreateMap<CreateCourseReviewDto, CourseReview>();
        }
    }
}
