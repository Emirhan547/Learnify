using Learnify.Entity.Abstract;

namespace Learnify.Entity.Concrete
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public int Order { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
