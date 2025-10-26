using Learnify.Entity.Abstract;
using System.Collections.Generic;

namespace Learnify.Entity.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
