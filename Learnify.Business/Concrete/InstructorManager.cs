using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMapper _mapper;

        public InstructorManager(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<ResultInstructorDto>> GetAllAsync()
        {
            // Tüm kullanıcıları getir
            var users = _userManager.Users.ToList();

            var instructors = new List<AppUser>();

            // Tüm kullanıcılar arasında asenkron olarak Instructor rolünde olanları bul
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Instructor"))
                    instructors.Add(user);
            }

            return _mapper.Map<List<ResultInstructorDto>>(instructors);
        }


        public async Task<ResultInstructorDto?> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return null;

            var isInstructor = await _userManager.IsInRoleAsync(user, "Instructor");
            if (!isInstructor)
                return null;

            return _mapper.Map<ResultInstructorDto>(user);
        }

        public async Task AddAsync(CreateInstructorDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return;

            if (!await _roleManager.RoleExistsAsync("Instructor"))
                await _roleManager.CreateAsync(new IdentityRole<int>("Instructor"));

            await _userManager.AddToRoleAsync(user, "Instructor");
        }

        public async Task UpdateAsync(UpdateInstructorDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
                return;

            _mapper.Map(dto, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return;

            await _userManager.DeleteAsync(user);
        }
    }
}
