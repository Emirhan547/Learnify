
namespace Learnify.Entity.Abstract
{
    public abstract class BaseEntity : IEntity, IAuditable, ISoftDelete
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
    }
}