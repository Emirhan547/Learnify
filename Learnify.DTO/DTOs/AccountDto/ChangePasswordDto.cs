using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.AccountDto
{
    public class ChangePasswordDto
    {

        public string CurrentPassword { get; set; } = string.Empty;


        public string NewPassword { get; set; } = string.Empty;


        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
