using Learnify.DTO.DTOs.AccountDto;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Learnify.Business.Abstract
{
    public interface IAccountService
    {
        Task<IdentityResult> AdminRegisterAsync(AdminRegisterDto dto);
        Task<SignInResult> LoginAsync(LoginDto dto);
        Task LogoutAsync();
    }
}
