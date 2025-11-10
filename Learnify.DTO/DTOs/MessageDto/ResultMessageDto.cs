
using Learnify.Entity.Enums;

namespace Learnify.DTO.DTOs.MessageDto
{
    public class ResultMessageDto
    {
        public int Id { get; set; }
        public string Subject { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime Date { get; set; }

        public string SenderName { get; set; } = "";
        public string ReceiverName { get; set; } = "";

        public MessageStatus Status { get; set; }
        public bool IsRead { get; set; }
    }
}
