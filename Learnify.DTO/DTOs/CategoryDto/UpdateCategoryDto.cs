using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.DTO.DTOs.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int CategoryID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
