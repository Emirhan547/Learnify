using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnify.Entity.Concrete
{
    public class Category
    {
        public int CategoryID { get; set; }

        // ✅ Veritabanındaki kolon adıyla eşleştirildi
        public string Name { get; set; } = string.Empty;

        public ICollection<Course>? Courses { get; set; }
    }
}
