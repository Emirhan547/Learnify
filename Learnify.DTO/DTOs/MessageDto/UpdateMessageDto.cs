using Learnify.Entity.Enums;

namespace Learnify.DTO.DTOs.MessageDto
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public MessageStatus Status { get; set; } = MessageStatus.Inbox;
    }
}
