using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.StudentDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public StudentManager(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<ResultStudentDto>> GetAllAsync()
        {
            var students = await _userManager.Users
                .Include(u => u.Enrollments)
                .Where(u => u.Profession == "Student" || string.IsNullOrEmpty(u.Profession))
                .ToListAsync();

            return _mapper.Map<List<ResultStudentDto>>(students);
        }

        public async Task<ResultStudentDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.Enrollments)
                .FirstOrDefaultAsync(u => u.Id == id && (u.Profession == "Student" || string.IsNullOrEmpty(u.Profession)));

            return user == null ? null : _mapper.Map<ResultStudentDto>(user);
        }

        public async Task<List<StudentCourseDto>> GetStudentCoursesAsync(int studentId)
        {
            var user = await _userManager.Users
                .Include(u => u.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Category)
                .Include(u => u.Enrollments)
                    .ThenInclude(e => e.Course)
                        .ThenInclude(c => c.Instructor)
                .FirstOrDefaultAsync(u => u.Id == studentId);

            if (user == null)
                return new List<StudentCourseDto>();

            return user.Enrollments
                .Where(e => e.Course != null)
                .Select(e => new StudentCourseDto
                {
                    CourseId = e.Course.Id,
                    Title = e.Course.Title,
                    CategoryName = e.Course.Category?.Name ?? "Bilinmiyor",
                    InstructorName = e.Course.Instructor?.FullName ?? "Bilinmiyor",
                    Price = e.Course.Price
                })
                .ToList();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
                await _userManager.DeleteAsync(user);
        }
    }
}
