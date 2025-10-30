using Learnify.Entity.Abstract;
using System;

namespace Learnify.Entity.Concrete
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public bool IsDraft { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsSpam { get; set; } = false;

        // 🔗 Navigation Properties
        public AppUser Sender { get; set; } = null!;
        public AppUser Receiver { get; set; } = null!;
    }
}
