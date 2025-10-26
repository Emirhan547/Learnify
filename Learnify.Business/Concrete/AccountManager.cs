using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Learnify.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AdminRegisterAsync(AdminRegisterDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                Profession = "Admin"
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Admin");

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDto dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
