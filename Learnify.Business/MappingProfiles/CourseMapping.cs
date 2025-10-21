using AutoMapper;
using Learnify.DTO.DTOs.CourseDto;
using Learnify.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Business.MappingProfiles
{
    public class CourseMapping : Profile
    {
        public CourseMapping()
        {
            CreateMap<Course, ResultCourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName))
                .ReverseMap();

            // Create ve Update DTO’lar
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();
        }
    }
}
