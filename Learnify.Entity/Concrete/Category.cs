// Entity/Concrete/Category.cs
using Learnify.Entity.Abstract;

namespace Learnify.Entity.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}