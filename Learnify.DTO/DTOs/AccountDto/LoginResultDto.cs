using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.AccountDto
{
    public class LoginResultDto
    {
        public bool Succeeded { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
