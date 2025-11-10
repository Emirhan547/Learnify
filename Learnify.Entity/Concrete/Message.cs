using Learnify.Entity.Abstract;
using Learnify.Entity.Enums;
using System;

namespace Learnify.Entity.Concrete
{
    public class Message : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public MessageStatus Status { get; set; } = MessageStatus.Inbox;
        public bool IsRead { get; set; }

        public AppUser Sender { get; set; } = null!;
        public AppUser Receiver { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}